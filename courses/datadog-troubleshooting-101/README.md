# Troubleshooting with Datadog 101: Slow Requests and Errors, with Claude + Datadog MCP 🔥

**Duration:** 1 session, about 90 minutes, plus questions
**Level:** Beginner (no Datadog experience needed)
**Format:** Presenter-led over screen share, with live demos in Datadog and Claude Code
**Goal:** Every developer can find out why a request is slow or why it failed, and can get Claude to do the advanced Datadog work.

---

## 📚 What this course is

In September 2026 we switched on Datadog's continuous profiler for every CiraNet .NET Framework API in dev, qa and prod. Most of the team uses Datadog's everyday features: traces, logs, dashboards. Few have used profiling or Datadog's newer debugging features, Exception Replay and Live Debugger, and fewer use Claude with the Datadog MCP server for the harder investigation work.

That combination is how we found the cause of the slow resident portal dashboard. The trace showed a 4-to-6-second gap after the SQL finished, and the profiler showed exactly which of our methods filled it. This course teaches that path for slow requests, then shows how Exception Replay and Live Debugger explain requests that fail. Each part ends with Claude doing the same investigation for you, and the last part shows how to check its answers.

## 🎯 What you'll be able to do

- ✅ Find an APM trace and read it: the Flame Graph, Waterfall and Span List views, the span tabs, and the Service page
- ✅ Find out why a request is slow: read a profile for your own service, flame graphs, Wall Time vs CPU Time, and the jump from one slow trace to its profile
- ✅ Find out why a request failed: the values Exception Replay captured, and a Live Debugger logpoint that logs values without a deploy
- ✅ Use Claude + Datadog MCP to investigate on your own: find slow endpoints, explain a slow request, name the method, explain an error from its snapshot, add a logpoint in dev, separate a regression from noise, draft a ticket
- ✅ Check Claude's work before acting on it

## 🎭 Who it's for

Developers, especially those who know Datadog's basics but haven't used profiling or its debugging features, and those who have never used Datadog at all. The session opens with a short APM tour and defines every term the first time it appears.

## 🚀 Before the session

**Everyone:** complete the [setup guide](SETUP-GUIDE.md) (about 10 minutes). It connects Claude Code to Datadog and ends with a one-minute check. Nobody's install time comes out of the session.

## 🗺️ The session

Each part shows a task by hand in Datadog, then Claude doing the same work, then a short break for questions.

| Minutes | Part | Demos |
| - | - | - |
| 0–4 | Welcome, goals, three words (service, span, trace) | |
| 4–28 | **APM orientation**, mostly live in Datadog: the Service page, endpoints (resources), slow traces, three trace views, the gap and the span tabs. Then what Claude can do with the Datadog MCP, and Claude splitting one slow request into SQL and our own code | D1 (browser), D2 (terminal) |
| 28–53 | **Why is it slow?** Tracing vs profiling, then live in Datadog on the same trace: its profile, reading a profile flame graph, and every dashboard request in Wall Time. Then Claude reading the profile and the code, and suggesting a fix | D3 (browser), D4 (terminal) |
| 53–79 | **Why did it fail?** Exception Replay on a real prod error, a Live Debugger logpoint in dev, then Claude explaining the error from its snapshot and adding a logpoint in dev | D5 (browser), D6 (browser + terminal), D7 (terminal) |
| 79–90 | **Checking Claude's work**: five habits and an "is this real?" investigation | D8 (terminal + browser) |
| 90–100 | Which tool for which question, gotchas, homework, questions, feedback | |

## 📁 Course structure

```text
datadog-troubleshooting-101/
├── README.md                      # This file
├── SETUP-GUIDE.md                 # Pre-course: connect Claude Code to Datadog
├── sessions/session-1/
│   ├── README.md                  # The lesson in text, with the worked examples
│   └── run-sheet.md               # Presenter: timing, demo scripts, recording, dry-run notes
├── resources/
│   ├── glossary.md                # Every term, in plain English
│   ├── click-paths.md             # Verified Datadog UI paths and deep links (K1–K7)
│   ├── demo-prompts.md            # Tested Claude prompts (P1–P10b)
│   ├── gotchas.md                 # Common mistakes (G1–G20)
│   ├── checking-claudes-work.md   # The five checking habits (C1–C5)
│   ├── cheat-sheet.md             # Handout (web view)
│   ├── cheat-sheet.tool.html      # Handout, one printable page
│   └── feedback-form.md           # Post-session form and success measure
├── slides/                        # deck.html (reveal.js, offline) and its source, deck-outline.md
├── assets/screenshots/            # Sanitized Datadog screenshots
├── scripts/                       # Screenshot sanitizer and its crop manifest
└── docs/                          # Maintainer records (not published)
```

## 🖥️ Running the slideshow

Open [`slides/deck.html`](slides/deck.html) in a browser and press **S** for speaker view: your notes, a timer and the next slide open in a second window that you keep off the shared screen. It runs offline. [`slides/README.md`](slides/README.md) has the keys, window switching and a PDF backup; the [run sheet](sessions/session-1/run-sheet.md) has the minute-by-minute plan.

The recording of the live session contains production data. It's stored internally in the DX team's Teams channel and is never committed to or linked from this repo.

## 🤝 Getting help

- **#ai-exchange** on Slack for setup problems or questions after the session
- The Datadog [profiler docs](https://docs.datadoghq.com/profiler/) and [MCP server setup](https://docs.datadoghq.com/bits_ai/mcp_server/setup/)

## 🎓 You're done when you can

- Open a slow trace and point to the time no span covers
- Get from that span to the method that filled it, in Wall Time
- Open an Error Tracking issue and read the values Exception Replay captured
- Ask Claude for the slowest endpoints of a service you own, or to explain one of its errors, and check which query it ran
