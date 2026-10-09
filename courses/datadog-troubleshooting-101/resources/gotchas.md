---
order: 40
---
# Gotchas

> Every one of these bit someone during the profiler rollout (DX-1605) or while building this course. Each has an ID so the slides, the cheat sheet and Claude prompts can point at it.

## G1. The Profiles page opens on CPU Time

- **Symptom:** a slow endpoint barely shows up in the profile. `GET /api/user/dashboard` uses only ~0.2 s of CPU per minute.
- **Why:** CPU Time only counts threads that are running. A request that waits on the database is mostly waiting.
- **Fix:** click the **Wall Time** card. Wall time counts elapsed time, running or waiting.
- **Seen live:** asked to draft a Jira ticket, Claude pulled the CPU profile and concluded our code "isn't the bottleneck". The same prompt using wall time found the reflection hotspot. Ask Claude which profile type it used.

## G2. Wall Time includes idle threads

- **Symptom:** the unfiltered Wall Time flame graph is mostly `Thread.Sleep` from Datadog's own background threads.
- **Why:** wall time counts every thread, including ones asleep on purpose.
- **Fix:** filter to an endpoint (**For Endpoints** on the right) and to our code (**Filter flame graph**).

## G3. The Method list cannot find callers

- **Symptom:** you search the **Method** list on the right for `GetUserDashboardItems` and it isn't there.
- **Why:** that list ranks methods by *self* time. A method that mostly calls other methods has almost none.
- **Fix:** use the **Filter flame graph** box above the graph instead, pick `method:"UserBP.GetUserDashboardItems()"` and choose **focus_on**. In a link, the full method value with the class and `()` is required; a bare name silently matches nothing.

## G4. Async code nests under the awaited call

- **Symptom:** `UserBP.GetUserDashboardItems` appears *under* `UserRepository.GetUserActivityFeed` and `CCCaching.AddOrGetCacheAsync`, as if the repository were slow.
- **Why:** with async/await, the code after an `await` runs as a continuation of the awaited task, so the profiler files it under that call.
- **Fix:** read down to the widest frame in our own namespace (`CiraNet...`). The repository isn't slow; it's the parent of the continuation.

## G5. Frames show line 0

- **Symptom:** every frame shows a method name but line 0.
- **Why:** the symbol (PDB) files the profiler needs for line numbers aren't deployed yet. DX-1570 tracks that; it isn't a profiler fault.
- **Fix:** use the method name and search the code for now.

## G6. No profiles before 2026-09-22

- **Symptom:** the Profiles tab on an older trace is empty.
- **Why:** the profiler was switched on across CiraNet on 2026-09-22.
- **Fix:** pick traces from that date on.

## G7. Low traffic gives thin profiles

- **Symptom:** a filtered flame graph says "No Wall Time data after applying filter operations", or shows a few jagged bars.
- **Why:** the profiler samples. Few requests in the window means few samples.
- **Fix:** widen the time range (1 day instead of 1 hour) before concluding anything.

## G8. Span counts are sampled

- **Symptom:** counting spans gives nonsense rates. A search showed "30 requests" in an hour for an endpoint the metrics put at ~558.
- **Why:** only a sample of successful spans is kept (indexed), while error spans are kept at a much higher rate. Counts and ratios from spans are biased.
- **Fix:** for request counts, error rates and percentiles, use the `trace.*` metrics (for example `trace.aspnet.request`). Ask Claude to use them too.

## G9. Some span filters silently return nothing

- **Symptom:** a query with `env:` or `service:` returns 0 results and no error, even though matching spans exist.
- **Why:** some span aggregations quietly ignore those filters.
- **Fix:** never read "0 results" as "none". Re-run grouping *by* env or service instead of filtering on it, or check the same query in the UI.

## G10. Check the operation and env selectors

- **Symptom:** the Service page numbers don't match what a colleague sees.
- **Why:** the **operation**, **env** and **version** selectors at the top (and the **Latency for** *operation* selector on the chart) change every number. Each ASP.NET endpoint reports under two operations, `aspnet.request` and `aspnet_webapi.request`. That's not a bug or a duplicate: the tracer's ASP.NET integration measures the whole request, and its Web API 2 integration measures the controller part inside it, so every request has a parent and a child span. On .NET Core, Datadog offers a setting (`DD_TRACE_SINGLE_SPAN_ASPNETCORE_ENABLED`) that records only one span; there's no equivalent for .NET Framework.
- **Fix:** check all three selectors before you compare or screenshot a number. To see one row per request in **APM → Traces**, switch **Spans** to **Traces**, or filter on one operation, for example `operation_name:aspnet.request`.

## G11. The MCP URL must ask for all toolsets

- **Symptom:** Claude can search spans but says it has no profiling or error-tracking tools.
- **Why:** the Datadog MCP server loads only its `core` toolset unless the URL asks for more.
- **Fix:** the URL must end in `?toolsets=all`. See the [setup guide](../SETUP-GUIDE.md). Restart Claude Code after changing it.

## G12. Claude only sees what Datadog records

- **Symptom:** Claude calls a latency spike "noise" or guesses at a cause, missing something the team knows.
- **Why:** app pool recycles, config changes and manual work often leave no event in Datadog.
- **Fix:** tell Claude what changed and when, then ask whether that fits the evidence. See [Checking Claude's work](checking-claudes-work.md).

## Exception Replay and Live Debugger

> Both features are new and still change often. These were hit while preparing the debugging part of the course (DX-1673) and in the Live Debugger trials (DX-1568). Tracer 3.54 on the dev web server, September 2026.

## G13. Exception Replay skips the first occurrence

- **Symptom:** an Error Tracking issue shows a stack trace but no variable values, with the message "Debug information is captured only from the second occurrence of this exception in the same service instance."
- **Why:** Exception Replay waits for an exception to happen a second time on the same server before it takes a snapshot. After that it takes at most one snapshot per exception type, per server, per hour.
- **Fix:** wait for the exception to happen again, or look at a busier issue. A rare exception may never get a snapshot.

## G14. Some exceptions never get a snapshot

- **Symptom:** even a repeated exception shows "Debug information could not be captured due to an internal error."
- **Why:** not known yet. It was seen on an exception thrown inside a NuGet library (no symbol files) on an async path. The two occurrences of that exception were also split into two separate Error Tracking issues.
- **Fix:** don't wait for it. Use a different issue, or place a Live Debugger logpoint near the line instead (dev only for now, G15).

## G15. Live Debugger needs a setting on the service itself

- **Symptom:** the service is missing from Live Debugger, or logpoints never become active.
- **Why:** Live Debugger needs `DD_DYNAMIC_INSTRUMENTATION_ENABLED=true` in the service's own configuration. Turning it on from Datadog's remote settings wasn't enough on our tracer, even on 3.54. Today only management-api in dev has the setting. DX-1573 tracks adding it to Web.config for every environment.
- **Fix:** use management-api in dev for now. For another service, ask the DX team.

## G16. Strict redaction blocks logpoints in prod

- **Symptom:** creating a logpoint on a prod service fails, or Claude reports that the service "is in strict redaction mode".
- **Why:** redaction is set per service and environment (**APM → Debugger → Configuration**, the **Redaction** column). Most of our prod services are set to **Strict**, and that mode blocks logpoints. **Targeted** mode allows them, with sensitive names still redacted. Exception Replay snapshots show the mode in a **Redaction** badge.
- **Fix:** debug in dev or qa. Logpoints in prod are possible: turn Live Debugger on for the service and switch its redaction to Targeted. That's a team decision about how much production data can show, not something to do for one investigation.

## G17. A logpoint on an `await` line never fires

- **Symptom:** the logpoint is active but nothing is ever captured.
- **Why:** in our optimized builds, a line that contains `await` can't be instrumented. Also, a line after an `await` that failed never runs, so a logpoint there looks broken when the line was simply never reached.
- **Fix:** put logpoints on plain statements: assignments, `if` conditions, `return` lines, `catch` blocks.

## G18. Captures appear on the session page first

- **Symptom:** you sent the test request, but searching Logs finds nothing.
- **Why:** the capture shows on the debug session page right away, but takes about three minutes to reach Logs search. A new logpoint also needs about 30 seconds to become active, and a session stops by itself after about 60 minutes.
- **Fix:** watch the session page. Wait until the logpoint shows as active before you send the request. Start a new session if the old one has stopped.

## G19. Leave the version empty while testing

- **Symptom:** a logpoint that worked yesterday matches no instances today.
- **Why:** the **Target specific instances** field can limit a logpoint to one version. A deploy replaces that version, and the logpoint then matches nothing, with no warning. Our dev services can deploy several times a day.
- **Fix:** leave **Target specific instances** empty while testing.

## G20. A logpoint can be created even when Datadog reports an error

- **Symptom:** creating a logpoint (in the UI, or by Claude through the MCP) returns an error such as a 503, but the logpoint is there when you look.
- **Why:** not known. The error came back although the logpoint had been saved. It happened in both P10 trials on 2026-09-30: a 503 once and a timeout once. Both times Claude listed the session, found the logpoint, and didn't retry.
- **Fix:** before you try again, list the session's logpoints (or ask Claude to). Retrying blindly creates a second logpoint that logs every request twice.

## G21. Async frames can show the wrong variables

- **Symptom:** in an Exception Replay snapshot, a frame from an `async` method shows variables that aren't in that method, or says a value isn't available.
- **Why:** the compiler rewrites async methods into a state machine, and the snapshot reads its fields. Seen on 2026-10-08 in the email-link error's snapshots.
- **Fix:** read the snapshot of the plain (non-async) frame where the exception was thrown, such as `TripleDesEncryption.Decrypt`. Treat names in async frames above it as hints.

## G22. The code view needs a GitLab sign-in

- **Symptom:** Live Debugger, or a stack trace in Error Tracking, shows blurred code and "Log in to your GitLab account to access this content". In Live Debugger you can't click a line, and the logpoint form (template, condition) doesn't appear.
- **Why:** Datadog loads source from GitLab with each viewer's own GitLab login. Seen on 2026-10-08 in a browser that wasn't signed in.
- **Fix:** click **Sign-in with GitLab** once in that browser. **Input method or file:line manually** finds the file without it, but still shows no form, so sign in.
