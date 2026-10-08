# Run Sheet — Troubleshooting with Datadog 101

> The presenter's page. Keep it open on your second screen next to the speaker notes. Slide numbers match [`slides/deck-outline.md`](../../slides/deck-outline.md); prompt IDs (P1–P10b) match [demo prompts](../../resources/demo-prompts.md).

**Duration:** about 90 minutes, plus about 10 minutes of questions, in a two-hour slot · **Format:** screen share, switching between slides, browser (Datadog) and terminal (Claude Code)

Each part follows the same pattern: explain on slides, show it by hand in Datadog, then show Claude doing the same work. Parts 1 to 3 end with a two-minute question break.

---

## The day before

- [ ] Send attendees the [setup guide](../../SETUP-GUIDE.md) if you haven't. Ask them to post in the thread if verification fails.
- [ ] **Re-find the live example.** Traces age out, so pick fresh ones:
  - [ ] Open the [slow dashboard search](https://us5.datadoghq.com/apm/traces?query=service%3Aresident-api%20resource_name%3A%22GET%20%2Fapi%2Fuser%2Fdashboard%22%20env%3Aprod%20%40duration%3A%3E5s) over the past day. Pick a trace of about 10 s or more whose waterfall shows the stored procedure followed by a gap. Keep its tab.
  - [ ] Check its span's **Profiles** tab has data (traces from 2026-09-22 on only).
  - [ ] Open the [filtered wall-time profile](https://us5.datadoghq.com/profiling/explorer?query=service%3Aresident-api%20env%3Aprod&profile_type=wall-time&endpoint=%7B%22incl%22%3A%5B%22GET%20%2Fapi%2Fuser%2Fdashboard%22%5D%7D&profiling-flame-graph__filter=focus_on%28method%3A%22UserBP.GetUserDashboardItems%28%29%22%29&viz=flame_graph). If it's empty at 1 hour, set the time range to 1 day.
- [ ] Run **P1** and **P4** once in Claude Code to confirm the Datadog MCP answers. Run **P9** once with the exception you pick for D5 below, and check that Claude finds the same issue.
- [ ] **Pick the Exception Replay example (D5).** Snapshots age and some samples have none, so pick one fresh:
  - [ ] Open the [Exception Replay snapshots search](https://us5.datadoghq.com/logs?query=source%3Add_debugger%20%40debugger.product%3Aer%20env%3Aprod&storage=flex_tier) over the past day (storage **Flex**). Note a service and method that come up often.
  - [ ] Open that service's issues in **Errors → Error Tracking** and find the issue whose **Stack Trace** shows **Exception Replay snapshot (on exit)**. Choose a harmless one: a validation or configuration error, with no customer names, emails, addresses or account numbers in the values. Keep its tab.
  - [ ] Also note one issue whose sample says **Exception Replay data was not captured**, for step 4 of D5.
- [ ] **Check Live Debugger in dev (D6).**
  - [ ] **APM → Debugger → Configuration**, environment **dev**: `management-api` shows **READY**. If it doesn't, the setting was removed from the dev server ([G15](../../resources/gotchas.md#g15-live-debugger-needs-a-setting-on-the-service-itself)); ask the DX team, or plan to use the fallback.
  - [ ] Run D6 once end to end ([K7](../../resources/click-paths.md#k7-live-debugger-add-a-logpoint)). Check that line 53 of `FilesController.cs` is still the `if` after `emailResourceKey` is assigned; if the code moved, use the new line number. Stop the session afterwards.

## 15 minutes before

- [ ] Close email, chat and anything with customer data. Turn on Do Not Disturb.
- [ ] Browser: tabs in order: (1) Datadog Traces search, (2) the trace you picked, (3) the filtered profile, (4) the Exception Replay issue you picked, (5) the issue with no snapshot, (6) **APM → Debugger**. Zoom to 125% so text reads over screen share.
- [ ] Terminal: Claude Code started in an empty folder, font size bumped up, window cleared. `/mcp` shows datadog-mcp connected. A second terminal tab with the `curl` command for D6 and D7 typed but not run.
- [ ] Deck open: `slides/deck.html`, press **S** for the speaker window (notes, timer, next slide) and keep it on your own screen; press **F** in the main window. Share the screen showing the main deck window, never the speaker window.
- [ ] Feedback form link, course site link and course Slack thread link ready to paste.
- [ ] Start the recording (see [Recording](#recording-internal-only)).

---

## Timing guide

| Clock | Segment | Slides | Window | Demo |
| - | - | - | - | - |
| 0:00–0:04 | Welcome, what you'll leave with, four words | 1–3 | Slides | |
| 0:04–0:11 | Part 1: APM orientation | 4–9 | Slides | |
| 0:11–0:18 | **Demo D1** | 10 | Browser | Search → trace → three views → the gap → SQL Queries |
| 0:18–0:20 | What Claude can do with the Datadog MCP | 11 | Slides | |
| 0:20–0:26 | **Demo D2** | 12 | Terminal | P2 → P3 |
| 0:26–0:28 | Questions | 12 | Slides | |
| 0:28–0:38 | Part 2: Why is it slow? Profiling | 13–18 | Slides | |
| 0:38–0:44 | **Demo D3** | 19 | Browser | Span Profiles tab → Open in Profiling → Wall Time → filter |
| 0:44–0:50 | **Demo D4** | 20 | Terminal | P4 → P5 |
| 0:50–0:51 | The contrast | 21 | Slides | |
| 0:51–0:53 | Questions | 21 | Slides | |
| 0:53–0:57 | Part 3: Why did it fail? Exception Replay, a real snapshot | 22–24 | Slides | |
| 0:57–1:02 | **Demo D5** | 25 | Browser | Issue → Stack Trace → snapshot values → a "not captured" issue |
| 1:02–1:04 | Live Debugger | 26 | Slides | |
| 1:04–1:11 | **Demo D6** | 27 | Browser + terminal | New session → logpoint on line 53 → `curl` → snapshot |
| 1:11–1:17 | **Demo D7** | 28 | Terminal | P9 → P10 → `curl` → P10b |
| 1:17–1:19 | Questions | 28 | Slides | |
| 1:19–1:24 | Part 4: Checking Claude's work; five habits; is this real? | 29–31 | Slides | |
| 1:24–1:30 | **Demo D8** | 32 | Terminal + browser | P6 → P7 → open one link → P7b |
| 1:30–1:35 | Which tool, gotchas, your turn | 33–35 | Slides | |
| 1:35–1:40 | Questions, feedback link | 35 | Slides | |

The slot runs to two hours, so about 20 minutes are spare. D7 and D8 are the demos most likely to run over, because each Claude prompt can take up to 3 minutes, so keep most of the spare time for them. Use the rest for longer question breaks.

> **Running late?** Cut in this order: shorten the question breaks to one question, skip step 4 of D5, skip P5 in D4 (show slide 21 instead), run only P9 in D7, skip P2 in D2 and go straight to P3, then shorten D1 to the trace and the gap. Never cut D8; checking Claude's work is the point.

---

## Demo scripts

Each demo lists what to switch to, what to do, and what you should see. If live data doesn't cooperate, use the fallback and say so out loud.

### D1. A slow request, end to end (7 min)

**Switch to:** the **Open the search** link on slide 10, or browser tab 1. The script is in slide 10's speaker notes; this is the checklist.

1. On **APM → Traces**, show the search: `service:resident-api resource_name:"GET /api/user/dashboard" env:prod @duration:>5s`. Point at **in Indexed Spans** once the window is older than 15 minutes. Flip **Spans** to **Traces** and back, to show one row per request, as slide 9 promised.
2. Open one of the slowest requests (ideally 10 s or more; tab 2 has a backup). Click through **Flame Graph**, **Waterfall** (press expand all) and **Span List** (group by Service, read Exec Time).
3. On the flame graph, point at the stored procedure, then the stretch with **nothing under the API span**, and ask what our code was doing.
4. Click the `resident-api` span and open **SQL Queries**. Point at **Profiles** without opening it: part two opens it in D3.

**Expected:** a 10+ second trace with a multi-second stored procedure and a multi-second gap after it. The Span List shows `resident-api` exec time in seconds.

**Fallback:** if no slow trace is in range, use the trace in tab 2, or slides 6–7 (the screenshots are from a real 11.6 s trace).
### D2. Claude finds the slow request (6 min)

**Switch to:** terminal. Paste [P2](../../resources/demo-prompts.md#p2-find-the-slowest-endpoints), then [P3](../../resources/demo-prompts.md#p3-explain-one-slow-request-including-the-gap). Narrate the tools Claude calls while it works.

1. **P2.** Talk about: we asked for the `trace.*` metrics on purpose, because span counts are sampled; check that it really used them. Say what p95 means. Expected: a ranked table, with rare and busy endpoints called out.
2. **P3.** Talk about: it found the same gap we just found by hand. Expected: the stored procedure takes several seconds, then a 4–6 s gap. If it also names `GetEnumFromDescription` from the profile, say that part 2 shows how to find that yourself.

Claude takes 1 to 2.5 minutes per prompt. While it works, explain what it's doing. Don't stare at the spinner.

**Fallback:** if a prompt stalls past 3 minutes, press Esc and move on; describe the expected answer from the prompts page. If Claude asks to authenticate, the token expired: `/mcp`, authenticate, continue.

### D3. Trace to profile (6 min)

**Switch to:** browser, tab 2.

1. On the `resident-api` span, open **Profiles**. Point at the thread timeline: idle during SQL, busy through the gap.
2. In the flame graph below, hover `UserBP.GetUserDashboardItems()`. Point out it nests under the repository call (async, G4) and shows line 0 (G5).
3. Click **Open in Profiling**, or go to tab 3.
4. Show **CPU Time** first and find the dashboard low in the endpoint list (G1). Click **Wall Time**.
5. Show the **Method** list failing to find `GetUserDashboardItems` (G3), then use **Filter flame graph** → `method:"UserBP.GetUserDashboardItems()"` → **focus_on**.
6. Read the stack down to `EnumExtensions.GetEnumFromDescription` and `Attribute.GetCustomAttribute`.

**Expected:** `UserBP.GetUserDashboardItems` → `DashboardHelper.GetActivities` → `EnumExtensions.GetEnumFromDescription`, with reflection underneath.

**Fallback:** "No Wall Time data after applying filter operations" means a thin window (G7). Widen to 1 day and say why. If still empty, use slide 18.

### D4. Claude reads the profile (6 min)

**Switch to:** terminal. Paste [P4](../../resources/demo-prompts.md#p4-name-the-method-with-the-profiler), then [P5](../../resources/demo-prompts.md#p5-our-code-or-the-database).

1. **P4.** Talk about: we asked for Wall Time on purpose; check that it really used it, and that it found the same reflection hotspot as the filter in D3. Expected: `GetEnumFromDescription` reflection is about a third of the endpoint.
2. **P5.** Talk about: the profiler can point *away* from our code. Expected: the database, `api_mp_SearchWorkflows`, with Entity Framework waiting on `ReadAsync`. Then move to slide 21 to show the two profiles side by side.

**Fallback:** as in D2. If time is short, skip P5 and use slide 21 to explain the contrast.

### Before D5 and D6: how the two features are switched on

- **Exception Replay** is on for our services through Datadog's remote settings. Nothing to do. It takes a snapshot from the second occurrence of an exception on a server, then at most one per exception type, per server, per hour ([G13](../../resources/gotchas.md#g13-exception-replay-skips-the-first-occurrence)), so a snapshot you saw yesterday may be the only one for that issue today. That is why D5 uses one you picked the day before.
- **Exception Replay from dev doesn't work yet.** The ticket planned to trigger an exception in dev, but the only known trigger (the same file-link request D6 uses) never produced a snapshot: the first occurrence is skipped, and the second fails with an "internal error" ([G14](../../resources/gotchas.md#g14-some-exceptions-never-get-a-snapshot)). Check the [snapshots search](https://us5.datadoghq.com/logs?query=source%3Add_debugger%20%40debugger.product%3Aer%20env%3Adev&storage=flex_tier) for `env:dev` before each delivery; if dev snapshots start appearing, a dev example can replace the prod one.
- **Live Debugger** needs `DD_DYNAMIC_INSTRUMENTATION_ENABLED=true` (with `DD_SYMBOL_DATABASE_UPLOAD_ENABLED` and `DD_EXCEPTION_REPLAY_ENABLED`) on the service itself. On management-api in dev these are set as IIS app-pool environment variables on the dev web server, which survive deploys. Datadog's remote enablement didn't work. Other services and environments wait for DX-1573 ([G15](../../resources/gotchas.md#g15-live-debugger-needs-a-setting-on-the-service-itself)).

### D5. A real Exception Replay snapshot (5 min)

**Switch to:** browser, tab 4 (the issue you picked the day before). **This is production data:** say so, show it live in Datadog, and don't screenshot it or paste it anywhere. Open objects with care: only ones you checked the day before.

1. Show the issue header: the exception type, the service and how often it happens.
2. Under **Error sample**, on the **Stack Trace** tab, find the frame with **Exception Replay snapshot (on exit)**. Open one or two objects you checked the day before with **›**. Read two or three values out loud and say what they tell you about the failure that the stack trace alone doesn't.
3. Point at the **Redaction** badge and at any `[REDACTED]` value.
4. Switch to tab 5, the issue whose sample says **Exception Replay data was not captured**, and read the reason ([G13](../../resources/gotchas.md#g13-exception-replay-skips-the-first-occurrence), [G14](../../resources/gotchas.md#g14-some-exceptions-never-get-a-snapshot)).

**Expected:** a stack trace with a snapshot panel under one frame, showing `this`, `exception` and several locals.

**Fallback:** if the sample now shows no snapshot, try **Previous** and **Next**, or run the [snapshots search](https://us5.datadoghq.com/logs?query=source%3Add_debugger%20%40debugger.product%3Aer%20env%3Aprod&storage=flex_tier) and open a newer one. If nothing works in two minutes, go back to slide 24, which shows a real snapshot, and say that the live one didn't cooperate. That is also a fair picture of a preview feature.

### D6. Add a logpoint in dev (7 min)

**Switch to:** browser, tab 6 (**APM → Debugger**). Full steps and labels: [K7](../../resources/click-paths.md#k7-live-debugger-add-a-logpoint).

1. **New Session**, scroll past Bits Live Debugger to **Start debugging manually**, then **+ New Session**.
2. Service `management-api`, environment `dev`. Leave **Target specific instances** empty ([G19](../../resources/gotchas.md#g19-leave-the-version-empty-while-testing)).
3. Search `FilesController`, pick the **CLASS** result, click the gutter at line 53. Say what the endpoint does: it serves files from links in our emails (such as logo images), and line 53 decides which kind of link the request has.
4. Fill in the logpoint:
   - **with template:** `Course demo: ERES={ERES} IDENT={IDENT} resource={emailResourceKey}`
   - **when:** `IDENT != null && contains(IDENT, "dx-training")`
   - **capture variables:** **All variables**, depth 3

   Explain the condition: only requests with `dx-training` in them are logged, so other people's requests in dev are not.
5. **Start Debug Session.** While it becomes active (about 30 seconds), repeat the point: no code change, no deploy, no restart.
6. **Switch to the terminal** (Git Bash) and run:

   ```bash
   curl -s -o /dev/null -w "%{http_code}\n" "https://management-api.dev.ciranet.com/api/files/filelink?ERES=live-debugger-demo&IDENT=hello-dx-training"
   ```

   It prints `500`. Say that this is expected: the demo value isn't a real encrypted key, and the logpoint line runs before the error.
7. **Back to the browser.** The capture is in the list under the code. Open it and show the arguments `ERES` and `IDENT`, the local `emailResourceKey`, and the user fields that are empty because the request was anonymous. Mention that **Open in Logs** shows the same entry after about three minutes ([G18](../../resources/gotchas.md#g18-captures-appear-on-the-session-page-first)).
8. Stop the session from its header.

**Expected:** one capture within a few seconds of the `curl`, with the message `Course demo: ERES=live-debugger-demo IDENT=hello-dx-training resource=live-debugger-demo`.

**Fallback:** if `management-api` is missing from the service list or the logpoint doesn't become active within two minutes, show the screenshot at the end of [K7](../../resources/click-paths.md#k7-live-debugger-add-a-logpoint) and explain the missing setting ([G15](../../resources/gotchas.md#g15-live-debugger-needs-a-setting-on-the-service-itself)). If it's active but nothing is captured, check that the condition text matches the `IDENT` value in the request.

### D7. Claude on an error, then a logpoint (6 min)

**Switch to:** terminal. Prompts: [P9, P10 and P10b](../../resources/demo-prompts.md#p9-explain-an-error-from-its-snapshot).

1. Paste **P9**, with the exception type and service you showed in D5. While it works (about 2 minutes), explain that it's finding the Error Tracking issue and then reading the snapshot.
2. When it answers, point out that it explains the failure from the captured values, and says what the snapshot can't show. It often suggests a logpoint to capture the missing value. Explain that Live Debugger only works on management-api in dev today, so P10 adds the same logpoint as D6 rather than one on this service.
3. Paste **P10**. Claude checks the line, then creates the session and the logpoint on management-api in dev, with the condition and no version pin. It takes up to 3 minutes; talk through what it's doing.
4. When it says the logpoint is ready, switch to the second terminal tab and run the `curl` from D6. It prints `500`, as before.
5. Paste **P10b**. Claude shows the captured values and disables the logpoint.
6. Finish with something like: "This is the only prompt today that changes something, so it runs in dev only, with a condition, and it ends by disabling the logpoint."

**Expected:** P9 names the field that failed validation and why; P10 gives a session link, the line, the condition and "wait about 30 seconds"; P10b shows `ERES`, `IDENT`, `emailResourceKey` and the nulls, and says the logpoint is disabled.

**Fallback:** if time is short, run only P9 and say that Claude can also do what D6 showed by hand. If Datadog returns an error while creating the logpoint, let Claude check whether it exists before trying again ([G20](../../resources/gotchas.md#g20-a-logpoint-can-be-created-even-when-datadog-reports-an-error)). If nothing is captured, check that the condition matches the `IDENT` value in the `curl`.

### D8. Is it real? Then check it (6 min)

**Switch to:** terminal.

1. Paste **P6**. Expected: not a code regression; one short burst across hosts, median flat, back to normal; a guessed cause.
2. Paste **P7**. Expected: the list of queries, and Claude admitting it used sampled spans rather than metrics. Point out that this is why we ask Claude which queries it ran.
3. **Switch to the browser.** Open one link from P7 and check one number (a time window or a count) against the answer.
4. **Back to the terminal.** Slide 31 didn't give the cause, so tell the room now: the app pool configuration change just before the spike, which restarts the app pools. Paste **P7b** with that context. Expected: Claude revises to a cold start after the recycle. Then explain the warm-up in your own words.
5. Finish with something like: "Claude found the right evidence, but we had to tell it the cause."

**Fallback:** if time is short, run only P6 and P7b and describe P7. If Claude answers P6 without guessing a cause, ask "what caused it?" before P7b.

---

## Wrap-up

- [ ] Paste the **feedback form** link in the meeting chat (form questions: [feedback form](../../resources/feedback-form.md)).
- [ ] Paste the **course Slack thread** link and repeat the homework: one investigation, one posted finding, within two weeks.
- [ ] Point at the [cheat sheet](../../resources/cheat-sheet.md) on the course site.
- [ ] Stop the recording.

---

## Recording (internal only)

The demos show real production data, so **the recording is internal only**. It never goes in this repo, and this repo never links to it.

1. **Start:** in the Teams meeting, **More actions (…) → Record and transcribe → Start recording**, right after the "15 minutes before" checklist. Say at the start that the session is recorded.
2. **Stop:** **More actions → Record and transcribe → Stop recording** after questions.
3. **Store:** Teams saves the recording to the organizer's OneDrive. Move (or upload) it into the **DX team's Teams channel → Files** (the channel's SharePoint library), in a `Training recordings` folder.
4. **Share:** post the channel link in the DX team channel and in the course Slack thread. Never paste it into this repo, the course site or anything public.
5. **Never** commit the video, a download of it, or its transcript here. `.gitignore` blocks common video files under `courses/`, but don't rely on it.

---

## Dry-run notes

Record the timing and anything that didn't work from the presenter's full dry run, then fold the fixes into the outline, the deck and this sheet.

| Segment | Planned | Actual | Notes / fixes |
| - | - | - | - |
| Welcome | 4 | | |
| Part 1 + D1 + D2 + questions | 24 | | |
| Part 2 + D3 + D4 + questions | 25 | | |
| Part 3 + D5–D7 + questions | 26 | | |
| Part 4 + D8 | 11 | | |
| Wrap-up + questions | 10 | | |

## After the session

- [ ] Check the feedback responses within a week and note changes here.
- [ ] Two weeks later, check the success measure ([feedback form](../../resources/feedback-form.md#success-measure)).
- [ ] Update this run sheet with anything you learned.
