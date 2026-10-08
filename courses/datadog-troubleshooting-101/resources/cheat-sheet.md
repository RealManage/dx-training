---
order: 60
---
# Cheat Sheet

> **Printing?** Use the [one-page printable version](cheat-sheet.tool.html) (fits one Letter page).

Items carry the IDs of their full versions in [prompts](demo-prompts.md) (P), [click paths](click-paths.md) (K), [gotchas](gotchas.md) (G) and [checking Claude](checking-claudes-work.md) (C). Edit those pages first.

## Prompts for Claude Code (Datadog MCP)

| | Ask |
| - | - |
| P1 | List the services that have continuous profiling data in the last hour. |
| P2 | Which prod endpoints in *service* have the worst p95 over the last 24 hours? Use the trace metrics, not span counts, and give me a link. |
| P3 | Find one slow *endpoint* request on prod *service* from the last hour and explain where the time went, including any gap the spans don't cover. |
| P4 | Pull the wall-time profile for *endpoint* on prod *service* over the last 24 hours. Which of our own methods take the most time? Include the link. |
| P5 | Is *endpoint* slow because of our C# or the database? Check traces and the profiler; tell me which queries you ran. |
| P6 | We got a latency alert on prod *service* around *time UTC*. Real regression or noise? Compare against the same time the day before. |
| P7 | Which exact queries, metrics and time windows did you use? Give me Datadog links. |
| P8 | Draft a Jira ticket (do not create it) for *endpoint*: summary, trace and profiler evidence with links, next step. |
| P9 | Find the most recent Exception Replay snapshot for *exception type* in prod *service* and explain what the captured variables tell us about why it failed. |
| P10 | **Dev only.** Start a Live Debugger session on *service* in dev and add a logpoint just before line *N* of *file*, logging *variables*, only when *condition*. Don't pin a version. Then: *show me what it captured, then disable it.* |

## Click paths (us5.datadoghq.com)

| | Where |
| - | - |
| K1 | **APM → Traces**: `service:… resource_name:"…" env:prod @duration:>5s`. Older than 15 min = indexed spans only. |
| K2 | Open a trace: **Flame Graph · Waterfall** (expand all) **· Span List** (group by Service, read Exec Time). |
| K3 | Click a span → tabs: Overview · Infrastructure · Metrics · Logs · **SQL Queries** · **Profiles**. |
| K4 | **APM → Service → *service***: check **operation**, **env**, **version** first. |
| K5 | **APM → Profiles → Explorer**: **Wall Time** → endpoint on the right → **Filter flame graph** → `method:"Class.Method()"` → focus_on. |
| K6 | **Errors → Error Tracking** → issue → **Stack Trace**: look for **Exception Replay snapshot**. Find issues with one: Logs (Flex) `source:dd_debugger @debugger.product:er`. |
| K7 | **APM → Debugger → New Session** → **Start debugging manually** → service, env → file → click the gutter → template, **when** → **Start Debug Session**. |

## Gotchas

- **G1** Profiles opens on CPU Time; use **Wall Time** for requests that wait.
- **G2** Wall Time counts idle threads; filter to an endpoint and our code.
- **G3** The Method list ranks self time; to find a caller, use **Filter flame graph**.
- **G4** Async work shows under the awaited call; read down to our widest frame.
- **G5** Frames show line 0 until DX-1570.
- **G6** No profiles before 2026-09-22.
- **G7** Thin or empty profile? Widen the time range.
- **G8** Span counts are sampled; use `trace.*` metrics for rates and percentiles.
- **G9** "0 results" may be a silently ignored filter; group instead, or check the UI.
- **G10** Check the operation and env selectors before comparing numbers.
- **G11** The MCP URL needs `?toolsets=all`; restart Claude Code after changing it.
- **G12** Claude only sees what Datadog records; tell it what changed.
- **G13** Exception Replay starts on the 2nd occurrence; at most 1 snapshot per type, server and hour.
- **G14** Some exceptions never get a snapshot ("internal error").
- **G15** Live Debugger needs a setting on the service: management-api in dev only, for now.
- **G16** Strict redaction (most prod services) blocks logpoints.
- **G17** Logpoints on `await` lines never fire.
- **G18** Watch the session page; Logs lag about 3 minutes.
- **G19** Leave **Target specific instances** empty while testing.
- **G20** Logpoint create "failed"? List the session before retrying.

## Check Claude's work

1. **C1** Ask which query it ran, and for a link.
2. **C2** Open the link; check service, env, time range, profile type, and one number.
3. **C3** Distrust "0 results" and rates from span counts.
4. **C4** Compare with the same time yesterday before calling it a regression.
5. **C5** Tell it what Datadog can't see: recycles, config changes, manual work.
