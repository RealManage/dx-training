# Run Sheet — Troubleshooting with Datadog 101

> The presenter's page. Keep it open on your second screen next to the speaker notes. Slide numbers match [`slides/deck-outline.md`](../../slides/deck-outline.md); prompt IDs (P1–P10b) match [demo prompts](../../resources/demo-prompts.md).

**Duration:** about 90 minutes, plus about 10 minutes of questions, in a two-hour slot · **Format:** screen share, switching between slides, browser (Datadog) and terminal (Claude Code)

Each part follows the same pattern: explain on slides, show it by hand in Datadog, then show Claude doing the same work. Parts 1 to 3 end with a two-minute question break.

---

## The day before

- [ ] Send attendees the [setup guide](../../SETUP-GUIDE.md) if you haven't. Ask them to post in the thread if verification fails.
- [ ] **Re-find the live example.** Traces age out, so pick fresh ones:
  - [ ] Open the [slow dashboard search](https://us5.datadoghq.com/apm/traces?query=service%3Aresident-api%20resource_name%3A%22GET%20%2Fapi%2Fuser%2Fdashboard%22%20env%3Aprod%20%40duration%3A%3E8s) over the past day. Pick a trace of about 10 s or more whose waterfall shows the stored procedure followed by a gap; anything over 8 s almost always has one. Keep its tab.
  - [ ] Check its span's **Profiles** tab has data (traces from 2026-09-22 on only).
  - [ ] Open the [filtered wall-time profile](https://us5.datadoghq.com/profiling/explorer?query=service%3Aresident-api%20env%3Aprod&profile_type=wall-time&endpoint=%7B%22incl%22%3A%5B%22GET%20%2Fapi%2Fuser%2Fdashboard%22%5D%7D&profiling-flame-graph__filter=focus_on%28method%3A%22UserBP.GetUserDashboardItems%28%29%22%29&viz=flame_graph). If it's empty at 1 hour, set the time range to 1 day.
- [ ] Run **P1** once in Claude Code to confirm the Datadog MCP answers, and run the D2 and D4 prompts (**P3**, **P4**) once with the trace link you picked. Run **P9** once with the exception you pick for D5 below, and check that Claude finds the same issue.
- [ ] **Pick the Exception Replay example (D5).** Snapshots age and some samples have none, so pick one fresh:
  - [ ] Open the [Exception Replay snapshots search](https://us5.datadoghq.com/logs?query=source%3Add_debugger%20%40debugger.product%3Aer%20env%3Aprod&storage=flex_tier) over the past day (storage **Flex**). Note a service and method that come up often.
  - [ ] Open that service's issues in **Errors → Error Tracking** and find the issue whose **Stack Trace** shows **Exception Replay snapshot (on exit)**. Choose a harmless one: a validation or configuration error, with no customer names, emails, addresses or account numbers in the values. Keep its tab.
  - [ ] Also note one issue whose sample says **Exception Replay data was not captured**, for step 4 of D5.
- [ ] **Check Live Debugger in dev (D6).**
  - [ ] **APM → Debugger → Configuration**, environment **dev**: `management-api` shows **READY**. If it doesn't, the setting was removed from the dev server ([G15](../../resources/gotchas.md#g15-live-debugger-needs-a-setting-on-the-service-itself)); ask the DX team, or plan to use the fallback.
  - [ ] Run D6 once end to end ([K7](../../resources/click-paths.md#k7-live-debugger-add-a-logpoint)). Check that line 53 of `FilesController.cs` is still the `if` after `emailResourceKey` is assigned; if the code moved, use the new line number. Stop the session afterwards.

## 15 minutes before

- [ ] Close email, chat and anything with customer data. Turn on Do Not Disturb.
- [ ] Browser: tabs in order: (1) the Datadog Dashboards list (the **Open Datadog** link on slide 5), (2) a backup slow trace you picked, (3) the all-requests profile (the **All requests** link on slide 11), (4) the Exception Replay issue you picked, (5) the issue with no snapshot, (6) **APM → Debugger**. Zoom to 125% so text reads over screen share.
- [ ] Terminal: Claude Code started in an empty folder with the code added for D4: `claude --add-dir <your ciranet-resident-api clone> --add-dir <your ciranet-api-common clone>`. Use a folder with no CLAUDE.md of its own, so nothing changes what Claude does on screen. Then font size bumped up, window cleared. `/mcp` shows datadog-mcp connected. A second terminal tab with the `curl` command for D6 and D7 typed but not run.
- [ ] Deck open: `slides/deck.html`, press **S** for the speaker window (notes, timer, next slide) and keep it on your own screen; press **F** in the main window. Share the screen showing the main deck window, never the speaker window.
- [ ] Feedback form link, course site link and course Slack thread link ready to paste.
- [ ] Start the recording (see [Recording](#recording-internal-only)).

---

## Timing guide

| Clock | Segment | Slides | Window | Demo |
| - | - | - | - | - |
| 0:00–0:04 | Welcome, what you'll leave with, three words | 1–3 | Slides | |
| 0:04–0:05 | Part 1: APM | 4 | Slides | |
| 0:05–0:18 | **Demo D1** | 5 | Browser | Service page → endpoint → slow traces → one trace → the gap → span tabs |
| 0:18–0:19 | What we just saw: the gap | 6 | Slides | |
| 0:19–0:21 | What Claude can do with the Datadog MCP | 7 | Slides | |
| 0:21–0:26 | **Demo D2** | 8 | Terminal | One prompt: where did the time go? |
| 0:26–0:28 | Questions | 8 | Slides | |
| 0:28–0:31 | Part 2: Why is it slow? Tracing vs profiling | 9–10 | Slides | |
| 0:31–0:43 | **Demo D3** | 11 | Browser | Span → Profiles → timeline → flame graph → all requests: Wall Time → filter |
| 0:43–0:44 | What we just saw: a slow lookup, many times | 12 | Slides | |
| 0:44–0:51 | **Demo D4** | 13 | Terminal | P4 with the D1 trace link: the profile, then the code |
| 0:51–0:53 | Questions | 13 | Slides | |
| 0:53–0:57 | Part 3: Why did it fail? Exception Replay, a real snapshot | 14–16 | Slides | |
| 0:57–1:02 | **Demo D5** | 17 | Browser | Issue → Stack Trace → snapshot values → a "not captured" issue |
| 1:02–1:04 | Live Debugger | 18 | Slides | |
| 1:04–1:11 | **Demo D6** | 19 | Browser + terminal | New session → logpoint on line 53 → `curl` → snapshot |
| 1:11–1:17 | **Demo D7** | 20 | Terminal | P9 → P10 → `curl` → P10b |
| 1:17–1:19 | Questions | 20 | Slides | |
| 1:19–1:24 | Part 4: Checking Claude's work; five habits; is this real? | 21–23 | Slides | |
| 1:24–1:30 | **Demo D8** | 24 | Terminal + browser | P6 → P7 → open one link → P7b |
| 1:30–1:35 | Which tool, gotchas, your turn | 25–27 | Slides | |
| 1:35–1:40 | Questions, feedback link | 27 | Slides | |


The slot runs to two hours, so about 20 minutes are spare. D7 and D8 are the demos most likely to run over, because each Claude prompt can take up to 3 minutes, so keep most of the spare time for them. Use the rest for longer question breaks.

> **Running late?** Cut in this order: shorten the question breaks to one question, skip step 4 of D5, skip D3's all-requests view (checklist steps 4 and 5), run only P9 in D7, then shorten D1: skip the Live Search step and the span tabs other than SQL Queries. Never cut D8; checking Claude's work is the point.

---

## Demo scripts

Each demo lists what to switch to, what to do, and what you should see. If live data doesn't cooperate, use the fallback and say so out loud.

### D1. A slow request, end to end (13 min)

**Switch to:** the **Open Datadog** link on slide 5, or browser tab 1. The full script is in slide 5's speaker notes; this is the checklist.

1. From the **Dashboards** list, hover **APM** in the left menu, then click it to open **APM Home**. In its **Service List**, type `resident-api` in **Search services** and click it.
2. **Service page**: env starts at `*` (all environments). Open the env list, point at the requests per environment, and choose **prod**. Point at the request, error and latency charts, then the **operation** selector (`aspnet_webapi.request` from this path). Explain the two operations (`aspnet.request` and `aspnet_webapi.request`): not a bug.
3. **Endpoints** on the left: say that each endpoint is a *resource*, then type `/api/user/dashboard` in **Search Endpoints**. It's listed once, for the selected operation. Click the GET row. In the panel that opens, scroll down to the **Traces** section (after **Span Summary**) and click **Trace Explorer**. It opens the Traces explorer filtered to the endpoint, with `env:prod` and `operation_name:aspnet_webapi.request` already set.
4. The label left of the time picker reads **Historical Search** (the panel's link opens historical data). Set **Past 15 Minutes** and point at the indexed span count above the list. Open the time picker and choose **LIVE · 15 Minutes** at the top: the label turns **Live Search** with a green LIVE badge, and the **Ok** count under **Status** jumps (4 to 125 when checked). Set **Past 1 Day**: back to Historical Search. Then add `@duration:>8s` (over 8 seconds, every request shows the gap; between 5 and 8 seconds some are mostly SQL). Flip **Spans** to **Traces** and back.
5. Open a slow one that looks like the example: **6+ spans**, and a **Latency Breakdown** bar about half green (resident-api), the rest orange and purple. Others are mostly SQL, mostly browser, or have only 3–4 spans. Tab 2 has a backup. **Flame Graph**, **Waterfall** (expand all), **Span List** (group by Service, read the **Exec Time** column in seconds).
6. On the flame graph, point at the stored procedure, then the stretch with **nothing under the API span**, and ask what our code was doing.
7. Click the `resident-api` span and click through **Overview**, **Infrastructure**, **Metrics**, **Logs** and **SQL Queries**, a few seconds each. Point at **Profiles** without opening it: part two opens it in D3.

**Expected:** a 10+ second trace with a multi-second stored procedure and a multi-second gap after it. The Span List shows `resident-api` exec time in seconds.

**Fallback:** if the Endpoints panel misbehaves, click **Traces search** on step 4 of the slide: it lands on the filtered result. If no slow trace is in range, use the trace in tab 2.

### D2. Claude helps analyze the slow request (5 min)

**Switch to:** the terminal. The prompt is on slide 8 and in its speaker notes ([P3](../../resources/demo-prompts.md#p3-where-did-the-time-go)); one click selects it in the notes. Paste it without pressing Enter, copy the trace URL from the D1 tab's address bar, paste it after the prompt, then press Enter. Copy the prompt first: copying it after the URL would replace the URL on the clipboard.

1. While it works (about half a minute), say which tools it calls: it reads the trace from the link and adds up its spans.
2. When it answers, compare it with what we found by hand: the total time, the stored procedure, and the time in our own code.

**Expected:** three plain bullets that match D1's trace: the total time, the SQL time with the dashboard stored procedure named, and several seconds left for our own code. It's the same request, so the numbers should match.

**Fallback:** if Claude can't open the link, paste only the number after `traceID=`. If a prompt stalls past 3 minutes, press Esc and describe the expected answer. If Claude asks to authenticate, the token expired: `/mcp`, authenticate, continue.

### D3. From the trace to the profile (12 min)

**Switch to:** the D1 browser tab, with the slow trace still open. The full script is in slide 11's speaker notes; this is the checklist.

1. Click the `resident-api` span, then **Profiles** (the last span tab); drag the panel up if it's small. Point at the thread timeline: idle during SQL, busy through the gap.
2. Click **Flame Graph** under the timeline. Width is time, left to right means nothing, read top to bottom. Gray **Standard Library** rows are framework; blue is our code. Point at **CPU Time** at the top: the thread was running, not waiting.
3. Read down: `CCCaching.AddOrGetCacheAsync` and `UserRepository.GetUserActivityFeed` sit on top because the code is async ([G4](../../resources/gotchas.md#g4-async-code-nests-under-the-awaited-call)); then `UserBP.GetUserDashboardItems`, `DashboardHelper.GetActivities`, and `EnumExtensions.GetEnumFromDescription` in several places. Hover one and read its time.
4. Click **All requests** on the slide (a new tab, so the trace stays open): APM → Profiles, resident-api in prod, the dashboard endpoint. It opens on **CPU Time**: stay there. Explain CPU vs Wall: the span's profile said CPU Time, so the gap is CPU work. **Wall Time** adds the waiting branches (other repository calls, SQL) and hides the helper, so don't switch.
5. Click **Filter flame graph**, choose **focus_on** from the list, then type `GetUserDashboardItems` inside its brackets and pick `method:"UserBP.GetUserDashboardItems()"`. The same shape as the one trace: `GetEnumFromDescription` under `GetUserDashboardItems`.
6. In passing: a profile that's mostly Entity Framework or the SQL reader waiting means the fix is in the database, not the C#.

**Expected:** the span's flame graph shows `GetEnumFromDescription` under `GetUserDashboardItems`; the all-requests view, filtered, shows the same.

**Fallback:** if the span's Profiles tab is empty, use the trace in tab 2 (you checked its profile the day before). An empty or thin filtered graph means a quiet window ([G7](../../resources/gotchas.md#g7-low-traffic-gives-thin-profiles)): set **Past 1 Day** and say why.

### D4. Claude reads the profile and the code (7 min)

**Switch to:** the terminal. The prompt is on slide 13 and in its speaker notes ([P4](../../resources/demo-prompts.md#p4-read-the-profile-and-the-code)); one click selects it in the notes. Paste it without pressing Enter, copy the trace URL from the D1 tab again, paste it after the prompt, then press Enter. Claude Code must have been started with `--add-dir` for the code (see 15 minutes before).

1. While it works (about two minutes; 2:15 in a dry run), say what it does: it gets this request's profile, then finds and reads the methods in our code. Scroll slowly so the room can follow.
2. When it answers, read its plain-language recap out loud, then compare it with D3: `GetEnumFromDescription` under `GetUserDashboardItems`, reflection on every call with nothing cached, run once per activity and property pair. Point at the fixes: cache the lookup, work out each category once.

**Expected:** a short answer naming `GetEnumFromDescription` (or `DashboardHelper.GetActivities` with it underneath), reflection through `Attribute.GetCustomAttribute`, the pairing in `UserBP`, and fixes. Both test runs also named the dashboard stored procedure as the database half of the request.

**Fallback:** if Claude can't open the link, paste only the number after `traceID=`. If it can't find the code, check that Claude Code was started with `--add-dir` for both folders. For stalls and authentication, as in D2.

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

**Fallback:** if the sample now shows no snapshot, try **Previous** and **Next**, or run the [snapshots search](https://us5.datadoghq.com/logs?query=source%3Add_debugger%20%40debugger.product%3Aer%20env%3Aprod&storage=flex_tier) and open a newer one. If nothing works in two minutes, go back to slide 16, which shows a real snapshot, and say that the live one didn't cooperate. That is also a fair picture of a preview feature.

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
4. **Back to the terminal.** Slide 23 didn't give the cause, so tell the room now: the app pool configuration change just before the spike, which restarts the app pools. Paste **P7b** with that context. Expected: Claude revises to a cold start after the recycle. Then explain the warm-up in your own words.
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
