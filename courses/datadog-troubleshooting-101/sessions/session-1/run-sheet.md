# Run Sheet — Troubleshooting with Datadog 101

> The presenter's page. Keep it open on your second screen next to the speaker notes. Slide numbers match [`slides/deck-outline.md`](../../slides/deck-outline.md); prompt IDs (P1–P10b) match [demo prompts](../../resources/demo-prompts.md).

**Duration:** about 85 minutes, plus questions, in a two-hour slot · **Format:** screen share, switching between slides, browser (Datadog) and terminal (Claude Code)

Each part follows the same pattern: explain on slides, show it by hand in Datadog, then show Claude doing the same work. Parts 1 to 3 end with a two-minute question break.

---

## The day before

- [ ] Send attendees the [setup guide](../../SETUP-GUIDE.md) if you haven't. Ask them to post in the thread if verification fails.
- [ ] **Re-find the live example.** Traces age out, so pick fresh ones:
  - [ ] Open the [slow dashboard search](https://us5.datadoghq.com/apm/traces?query=service%3Aresident-api%20resource_name%3A%22GET%20%2Fapi%2Fuser%2Fdashboard%22%20env%3Aprod%20%40duration%3A%3E8s) over the past day. Pick a trace of about 10 s or more whose waterfall shows the stored procedure followed by a gap; anything over 8 s almost always has one. Keep its tab.
  - [ ] Check its span's **Profiles** tab has data (traces from 2026-09-22 on only).
  - [ ] Open the [filtered wall-time profile](https://us5.datadoghq.com/profiling/explorer?query=service%3Aresident-api%20env%3Aprod&profile_type=wall-time&endpoint=%7B%22incl%22%3A%5B%22GET%20%2Fapi%2Fuser%2Fdashboard%22%5D%7D&profiling-flame-graph__filter=focus_on%28method%3A%22UserBP.GetUserDashboardItems%28%29%22%29&viz=flame_graph). If it's empty at 1 hour, set the time range to 1 day.
- [ ] Run **P1** once in Claude Code to confirm the Datadog MCP answers, and run the D2 and D4 prompts (**P3**, **P4**) once with the trace link you picked. Run **P9** once with the exception you pick for D5 below, and check that Claude finds the same issue.
- [ ] **Check the Exception Replay example (D5).** Open the [Error Tracking link](https://us5.datadoghq.com/error-tracking?query=service%3Amanagement-api%20env%3Aprod%20error.type%3ASystem.FormatException) and set **Past 1 Week**. Open the issue in `System.Convert.FromBase64_Decode`: its sample should show **Exception Replay snapshot (on exit)** under `TripleDesEncryption.Decrypt`. If no sample in the week has one, use the identity-api fallback: in Error Tracking, find a `DbEntityValidationException` issue in identity-api whose sample has a snapshot with no customer data, and keep it in tab 4.
- [ ] **Check Live Debugger in dev (D6).**
  - [ ] **APM → Debugger → Configuration**, environment **dev**: `management-api` shows **READY**. If it doesn't, plan to use the backup slide ([G15](../../resources/gotchas.md#g15-live-debugger-needs-a-setting-on-the-service-itself)).
  - [ ] **Sign in to GitLab inside Datadog.** Open any code view (Live Debugger, or a stack trace in Error Tracking) and click **Sign-in with GitLab**. Without it the code doesn't load and you can't click line 53 ([G22](../../resources/gotchas.md#g22-the-code-view-needs-a-gitlab-sign-in)).
  - [ ] Run D6 once end to end. Check that line 53 of `FilesController.cs` is still the `if` after `emailResourceKey` is assigned; if the code moved, use the new line number. Stop the session afterwards.

## 15 minutes before

- [ ] Close email, chat and anything with customer data. Turn on Do Not Disturb.
- [ ] Browser: tabs in order: (1) the Datadog Dashboards list (the **Open Datadog** link on slide 5), (2) a backup slow trace you picked, (3) the all-requests profile (the **All requests** link on slide 11), (4) the identity-api fallback issue, only if you needed it. Zoom to 125% so text reads over screen share.
- [ ] Terminal: Claude Code started in an empty folder with the code added for D4: `claude --add-dir <your ciranet-resident-api clone> --add-dir <your ciranet-api-common clone> --add-dir <your ciranet-management-api clone>`. Use a folder with no CLAUDE.md of its own, so nothing changes what Claude does on screen. Then font size bumped up, window cleared. `/mcp` shows datadog-mcp connected. A second terminal tab with the `curl` command for D6 and D7 typed but not run.
- [ ] Deck open: `slides/deck.html`, press **S** for the speaker window (notes, timer, next slide) and keep it on your own screen; press **F** in the main window. Share the screen showing the main deck window, never the speaker window.
- [ ] Course site link and course Slack thread link ready to paste.
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
| 0:53–0:56 | Part 3: Finding the cause of an error; Exception Replay | 14–15 | Slides | |
| 0:56–1:01 | **Demo D5** | 16 | Browser | Error Tracking → the issue → Stack Trace → the Decrypt snapshot → a redacted value |
| 1:01–1:03 | Live Debugger | 17 | Slides | |
| 1:03–1:09 | **Demo D6** | 18 | Browser | New session → logpoint on line 53 → test request link → capture → stop |
| 1:09–1:17 | **Demo D7** | 19 | Terminal | P9 with the D5 issue link: the snapshot, then the code |
| 1:17–1:19 | Questions | 19 | Slides | |
| 1:19–1:25 | Part 4: Checking Claude's work; three habits; is this real? | 20–22 | Slides | |
| 1:25–1:29 | Which tool for which question, your turn | 23–24 | Slides | |
| 1:29–1:40 | Questions | 24 | Slides | |


The slot runs to two hours, so about 20 minutes are spare. D4 and D7 are the demos most likely to run over, because each Claude prompt can take up to 2 or 3 minutes, so keep some of the spare time for them. Use the rest for longer question breaks.

> **Running late?** Cut in this order: shorten the question breaks to one question, skip D3's all-requests view (checklist steps 4 and 5), then shorten D1: skip the Live Search step and the span tabs other than SQL Queries.

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
4. Click **All requests** on the slide (a new tab, so the trace stays open): APM → Profiles, resident-api in prod, the dashboard endpoint (ticked in the list on the right, maybe scrolled out of view). Set **Past 1 Day**: a single hour, especially a quiet one, has too few samples and shows only some of the call sites. It opens on **CPU Time**: stay there. Explain CPU vs Wall: the span's profile said CPU Time, so the gap is CPU work. **Wall Time** adds the waiting branches (other repository calls, SQL) and hides the helper, so don't switch.
5. Click **Filter flame graph**, choose **focus_on** from the list, then type `GetUserDashboardItems` inside its brackets and pick `method:"UserBP.GetUserDashboardItems()"`. The same shape as the one trace: `GetEnumFromDescription` under `GetUserDashboardItems`.
6. In passing: a profile that's mostly Entity Framework or the SQL reader waiting means the fix is in the database, not the C#.

**Expected:** the span's flame graph shows `GetEnumFromDescription` under `GetUserDashboardItems`; the all-requests view, filtered, shows the same.

**Fallback:** if the span's Profiles tab is empty, use the trace in tab 2 (you checked its profile the day before). If the graph is still thin at Past 1 Day, see [G7](../../resources/gotchas.md#g7-low-traffic-gives-thin-profiles).

### D4. Claude reads the profile and the code (7 min)

**Switch to:** the terminal. The prompt is on slide 13 and in its speaker notes ([P4](../../resources/demo-prompts.md#p4-read-the-profile-and-the-code)); one click selects it in the notes. Paste it without pressing Enter, copy the trace URL from the D1 tab again, paste it after the prompt, then press Enter. Claude Code must have been started with `--add-dir` for the code (see 15 minutes before).

1. While it works (about two minutes; 2:15 in a dry run), say what it does: it gets this request's profile, then finds and reads the methods in our code. Scroll slowly so the room can follow.
2. When it answers, read its plain-language recap out loud, then compare it with D3: `GetEnumFromDescription` under `GetUserDashboardItems`, reflection on every call with nothing cached, run once per activity and property pair. Point at the fixes: cache the lookup, work out each category once.

**Expected:** a short answer naming `GetEnumFromDescription` (or `DashboardHelper.GetActivities` with it underneath), reflection through `Attribute.GetCustomAttribute`, the pairing in `UserBP`, and fixes. Both test runs also named the dashboard stored procedure as the database half of the request.

**Fallback:** if Claude can't open the link, paste only the number after `traceID=`. If it can't find the code, check that Claude Code was started with `--add-dir` for both folders. For stalls and authentication, as in D2.

### Before D5 and D6

Part 3 is a show and tell: most of the room won't use these tools often. Keep the talking short and the demos moving.

- **Exception Replay** is on for our services through Datadog's remote settings. It takes a snapshot from the second occurrence of an exception on a server, then at most one per exception type, per server, per hour ([G13](../../resources/gotchas.md#g13-exception-replay-skips-the-first-occurrence)).
- **Live Debugger** needs a setting on the service itself ([G15](../../resources/gotchas.md#g15-live-debugger-needs-a-setting-on-the-service-itself)). In dev, management-api showed **READY** on 2026-10-08 (so did identity-api, legacy-api and mobile-api). Prod uses **Strict** redaction, which blocks logpoints ([G16](../../resources/gotchas.md#g16-strict-redaction-blocks-logpoints-in-prod)).

### D5. An error from our email links (5 min)

**Switch to:** the **Error Tracking** link on slide 16. The full script is in slide 16's speaker notes; this is the checklist. Production data: no screenshots.

1. Set **Past 1 Week** (the link carries no time range). Two issues share the Base-64 message: open the one in **`System.Convert.FromBase64_Decode`**. The one in `TripleDesEncryption.Decrypt` has no snapshot.
2. Point at the count, then scroll to **Error sample**. The **Stack Trace** tab opens on a sample with **Exception Replay snapshot (on exit)** under `TripleDesEncryption.Decrypt(cipherString, key, useHashing)`.
3. Read the red message (not a valid Base-64 string). Expand `cipherString`: "The value was redacted based on its name". Point at **Redaction: Targeted**.
4. Open only the `Decrypt` snapshot: in async frames above it, variable names can be wrong or missing ([G21](../../resources/gotchas.md#g21-async-frames-can-show-the-wrong-variables)).

**Expected:** the snapshot on `Decrypt` with `useHashing = True`, `@exception = FormatException`, `resultArray = null`, and `cipherString` and `key` redacted.

**Fallback:** if the sample says **Exception Replay data was not captured**, click **Previous**. If no sample in the week has a snapshot, open the identity-api issue in tab 4 and say this error had no snapshot this week.

### D6. Add a logpoint in dev (6 min)

**Switch to:** the **Live Debugger** link on slide 18 (APM → Debugger). The full script is in slide 18's speaker notes; the template and condition are copy boxes there.

1. **New Session**: skip the **Bits Live Debugger** box and click **+ New Session** under **Start debugging manually**.
2. `management-api`, `dev`, leave **Target specific instances** empty ([G19](../../resources/gotchas.md#g19-leave-the-version-empty-while-testing)). Search `FilesController`, pick the **CLASS** result, and click in the code to close the method list. **Don't press Escape**: it closes the dialog.
3. Scroll to line 53 and click left of the line number. Paste the template over **with template**, click **+ Condition** and paste the condition into **when**. Capture variables is already **All variables**, depth 3.
4. **Start Debug Session**. Wait until the logpoint says **Waiting for events** (about 30 s).
5. Click **Test request** on the slide. The new tab shows "Error retrieving file by link." The capture appears under the code within about 15 s; if it doesn't, the list stopped refreshing: switch the time range above it from **LIVE** to **Past 15 Minutes** and back to **LIVE**. Open it: **Values for line 53** shows `ERES` and `IDENT`.
6. Close the panel and click the **pause icon** next to **Active** (top right): the session turns **INACTIVE**.

**Expected:** one capture, `Course demo: ERES=live-debugger-demo IDENT=hello-dx-training resource=live-debugger-demo`.

**Fallback:** if the code doesn't load, you're not signed in to GitLab ([G22](../../resources/gotchas.md#g22-the-code-view-needs-a-gitlab-sign-in)). If management-api isn't listed, or nothing is captured within a minute, click **Backup** on the card. If the test link fails, run it from Git Bash: `curl -s -o /dev/null -w "%{http_code}\n" "https://management-api.dev.ciranet.com/api/files/filelink?ERES=live-debugger-demo&IDENT=hello-dx-training"` (prints 500).

### D7. Claude explains the error (8 min)

**Switch to:** the terminal. The prompt is on slide 19 and in its speaker notes ([P9](../../resources/demo-prompts.md#p9-explain-an-error-from-its-snapshot)); one click selects it in the notes. Paste it without pressing Enter, copy the URL from the D5 tab, paste it after the prompt, then press Enter. Claude Code needs `--add-dir` for management-api and api-common (see 15 minutes before).

1. While it works (about two minutes; 98–102 s in testing), say what it does: it reads the issue and its snapshot, then finds and reads the methods in our code. Scroll slowly.
2. When it answers, compare with D5 (not valid Base-64, values redacted), then point at the code finding: a bad link was meant to return Not Found, but the exception comes first, so the user gets a 500.

**Expected:** four short bullets: the bad Base-64 link value; what the snapshot hides; a 500 with "Error retrieving file by link."; and the fix (decrypt safely and let the existing "invalid parameters" check return 404).

**Note:** D6's test request throws the same exception in dev and lands in this same issue, so the newest sample is your dev request. The prompt asks for a prod sample with a snapshot for that reason; if Claude mentions the dev request, say it's ours from D6.

**Fallback:** if Claude can't open the link, paste only the issue ID (the value after `issueId` in the link). For stalls and authentication, as in D2.

---

## Wrap-up

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
| Part 4 | 6 | | |
| Wrap-up + questions | 15 | | |

## After the session

- [ ] Two weeks later, check the success measure ([feedback form](../../resources/feedback-form.md#success-measure)).
- [ ] Update this run sheet with anything you learned.
