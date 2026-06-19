// =============================================================================
// DX Training site builder — renders every opted-in course into public/.
//
// A course opts in by placing a `site.config.json` in its directory. This
// script discovers those, builds a navigation model per course, renders each
// published page with the shared engine, and emits a catalog landing page.
// The course markdown remains the single source of truth; only public/ is
// written. Run with `npm run build` (or `node build.mjs`).
// =============================================================================

import { fileURLToPath } from "node:url";
import {
  readFileSync,
  writeFileSync,
  mkdirSync,
  readdirSync,
  statSync,
  copyFileSync,
  rmSync,
  existsSync,
} from "node:fs";
import path from "node:path";
import {
  renderMarkdown,
  renderCodeBody,
  pageShell,
  catalogPage,
  firstH1,
  humanize,
  esc,
  THEMES,
} from "./lib/render.mjs";

const SITE_DIR = path.dirname(fileURLToPath(import.meta.url));
const ROOT = path.dirname(SITE_DIR);
const COURSES_DIR = path.join(ROOT, "courses");
const OUT_DIR = path.join(ROOT, "public");

const toPosix = (p) => p.split(path.sep).join("/");
const posix = path.posix;

// ---- file classification ----------------------------------------------------
const CODE_EXT = {
  ".ts": "typescript", ".tsx": "typescript", ".js": "javascript", ".mjs": "javascript",
  ".yml": "yaml", ".yaml": "yaml", ".json": "json", ".cs": "csharp", ".sh": "bash",
  ".css": "css", ".html": "html", ".py": "python", ".sql": "sql", ".feature": "gherkin",
};
const ASSET_EXT = new Set([".png", ".jpg", ".jpeg", ".gif", ".svg", ".webp", ".ico"]);

// Dirs that never carry publishable content (build output, vcs, editor, deps).
const HARD_IGNORE_DIRS = new Set([
  "node_modules", ".git", ".github", "bin", "obj", "Debug", "Release",
  ".vs", ".vscode", ".idea", "site", "public",
]);
// Files never published regardless of course config.
const HARD_IGNORE_FILES = new Set([
  "CLAUDE.md", ".gitignore", ".DS_Store", "Thumbs.db", "package-lock.json",
  "site.config.json",
]);
const HARD_IGNORE_FILE_RE = /\.(csproj|sln|user|suo|cache|pdb|dll|exe)$/i;

function kindOf(rel) {
  const base = path.basename(rel);
  if (HARD_IGNORE_FILES.has(base) || HARD_IGNORE_FILE_RE.test(base)) return null;
  const ext = path.extname(rel).toLowerCase();
  if (ext === ".md") return "md";
  if (CODE_EXT[ext]) return "code";
  if (ASSET_EXT.has(ext)) return "asset";
  return null;
}

function srcToOut(rel, kind) {
  if (kind === "md") {
    if (path.basename(rel) === "README.md")
      return toPosix(posix.join(posix.dirname(rel), "index.html")).replace(/^\.\//, "");
    return rel.replace(/\.md$/i, ".html");
  }
  if (kind === "code") return rel + ".html";
  return rel; // asset, copied verbatim
}

// relative href from one output path to another (both posix, course-relative)
function relHref(fromOut, toOut) {
  const r = toPosix(posix.relative(posix.dirname("/" + fromOut), "/" + toOut));
  return r || path.basename(toOut);
}

// ---- exclude matching (per-course) ------------------------------------------
function makeExcluder(patterns = []) {
  return (rel) => {
    const segs = rel.split("/");
    for (const p of patterns) {
      if (p.startsWith("**/")) {
        if (segs.includes(p.slice(3))) return true;
      } else if (rel === p || rel.startsWith(p + "/")) {
        return true;
      }
    }
    return false;
  };
}

// ---- directory helpers ------------------------------------------------------
function entries(absDir) {
  if (!existsSync(absDir)) return [];
  return readdirSync(absDir).map((name) => {
    const abs = path.join(absDir, name);
    return { name, abs, isDir: statSync(abs).isDirectory() };
  });
}

// Recursively collect publishable course-relative files under a course subdir.
function walkPublishable(courseDir, relDir, excluded, acc) {
  for (const e of entries(path.join(courseDir, relDir))) {
    const rel = relDir ? `${relDir}/${e.name}` : e.name;
    if (e.isDir) {
      if (HARD_IGNORE_DIRS.has(e.name) || excluded(rel)) continue;
      walkPublishable(courseDir, rel, excluded, acc);
    } else {
      if (excluded(rel)) continue;
      if (kindOf(rel)) acc.push(rel);
    }
  }
  return acc;
}

const naturalCmp = (a, b) =>
  a.localeCompare(b, undefined, { numeric: true, sensitivity: "base" });

// ============================================================================
// Pass 1 — build a model per course (published set, sidebar, labels, sections)
// ============================================================================
function buildCourseModel(slug) {
  const courseDir = path.join(COURSES_DIR, slug);
  const config = JSON.parse(readFileSync(path.join(courseDir, "site.config.json"), "utf8"));
  const excluded = makeExcluder(config.exclude);
  const heroLen = (THEMES[config.theme] || THEMES.indigo).heroes.length;

  const labelText = (rel) => {
    if (config.labels && config.labels[rel]) return config.labels[rel];
    const abs = path.join(courseDir, rel);
    if (rel.toLowerCase().endsWith(".md") && existsSync(abs)) {
      const h1 = firstH1(readFileSync(abs, "utf8"));
      if (h1) return h1;
    }
    const base = path.basename(rel);
    if (base === "README.md") return humanize(path.basename(path.dirname(rel)) || slug);
    return humanize(base);
  };

  const published = new Map(); // rel -> { kind, section, heroIndex }
  const sidebar = []; // [{ label, items: [{ src, label }] }]
  const add = (rel, section, heroIndex) => {
    const kind = kindOf(rel);
    if (!kind || !existsSync(path.join(courseDir, rel))) return false;
    if (!published.has(rel)) published.set(rel, { kind, section, heroIndex });
    return true;
  };

  config.nav.forEach((entry, ni) => {
    const heroIndex = ni % heroLen;
    const section = entry.section || entry.label || "Overview";

    if (entry.path) {
      if (add(entry.path, section, heroIndex))
        sidebar.push({ label: null, items: [{ src: entry.path, label: entry.label || labelText(entry.path) }] });
      return;
    }

    if (entry.paths) {
      const items = entry.paths
        .filter((p) => add(p, section, heroIndex))
        .map((p) => ({ src: p, label: labelText(p) }));
      if (items.length) sidebar.push({ label: section, items });
      return;
    }

    if (entry.dir) {
      // publish the whole subtree so deep links resolve
      for (const rel of walkPublishable(courseDir, entry.dir, excluded, []))
        add(rel, section, heroIndex);

      // sidebar items = immediate children with a landing page + direct .md
      const items = [];
      const dirAbs = path.join(courseDir, entry.dir);
      const dirEntries = entries(dirAbs)
        .filter((e) => !HARD_IGNORE_DIRS.has(e.name) && !excluded(`${entry.dir}/${e.name}`))
        .sort((a, b) => naturalCmp(a.name, b.name));
      // a README directly in the section dir is the section landing, listed first
      const ownReadme = `${entry.dir}/README.md`;
      if (published.has(ownReadme)) items.push({ src: ownReadme, label: labelText(ownReadme) });
      for (const e of dirEntries) {
        const rel = `${entry.dir}/${e.name}`;
        if (e.isDir) {
          const idx = `${rel}/README.md`;
          if (published.has(idx)) items.push({ src: idx, label: labelText(idx) });
        } else if (kindOf(rel) === "md" && e.name !== "README.md") {
          items.push({ src: rel, label: labelText(rel) });
        }
      }
      sidebar.push({ label: section, items });
    }
  });

  // Synthetic folder-index pages: any content directory without its own
  // README still gets a browsable index, so directory links resolve.
  const synth = new Map(); // dir -> { section, heroIndex }
  const contentDirs = new Set();
  for (const rel of published.keys()) {
    let d = posix.dirname(rel);
    while (d && d !== ".") {
      contentDirs.add(d);
      d = posix.dirname(d);
    }
  }
  const metaUnder = (d) => {
    for (const [rel, m] of published) if (rel === d || rel.startsWith(d + "/")) return m;
    return { section: "", heroIndex: 0 };
  };
  for (const d of contentDirs)
    if (!published.has(d + "/README.md")) {
      const m = metaUnder(d);
      synth.set(d, { section: m.section, heroIndex: m.heroIndex });
    }

  const linear = sidebar.flatMap((g) => g.items.map((it) => it.src));
  const linearSet = new Set(linear);

  // nearest sidebar-item ancestor (for back-link on deep, non-sidebar pages)
  const nearestAncestor = (rel) => {
    let best = null, bestLen = -1;
    for (const cand of linear) {
      if (cand === rel) continue;
      const cdir = posix.dirname(cand);
      if ((rel === cdir || rel.startsWith(cdir + "/")) && cdir.length > bestLen) {
        best = cand;
        bestLen = cdir.length;
      }
    }
    return best;
  };

  return { slug, config, courseDir, published, synth, sidebar, linear, linearSet, nearestAncestor, labelText };
}

// ============================================================================
// Pass 2 — render every published page using the model + global course map
// ============================================================================
const warnings = [];

function renderCourse(model, coursesBySlug) {
  const { slug, config, courseDir, published, synth, sidebar, linear, linearSet, nearestAncestor, labelText } = model;
  const courseTitle = config.title;
  const brandMark = config.mark || "•";
  const short = config.short || config.mark || courseTitle;

  // resolve a same-course target (file or directory) to an output path, or null
  const resolveLocal = (target) => {
    if (published.has(target)) return srcToOut(target, published.get(target).kind);
    if (published.has(target + "/README.md")) return srcToOut(target + "/README.md", "md");
    if (synth.has(target)) return target + "/index.html";
    return null;
  };

  // link rewriter bound to a specific source/output page
  const makeRewrite = (srcRel, outRel) => (href) => {
    if (/^(https?:|mailto:|#)/i.test(href)) return href;
    const [rawPath, anchor] = href.split("#");
    const frag = anchor ? "#" + anchor : "";
    if (!rawPath) return href;

    const target = toPosix(posix.normalize(posix.join(posix.dirname(srcRel), rawPath))).replace(/\/$/, "");
    const dirLink = rawPath.endsWith("/");

    // same-course
    if (!target.startsWith("..")) {
      const out = resolveLocal(target);
      if (out) return relHref(outRel, out) + frag;
      warnings.push(`${slug}/${srcRel} → ${href} (not published)`);
      return rawPath + frag;
    }

    // cross-course: resolve to another course's published page if possible
    const absTarget = path.resolve(courseDir, target);
    const fromCourses = toPosix(path.relative(COURSES_DIR, absTarget));
    if (!fromCourses.startsWith("..")) {
      const otherSlug = fromCourses.split("/")[0];
      let otherRel = fromCourses.slice(otherSlug.length + 1);
      const other = coursesBySlug.get(otherSlug);
      if (other) {
        if (!otherRel || dirLink || !path.extname(otherRel)) otherRel = posix.join(otherRel, "README.md");
        if (other.published.has(otherRel)) {
          const fromFull = `${slug}/${outRel}`;
          const toFull = `${otherSlug}/${srcToOut(otherRel, other.published.get(otherRel).kind)}`;
          return toPosix(posix.relative(posix.dirname("/" + fromFull), "/" + toFull)) + frag;
        }
      }
    }
    warnings.push(`${slug}/${srcRel} → ${href} (external to published site)`);
    return rawPath + frag;
  };

  const navHtml = (activeSrc, outRel) => {
    let out = "";
    for (const g of sidebar) {
      const items = g.items
        .map((it) => {
          const href = relHref(outRel, srcToOut(it.src, published.get(it.src)?.kind || "md"));
          const on = it.src === activeSrc ? " on" : "";
          return `<li><a class="${on.trim()}" href="${href}">${esc(it.label)}</a></li>`;
        })
        .join("");
      if (g.label === null) out += `<ul class="nav-top">${items}</ul>`;
      else out += `<div class="nav-group"><div class="nav-h">${esc(g.label)}</div><ul>${items}</ul></div>`;
    }
    return out;
  };

  const sectionLandingSrc = (section) => {
    for (const g of sidebar) if (g.label === section && g.items.length) return g.items[0].src;
    return null;
  };

  const crumbs = (srcRel, outRel, meta) => {
    const homeHref = relHref(outRel, "index.html");
    const parts = [`<a href="${homeHref}">${esc(short)}</a>`];
    if (srcRel !== "README.md") {
      const landing = sectionLandingSrc(meta.section);
      if (landing && landing !== srcRel)
        parts.push(`<a href="${relHref(outRel, srcToOut(landing, published.get(landing)?.kind || "md"))}">${esc(meta.section)}</a>`);
      else parts.push(`<span>${esc(meta.section)}</span>`);
      const label = labelText(srcRel);
      if (landing !== srcRel) parts.push(`<span class="cur">${esc(label)}</span>`);
    }
    return parts.join('<span class="sep">›</span>');
  };

  const pager = (srcRel, outRel) => {
    const idx = linear.indexOf(srcRel);
    const link = (s, dir) =>
      `<a class="pg pg-${dir}" href="${relHref(outRel, srcToOut(s, published.get(s)?.kind || "md"))}"><span class="pg-d">${
        dir === "prev" ? "Previous" : "Next"
      }</span><span class="pg-t">${esc(labelText(s))}</span></a>`;
    if (idx !== -1) {
      let out = "";
      if (idx > 0) out += link(linear[idx - 1], "prev");
      if (idx < linear.length - 1) out += link(linear[idx + 1], "next");
      return `<nav class="pager">${out}</nav>`;
    }
    const back = nearestAncestor(srcRel) || "README.md";
    return `<nav class="pager"><a class="pg pg-prev" href="${relHref(outRel, srcToOut(back, published.get(back)?.kind || "md"))}"><span class="pg-d">Back to</span><span class="pg-t">${esc(
      labelText(back),
    )}</span></a></nav>`;
  };

  let nPages = 0, nCode = 0, nAssets = 0;
  for (const [srcRel, meta] of published) {
    const outRel = srcToOut(srcRel, meta.kind);
    const outAbs = path.join(OUT_DIR, slug, outRel);
    mkdirSync(path.dirname(outAbs), { recursive: true });

    if (meta.kind === "asset") {
      copyFileSync(path.join(courseDir, srcRel), outAbs);
      nAssets++;
      continue;
    }

    const activeSrc = linearSet.has(srcRel) ? srcRel : nearestAncestor(srcRel);
    const homeHref = relHref(outRel, "index.html");
    // catalog lives at public/index.html — relative from this page's full output path
    const fromFull = `${slug}/${outRel}`;
    const toCatalog = toPosix(posix.relative(posix.dirname("/" + fromFull), "/index.html"));

    const raw = readFileSync(path.join(courseDir, srcRel), "utf8");
    let heroTitle, chipsHtml, body, pageTitle;
    if (meta.kind === "md") {
      const r = renderMarkdown(raw, { rewrite: makeRewrite(srcRel, outRel), fallbackTitle: labelText(srcRel) });
      heroTitle = r.title;
      chipsHtml = r.chipsHtml;
      body = r.body;
      pageTitle = heroTitle.replace(/<[^>]+>/g, "");
      nPages++;
    } else {
      const lang = CODE_EXT[path.extname(srcRel).toLowerCase()] || "text";
      const fname = path.basename(srcRel);
      heroTitle = esc(fname);
      chipsHtml = `<ul class="chips"><li><span class="chip-k">type</span><span class="chip-v">${esc(lang)} source</span></li></ul>`;
      body = renderCodeBody(raw, lang, fname);
      pageTitle = fname;
      nCode++;
    }

    const html = pageShell({
      theme: config.theme,
      pageTitle,
      heroTitle,
      chipsHtml,
      body,
      navHtml: navHtml(activeSrc, outRel),
      crumbsHtml: crumbs(srcRel, outRel, meta),
      pagerHtml: pager(srcRel, outRel),
      heroIndex: meta.heroIndex,
      courseTitle,
      brandMark,
      homeHref,
      catalogHref: toCatalog,
      extLink: config.extLink || null,
    });
    writeFileSync(outAbs, html);
  }

  // synthetic folder-index pages
  const childrenOf = (d) => {
    const seen = new Map();
    for (const rel of published.keys()) {
      if (!rel.startsWith(d + "/")) continue;
      const rest = rel.slice(d.length + 1);
      const seg = rest.split("/")[0];
      const childPath = `${d}/${seg}`;
      if (rest.includes("/")) {
        if (!seen.has(childPath)) seen.set(childPath, { dir: true });
      } else {
        seen.set(childPath, { dir: false, rel });
      }
    }
    return [...seen.entries()]
      .map(([childPath, info]) => {
        if (info.dir) {
          const out = resolveLocal(childPath);
          const label = published.has(childPath + "/README.md")
            ? labelText(childPath + "/README.md")
            : humanize(path.basename(childPath));
          return out ? { out, label, dir: true } : null;
        }
        const meta = published.get(childPath);
        const label = meta.kind === "code" ? path.basename(childPath) : labelText(childPath);
        return { out: srcToOut(childPath, meta.kind), label, dir: false };
      })
      .filter(Boolean)
      .sort((a, b) => (a.dir === b.dir ? naturalCmp(a.label, b.label) : a.dir ? -1 : 1));
  };

  let nSynth = 0;
  for (const [d, meta] of synth) {
    const outRel = `${d}/index.html`;
    const outAbs = path.join(OUT_DIR, slug, outRel);
    mkdirSync(path.dirname(outAbs), { recursive: true });
    const items = childrenOf(d)
      .map(
        (c) =>
          `<li class="folder-item ${c.dir ? "is-dir" : "is-file"}"><a href="${relHref(outRel, c.out)}">${esc(
            c.label,
          )}</a></li>`,
      )
      .join("");
    const body = `<p class="codenote">Folder contents — ${childrenOf(d).length} item(s).</p>
<ul class="folder">${items}</ul>`;
    const fromFull = `${slug}/${outRel}`;
    const toCatalog = toPosix(posix.relative(posix.dirname("/" + fromFull), "/index.html"));
    const activeSrc = nearestAncestor(d);
    const html = pageShell({
      theme: config.theme,
      pageTitle: humanize(path.basename(d)),
      heroTitle: esc(humanize(path.basename(d))),
      chipsHtml: "",
      body,
      navHtml: navHtml(activeSrc, outRel),
      crumbsHtml: crumbs(d, outRel, meta),
      pagerHtml: pager(d, outRel),
      heroIndex: meta.heroIndex,
      courseTitle,
      brandMark,
      homeHref: relHref(outRel, "index.html"),
      catalogHref: toCatalog,
      extLink: config.extLink || null,
    });
    writeFileSync(outAbs, html);
    nSynth++;
  }

  console.log(`  ${slug}: ${nPages} pages, ${nCode} code views, ${nSynth} folder indexes, ${nAssets} assets`);
  return { slug, nPages, nCode, nSynth, nAssets };
}

// ============================================================================
// Main
// ============================================================================
const slugs = entries(COURSES_DIR)
  .filter((e) => e.isDir && existsSync(path.join(e.abs, "site.config.json")))
  .map((e) => e.name)
  .sort(naturalCmp);

if (!slugs.length) {
  console.error("No courses with site.config.json found under courses/.");
  process.exit(1);
}

// clean output
rmSync(OUT_DIR, { recursive: true, force: true });
mkdirSync(OUT_DIR, { recursive: true });

console.log(`Building ${slugs.length} course(s) into ${path.relative(ROOT, OUT_DIR)}/`);
const models = slugs.map(buildCourseModel);
const bySlug = new Map(models.map((m) => [m.slug, m]));
for (const m of models) renderCourse(m, bySlug);

// catalog landing page
const catalog = models.map((m) => ({
  slug: m.slug,
  title: m.config.title,
  tagline: m.config.tagline,
  theme: m.config.theme,
  mark: m.config.mark,
  level: m.config.level,
  duration: m.config.duration,
}));
writeFileSync(path.join(OUT_DIR, "index.html"), catalogPage(catalog));

console.log(`Catalog + ${models.length} courses written.`);
if (warnings.length) {
  console.log(`\n${warnings.length} link(s) point outside the published site:`);
  for (const w of warnings.slice(0, 25)) console.log(`  - ${w}`);
  if (warnings.length > 25) console.log(`  …and ${warnings.length - 25} more`);
}
