// =============================================================================
// Shared rendering engine for the DX Training site.
// Pure, course-agnostic: markdown -> designed HTML, themeable design system,
// page shell, catalog page. The orchestrator (build.mjs) supplies course
// context (nav, link rewriting, theme name) and writes the output.
// =============================================================================

import MarkdownIt from "markdown-it";
import hljs from "highlight.js";

// `highlight` is defined below; this closure resolves it lazily at render time.
const md = new MarkdownIt({
  html: false,
  linkify: false,
  typographer: true,
  highlight: (str, lang) => highlight(str, lang),
});

// ---- text helpers -----------------------------------------------------------
export const esc = (s) =>
  String(s)
    .replace(/&/g, "&amp;")
    .replace(/</g, "&lt;")
    .replace(/>/g, "&gt;")
    .replace(/"/g, "&quot;");

// Build-time syntax highlighting -> token spans. No client JS is shipped; the
// colors come from the `.code .hljs-*` rules in the stylesheet. Falls back to
// plain escaped text for unknown languages or unparseable snippets.
export const highlight = (code, lang) => {
  if (lang && lang !== "text" && hljs.getLanguage(lang)) {
    try {
      return hljs.highlight(code, { language: lang, ignoreIllegals: true }).value;
    } catch {
      /* fall through */
    }
  }
  return esc(code);
};

export const slug = (s) =>
  String(s)
    .toLowerCase()
    .replace(/<[^>]+>/g, "")
    .replace(/[^a-z0-9]+/g, "-")
    .replace(/(^-|-$)/g, "");

export const inlineMd = (s) =>
  esc(s)
    .replace(/`([^`]+)`/g, "<code>$1</code>")
    .replace(/\*\*([^*]+)\*\*/g, "<strong>$1</strong>")
    .replace(/\*([^*]+)\*/g, "<em>$1</em>");

// Humanize a path segment into a label: "session-1" -> "Session 1".
export const humanize = (name) =>
  name
    .replace(/\.[^.]+$/, "")
    .replace(/[-_]+/g, " ")
    .replace(/\b\w/g, (c) => c.toUpperCase());

// First H1 of a markdown source, stripped of trailing emoji noise.
// Parse a leading YAML-ish frontmatter block (flat `key: value` scalars only).
// Returns the parsed data and the content with the block removed. Files without
// a leading `---` fence are returned unchanged with empty data.
export function parseFrontmatter(raw) {
  const m = raw.match(/^---\r?\n([\s\S]*?)\r?\n---[ \t]*\r?\n?/);
  if (!m) return { data: {}, content: raw };
  const data = {};
  for (const line of m[1].split(/\r?\n/)) {
    const mm = line.match(/^([A-Za-z0-9_-]+)\s*:\s*(.*)$/);
    if (!mm) continue;
    let v = mm[2].trim();
    if ((v[0] === '"' && v.at(-1) === '"') || (v[0] === "'" && v.at(-1) === "'")) v = v.slice(1, -1);
    const n = Number(v);
    data[mm[1]] = v !== "" && Number.isFinite(n) ? n : v;
  }
  return { data, content: raw.slice(m[0].length) };
}

export function firstH1(raw) {
  const m = parseFrontmatter(raw).content.match(/^#\s+(.+)$/m);
  return m ? m[1].trim() : null;
}

// ---- markdown -> html -------------------------------------------------------
function buildStepper(body) {
  // Only convert simple left-to-right / top-down flowcharts into a stepper.
  if (!/^\s*(graph|flowchart)\b/.test(body)) return "";
  const seen = new Map();
  const re = /([A-Za-z0-9_]+)\[([^\]]+)\]/g;
  let m;
  while ((m = re.exec(body))) if (!seen.has(m[1])) seen.set(m[1], m[2]);
  if (seen.size < 2) return "";
  const lis = [...seen.values()]
    .map(
      (label, i) =>
        `<li class="step"><span class="step-n">${i + 1}</span><span class="step-t">${esc(
          label,
        )}</span></li>`,
    )
    .join("");
  return `<ol class="stepper" aria-label="Learning path">${lis}</ol>`;
}

const CALLOUT_RULES = [
  [/engineering lead|trainer note|instructor/i, "lead", "👔"],
  [/\bpro tip\b|\btip:/i, "tip", "💡"],
  [
    /golden prerequisite|litmus|the reframe|\bdefault\b|\bexception\b|fail forward|one-question|the shift|key (idea|takeaway)/i,
    "key",
    "🎯",
  ],
  [/\btrap\b|warning|red flag|important|caveat|\bnever\b|do not\b|don't\b|gotcha/i, "warn", "⚠️"],
];
function calloutVariant(text) {
  for (const [re, cls, icon] of CALLOUT_RULES) if (re.test(text)) return { cls, icon };
  return { cls: "note", icon: "📌" };
}

function transform(html, rewrite, stepper) {
  // code fences -> styled blocks with a language label + scroll wrapper
  html = html.replace(
    /<pre><code(?: class="language-([^"]+)")?>([\s\S]*?)<\/code><\/pre>/g,
    (_m, lang, code) => {
      const label = lang || "text";
      return `<figure class="code"><figcaption class="code-bar"><span class="dots"><i></i><i></i><i></i></span><span class="code-lang">${esc(
        label,
      )}</span></figcaption><div class="code-scroll"><pre><code class="hljs">${code}</code></pre></div></figure>`;
    },
  );

  // blockquotes -> callouts (variant chosen by leading keyword)
  html = html.replace(/<blockquote>([\s\S]*?)<\/blockquote>/g, (_m, inner) => {
    const text = inner.replace(/<[^>]+>/g, " ");
    const v = calloutVariant(text);
    return `<aside class="callout cv-${v.cls}"><div class="cv-ic" aria-hidden="true">${v.icon}</div><div class="cv-body">${inner}</div></aside>`;
  });

  // task list items -> styled checkboxes. Emit the checkbox as a direct child of
  // <li> (before any <p>) so it sizes in both tight and loose lists: a loose list
  // wraps item text in <p>, and an inline <span> inside <p> ignores width/height,
  // which would collapse the checkbox to a thin vertical bar.
  html = html.replace(/<li>(\s*<p>)?\s*\[( |x|X)\]\s*/g, (_m, p, c) => {
    const done = c.toLowerCase() === "x";
    return `<li class="task${done ? " done" : ""}"><span class="cb" aria-hidden="true"></span>${p ? "<p>" : ""}`;
  });

  // heading anchors
  html = html.replace(/<(h[2-4])>([\s\S]*?)<\/\1>/g, (_m, tag, inner) => {
    const id = slug(inner);
    return `<${tag} id="${id}"><a class="anchor" href="#${id}" aria-hidden="true">#</a>${inner}</${tag}>`;
  });

  // tables -> horizontal scroll wrapper
  html = html
    .replace(/<table>/g, '<div class="table-wrap"><table>')
    .replace(/<\/table>/g, "</table></div>");

  // learning-path stepper placeholder
  if (stepper) html = html.replace(/<p>STEPPERPLACEHOLDER<\/p>/, stepper);

  // rewrite internal links via the orchestrator-supplied callback
  html = html.replace(/href="([^"]+)"/g, (_m, href) => `href="${rewrite(href)}"`);

  return html;
}

// Render markdown body -> { title, chipsHtml, body }. `rewrite` maps an href.
// `fallbackTitle` is used when the source has no H1.
export function renderMarkdown(raw, { rewrite, fallbackTitle }) {
  let src = parseFrontmatter(raw).content;
  let stepper = "";
  src = src.replace(/```mermaid\n([\s\S]*?)```/g, (m, body) => {
    const s = buildStepper(body);
    if (!s) return m; // leave non-flowchart mermaid as a normal code block
    stepper = s;
    return "\n\nSTEPPERPLACEHOLDER\n\n";
  });

  const lines = src.split("\n");
  let title = fallbackTitle;
  const chips = [];
  const out = [];
  let titleFound = false;
  for (let i = 0; i < lines.length; i++) {
    const ln = lines[i];
    if (!titleFound && /^#\s+/.test(ln)) {
      title = ln.replace(/^#\s+/, "").trim();
      titleFound = true;
      let j = i + 1;
      while (j < lines.length && lines[j].trim() === "") j++;
      while (j < lines.length) {
        // meta lines like "**Duration:** 2 hours" become hero chips
        const mm = lines[j].match(/^\*\*([^*:]+):\*\*\s*(.+?)\s*$/);
        if (!mm) break;
        chips.push({ k: mm[1].trim(), v: mm[2].trim() });
        j++;
      }
      i = j - 1;
      continue;
    }
    out.push(ln);
  }

  const body = transform(md.render(out.join("\n")), rewrite, stepper);
  const chipsHtml = chips.length
    ? `<ul class="chips">${chips
        .map(
          (c) =>
            `<li><span class="chip-k">${esc(c.k)}</span><span class="chip-v">${inlineMd(c.v)}</span></li>`,
        )
        .join("")}</ul>`
    : "";
  return { title: inlineMd(title || ""), chipsHtml, body };
}

// Render a source code file as a read-only code-view page body.
export function renderCodeBody(raw, lang, fname) {
  return `<p class="codenote">Source example from the course — rendered read-only. The canonical file lives in the repository.</p>
<figure class="code code-full"><figcaption class="code-bar"><span class="dots"><i></i><i></i><i></i></span><span class="code-lang">${esc(
    lang,
  )}</span><span class="code-file">${esc(fname)}</span></figcaption><div class="code-scroll"><pre><code class="hljs">${highlight(
    raw,
    lang,
  )}</code></pre></div></figure>`;
}

// ---- page shell -------------------------------------------------------------
// All fields are pre-rendered HTML strings except plain text titles/labels.
export function pageShell({
  theme,
  pageTitle, // plain text, for <title>
  heroTitle, // html
  chipsHtml,
  body,
  navHtml,
  crumbsHtml,
  pagerHtml,
  heroIndex,
  courseTitle,
  brandMark,
  homeHref, // course home, relative to this page
  catalogHref, // hub catalog, relative to this page
  extLink, // { href, label } | null
}) {
  const ext = extLink
    ? `<a class="topbar-ext" href="${esc(extLink.href)}" target="_blank" rel="noopener">${esc(
        extLink.label,
      )} ↗</a>`
    : "";
  return `<!doctype html>
<html lang="en">
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<title>${esc(pageTitle)} · ${esc(courseTitle)}</title>
<style>${css(theme)}</style>
<script>${THEME_JS}</script>
</head>
<body>
<header class="topbar">
  <a class="brand" href="${homeHref}"><span class="brand-mark">${esc(
    brandMark,
  )}</span><span class="brand-txt">${esc(courseTitle)}</span></a>
  <div class="topbar-right">
    <a class="topbar-ext topbar-hub" href="${catalogHref}">All courses</a>
    ${ext}
    <button id="themeBtn" class="theme-toggle" type="button" aria-label="Toggle dark mode" title="Toggle dark mode">☾</button>
  </div>
</header>
<div class="layout">
  <aside class="sidebar">
    <details class="navwrap" open>
      <summary>On this course</summary>
      <nav class="nav">${navHtml}</nav>
    </details>
  </aside>
  <main class="content">
    <section class="hero hero-${heroIndex}">
      <div class="wrap">
        <nav class="crumbs">${crumbsHtml}</nav>
        <h1>${heroTitle}</h1>
        ${chipsHtml}
      </div>
    </section>
    <article class="prose wrap">
${body}
    </article>
    <div class="wrap">${pagerHtml}</div>
    <footer class="sitefoot wrap">
      <span>RealManage DX · ${esc(courseTitle)}</span>
      <a href="${catalogHref}">All courses</a>
    </footer>
  </main>
</div>
</body>
</html>`;
}

// ---- catalog (hub) page -----------------------------------------------------
export function catalogPage(courses) {
  const cards = courses
    .map((c) => {
      const chips = [c.level, c.duration]
        .filter(Boolean)
        .map((v) => `<span class="cc-chip">${esc(v)}</span>`)
        .join("");
      return `<a class="course-card cc-${c.theme}" href="${esc(c.slug)}/index.html">
      <span class="cc-mark">${esc(c.mark || "•")}</span>
      <span class="cc-body">
        <span class="cc-title">${esc(c.title)}</span>
        <span class="cc-tag">${esc(c.tagline || "")}</span>
        <span class="cc-chips">${chips}</span>
      </span>
      <span class="cc-go">Start →</span>
    </a>`;
    })
    .join("\n");

  return `<!doctype html>
<html lang="en">
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<title>RealManage DX Training</title>
<style>${css("hub")}
.hub-main{max-width:920px;margin:0 auto;padding:0 24px 64px}
.hub-hero{color:#fff;background:${THEMES.hub.heroes[0]};padding:64px 0 56px;margin-bottom:8px}
.hub-hero .wrap{max-width:920px}
.hub-hero h1{font-size:clamp(30px,4.5vw,46px);margin:.1em 0 .25em;letter-spacing:-.02em;line-height:1.1}
.hub-hero p{font-size:18px;max-width:60ch;margin:0;color:rgba(255,255,255,.9)}
.hub-eyebrow{font-size:12px;font-weight:800;letter-spacing:.16em;text-transform:uppercase;color:rgba(255,255,255,.8)}
.courses{display:grid;gap:16px;margin-top:34px}
.course-card{display:flex;align-items:center;gap:18px;padding:22px;border:1px solid var(--line);border-radius:var(--radius);
  background:var(--bg);box-shadow:var(--shadow);color:var(--ink)}
.course-card:hover{text-decoration:none;border-color:var(--brand);transform:translateY(-1px)}
.cc-mark{flex:0 0 auto;display:grid;place-items:center;width:54px;height:54px;border-radius:13px;color:#fff;font-size:17px;font-weight:800;
  background:linear-gradient(135deg,var(--brand-d),var(--accent))}
.cc-indigo .cc-mark{background:linear-gradient(135deg,#1d3f93,#0f9d8c)}
.cc-teal .cc-mark{background:linear-gradient(135deg,#0a6e60,#5b6dd0)}
.cc-violet .cc-mark{background:linear-gradient(135deg,#533aa6,#e0843c)}
.cc-body{display:flex;flex-direction:column;gap:4px;min-width:0;flex:1}
.cc-title{font-size:19px;font-weight:700;line-height:1.2}
.cc-tag{color:var(--muted);font-size:14.5px}
.cc-chips{display:flex;flex-wrap:wrap;gap:6px;margin-top:6px}
.cc-chip{font-size:11.5px;font-weight:600;color:var(--brand-d);background:var(--key-bg);border:1px solid var(--line);
  padding:2px 9px;border-radius:999px}
.cc-go{flex:0 0 auto;align-self:center;font-weight:700;color:var(--brand-d);white-space:nowrap}
.hub-foot{max-width:920px;margin:0 auto;padding:24px;color:var(--muted);font-size:13.5px;border-top:1px solid var(--line)}
@media (max-width:620px){ .course-card{flex-wrap:wrap} .cc-go{margin-left:auto} }
</style>
<script>${THEME_JS}</script>
</head>
<body>
<header class="topbar">
  <a class="brand" href="index.html"><span class="brand-mark">DX</span><span class="brand-txt">RealManage DX Training</span></a>
  <div class="topbar-right">
    <button id="themeBtn" class="theme-toggle" type="button" aria-label="Toggle dark mode" title="Toggle dark mode">☾</button>
  </div>
</header>
<section class="hub-hero">
  <div class="wrap" style="max-width:920px;margin:0 auto;padding:0 24px">
    <div class="hub-eyebrow">RealManage · Developer Experience</div>
    <h1>Training Hub</h1>
    <p>Hands-on, AI-assisted engineering courses. Pick a course to begin — each is self-paced and self-contained.</p>
  </div>
</section>
<main class="hub-main">
  <div class="courses">
${cards}
  </div>
</main>
<footer class="hub-foot">RealManage DX · Internal training. ${courses.length} courses available.</footer>
</body>
</html>`;
}

// ============================================================================
// THEMES — brand/accent palettes (light + dark) and section hero gradients
// ============================================================================
export const THEMES = {
  indigo: {
    brand: "#2b59c3", brandD: "#1d3f93", accent: "#0f9d8c", accentD: "#0b7d70",
    dBrand: "#74a0f2", dBrandD: "#a9c4f7", dAccent: "#3ed7c4", dAccentD: "#5fe3d2",
    heroes: [
      "linear-gradient(135deg,#16306f 0%,#2b59c3 55%,#0f9d8c 130%)",
      "linear-gradient(135deg,#16306f,#2b59c3 70%,#3f7fd6 130%)",
      "linear-gradient(135deg,#143a6b,#1f6fae 60%,#0f9d8c 130%)",
      "linear-gradient(135deg,#0c5b54,#0f9d8c 60%,#2b59c3 140%)",
      "linear-gradient(135deg,#3a2d72,#5b4bbf 60%,#2b59c3 130%)",
      "linear-gradient(135deg,#8a3b1f,#c2410c 55%,#b7791f 130%)",
    ],
  },
  teal: {
    brand: "#0e8f7e", brandD: "#0a6e60", accent: "#5b6dd0", accentD: "#4757b0",
    dBrand: "#3ed7c4", dBrandD: "#66e3d3", dAccent: "#a3aef0", dAccentD: "#c2caf7",
    heroes: [
      "linear-gradient(135deg,#0a5d54 0%,#0e8f7e 55%,#5b6dd0 135%)",
      "linear-gradient(135deg,#0a5d54,#0e8f7e 70%,#1fae9c 130%)",
      "linear-gradient(135deg,#0c5b6e,#1f9eae 60%,#5b6dd0 135%)",
      "linear-gradient(135deg,#324a9c,#5b6dd0 55%,#0e8f7e 140%)",
      "linear-gradient(135deg,#2d6a72,#0e8f7e 60%,#3fae8c 130%)",
      "linear-gradient(135deg,#1f7a5f,#3fae6c 55%,#b7a01f 135%)",
    ],
  },
  violet: {
    brand: "#6d4bd0", brandD: "#533aa6", accent: "#e0843c", accentD: "#c2701f",
    dBrand: "#b69cf5", dBrandD: "#cdbcfa", dAccent: "#f0a85f", dAccentD: "#f4bd82",
    heroes: [
      "linear-gradient(135deg,#3a2d72 0%,#6d4bd0 55%,#e0843c 140%)",
      "linear-gradient(135deg,#3a2d72,#6d4bd0 70%,#8b6ee0 130%)",
      "linear-gradient(135deg,#4a2d8c,#7d5bd0 60%,#e0843c 140%)",
      "linear-gradient(135deg,#6d3b9c,#a05bd0 55%,#6d4bd0 135%)",
      "linear-gradient(135deg,#8a4b1f,#e0843c 55%,#6d4bd0 140%)",
      "linear-gradient(135deg,#2d3a8c,#5b6dd0 55%,#6d4bd0 130%)",
    ],
  },
  hub: {
    brand: "#2b59c3", brandD: "#1d3f93", accent: "#0f9d8c", accentD: "#0b7d70",
    dBrand: "#74a0f2", dBrandD: "#a9c4f7", dAccent: "#3ed7c4", dAccentD: "#5fe3d2",
    heroes: ["linear-gradient(135deg,#16306f 0%,#5b4bbf 45%,#0f9d8c 120%)"],
  },
};

// ============================================================================
// css(themeName) — the design system, with brand/accent/hero from the theme.
// Inlined into every page so each page is fully self-contained.
// ============================================================================
export function css(themeName) {
  const t = THEMES[themeName] || THEMES.indigo;
  const heroCss = t.heroes.map((g, i) => `.hero-${i}{background:${g}}`).join("\n");
  return `
:root{
  --bg:#ffffff; --soft:#f5f8fc; --soft2:#eef3f9; --ink:#16202c; --muted:#5b6b7c;
  --line:#e2e8f1; --line2:#d4deea; --topbar-bg:rgba(255,255,255,.92); --cb-bg:#ffffff;
  --brand:${t.brand}; --brand-d:${t.brandD}; --accent:${t.accent}; --accent-d:${t.accentD};
  --code-bg:#0e1a2b; --code-ink:#d7e2f2; --code-line:#223450;
  --lead:#b7791f; --lead-bg:#fff8ec; --tip:#0b7d70; --tip-bg:#e9faf6;
  --key:${t.brandD}; --key-bg:#eef3ff; --warn:#c2410c; --warn-bg:#fff3ec; --note:#3a5a8c; --note-bg:#eef3fb;
  --radius:14px; --shadow:0 1px 2px rgba(16,32,54,.06),0 8px 24px rgba(16,32,54,.06);
}
@media (prefers-color-scheme:dark){ :root:not([data-theme="light"]){
  --bg:#0d1117; --soft:#161b22; --soft2:#1c2632; --ink:#e6edf3; --muted:#9bacbd;
  --line:#283039; --line2:#39434f; --topbar-bg:rgba(13,17,23,.85); --cb-bg:#1c2632;
  --brand:${t.dBrand}; --brand-d:${t.dBrandD}; --accent:${t.dAccent}; --accent-d:${t.dAccentD};
  --code-bg:#080c12; --code-ink:#d7e2f2; --code-line:#1e2a3d;
  --lead:#e0b85f; --lead-bg:#2a2210; --tip:#3ed7c4; --tip-bg:#0e2723;
  --key:${t.dBrand}; --key-bg:#15233f; --warn:#f08a52; --warn-bg:#2c1b11; --note:#7fa8e0; --note-bg:#141f2e;
  --shadow:0 1px 2px rgba(0,0,0,.4),0 8px 24px rgba(0,0,0,.35);
}}
:root[data-theme="dark"]{
  --bg:#0d1117; --soft:#161b22; --soft2:#1c2632; --ink:#e6edf3; --muted:#9bacbd;
  --line:#283039; --line2:#39434f; --topbar-bg:rgba(13,17,23,.85); --cb-bg:#1c2632;
  --brand:${t.dBrand}; --brand-d:${t.dBrandD}; --accent:${t.dAccent}; --accent-d:${t.dAccentD};
  --code-bg:#080c12; --code-ink:#d7e2f2; --code-line:#1e2a3d;
  --lead:#e0b85f; --lead-bg:#2a2210; --tip:#3ed7c4; --tip-bg:#0e2723;
  --key:${t.dBrand}; --key-bg:#15233f; --warn:#f08a52; --warn-bg:#2c1b11; --note:#7fa8e0; --note-bg:#141f2e;
  --shadow:0 1px 2px rgba(0,0,0,.4),0 8px 24px rgba(0,0,0,.35);
}
*{box-sizing:border-box}
html{-webkit-text-size-adjust:100%}
body{margin:0;background:var(--bg);color:var(--ink);
  font:400 17px/1.7 -apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,Helvetica,Arial,sans-serif;
  -webkit-font-smoothing:antialiased;}
a{color:var(--brand);text-decoration:none}
a:hover{text-decoration:underline}
img{max-width:100%}

.topbar{position:sticky;top:0;z-index:30;height:56px;display:flex;align-items:center;justify-content:space-between;
  padding:0 18px;background:var(--topbar-bg);backdrop-filter:saturate(1.4) blur(8px);border-bottom:1px solid var(--line)}
.brand{display:flex;align-items:center;gap:10px;color:var(--ink);font-weight:700}
.brand:hover{text-decoration:none}
.brand-mark{display:grid;place-items:center;min-width:30px;height:30px;padding:0 7px;border-radius:8px;color:#fff;font-size:12px;font-weight:800;
  background:linear-gradient(135deg,var(--brand-d),var(--accent))}
.brand-txt{font-size:15px;letter-spacing:.2px}
.topbar-right{display:flex;align-items:center;gap:14px}
.topbar-ext{font-size:13px;color:var(--muted)}
.topbar-hub{font-weight:600}
.theme-toggle{display:grid;place-items:center;width:34px;height:34px;padding:0;font-size:16px;line-height:1;cursor:pointer;
  color:var(--ink);background:var(--soft);border:1px solid var(--line);border-radius:9px}
.theme-toggle:hover{background:var(--soft2);border-color:var(--line2)}

.layout{max-width:1240px;margin:0 auto;display:grid;grid-template-columns:280px minmax(0,1fr)}
.sidebar{position:sticky;top:56px;align-self:start;height:calc(100vh - 56px);overflow:auto;
  padding:22px 16px;border-right:1px solid var(--line);background:var(--bg)}
.content{min-width:0}
.wrap{max-width:780px;margin-inline:auto;padding-inline:24px}

.navwrap>summary{display:none;cursor:pointer;font-weight:700;padding:8px 12px;border:1px solid var(--line);border-radius:10px;list-style:none}
.navwrap>summary::-webkit-details-marker{display:none}
.nav{margin-top:4px}
.nav ul{list-style:none;margin:0;padding:0}
.nav-top a{font-weight:700}
.nav-group{margin-top:18px}
.nav-h{font-size:11px;font-weight:800;letter-spacing:.12em;text-transform:uppercase;color:var(--muted);margin:0 12px 6px}
.nav a{display:block;padding:7px 12px;margin:2px 0;border-radius:9px;color:var(--ink);font-size:14.5px;line-height:1.35}
.nav a:hover{background:var(--soft);text-decoration:none}
.nav a.on{background:var(--key-bg);color:var(--brand-d);font-weight:700;box-shadow:inset 3px 0 0 var(--brand)}
.nav-sub{margin:1px 0 4px 14px;border-left:1px solid var(--line)}
.nav-sub a{font-size:13px;color:var(--muted);padding:5px 10px}
.nav-sub a.on{color:var(--brand-d)}

.hero{color:#fff;background:${t.heroes[0]};padding:40px 0 34px}
${heroCss}
.hero h1{margin:.35em 0 0;font-size:clamp(26px,3.6vw,38px);line-height:1.18;letter-spacing:-.01em}
.hero .crumbs{font-size:13.5px;color:rgba(255,255,255,.85)}
.hero .crumbs a{color:#fff;opacity:.92}
.crumbs .sep{margin:0 8px;opacity:.6}
.crumbs .cur{opacity:.85}
.chips{display:flex;flex-wrap:wrap;gap:8px;list-style:none;margin:18px 0 0;padding:0}
.chips li{display:flex;align-items:center;gap:7px;background:rgba(255,255,255,.16);border:1px solid rgba(255,255,255,.25);
  padding:5px 11px;border-radius:999px;font-size:13px;color:#fff}
.chip-k{font-weight:700;text-transform:uppercase;letter-spacing:.06em;font-size:10.5px;opacity:.85}

.prose{padding-top:30px;padding-bottom:8px}
.prose h2{font-size:25px;line-height:1.25;margin:2.1em 0 .5em;padding-top:.5em;letter-spacing:-.01em;border-top:1px solid var(--line)}
.prose h2:first-of-type{border-top:0;margin-top:.4em}
.prose h3{font-size:19px;margin:1.7em 0 .4em}
.prose h4{font-size:16px;margin:1.5em 0 .3em;color:var(--brand-d)}
.prose p{margin:.85em 0}
.prose ul,.prose ol{margin:.7em 0;padding-left:1.4em}
.prose li{margin:.32em 0}
.prose li::marker{color:var(--accent)}
.prose strong{font-weight:700}
.prose hr{border:0;border-top:1px solid var(--line);margin:2em 0}
.prose code{background:var(--soft2);border:1px solid var(--line);border-radius:6px;padding:.1em .42em;font-size:.86em;
  font-family:ui-monospace,SFMono-Regular,"SF Mono",Menlo,Consolas,monospace}
.anchor{float:left;margin-left:-1.1em;padding-right:.35em;color:var(--line2);opacity:0;font-weight:400;text-decoration:none}
h2:hover .anchor,h3:hover .anchor,h4:hover .anchor{opacity:1}
.prose blockquote{margin:1em 0}

.callout{display:flex;gap:14px;margin:1.4em 0;padding:16px 18px;border-radius:var(--radius);
  border:1px solid var(--line2);background:var(--note-bg);box-shadow:var(--shadow)}
.callout .cv-ic{font-size:20px;line-height:1.3;flex:0 0 auto}
.callout .cv-body{min-width:0}
.callout .cv-body>:first-child{margin-top:0}
.callout .cv-body>:last-child{margin-bottom:0}
.cv-lead{background:var(--lead-bg);box-shadow:inset 4px 0 0 var(--lead),var(--shadow);border-color:color-mix(in srgb,var(--lead) 40%,var(--bg))}
.cv-tip{background:var(--tip-bg);box-shadow:inset 4px 0 0 var(--tip),var(--shadow);border-color:color-mix(in srgb,var(--tip) 40%,var(--bg))}
.cv-key{background:var(--key-bg);box-shadow:inset 4px 0 0 var(--brand),var(--shadow);border-color:color-mix(in srgb,var(--brand) 40%,var(--bg))}
.cv-warn{background:var(--warn-bg);box-shadow:inset 4px 0 0 var(--warn),var(--shadow);border-color:color-mix(in srgb,var(--warn) 40%,var(--bg))}
.cv-note{background:var(--note-bg);box-shadow:inset 4px 0 0 var(--note),var(--shadow);border-color:color-mix(in srgb,var(--note) 40%,var(--bg))}

.code{margin:1.4em 0;border-radius:var(--radius);overflow:hidden;border:1px solid var(--code-line);box-shadow:var(--shadow);background:var(--code-bg)}
.code-bar{display:flex;align-items:center;gap:10px;padding:9px 14px;background:#0a1422;border-bottom:1px solid var(--code-line)}
.dots{display:inline-flex;gap:6px}
.dots i{width:10px;height:10px;border-radius:50%;background:#2a3c58;display:inline-block}
.dots i:first-child{background:#ff5f57}.dots i:nth-child(2){background:#febc2e}.dots i:last-child{background:#28c840}
.code-lang{font-size:11px;font-weight:700;letter-spacing:.1em;text-transform:uppercase;color:#7f93b3}
.code-file{margin-left:auto;font-size:12px;color:#9fb0cc;font-family:ui-monospace,Menlo,Consolas,monospace}
.code-scroll{overflow-x:auto}
.code pre{margin:0;padding:16px 18px;min-width:0}
.code code{background:none;border:0;padding:0;color:var(--code-ink);font-size:13.5px;line-height:1.65;
  font-family:ui-monospace,SFMono-Regular,"SF Mono",Menlo,Consolas,monospace;white-space:pre}
.code-full pre{font-size:13px}
.code .hljs-comment,.code .hljs-quote{color:#5c6e91;font-style:italic}
.code .hljs-keyword,.code .hljs-selector-tag,.code .hljs-section,.code .hljs-doctag{color:#c099ff}
.code .hljs-literal,.code .hljs-number{color:#ff966c}
.code .hljs-string,.code .hljs-regexp,.code .hljs-addition,.code .hljs-meta .hljs-string{color:#c3e88d}
.code .hljs-title,.code .hljs-title.function_{color:#82aaff}
.code .hljs-attr,.code .hljs-attribute,.code .hljs-property{color:#ffc777}
.code .hljs-built_in,.code .hljs-type,.code .hljs-title.class_,.code .hljs-class .hljs-title{color:#4fd6be}
.code .hljs-meta,.code .hljs-meta .hljs-keyword{color:#86e1fc}
.code .hljs-name,.code .hljs-tag{color:#ff757f}
.code .hljs-symbol,.code .hljs-bullet,.code .hljs-link{color:#86e1fc}
.code .hljs-variable,.code .hljs-template-variable,.code .hljs-params{color:#c8d3f5}
.code .hljs-deletion{color:#ff757f}
.code .hljs-emphasis{font-style:italic}.code .hljs-strong{font-weight:700}
.codenote{color:var(--muted);font-size:14px;font-style:italic}

.folder{list-style:none;margin:1.2em 0;padding:0;display:grid;gap:8px}
.folder-item a{display:flex;align-items:center;gap:10px;padding:11px 14px;border:1px solid var(--line);border-radius:10px;
  background:var(--bg);color:var(--ink);box-shadow:var(--shadow);font-weight:600}
.folder-item a:hover{border-color:var(--brand);text-decoration:none;background:var(--soft)}
.folder-item a::before{font-size:15px}
.folder-item.is-dir a::before{content:"📁"}
.folder-item.is-file a::before{content:"📄"}

.table-wrap{overflow-x:auto;margin:1.4em 0;border:1px solid var(--line);border-radius:var(--radius);box-shadow:var(--shadow)}
table{border-collapse:collapse;width:100%;font-size:14.5px}
thead th{background:var(--soft2);text-align:left;font-weight:700;color:var(--ink)}
th,td{padding:10px 14px;border-bottom:1px solid var(--line);vertical-align:top}
tbody tr:nth-child(even){background:var(--soft)}
tbody tr:last-child td{border-bottom:0}

.prose ul:has(>li.task){list-style:none;padding-left:.2em}
li.task{display:flex;align-items:flex-start;gap:10px;margin:.4em 0}
li.task .cb{flex:0 0 auto;margin-top:.28em;width:17px;height:17px;border:2px solid var(--line2);border-radius:5px;background:var(--cb-bg)}
li.task.done .cb{background:var(--accent);border-color:var(--accent);position:relative}
li.task.done .cb::after{content:"";position:absolute;left:4px;top:0px;width:5px;height:10px;border:solid #fff;border-width:0 2.5px 2.5px 0;transform:rotate(45deg)}
li.task.done{color:var(--muted)}
li.task p{margin:0}

.stepper{list-style:none;display:flex;flex-wrap:wrap;gap:10px;margin:1.6em 0;padding:0;counter-reset:s}
.stepper .step{flex:1 1 160px;display:flex;align-items:center;gap:11px;padding:14px;border:1px solid var(--line2);
  border-radius:var(--radius);background:var(--soft);box-shadow:var(--shadow);position:relative}
.step-n{flex:0 0 auto;display:grid;place-items:center;width:28px;height:28px;border-radius:50%;color:#fff;font-weight:800;font-size:14px;
  background:linear-gradient(135deg,var(--brand-d),var(--accent))}
.step-t{font-size:14px;font-weight:600;line-height:1.3}

.pager{display:flex;gap:14px;margin:34px 0 8px;flex-wrap:wrap}
.pg{flex:1 1 200px;display:flex;flex-direction:column;gap:3px;padding:14px 16px;border:1px solid var(--line);
  border-radius:var(--radius);background:var(--bg);box-shadow:var(--shadow)}
.pg:hover{border-color:var(--brand);text-decoration:none;background:var(--soft)}
.pg-next{text-align:right;margin-left:auto}
.pg-d{font-size:11px;font-weight:700;letter-spacing:.1em;text-transform:uppercase;color:var(--muted)}
.pg-t{font-weight:700;color:var(--brand-d)}
.sitefoot{display:flex;justify-content:space-between;align-items:center;gap:12px;flex-wrap:wrap;
  margin-top:34px;padding-top:18px;padding-bottom:48px;border-top:1px solid var(--line);color:var(--muted);font-size:13.5px}

@media (max-width:900px){
  .layout{grid-template-columns:1fr}
  .sidebar{position:static;height:auto;overflow:visible;border-right:0;border-bottom:1px solid var(--line);padding:14px 16px}
  .navwrap>summary{display:block}
  .navwrap[open]>summary{margin-bottom:8px}
  .brand-txt{font-size:14px}
}
@media print{ .topbar,.sidebar,.pager{display:none} .layout{display:block} }
`;
}

// ============================================================================
// THEME_JS — restore saved theme before paint, wire the toggle. Choice is
// shared across all courses (one key) so it persists hub-wide.
// ============================================================================
export const THEME_JS = `
(function(){
  var d=document.documentElement, k="rmdx-theme", s=null;
  try{ s=localStorage.getItem(k); }catch(e){}
  if(s){ d.setAttribute("data-theme", s); }
  function eff(){
    var t=d.getAttribute("data-theme");
    if(t) return t;
    return (window.matchMedia && matchMedia("(prefers-color-scheme:dark)").matches) ? "dark" : "light";
  }
  function paint(){ var b=document.getElementById("themeBtn"); if(b){ b.textContent = eff()==="dark" ? "☀" : "☾"; } }
  function toggle(){ var n = eff()==="dark" ? "light" : "dark"; d.setAttribute("data-theme", n); try{ localStorage.setItem(k, n); }catch(e){} paint(); }
  document.addEventListener("DOMContentLoaded", function(){
    paint();
    var b=document.getElementById("themeBtn");
    if(b){ b.addEventListener("click", toggle); }
  });
})();
`;
