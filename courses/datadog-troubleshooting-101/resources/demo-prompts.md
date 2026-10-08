---
order: 30
---
# Demo Prompts

> Copy-paste prompts for Claude Code with the Datadog MCP server. Each was run **twice** against our Datadog account and kept only if both runs gave a useful answer: P1–P8 on 2026-09-25 (Claude Code 2.1.282), P9–P10b on 2026-09-30. Every prompt only reads, except P10, which adds a Live Debugger logpoint in dev. Claude's wording changes every run; "a good answer" lists what should be in it, not the exact text.
>
> **Answers contain internal detail.** Claude will print host names, database servers, client locations, trace IDs and links carrying internal IDs. That's fine on an internal screen share. Never paste an answer into this repo, a public page or a ticket visible outside RealManage.

Set up first with the [setup guide](../SETUP-GUIDE.md). Each prompt takes 30 seconds to 3 minutes. P1–P8 are about slow requests and checking Claude's work; P9–P10b are about errors.

---

## P1. Is it connected?

```text
Using the Datadog MCP, list the services that have continuous profiling data in the last hour.
```

- **Shows:** the connection works and the profiling tools are loaded.
- **A good answer:** about a dozen .NET services, including `resident-api`, `management-api` and `identity-api`, and a note that the order means nothing.
- **Red flags:** "I don't have profiling tools" means the URL is missing `?toolsets=all` ([G11](gotchas.md#g11-the-mcp-url-must-ask-for-all-toolsets)).

## P2. Find the slowest endpoints

```text
Which prod endpoints in resident-api have the worst p95 latency over the last 24 hours? Use the trace metrics rather than counting spans, and give me a Datadog link to check it.
```

- **Shows:** Claude choosing the unsampled `trace.aspnet.request` metric and grouping by endpoint.
- **A good answer:** a ranked table with p95 and hit counts. It separates rare endpoints (a few requests, so the p95 is one slow call) from busy ones. `POST /api/account/statement/generate`, `GET /api/community/dashboard/cashinvestment` and `POST /api/user/create` ranked high in both test runs; `GET /api/user/dashboard` showed as a busy endpoint with a slow tail.
- **Red flags:** p95 or rates computed from span counts ([G8](gotchas.md#g8-span-counts-are-sampled)). In testing, the two runs used different aggregation windows and got different numbers. Both were reasonable, which is why you ask ([C1](checking-claudes-work.md)).

## P3. Explain one slow request, including the gap

```text
Find one slow GET /api/user/dashboard request on prod resident-api from the last hour and explain where the time went, including any gap the spans don't cover.
```

- **Shows:** Claude picking a trace, laying out its timeline and noticing time no span covers.
- **A good answer:** the stored procedure `api_rp_GetResidentPortalUserDashboardInfo` takes the biggest covered chunk (several seconds), then about **4–6 seconds with no child spans** inside the API. The best answers pull the profile for that trace and name `UserBP.GetUserDashboardItems` / `EnumExtensions.GetEnumFromDescription`. Some also point out time outside the server span, in the browser.
- **Red flags:** it can't find a slow request in the last hour (traffic varies; say "last 4 hours"), or it only reports the SQL time and ignores the gap.

## P4. Name the method with the profiler

```text
Pull the wall-time profile for GET /api/user/dashboard on prod resident-api over the last 24 hours and tell me which of our own methods take the most time. Include the Datadog link.
```

- **Shows:** a wall-time flame graph filtered to one endpoint, summarized as "our methods, ranked".
- **A good answer:** `UserBP.GetUserDashboardItems` as the entry point, most of it in `DashboardHelper.GetActivities`. `EnumExtensions.GetEnumFromDescription` is about a third of the endpoint, and almost all of it is reflection (`Attribute.GetCustomAttribute`). Both test runs agreed. It should also say the endpoint is a small share of the whole service.
- **Red flags:** a CPU-time profile instead of wall time ([G1](gotchas.md#g1-the-profiles-page-opens-on-cpu-time)).

## P5. Our code or the database?

```text
Is GET /api/wf/search on prod management-api slow because of our C# code or because of the database? Check the traces and the profiler, and tell me which queries you ran.
```

- **Shows:** the contrast case, where the profiler points away from our code.
- **A good answer:** **the database**. The stored procedure `api_mp_SearchWorkflows` is most of each slow request, and the profile is mostly Entity Framework and `SqlDataReader.ReadAsync` waiting for rows. Our own code (`WorkflowBP`) is a small share. Large result sets make the worst cases worse.
- **Red flags:** blaming the C# because Entity Framework frames count as "our" process.

## P6. Is it real?

```text
We got a Watchdog latency alert on prod management-api around 2026-09-22 18:40 UTC. Is it a real regression or noise? Compare against the same time the day before.
```

- **Shows:** the "regression or noise?" habit: compare against a baseline.
- **A good answer:** not a code regression. One short burst of slow requests (about 18:40–18:45) across several hosts at once, the median unchanged, back to normal by the next window, and no prod deploy in the window.
- **Red flags:** calling it a regression without a baseline. Both test runs also *guessed* a cause (one said a shared dependency) because Datadog had no record of the real one. That is what P7 and P7b are for.

## P7. Which queries did you run? (follow-up to P6)

```text
Which exact queries, metrics and time windows did you use for that answer? Give me Datadog links so I can check the numbers myself.
```

- **Shows:** the most useful checking question there is.
- **A good answer:** a list of queries with links. In testing, both runs corrected themselves here: they had used sampled spans rather than the `trace.*` metrics, and one had mixed request and SQL spans in a service-wide percentile. Open one link and check one number in the UI.

## P7b. Tell it what Datadog can't see (follow-up to P7)

```text
Context you could not see in Datadog: at about 18:35-18:40 UTC we changed the IIS app pool configuration on every prod web host, which recycles the app pools. Does that change your conclusion?
```

- **Shows:** Claude revising its explanation when a human supplies the missing fact ([G12](gotchas.md#g12-claude-only-sees-what-datadog-records)).
- **A good answer:** a cold start after the recycle. It fits the timing, every host at once, and time spent inside the app before the first database call. Still not a code regression. Both test runs revised this way.

## P8. Draft the ticket

```text
Draft a Jira ticket (do not create it) for the slow GET /api/user/dashboard endpoint on prod resident-api: summary, evidence from traces and the profiler with Datadog links, and a suggested next step.
```

- **Shows:** turning an investigation into a ticket someone can act on.
- **A good answer:** a summary, the stored-procedure time, the gap, the reflection hotspot from the wall-time profile, links, and a next step. Both test runs added useful extra findings.
- **Red flags:** in one test run Claude used the **CPU Time** profile and wrote that our code "isn't the bottleneck". The wall-time run found the reflection hotspot. Always ask which profile type backed a claim before you file it ([G1](gotchas.md#g1-the-profiles-page-opens-on-cpu-time)).

## P9. Explain an error from its snapshot

```text
Find the most recent Exception Replay snapshot for a DbEntityValidationException in prod identity-api and explain what the captured variables tell us about why it failed. Tell me which tools you used, and give me the Error Tracking link.
```

In the session, swap in the exception type and service you showed in D5. On the cheat sheet: *Find the most recent Exception Replay snapshot for* exception type *in prod* service *and explain what the captured variables tell us about why it failed.*

- **Shows:** Claude reading Error Tracking and an Exception Replay snapshot, then reasoning from the captured values rather than only the stack trace.
- **A good answer:** the issue link; the failing field (`PORTAL_TYPE`, limited to one character) from `entityValidationErrors`; that Entity Framework rejected the save before SQL Server (`InnerException` null, validation on); and what the snapshot *can't* show (the rejected value, cut off by the capture depth). Both test runs also noticed the same bug split across two Error Tracking issues and suggested a logpoint to capture the missing value.
- **Red flags:** a cause stated as fact that the snapshot doesn't show. Ask "what in the snapshot shows that?" ([C1](checking-claudes-work.md)).

## P10. Add a logpoint in dev

```text
On management-api in dev, start a Live Debugger session and add a logpoint just before line 53 of FilesController.cs. Log ERES, IDENT and emailResourceKey, and only log when IDENT contains "dx-training". Don't pin it to a version. Tell me when it's ready for a test request.
```

- **Shows:** Claude doing what D6 did by hand: it checks the line, creates the session and a conditional logpoint, and says when to test.
- **A good answer:** a session link, the file and line, the message template, the condition, "not pinned", and "wait about 30 seconds". In both test runs it checked that line 53 matched the build running in dev, and warned that the condition would miss real, encrypted `IDENT` values. In both runs Datadog reported an error while creating the logpoint although it was created; Claude checked instead of retrying ([G20](gotchas.md#g20-a-logpoint-can-be-created-even-when-datadog-reports-an-error)).
- **This one changes something.** It is the only demo prompt that isn't read-only. Keep it to dev, always with a condition, and always finish with P10b. Prod services are in Strict redaction and refuse logpoints anyway ([G16](gotchas.md#g16-production-blocks-logpoints)).
- **Red flags:** Claude offering to change the service's Live Debugger settings (for example `enable_live_debugger`). Say no; that's a settings change, not a logpoint ([G15](gotchas.md#g15-live-debugger-needs-a-setting-on-the-service-itself)).

Then send the test request from Git Bash (it returns `500`, which is expected):

```bash
curl -s -o /dev/null -w "%{http_code}\n" "https://management-api.dev.ciranet.com/api/files/filelink?ERES=live-debugger-demo&IDENT=hello-dx-training"
```

## P10b. Read the capture, then clean up (follow-up to P10)

```text
I sent the test request. Show me what the logpoint captured, then disable the logpoint.
```

- **Shows:** Claude reading the snapshot and cleaning up after itself.
- **A good answer:** the logged message, a table of `ERES`, `SERES`, `IDENT`, `lid` and `emailResourceKey` with their values, and confirmation that the logpoint is disabled. Both test runs did this in about 30 seconds; the logpoints were confirmed disabled afterwards.
- **Red flags:** "nothing captured" within a minute of the request usually means the condition didn't match or the logpoint wasn't active yet ([G18](gotchas.md#g18-captures-appear-on-the-session-page-first)). Ask Claude to check the logpoint's status before you send another request.

---

## Tips

- **Name the environment.** Say "prod" (or qa, dev) in every prompt.
- **Name the time window.** "Last 24 hours" or an exact UTC time.
- **Ask for the evidence.** "Give me a Datadog link" and "which queries did you run" cost nothing.
- **Keep it read-only.** The Datadog MCP server can also *change* things (monitors, dashboards, notebooks, Live Debugger settings). For investigations, ask Claude to only read, and say no if it asks to create or update anything. The one exception is a conditional logpoint in dev, as in P10, and even then finish with P10b.
