# Troubleshooting with Datadog 101 — Slides

The presenter deck, built on [reveal.js](https://revealjs.com). reveal.js and the fonts are copied into this folder, so the deck runs fully offline. The look follows the RealManage brand (Navy and Tile, Optima-style headlines with the bundled Marcellus fallback, Work Sans body).

## Files

| File | What it is |
| - | - |
| `deck.html` | The deck: 24 slides plus 1 backup slide for D6, 4 part dividers, 7 demo markers (D1–D7) |
| `deck-outline.md` | The per-slide source (Message / Visual / On-slide text / Say). **Edit this first**; the deck's speaker notes are its Say track, word for word |
| `theme/dd101.css` | Deck theme: brand palette, type, layout pieces |
| `fonts/` | Work Sans and Marcellus (SIL Open Font License, license texts included) |
| `img/` | RealManage logo files |
| `reveal/` | Copied reveal.js 5.2.1 (core plus the notes plugin). Do not edit. |

Screenshots come from `../assets/screenshots/` (sanitized; see `../scripts/`).

## Present

1. Open `deck.html` in a browser: double-click it, or open it from the course site.
2. Press **S**. A second window opens with **your speaker notes, a timer, and the next slide**. Keep it on your own screen.
3. In the meeting, **share only the main deck window** (or the screen it's on), never the speaker window. The audience never sees the notes.
4. Press **F** in the main window for fullscreen.

| Key | Action |
| - | - |
| `→` / `Space` | Next slide |
| `←` | Previous |
| **`S`** | **Speaker view** (notes, timer, next slide) |
| `F` | Fullscreen |
| `Esc` / `O` | Slide overview (jump to any slide) |
| `B` | Black the screen |

## Switching to the browser and terminal

Demo slides (D1–D7) say which window to switch to, and the [run sheet](../sessions/session-1/run-sheet.md) has the steps. Keep the deck, Datadog and Claude Code as three windows on the shared screen and **Alt+Tab** between them. Coming back, the deck is where you left it. If you share a single window instead of the screen, you'll have to re-share on each switch, so sharing the screen is smoother.

## PDF backup

Open `deck.html?print-pdf`, then Print → Save as PDF with **Background graphics** on and margins **None**. Keep it on your machine in case a browser misbehaves.

## Changing the deck

1. Edit `deck-outline.md` (message, on-slide text, Say).
2. Make the same change in `deck.html`. Speaker notes live in each slide's `<aside class="notes">` and must match the outline's Say text.
3. Open the deck and press **S** to check the slide and its notes.

Datadog changes its UI, so re-shoot screenshots with `../scripts/sanitize_screenshots.py` when a slide no longer matches the product.
