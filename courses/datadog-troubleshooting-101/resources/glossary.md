---
order: 10
---
# Glossary

> The words this course uses, in the order you meet them. If a slide or a Claude answer uses a term you don't know, it's probably here.

## Tracing (APM)

| Term | What it means |
| - | - |
| **Service** | One application that reports to Datadog, named by its `service` tag. `resident-api` and `management-api` are services. |
| **Resource** | The label on a span that says what kind of work it was: the endpoint for a request (`GET /api/user/dashboard`), or the query for a SQL call. Every request to the same endpoint makes a new span with the same resource, so many spans share one resource. |
| **Span** | One timed unit of work, done once: a request, a SQL call, an outbound HTTP call. It has a start, a duration and tags, including its service and resource. Each rectangle in a trace is one span. |
| **Trace** | All the spans for one request, across every service it touched, arranged parent to child. |
| **Operation** | The kind of span, such as `aspnet.request` or `aspnet_webapi.request`. One ASP.NET request produces both, which is why endpoints can appear twice. |
| **Env** | Which environment a span came from (`prod`, `qa`, `dev`). Always check it before trusting a number. |
| **Indexed span** | A span Datadog kept after the live 15-minute window, because a retention filter chose it. Searches older than 15 minutes only see indexed spans. |
| **Retention filter** | The rule that decides which spans are kept. It keeps a sample, not everything, so span counts are not request counts. |
| **Exec time** | Time a span spent itself, not waiting on its children. In a trace's Span List, a service's exec time is the time spent in its own code. |
| **Gap** | Time inside a span that no child span covers. It is time in our own code, which tracing can't see and profiling can. |
| **`trace.*` metrics** | Request, error and latency metrics computed from every request before sampling. Use these, not span counts, for rates and percentiles. |

## Profiling

| Term | What it means |
| - | - |
| **Continuous profiler** | An agent inside the process that samples what every thread is doing, all the time, with low overhead. |
| **Profile** | The collected samples for a service over a time window. |
| **Stack trace** | The chain of method calls a thread was in when sampled, from the entry point down to the method running right then. |
| **Flame graph** | Stack traces merged into one picture. Width is time, callers are above callees. |
| **Self time** | Time a method spent running its own code, not in methods it called. |
| **CPU Time** | Time threads spent actually running on a CPU. Waiting doesn't count. |
| **Wall Time** | Elapsed clock time, running or waiting. Includes idle threads, so filter to your code. |
| **Profile type** | What a profile measures. For our .NET services: CPU, Wall Time, Exceptions, Lock Contention and GC. Allocation profiling is off. |
| **Endpoint** | In the profiler, the request route a sample belongs to, so you can see one endpoint's code. |

## Debugging (Part 3)

| Term | What it means |
| - | - |
| **Error Tracking issue** | One group of the same exception, such as every `DbEntityValidationException` thrown from the same place. Its page shows how often it happens and sample stack traces. |
| **Exception Replay** | A Datadog feature that saves the arguments and local variables of each frame when an exception happens again, and shows them in the Error Tracking issue. |
| **Snapshot** | The saved values from one moment in running code: arguments, local variables and object fields. Exception Replay and Live Debugger both produce snapshots. |
| **Live Debugger** | A Datadog feature that logs variable values from a chosen line of running code, without a code change, deploy or restart. Also called Dynamic Instrumentation. |
| **Debug session** | A Live Debugger container for logpoints. It stops by itself after about 60 minutes. |
| **Logpoint** | A file and line in a debug session. Just before that line runs, it writes a log message and can capture a snapshot. |
| **Condition** | An expression on a logpoint, such as `contains(IDENT, "dx-training")`, so it only logs the requests you care about. |
| **Redaction mode** | How Datadog hides sensitive values in snapshots. **Targeted** hides values whose names look sensitive (passwords, tokens, user IDs) and shows them as `[REDACTED]`. **Strict** is tighter and blocks Live Debugger logpoints. It's set per service and environment; most of our prod services use Strict. |

## Everything else

| Term | What it means |
| - | - |
| **p50 / p95** | The latency half of requests beat / the latency 95% of requests beat. p95 shows the slow tail. |
| **Watchdog** | Datadog's automatic anomaly detection. It raises "stories" when a metric moves unusually. Not every story is a regression. |
| **MCP** | Model Context Protocol, the way Claude Code connects to outside tools. The Datadog MCP server lets Claude query our Datadog account. |
| **Toolset** | A group of Datadog MCP tools. Only `core` is on by default; the course needs `?toolsets=all`. |
