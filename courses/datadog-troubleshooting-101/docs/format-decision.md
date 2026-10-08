# Decision: Presentation Format

**Decided:** 2026-09-25 by Edgar Sanchez · **Chosen:** reveal.js deck in the repo (`slides/deck.html`)

## Context

DX-1673 left the presentation surface open. It asked for the same two or three sample slides to be built in each candidate format, the differences explained, and the presenter to choose before the full deck was built. The course text (content, speaker script, prompts) lives in the repo as Markdown regardless.

The same three slides from `slides/deck-outline.md` were built in each option: slide 7 (screenshot-led), slide 12 (diagram-led) and slide 20 (demo hand-off). All three used the RealManage brand palette. The samples were kept outside the repo.

## Options compared

| | reveal.js in the repo | claude.ai Slides artifact | Claude Design artifact |
| - | - | - | - |
| Presenter view, notes hidden | Speaker window (S) with notes, timer and next slide; the shared window shows none. Proven by the CD 101 decks. | Has speaker notes, readable by anyone who can open the deck. Whether they reach PowerPoint's presenter view after PPTX export was **not verified** (the session's browser was not signed in to claude.ai). | No speaker notes; a script could only be visible stickies on the canvas. |
| Over screen share | Crisp 16:9 in a browser; fonts bundled | Crisp 16:9 in Present mode | A canvas of artboards, not a slideshow |
| Switching to terminal / Datadog | Browser tab or window; Alt+Tab | Same | Awkward: the canvas loses your place |
| Offline | Fully offline | Needs internet and a claude.ai sign-in (exports work offline) | Needs internet |
| Edits flow back to the repo | The deck is repo text, reviewed with the outline | Edited in claude.ai; only the outline is in the repo, so it can drift | Same as Slides |
| Export / sharing | Published with the course on GitLab Pages; PDF via `?print-pdf` | Private until shared; PPTX/PDF download | PDF/PPTX/HTML export |
| Editing ease | HTML, or ask Claude | Easiest: WYSIWYG | WYSIWYG on a canvas |

Claude Design was available on the account (its artifact type was listed), which answered the ticket's question about whether it is enabled.

## Decision and consequences

reveal.js: the only option with notes proven hidden from the audience, working offline, and living in the repo as reviewable text next to its outline. It also matches the CD 101 precedent.

- The copied reveal.js runtime is shared by rule: `.gitignore` re-includes `courses/*/slides/reveal/dist/` for every course, replacing the CD 101-only lines.
- The deck's speaker notes must match `deck-outline.md`'s Say track; change the outline first.
- The deck and screenshots are published on the public course site, which is why every screenshot is sanitized.
