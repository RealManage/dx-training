# Troubleshooting with Datadog 101: Course Context

## Course overview

A single presenter-led session of about 85 minutes, plus questions, in a two-hour slot. It teaches developers to troubleshoot slow requests and errors in Datadog: read APM traces, read continuous-profiler data (why is it slow?), see what Exception Replay and Live Debugger capture (why did it fail?), and at the end of each part watch Claude Code with the Datadog MCP server do the same work. The last part is checking Claude's work. It is built around one real example: the slow `GET /api/user/dashboard` on `resident-api`. It follows the DX-1605 profiler rollout; the course ticket is DX-1673.

## Target audience

RealManage developers. Most know Datadog's everyday features; some have never used Datadog. Assume no profiling knowledge. Define terms on first use.

## Course structure

- **Duration:** about 85 minutes plus questions, one session. Parts: APM tour, why is it slow (profiling), finding the cause of an error (Exception Replay, Live Debugger), checking Claude's work. Parts 1 to 3 each end with a Claude demo of the same task, then a short question break
- **Format:** a reveal.js deck (`slides/deck.html`, offline, S = speaker view) plus live demos in the browser (Datadog, US5 site) and the terminal (Claude Code). Why reveal.js: `docs/format-decision.md`
- **Level:** Beginner
- **Prerequisite:** attendees complete `SETUP-GUIDE.md` beforehand

## Where each thing lives (edit the owner first)

| Content | Owner file | Referenced from |
| - | - | - |
| Per-slide message and speaker script | `slides/deck-outline.md` | `slides/deck.html` (its `<aside class="notes">` = the Say track, word for word) |
| Timing, demo steps, recording | `sessions/session-1/run-sheet.md` | outline, README |
| Prompts (P1–P10b) and good-answer criteria | `resources/demo-prompts.md` | run sheet, cheat sheet |
| UI paths and deep links (K1–K7) | `resources/click-paths.md` | lesson, cheat sheet |
| Gotchas (G1–G20) | `resources/gotchas.md` | lesson, outline, cheat sheet |
| Checking habits (C1–C3) | `resources/checking-claudes-work.md` | lesson, cheat sheet |
| Worked-example facts | `sessions/session-1/README.md` §4 | outline |
| Exception Replay and Live Debugger facts (Part 3) | `sessions/session-1/README.md` §5, `resources/gotchas.md` G13–G20 | outline, run sheet D5, D6, D7 |

`resources/cheat-sheet.md` is the one deliberate copy (a printed page can't follow links). Its items carry IDs; update it whenever an owner changes. `resources/cheat-sheet.tool.html` is its one-page printable rendering: change both together, and after any edit print the `.tool.html` to PDF (Letter) and confirm it is still one page.

## Important notes

- **This repo is mirrored publicly to GitHub and published on GitLab Pages.** Nothing committed may contain customer data, customer or community IDs, request URLs carrying IDs, client IPs or locations, host names (web servers, SQL servers), trace or span IDs, org or user IDs, Watchdog story or monitor IDs, secrets or keys. Service names, endpoint paths, stored-procedure names and class/method names are acceptable.
- **Never paste a Claude or Datadog answer verbatim.** Answers routinely include host names, client locations, trace IDs and org-switch links. Summarize the finding in your own words.
- **Deep links must carry no IDs.** Use query, profile type, endpoint and filter parameters only, and no time range, so links stay valid. Claude's profiler links embed an org-switch ID: never commit them.
- **Screenshots:** capture raw images outside the repo, then run `scripts/sanitize_screenshots.py --raw-dir <outside-folder>`. It crops and fills per `scripts/screenshots.json` and strips metadata. Crop out the address bar, the left nav (it shows the signed-in user) and trace headers (trace ID, client location). Look at every output image before committing it.
- **Leak check before every commit** (run from the course folder; any hit needs a look):

  ```bash
  grep -rnE '([0-9]{1,3}\.){3}[0-9]{1,3}|[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}|[0-9a-f]{24,}|[0-9]{16,}|switch_to_user|trace_id=|@[a-z0-9.-]+\.(com|net)' --include='*.md' --include='*.json' --include='*.html' .
  ```

- **Presenter script voice:** the Say track and any quoted presenter lines are plain, conversational English for teammates, many of whom speak English as a second language: full sentences, common words, no dramatic fragments, no metaphors or human traits for software, no ALL CAPS emphasis. It must sound like something the presenter would actually say. Avoid phrasing that reads as machine-written: "it's not X, it's Y" contrasts, "here's the thing", "the key is", "that's where X comes in", neat groups of three, questions answered straight away, and colons or dashes used for effect.
- **Datadog is read-only for course work.** Verify UI steps in the browser but never change settings, monitors, retention filters or dashboards. Demo prompts must only read. The exceptions are D6 and P10 in D7: a temporary Live Debugger session and conditional logpoint on `management-api` in dev, disabled afterwards. Never change redaction or Live Debugger settings.
- **Exception Replay snapshots from prod carry production values.** Show them live in Datadog. Commit a screenshot of one only after choosing a snapshot with no customer data (type names and schema messages, not names, emails, addresses or record IDs), keeping every object collapsed, cropping out the sample header, and reviewing the output by eye. `exception-replay-snapshot.png` is the one that passed.
- **Re-verify before each delivery.** Datadog's UI changes. Re-walk `click-paths.md`, re-run the prompts, and update the "verified" dates. Record results in `docs/verification-log.md`.
- **Recordings stay internal** (DX team's Teams channel). Never commit or link a recording or transcript.
