# DX Training site builder

Renders the training courses into a single, designed, self-contained static
site under `public/`, published via **GitLab Pages**. The course markdown is the
single source of truth — this builder only ever writes to `public/`.

```
site/
├── build.mjs        # discovery + nav model + orchestration + catalog
├── lib/render.mjs   # rendering engine (markdown→HTML, themeable CSS, page shell)
└── package.json     # one dependency: markdown-it
courses/<course>/site.config.json   # per-course config (opt-in)
public/              # build output (git-ignored)
```

## Build locally

```bash
cd site
npm install       # first time only
npm run build     # writes ../public
```

Then open `public/index.html` (the catalog) in a browser. Every page is fully
self-contained — CSS and the theme toggle are inlined, no network calls — so it
works straight off the filesystem and over Pages alike. Internal links are
relative, so the site works under any subpath (e.g. `/<project>/<course>/`).

## How a course gets published

A course is included **iff** it has a `courses/<course>/site.config.json`. To
add a new course to the site, drop that file in — nothing else to register.

The builder, per course:

1. Reads `site.config.json` for title, theme, and the nav model.
2. Publishes the pages reachable from `nav` (whole subtree for `dir` sections),
   minus hard ignores and per-course `exclude`s.
3. Renders each `.md` to a designed page and each example source file to a
   read-only "code view"; copies image assets verbatim.
4. Generates a folder-index page for any content directory without its own
   `README.md`, so directory links resolve.
5. Emits a catalog landing page (`public/index.html`) linking every course.

Links that point outside the published set are reported at the end of the build
so they can be fixed or deliberately ignored.

### `site.config.json` schema

```jsonc
{
  "title":   "Continuous Delivery 101",     // full course title (hero, <title>)
  "short":   "CD 101",                       // breadcrumb root label
  "mark":    "CD",                            // brand chip text
  "theme":   "indigo",                        // indigo | teal | violet
  "tagline": "One-line description.",         // catalog card + meta
  "level":   "Intermediate",                  // catalog chip (optional)
  "duration":"3 sessions (2h each)",          // catalog chip (optional)
  "extLink": { "href": "https://…", "label": "Built on …" },  // top-bar link (optional)

  "nav": [
    { "path": "README.md", "label": "Course Overview" },   // single page
    { "section": "Sessions", "dir": "sessions" },           // auto-list a directory
    { "section": "Reference", "paths": ["docs/a.md", "docs/b.md"] }  // explicit list
  ],

  "exclude": ["**/sandbox", "docs/course-feedback"],  // drop paths (optional)
  "labels":  { "sessions/session-1/README.md": "1 · Intro" }  // sidebar/label overrides
}
```

Section forms:

- **`path`** — one top-level link (e.g. the course README).
- **`dir`** — auto-discovers immediate children (subdir landing pages + loose
  `.md`) for the sidebar, and publishes the entire subtree so deep links work.
- **`paths`** — an explicit, ordered list of pages (use for curated subsets,
  e.g. picking specific files out of a `docs/` folder).

`exclude` entries are either a path prefix (`docs/course-feedback`) or a
segment match (`**/sandbox` drops any directory named `sandbox`). Build output,
`node_modules`, `bin/`, `obj/`, editor dirs, `CLAUDE.md`, project files, and
`site.config.json` itself are always ignored.

### Themes

Three palettes live in `lib/render.mjs` → `THEMES` (`indigo`, `teal`,
`violet`), each defining light/dark brand colors and section hero gradients.
Add a key there to introduce a new palette; reference it by name in a config.

## Deployment (GitLab Pages)

The root `.gitlab-ci.yml` defines a `pages` job that runs `npm install &&
npm run build` and publishes `public/`. It deploys from the default branch and
builds (without deploying) on merge requests.

- **Public site from a private repo:** Pages visibility is independent of repo
  visibility — set it under *Settings → General → Visibility → Pages* (or it is
  public by default if instance-wide Pages access control is off).
- The site is served at the project's Pages URL with each course under
  `/<course>/`; the catalog is at the root.
