# Verification Log

Evidence that the course content was checked against the live systems, and what is still pending. Not published on the course site. Summaries only: no raw outputs, IDs or host names.

## Live Datadog UI walk — 2026-09-25

Walked read-only in the Claude desktop app's built-in browser, signed in to the US5 org. Nothing was changed in Datadog.

| Path | Result |
| - | - |
| K1 APM → Traces, search by service / resource / env / `@duration`, Spans \| Traces toggle | ✅ Matches. Live window reads "All Ingested Spans (Live Search)"; older windows read "Indexed Spans" with a "retention mechanisms" link. Deep link verified. |
| K2 Trace views | ✅ Flame Graph, Waterfall, Span List **and Map** (the ticket lists three). Waterfall starts collapsed; "expand all" needed. |
| K3 Span tabs | ✅ Overview, Infrastructure, Metrics, Logs, Network, Processes, SQL Queries, Profiles. ⚠️ **Metrics** shows .NET runtime metrics for the process (CPU, memory), not the service's request/error/latency metrics as the ticket describes. **Infrastructure** shows host, system info and tags. |
| K3 Profiles tab | ✅ Process \| Span & Children, thread timeline, Summary \| Call Tree \| Flame Graph, Open in Profiling. Confirms "Code Hotspots" is now "Profiles". |
| K4 Service page | ✅ Left nav exactly as the ticket lists (Frontend Usage marked Preview), plus Investigations and Dashboards. Deep link verified. ⚠️ Operation selector defaulted to **All server operations** with env **prod** for both resident-api and management-api, not `aspnet_webapi.request` / env `*` as the ticket says (may be a per-user remembered choice). The latency chart has its own operation picker. |
| K5 Profiles explorer | ✅ Opens on CPU Time; Wall Time card; Allocated Memory "Not enabled"; Method list; Filter flame graph with highlight / focus_on / show_from / ignore_from / hide. ⚠️ `/apm/profiles` is a **404**; the page is `/profiling/explorer`. ⚠️ A filter link with a bare `GetUserDashboardItems` silently matches nothing; the value must be `method:"UserBP.GetUserDashboardItems()"`. Final deep link verified with the default 1-hour window (data present). |
| Worked example: dashboard | ✅ Live traces 11–15 s: SP 5–10 s then a 4–5 s gap. Profile shows GetUserDashboardItems → GetActivities → GetEnumFromDescription → GetCustomAttribute. |
| Worked example: /api/wf/search | ✅ DB-bound (api_mp_SearchWorkflows; EF / ReadAsync). ⚠️ p95 over 24 h was ~4–9 s (metric vs spans), not ~62 s; p99 is 76–85 s. The course states "long tail", not a p95 figure. |
| Worked example: Watchdog 2026-09-22 | ✅ `trace.aspnet.request` p95 0.78 / 0.73 / 0.71 / 0.74 / **2.27** / 0.68 s in 10-min buckets from 18:00 UTC; day before 0.68 s at 18:40; p50 ~8 ms throughout. Metrics are retained, so this demo won't age out. |

Screenshots were captured from this walk (full-resolution captures of the page area, kept outside the repo), sanitized with `scripts/sanitize_screenshots.py`, and each output was reviewed by eye.

## Demo prompts — 2026-09-25

Each prompt run twice with `claude -p` (Claude Code 2.1.282), allowed only read-only Datadog MCP tools, from an empty folder. P7/P7b run as follow-ups in both P6 sessions.

| Prompt | Run 1 | Run 2 | Notes |
| - | - | - | - |
| P1 connected? | ✅ 13 services | ✅ 13 services | Identical. |
| P2 slowest endpoints | ✅ | ✅ | Both used `trace.aspnet.request`; different aggregation windows → different numbers, similar ranking. Used as a C1 example. |
| P3 one slow request | ✅ used profiler | ✅ no profiler, suggested it | Both found SP ~10 s + ~4.7 s gap. Span-based "30 requests/hour" used as a C3 example. |
| P4 wall-time profile | ✅ | ✅ | Same ranking: GetEnumFromDescription ~34% of endpoint, 88–94% reflection. |
| P5 C# or DB? | ✅ database | ✅ database | Both named api_mp_SearchWorkflows and ReadAsync waiting. |
| P6 is it real? | ✅ not a regression | ✅ "noise" | Both guessed a cause; neither could see the recycle. |
| P7 which queries? | ✅ self-corrected | ✅ self-corrected | Both admitted using sampled spans, not metrics. |
| P7b recycle context | ✅ cold start | ✅ cold start | Both revised. |
| P8 draft ticket | ⚠️ used CPU Time, said "not our code" | ✅ wall time, found reflection | Kept as a checking example (G1). |

## Setup guide — 2026-09-25

Run literally with `CLAUDE_CONFIG_DIR` pointed at a throwaway folder, from an empty working directory, in **Git Bash** and **PowerShell**:

- `claude mcp add --transport http --scope user datadog-mcp "…?toolsets=all"` ✅ (confirmation line omits the query string; `get` shows it saved)
- `claude mcp list` / `get` ✅ "! Needs authentication", Scope "User config"
- URL without `?toolsets=all` ✅ accepted silently, same status
- Wrong site (`mcp.datadoghq.com`) ✅ accepted silently, same status
- Duplicate add ✅ "already exists"
- No `--scope` ✅ lands in "Local config (private to you in this project)"
- Isolation proof: a hash of the real config's MCP server entries was identical before and after.
- Post-OAuth state and the verification prompt checked on an already-authenticated setup: `✔ Connected`, P1 returns 13 services.

## Deck and final sweep — 2026-09-25

- Format chosen after comparing samples: reveal.js (`docs/format-decision.md`).
- All 27 slides rendered at 1280×720 with headless Edge and reviewed; the published copy renders the same.
- Speaker notes match `deck-outline.md`'s Say track for all 27 slides (checked programmatically).
- Chart colors on slide 24 pass the palette validator (lightness, chroma, CVD separation, contrast).
- Site build: no new link warnings; every published image, font, stylesheet and script resolves; all anchors valid on both the site and GitHub/GitLab.
- Leak check clean over the course folder and the root README change.
- The three published deep links (slow dashboard traces, resident-api Service page, filtered wall-time profile) re-opened live and showed data.
- Cheat sheet printable version prints on one Letter page.
- Not repeated at the end: the demo prompts (run twice each earlier the same day).

## Part 5: Exception Replay and Live Debugger — 2026-09-28

Scope added to DX-1673 on 2026-09-28. Trials ran against management-api in dev, where the Live Debugger and Exception Replay flags were set as app-pool environment variables and the tracer was upgraded to 3.54.0 (DX-1570). Everything else was read-only.

| Check | Result |
| - | - |
| Datadog docs (Exception Replay, Live Debugger) | ✅ Exception Replay is GA and on through remote settings. Live Debugger is GA but needs the local `DD_DYNAMIC_INSTRUMENTATION_ENABLED` flag. |
| Remote Live Debugger enablement | ⚠️ Didn't take effect on resident-api dev, even on tracer 3.54: no probe capabilities. Only the local flag works (G15). |
| Exception Replay, dev trigger | ❌ The file-link request throws a `FormatException` inside a NuGet decryption library on an async path. First occurrence: "captured only from the second occurrence". Second: "could not be captured due to an internal error". The two occurrences were also grouped into two separate issues. No dev snapshot at all in the 24 hours after the flags went on (checked 22:00 UTC). So the planned dev demo can't run; D7 shows a prod snapshot live in Datadog (G13, G14). AC 18 was reworded to match on 2026-09-28: a prod snapshot, and a dev example once dev snapshots are available. |
| Exception Replay, prod | ✅ 53 snapshots in 24 hours (identity-api, budget-api) via Logs, Flex storage, `source:dd_debugger @debugger.product:er`. The issue page shows **Exception Replay snapshot (on exit)** under a frame with argument and local values and a **Redaction** badge. ⚠️ The default sample isn't always the captured one: one issue's first two samples said "not captured" while its sibling issue showed a snapshot. |
| Redaction | ✅ Snapshots seen showed sensitive names as `[REDACTED]`. One prod snapshot was captured for slide 28 (see Screenshot); the rest stayed on screen. **APM → Debugger → Configuration** lists redaction per service and environment: most prod services are **Strict**, which blocks logpoints (G16). ⚠️ The TripleDES `key` parameter concern from DX-1568 is still untested: no snapshot was ever taken on that path, and the Live Debugger demo line runs before it. |
| Live Debugger, dev trial | ✅ Session with a logpoint just before `FilesController.cs` line 53 (a plain `if`, not an `await` line), condition `IDENT != null && contains(IDENT, "dx-training")`, all variables at depth 3. Active about 30 s after creation. One `curl` (HTTP 500, expected) gave a capture on the session page immediately, and in Logs search about 3 minutes later (G18). The captured values were the demo inputs, nulls and the anonymous controller's empty user fields. The logpoint was disabled afterwards. |
| K6 and K7 UI walk | ✅ Labels as written: **Errors → Error Tracking → Issues**, **Error sample**, **Stack Trace**, **Previous / Next**; **APM → Debugger**, **New Session** (Bits Live Debugger preview, free until 2026-09-30), **Start debugging manually**, **+ New Session**, **Add a logpoint to start debugging**, **Choose service to instrument**, **Environment**, **Target specific instances**, **Search for a symbol or file**, the gutter tip, **Start Debug Session**, and on the session page **Emit Log / with template / when / capture variables / All variables / depth**, **Add Another Logpoint**, **Debug With Bits**, **Open in Logs**. ⚠️ Not walked: the logpoint form inside the create dialog (the trial logpoint was created through the Datadog MCP; the same form was verified on the session page) and the stop control in an active session's header. Confirm both in the dry run. |
| Screenshot | ✅ `live-debugger-session.png` from the dev trial session page, sanitized: the owner's name filled in two places. No host, IDs or IPs visible. It shows seven lines of dev source around the logpoint. ✅ `exception-replay-snapshot.png` from a prod identity-api `DbEntityValidationException`: chosen because its values are type names and a schema validation message, every object left collapsed, cropped to the stack trace (no sample header, time, version, host or nav). Reviewed: nothing needed filling. |
| Deck | ✅ 34 slides; the new and changed slides (1, 2, 4, 26–34) rendered at 1280×720 and reviewed. Speaker notes match the outline's Say track for all 34 slides (checked programmatically). |
| Cheat sheet | ✅ Printable version still one Letter page (content about 908 of 979 px). |
| Site build and anchors | ✅ No new link warnings; all 43 cross-page anchors resolve on both the site and GitHub. Leak check clean. |

## Restructure and Claude debugging prompts — 2026-09-30

Edgar asked to give Exception Replay and Live Debugger the same weight as profiling and to show Claude doing both kinds of investigation. The course is now **Troubleshooting with Datadog 101** (folder `datadog-troubleshooting-101`), in five parts: APM tour, why is it slow (profiling), why did it fail (Exception Replay, Live Debugger), investigating with Claude, checking Claude's work. Demo IDs follow the new running order (D1–D9); prompt IDs are unchanged, with P9–P10b added. DX-1673 was updated to match the same day: the new title, the part order in section 7, and AC 22 for the P9 and P10 prompts.

| Check | Result |
| - | - |
| P9 explain an error (read-only tools) | ✅ Run twice. Both found the identity-api `DbEntityValidationException` issue and its snapshot, explained the failure from the captured values (`PORTAL_TYPE` over its 1-character limit, rejected by Entity Framework before SQL), said what the snapshot couldn't show, spotted the bug split across two issues, and suggested a logpoint. About 2 minutes each. |
| P10 / P10b logpoint in dev (`enable_live_debugger` not allowed) | ✅ Run twice. Each created one session and one conditional logpoint on `FilesController.cs` line 53, unpinned, checked the line against the dev build, and said when to test. After one `curl` (HTTP 500, expected), P10b showed the captured values and disabled the logpoint. P10 about 3.5 minutes, P10b about 30 seconds. Both logpoints confirmed disabled afterwards, one per session. ⚠️ Both runs got an error from Datadog while creating the logpoint (a 503, then a timeout) although it was created; Claude checked instead of retrying (G20). |
| Deck | ✅ 35 slides. Renumbered and re-labelled; the new and changed slides (1, 2, 24, 26, 29) rendered and reviewed. Speaker notes match the outline for all 35 slides, including multi-paragraph Say text. |
| Cheat sheet | ✅ P9, P10 and G20 added; the printable version still prints on one Letter page (content about 926 of 979 px). |
| Site build, anchors, leak check | ✅ See the commit for this date. |

## Claude demos after each part — 2026-10-03

Edgar asked to spread the Claude demos through the session instead of saving them for the end, to add a short question break after each part, and to keep the Say track sounding like something he would say. Each part now shows the task by hand, then Claude doing the same work: P2 and P3 close Part 1, P4 and P5 close Part 2, and P9, P10 and P10b close Part 3. Part 4 is checking Claude's work, and "Which tool for which question" moved to the wrap-up as a recap. The separate Claude part and its divider are gone, so there are four parts and eight demos, renumbered in running order (D1–D8). Prompt IDs are unchanged. The slot is two hours (9:30–11:30); the plan is about 90 minutes plus about 10 minutes of questions. Every Say paragraph was re-read for flow and voice, and clock marks were recalculated from the Say word counts.

| Check | Result |
| - | - |
| Prompts used across parts | ✅ P2–P5 each name the service and endpoint, so none depends on an earlier prompt's context. Splitting them across parts needs no shared Claude session. |
| Deck | ✅ 35 slides, 4 dividers, 8 demos. Changed slides rendered and reviewed. Speaker notes match the outline for all 35 slides. |
| Independent review | ✅ A separate reviewer read the outline, run sheet and prompts cold, as a first-time attendee, for flow, voice and consistency. It confirmed the IDs, links, clock marks and speaker notes, and found terms used before they were defined (trace metrics, p95, Exec Time, Error Tracking, Watchdog, Targeted and Strict redaction), two pairs of slides that seemed to contradict each other (left-to-right in trace vs profile flame graphs, CPU time on slides 16 and 17), the unexplained switch from identity-api to management-api in D7, the missing `@` in the D1 search, and the recording never being announced. All fixed. Teaching-strategy questions went to Edgar. |
| Site build, anchors, leak check | ✅ See the commit for this date. |

## Part 4 and the wrap-up trimmed — 2026-10-09

Edgar's review: five checking habits became three (C1 now covers asking for the query and a link and checking the results; the baseline habit was dropped), the "seen while building this course" examples left the speaker notes, D8 shows its three prompts on the slide and as copy boxes, the gotchas slide and the feedback form were removed (no form exists), and the homework points to the sdlc plugin for tickets. The deck is 25 slides plus 1 backup.

| Check | Result |
| - | - |
| D8 prompts in one conversation (P6 → P7 → P7b) | ✅ The 2026-09-22 traces are still searchable (17 days later). P6 (80 s): a real but short slowdown, not a code regression, guessed cause the shared database. P7 (49 s): every query with links, then an unprompted correction: it had used sampled spans and mixed database and HTTP spans into request latency; it suggested the `trace.*` metric instead. P7b (42 s): dropped the database theory; slow bursts moved server to server between 18:39 and 18:42; a cold start after each recycle; suggested IIS Application Initialization and one-at-a-time recycles. Answers name hosts and a SQL server: fine on screen, never in a commit. |
| D8 without P7 | ✅ Edgar dropped P7 from the demo (slide 21 covers the habit; P7 returned 7 to 11 links, too many to check live). P6 then P7b in one conversation: 47 s and 20 s; P7b still dropped the database guess for a cold start after the recycle. D8 is retitled "Is It Real? Then Add Context". |
| D8 dropped | Edgar dropped D8 and its slide; slide 22's notes now tell the story (right that it wasn't a regression, wrong guess at the database, revised to an app pool warm-up once told) and say the IIS warm-up (Application Initialization) was fixed this week. The session is now about 85 minutes plus questions; 24 slides plus 1 backup; the D6 Backup link points at slide 25. |
| Deck | ✅ 25 slides plus 1 backup; slides 21, 23, 24 and 25 rendered and reviewed; speaker notes match the outline for all 26; the D6 card's Backup link points at the new slide 26. |

## Part 3 as a show and tell on one email-link error — 2026-10-08

Part 3 is now titled "Finding the Cause of an Error" and follows one production error from our email images: management-api's `GET /api/files/filelink` fails with a `FormatException` ("not a valid Base-64 string") in `TripleDesEncryption.Decrypt`. Edgar framed the part as a show and tell: most of the room won't use these tools, so the goal is awareness. The real-snapshot screenshot slide and the recap were dropped; D7 is one read-only prompt; P10 and P10b are kept but not used live. One backup slide (the Live Debugger screenshot) follows the wrap-up. The deck is 26 slides plus 1 backup.

| Check | Result |
| - | - |
| Snapshots for the example | ✅ Exception Replay snapshot logs for this error: requests on Oct 5 (two) and Oct 6 (one), each with `Decrypt` and `GetFileByEmailResource` frames. The oldest Exception Replay snapshot stored for any service is from Sept 24, so retention is at least 14 days. |
| D5 path | ✅ The Error Tracking link filtered by `error.type:System.FormatException` (free text `FormatException` matches nothing). Two issues share the Base-64 message: the one at `TripleDesEncryption.Decrypt` (19 in the week, newest sample says "Exception Replay data was not captured") and the one at `System.Convert.FromBase64_Decode` (468 in the week). With **Past 1 Week**, the second one's default sample is the Oct 6 request with **Exception Replay snapshot (on exit)** under `Decrypt(cipherString, key, useHashing)`, **Redaction: Targeted**; `cipherString` expands to "The value was redacted based on its name". Previous/Next step through samples (Next is newest). The panel's own time picker didn't change the samples; the page's did. |
| D6 path | ✅ APM → Debugger (`/debugging`): New Session, Sessions List, Configuration; the Bits preview now says free until November 30, 2026. Dev: management-api, identity-api, legacy-api and mobile-api show **READY** (Targeted); prod is Strict. The code view needs a GitLab sign-in through Datadog (new G22); without it the logpoint form doesn't appear. Signed in: search `FilesController` → CLASS → a method list opens (Escape closes the whole dialog; click the code instead) → line 53 is still the `if`. The form: **Emit Log with template** (prefilled), **+ Condition** opens **when**, capture variables **All variables, depth 3** by default. The condition typed cleanly. **Start Debug Session** → ACTIVE (58 min); the logpoint went from "Instrumenting your code" to "Waiting for events" in about 30 s. The test URL opened in a browser tab ("Error retrieving file by link.") produced exactly one capture within about 15 s; **Values for line 53** shows `ERES` and `IDENT`. The pause icon next to Active stops the session (INACTIVE, "Session updated successfully!"). An earlier conditional logpoint created through the API behaved the same and was disabled. |
| New P9 (D7) | ✅ Three fresh Claude Code runs (empty folder, `--add-dir` for management-api and api-common). The final prompt (four bullets, "Don't quote any values") answered in 76 s: invalid Base-64 link value, values redacted, a 500 with "Error retrieving file by link.", and a safe decrypt so the existing "invalid parameters" check returns 404. Two earlier, looser prompts (102 and 123 s) also found a stray character after the Base-64 padding in the request URL on the trace (not redacted); one printed a decoded value, which is why the prompt now forbids quoting values. |
| Deck | ✅ 26 slides plus 1 backup; new and changed slides rendered (with Chrome; headless Edge stopped writing screenshots); speaker notes match the outline for all 27. Slides 1–13 unchanged except slide 2's card 3 ("Find the cause of an error"), approved. |

## Part 2 taught live on the D1 trace — 2026-10-08

Part 1 was locked after a practice run. Part 2 now follows the same pattern: tracing vs profiling on a slide, a live D3 on the same trace as D1, a one-slide recap, then D4 with the same trace link. The flame-graph, CPU vs Wall, trace-to-profile, filtered-profile and database-contrast slides became steps or lines in D3, and the D1 backup slides were dropped. The deck is 27 slides.

| Check | Result |
| - | - |
| D3 on the D1 trace (an 8.7 s request, 6 spans) | ✅ Click the resident-api span → **Profiles** (last span tab): **Process / Span & Children**, a thread timeline (CPU Time, Thread Sample, Off-Span Activity), and **Summary / Call Tree / Flame Graph** tabs. The timeline is idle until the SQL finishes (about 4.5 s) and busy until the end. **Flame Graph** shows **Thread Activity: CPU Time 3.81 s**, gray **Standard Library** rows, and our code in blue: `CCCaching.AddOrGetCacheAsync` → `UserRepository.GetUserActivityFeed` → `UserBP.GetUserDashboardItems` → `DashboardHelper.GetActivities` and `EnumExtensions.GetEnumFromDescription` in several places. **Open in Profiling** now opens this process's single profile, not the explorer, so D3 no longer uses it. |
| All dashboard requests (APM → Profiles → Explorer) | ✅ The slide's **All requests** link (service, env, endpoint; no time range) opens on **CPU Time** with the dashboard endpoint filtered. Clicking the **Wall Time** card works; typing `GetUserDashboardItems` in **Filter flame graph** suggests `method:"UserBP.GetUserDashboardItems()"`, then offers highlight / **focus_on** / show_from / ignore_from / hide. `focus_on` shows the same stack as the one trace. |
| All requests: CPU Time, not Wall Time (dry run, same day) | ❌→✅ In Edgar's dry run (an evening hour, 5 ms per minute for the endpoint), **Wall Time** with `focus_on(GetUserDashboardItems)` was dominated by waiting branches (`UserRepository.GetUserDashboard`, `GetUserPropertyList`, `GetCollectionMessage` → `PaymentRepository`), with one small `GetEnumFromDescription` box, so it didn't look like the trace. **CPU Time** in the same kind of window shows `GetUserDashboardItems_Lambda29` → `GetEnumFromDescription` and `GetActivities` → `GetActivityActionCode` → `GetEnumFromDescription`, matching the span's profile (which is CPU Time). D3 now stays on CPU Time and says why. The filter box offers the operations first: choose **focus_on**, then type the method inside its brackets. A stage direction containing `()` wasn't styled as an action; the method name is now written without brackets. |
| New P4 (D4): the trace link, then the code | ✅ Two fresh Claude Code runs on the same 8.7 s trace: 112 s and 74 s, same findings (`GetEnumFromDescription` reflection, nothing cached; the every-activity × every-property pairing in `UserBP`; cache the lookup and work out each category once; the 4.1 s stored procedure named as the database half). The first run started inside a folder whose CLAUDE.md told Claude to write a work-log line, and it did; the second started in an empty folder with `--add-dir` for `ciranet-resident-api` and `ciranet-api-common` and did not. The run sheet now says to start Claude Code that way. Answers include an org-switch link: fine on screen, never committed. |
| Deck | ✅ 27 slides; new and changed slides rendered and reviewed; speaker notes match the outline for all 27. Slides 1–8 unchanged. |

## Part 1 taught live, and a simpler D2 prompt — 2026-10-08

Edgar ran Part 1 for a teammate. The feedback: "resource" still read as the same thing as a span; "APM" was never spelled out; describing Live Search from a screenshot was hard to follow; slides 5–9 explained screenshots and D1 then repeated them almost word for word; the span-list caption pointed at a number only our own card showed; the D2 prompt (worst p95 endpoints) was too complex, its answer too long, and the dashboard wasn't in it; and the presenter had to jump between the notes, the run sheet and the prompts page during demos.

Part 1 is now: three words (service, span, trace) → divider with APM spelled out → a "path we'll take" slide → a 12-minute live D1 that starts on the resident-api Service page, introduces "resource" on its Endpoints list, shows Live Search vs Indexed Spans by changing the time range, opens a slow trace and clicks through the span tabs → a one-slide recap of the gap → what Claude can do → D2 with one short prompt shown on the slide and in the notes. The old screenshot slides are backups after the wrap-up (33–36). Slides 13–35 are now 10–32.

| Check | Result |
| - | - |
| D1 path, live in the browser pane (read-only) | ✅ Service page → **Endpoints** → **Search Endpoints** `/api/user/dashboard` lists two GET rows (one per operation) and an OPTIONS row → clicking a GET row opens the endpoint panel → its **Traces** section, near the bottom after **Span Summary**, has a **Trace Explorer** button that opens the Traces explorer filtered to the endpoint with `operation_name:aspnet.request`. (A **View Traces** button also exists, but only inside the panel's **Frontend Impact** section, grouped by browser view; Edgar couldn't find it in a dry run, so the script doesn't use it.) The Service page's own **Traces** section also has a **Trace Explorer** button. In the Traces explorer the label next to **Spans / Traces** reads **Live Search** for the past 15 minutes. |
| How often a slow dashboard request shows the gap | ✅ Last 24 hours, prod, one span per request: 29 requests over 5 s and 13 over 8 s. All 13 over 8 s had 3 s or more of resident-api time after the SQL (most 5–11 s); 5 of the 11 between 5 and 7.5 s were mostly SQL. 7 to 22 requests over 8 s on every day of the last week. So D1 and P3 use `@duration:>8s`. |
| New P3 (D2), run as a fresh Claude Code would | ✅ Six runs while tuning. With "last hour, over 5 s" two of four landed on a 7.3 s mostly-SQL request. With "last 24 hours, over 8 s" both runs found a 20.8 s request: about 13 s of SQL, mostly the dashboard procedure, and about 7.5 s of our own code after it, in three plain bullets, no profiler names, 1–3 minutes. Raw tool output carries client IPs and locations, so only Claude's answer belongs on screen. |
| D1 from any Datadog page (added later the same day) | ✅ Dashboards list → hover **APM** shows a menu (Services, Endpoints, Traces, …); clicking **APM** opens **APM Home**. Its **Service List** has **Search services**; `resident-api` opens the Service page with env `*` (all environments) and operation `aspnet_webapi.request`. The env list shows requests per environment; choose prod. **Search Endpoints** `/api/user/dashboard` then shows one GET row and no OPTIONS row; the endpoint panel's **Trace Explorer** opens with `env:prod operation_name:aspnet_webapi.request resource_name:"GET /api/user/dashboard"`. Slide 5 (the path) was dropped; D1 starts from an **Open Datadog** link and has seven steps. |
| Live vs Historical Search, re-checked in a dry run (same day) | ❌→✅ The D1 step said a label next to **Spans / Traces** reads Live Search at 15 minutes and Indexed Spans for older ranges. Edgar found nothing there. Now: the label sits left of the time picker and reads **Historical Search** or **Live Search**; the endpoint panel's Trace Explorer link opens with `historicalData=true`, so even Past 15 Minutes is historical (4 indexed spans). Live is its own time-picker entry, **LIVE · 15 Minutes** (green LIVE badge); the same 15 minutes then showed 125 OK spans. Past 1 Day is historical (771 indexed spans). Clicking the label shows Datadog's explanation of Live Search, Indexed Spans and Indexed Traces. The **in** selector next to the search bar now reads **All Spans**. D1 script, slide step 4, run sheet, K1 and the session README updated; backup slide 32's screenshot still shows the old layout. |
| P3 (D2) changed to read the D1 trace from its link | ✅ In a dry run Edgar found that slow dashboard requests are slow for different reasons (mostly SQL, mostly the browser, or only 3–4 spans), so "find one over 8 s" could land on a request that doesn't look like D1's. P3 now takes the trace link from the D1 tab. Two fresh Claude Code runs on an 8.7 s trace with 6 spans, link below the prompt and on the same line: both under 30 seconds, 8.7 s total, about 4.4 s SQL (4.1 s in the dashboard procedure), about 4.3 s of our own code. The trace URL from the explorer is about 800 characters and carries `traceID=`; Claude read it either way. D1's stage direction now says how to pick a trace like the example. |
| Deck | ✅ 32 slides plus 4 backups. New and changed slides rendered and reviewed. Speaker notes match the outline for all 36. |
| Cheat sheet | ✅ P3 and K1 updated; the printable version still prints on one Letter page. |
| Site build, anchors, leak check | ✅ See the commit for this date. |

## Pending human verification

- [ ] **OAuth on a clean machine (AC 6).** On a Windows profile that has never connected Datadog: follow SETUP-GUIDE Steps 1–4 exactly, including `/mcp` → authenticate → browser consent → restart → P1. Note any wording in the `/mcp` menu that differs from Step 3. Tester / date / shell / result: ______
- [ ] **Presenter dry run (AC 16).** Full run with the final deck (about 90 minutes plus question breaks); fill in the run sheet's dry-run table and fold fixes back into the outline, deck and run sheet. Date / notes: ______
- [ ] **Teams recording steps.** The menu path in the run sheet wasn't walked in this session; confirm it during the dry run.
- [ ] **Part 3 in the dry run.** Run D5 with a snapshot picked the day before, and D6 end to end from the create dialog, including stopping the session. Note any label that differs from K6/K7.
