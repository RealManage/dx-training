# CD 101 — Slides

Presenter decks for Continuous Delivery 101. Built on [reveal.js](https://revealjs.com)
(vendored locally under `reveal/`, so the decks run fully offline — no network in the room).

## Files

| File | What it is |
| - | - |
| `session-1.html` | Session 1 deck — *Why CD & the Minimums* (8 slides) |
| `session-1-outline.md` | The per-slide spec (Message / Visual / On-slide text / Say) the deck is built from |
| `session-2.html` | Session 2 deck — *Trunk-Based Development & CI* (15 slides + 3 section dividers) |
| `session-2-outline.md` | The per-slide spec for Session 2 |
| `theme/cd101.css` | Deck theme — the course's indigo palette + diagram styling |
| `reveal/` | Vendored reveal.js 5.2.1 (core + notes plugin). Do not edit. |

## Present

Open `session-1.html` in any browser (double-click, or `file://…`). Then:

| Key | Action |
| - | - |
| `→` / `Space` | Next slide |
| `←` | Previous |
| `F` | Fullscreen |
| **`S`** | **Speaker view** — opens a second window with your *Say* notes, a timer, and the next slide. Put it on your laptop screen; put fullscreen on the projector. |
| `Esc` / `O` | Slide overview |
| `B` | Black the screen (pause) |

Speaker notes (the *Say* track) live on every slide and only show in speaker view — never on the projector.

## PDF backup

Append `?print-pdf` to the URL (`session-1.html?print-pdf`), then Print → Save as PDF,
**Background graphics ON**, margins None. Keeps the dark theme.

## Diagrams

The conceptual visuals (roots timeline, vicious-cycle ring, two-rail pipeline, the floor,
value-stream flow, stopwatch) are inline SVG in the HTML — edit them there. Slides 1 and 7
use the course hero gradients; drop a real photo into slides 1–2 later via reveal's
`data-background-image` if you want.

## Future sessions

Copy an existing deck to `session-3.html`, reuse `reveal/` and `theme/cd101.css`, and
rebuild the slide list from that session's outline. Session 2 added a few layout
primitives to `theme/cd101.css` (section dividers, the without/with contrast, the
editor hand-off pill) under a labeled "Session 2" block — reuse them as needed.
