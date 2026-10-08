# Troubleshooting with Datadog 101 — Deck Outline

> **How to use this.** This outline is the source of truth for the deck. Edit here first, then update the deck. Each slide has four parts:
> **Message**, the one idea the slide carries. **Visual**, what's on screen. **On-slide text**, the few words the audience reads. **Say**, the presenter's talking track: it goes into the speaker notes, **never on the slide**.
>
> If a slide needs more than one sentence of on-slide text, it's doing too much. Push it into *Say* or split it.
>
> **How Say is written:** plain, conversational English, the way you'd explain something to teammates, many of whom speak English as a second language. Use full sentences and common words. It should sound like something the presenter would actually say out loud. Don't open with short dramatic fragments ("Four words."), don't use metaphors or give software human traits ("people live in APM", "tracing sees a blank"), and don't use ALL CAPS for emphasis. Avoid phrasing that sounds machine-written: "it's not X, it's Y" contrasts, "here's the thing", "the key is", "that's where X comes in", neat groups of three, a question you answer yourself straight away, and colons or dashes used for effect. Stage directions go in parentheses. Split the Say text into short paragraphs, one idea each (indent continuation paragraphs two spaces under the bullet); each becomes its own paragraph in the speaker notes.
>
> **Timing:** about 90 minutes, plus about 10 minutes of questions (two minutes at the end of parts 1 to 3, and five at the end), in a two-hour slot. 31 slides plus 4 backup slides, 4 part dividers and 8 live demos (**D1–D8**). Part 1 is taught mostly live in Datadog. Each part shows a task by hand in Datadog first, then a demo of Claude doing the same work. The minute budget per part is in each part heading, and per demo in the demo slides. The [run sheet](../sessions/session-1/run-sheet.md) has the clock times, window switches and fallbacks; the [demo prompts](../resources/demo-prompts.md) have the exact text to paste.
>
> **IDs:** P = prompt, G = gotcha, K = click path, C = check-Claude step. The slides point at them rather than repeating them.

---

## Slide 1 — Title  ·  0:00

- **Message:** Today you learn to use Datadog, with Claude, to troubleshoot a slow request and an error.
- **Visual:** Course title on a plain background, with the RealManage logo.
- **On-slide text:** "Troubleshooting with Datadog 101" · "Slow requests and errors, with Claude + Datadog MCP" · "RealManage · DX Training"
- **Say:** Hey everyone, thanks for joining. Today I want to show you how to use Datadog, along with Claude, to troubleshoot a slow request and an error.

  This session will be recorded, so you can watch it again later.

  Before we start: if the setup guide worked for you, could you send a thumbs up reaction? (Comment on the number of thumbs up reactions.) If it didn't work, that's OK. Just follow along for now. Reach out to me after the session and I can help you.

## Slide 2 — What you'll leave with  ·  0:01

- **Message:** Four skills: read a trace, find out why a request is slow, find out why it failed, and get Claude to do the research. Claude shows up in every part.
- **Visual:** Four tall numbered cards across the slide. No agenda or minutes: the timings live in the run sheet.
- **On-slide text:** "1 · Find and understand a trace" · "2 · Why is it slow?" · "3 · Why did it fail?" · "4 · Investigate with Claude"
- **Say:** By the end of this session, you should be able to do four things.

  First, find a trace in Datadog and understand it. We'll start with a short refresher on Datadog APM.

  Second, find out why a request was slow. For that we will use the profiler, which is new for most of us.

  Third, find out why a request failed. For that we will use two newer Datadog features, Exception Replay and Live Debugger.

  And fourth, ask Claude to do this research for you with the Datadog MCP server. I think this one is the most important. You don't need to be an expert with Datadog's advanced features to use them, because Claude writes the queries for you. You just need to know what to ask.

  So in each of the first three parts, I'll first show you how to do it manually in Datadog and then I'll ask Claude to do the same thing for us. In the last part, we'll look at ways to check Claude's results because sometimes they can be wrong.

  I'll stop at the end of the first three parts in case anyone has any questions. But feel free to interrupt me at any time. And just a disclaimer: I'm definitely not a Datadog expert myself, but Datadog has been extremely useful to me and I learn something new about it every day.

## Slide 3 — Three words  ·  0:03

- **Message:** Service, span and trace are the words you need for the first part.
- **Visual:** A simplified trace drawn the way Datadog's trace flame graph draws it, based on the real dashboard trace: time left to right, one row per level of calls. Row 1: resident-ui (the browser). Row 2: resident-api, GET /api/user/dashboard. Row 3: three SQL spans from rm_accumulator, side by side, the first showing the stored procedure's name. A gold border around the whole diagram marks the trace. The diagram fills the slide's width; the two small SQL spans are wide enough to read as separate spans on the same row. Below it, a key in one row: service = the color, span = one rectangle, trace = the gold border. (Resource is introduced live on the Service page in D1. The real trace also has a second resident-api row, the two operations, which D1 explains.)
- **On-slide text:** "Service · Span · Trace"
- **Say:** There are three words we'll use a lot today.

  A service is one application that sends data to Datadog, for example resident-api. On this slide, the color of each bar represents the service it belongs to.

  A span is one piece of work, with a start time and a duration, for example one request, one SQL call, or one call to another API. Each colored rectangle on this slide represents one span.

  A trace is all the spans for one request, across every service it went through. On the slide, it's everything inside the gold border: the browser, the API and the database.

  Time goes from left to right, and each row is called by the row above it. The orange bar at the top is the resident-ui service, which runs in the browser. It calls resident-api, and resident-api calls the database.

  I'll be using this user dashboard request as the example for most of today. I picked it because it's easy to understand and shows the problem clearly, not to pick on the resident portal. You'll actually find the same thing in most, if not all, of our services.

  The main thing to remember is that a trace is made of spans.

---

## Part 1 — APM orientation  ·  24 minutes with questions (0:04–0:28)

## Slide 4 — Divider: Part 1

- **Message:** APM is the part of Datadog that collects traces. This part is mostly a live tour of it.
- **Visual:** Divider: solid navy, part number and title.
- **On-slide text:** "Part 1 of 4" · "Finding your way around APM"
- **Say:** In part one we'll look around Datadog APM. APM stands for Application Performance Monitoring. It's the part of Datadog that collects traces from our services.

  We won't cover every feature. The goal is for you to get familiar with the common features and know how to ask Claude to do the grunt work for you. Most of this part is a live demo, so you'll see the real pages.

  At the end of this part, we'll ask Claude to find a slow request and help us understand it.

## Slide 5 — DEMO D1: a slow request, end to end  ·  13 minutes (0:05–0:18)

- **Message:** Watch the whole path live, starting from any Datadog page: APM, the Service page, the endpoint, its slow requests, one trace in three views, the gap, and the span tabs. Why it was slow comes in part two.
- **Visual:** Demo card: "D1" and "13 min" tags, "Switch to the browser · Datadog", seven numbered steps, and the search string. Step 1 carries an "Open Datadog ↗" link to the Dashboards list, so the demo starts where anyone would and navigates to APM by hand. Step 4 carries a "Traces search ↗" link (K1) that lands on the filtered result, as a shortcut if the Endpoints path misbehaves.
- **On-slide text:** "Demo · D1 · 13 min" · "1. Open Datadog → APM → search resident-api [Open Datadog ↗]" · "2. Service page: set env to prod, check the operation" · "3. Endpoints: search /api/user/dashboard, then Traces → Trace Explorer" · "4. Historical vs Live Search, then add @duration:>8s [Traces search ↗]" · "5. Open a slow one: Flame Graph → Waterfall → Span List" · "6. Find the gap" · "7. Click the resident-api span: look at each tab, stop at Profiles" · `service:resident-api resource_name:"GET /api/user/dashboard" env:prod @duration:>8s`
- **Say:** In this demo, I'll start from the main Datadog page and go to APM. From there we'll open the Service page for resident-api, find the slow requests for the user dashboard, open one of them, and look at its trace.

  (Click Open Datadog on the slide. It opens the Dashboards list.) You can get to APM from any page in Datadog. (Hover APM in the left menu.) If you hover over APM, you get a menu with its main pages. (Click APM.) If you click it, you land on APM Home. (Scroll down to the Service List, type resident-api in Search services, and click it.)

  This is the Service page for resident-api. It's like a home page for one service. At the top you can see how many requests it handles, how many errors it has, and how fast it is. These charts count every request, because they're built from trace metrics.

  Before you trust any number on this page, check the selectors at the top. (Point at env.) This one is the environment. Right now it says star, which means all environments together. (Open the env list.) You can see how many requests each environment gets. (Choose prod.) We want prod. (Point at operation.) This one is the operation. Our ASP.NET endpoints are measured twice, once by ASP.NET and once by Web API, so each request has two spans with different operation names: aspnet.request and aspnet_webapi.request.

  This isn't a bug. ASP.NET measures the whole request, and Web API measures the part inside our controller, so the outer span is always a little longer. On .NET Core, Datadog has a setting that records only one span per request. That setting doesn't exist for our .NET Framework APIs, so on those you'll always see two.

  (Click Endpoints on the left.) This is the list of endpoints for resident-api. Datadog calls each of these a resource. A resource is a name, like GET /api/user/dashboard. Every time someone opens the user dashboard, Datadog records a new span, and all of those spans have that same resource name.

  (Type /api/user/dashboard in Search Endpoints.) Here's the user dashboard endpoint. It's listed once, for the operation we picked at the top.

  (Click the GET row. A panel opens with everything about this endpoint. Scroll down the panel to the Traces section, and click Trace Explorer on its right.) Trace Explorer opens the trace search for this endpoint, with the environment and operation already in the search, so we get one row per request. You can also get here from APM, then Traces.

  (Point at Historical Search, to the left of the time range.) Look here, next to the time range. It says Historical Search. That means we're searching indexed spans. Datadog keeps only a sample of our spans long term, chosen by retention filters, so the number of rows is not the number of requests. (Set the time range to Past 15 Minutes, and point at the number of indexed spans above the list.) For the last fifteen minutes, we only have a handful.

  (Open the time picker again and choose LIVE 15 Minutes, at the top of the list.) Now it says Live Search, with a green Live label. Live Search looks at every span we sent, but only for the last fifteen minutes. (Point at the Ok count under Status, on the left.) It's the same fifteen minutes, and now we see every request. (Set the time range to Past 1 Day.) For older data, we're back to Historical Search. That's fine for finding slow requests, as long as you remember it's only a sample.

  (In the search bar, add @duration:>8s and press Enter.) I'll add a duration filter, so we only see requests that took more than eight seconds. Every row here is now one slow user dashboard request.

  (Switch Spans to Traces, then back.) If I switch from Spans to Traces, I get one row per request instead of one row per span. Here it looks the same, because our search already picks one operation. Without that, the Spans view would show each request twice.

  (Open a slow one that looks like our example: 6 or more spans, and a Latency Breakdown bar that is about half green, with the rest orange and purple. If none is in range, use the trace in browser tab 2.) Let's open one of the slow ones. This one took about … seconds. (Read the number off the screen.)

  (Flame Graph tab.) This is the flame graph. It looks similar to the diagram on the vocabulary slide from earlier. At the top, in orange, is the resident-ui span, which runs in the browser. Under it, in green, are the two resident-api spans, the two operations. Under those, in purple, are the database spans: a stored procedure and two short queries.

  (Hover the stored procedure.) The stored procedure took about … seconds.

  (Point at the empty space after the SQL calls.) And here's the gap. After the SQL calls finish, the resident-api span keeps running for a few more seconds, with nothing under it. So what was our code doing during that time? The trace itself can't tell us, but we'll soon look at a feature that can.

  (Waterfall tab, click expand all.) The waterfall shows the same spans as a list on a timeline. Click expand all, so you see every child span.

  (Span List tab, group by service.) The span list adds up the time by service. The Exec Time column shows how long each service spent in its own code, without the calls it made. For resident-api it's about … seconds, and most of that is the gap.

  (Click the resident-api span.) When I click a span, I get these tabs. (Click Overview.) Overview shows the span's attributes, like environment, version, status code and URL. (Click Infrastructure.) Infrastructure shows the host that handled the request. (Click Metrics.) Metrics shows the .NET runtime metrics for that process, like CPU and memory. (Click Logs.) Logs shows the logs for this trace, if the service sends them to Datadog. (Click SQL Queries.) SQL Queries lists every database call in the request, slowest first. Here's our stored procedure at the top.

  (Point at the Profiles tab, but don't open it.) The last tab, Profiles, can tell us what our code was doing during the gap. We'll open it in part two.

  (If someone asks why the resident-ui bar is longer than the API: that's the browser's own timing for the whole page, which we won't cover today.)

## Slide 6 — What we just saw: the gap  ·  0:18

- **Message:** Recap of the demo in one picture: after the SQL, the API span keeps running with nothing under it, and that time is our own code.
- **Visual:** `trace-flame-graph-gap.png` with a box over the empty stretch under the resident-api spans, labeled "≈ 6 s, nothing under the API span". Caption underneath.
- **On-slide text:** "What we just saw" · "≈ 6 s, nothing under the API span" · "Tracing records the calls out of our code. When our own code is busy, tracing has no data for that time."
- **Say:** Here's what we just saw, in one picture. This is a real request from production. The stored procedure took about five seconds. Then the resident-api span kept running for about six more seconds, with nothing under it.

  Tracing only records calls from our code to other systems, like SQL, HTTP calls or the cache. When our own code is busy, for example running a loop, using reflection or building objects, tracing has no data for that time.

  So from the trace we know the request was slow, and we know the database was not the only reason. But we don't know which method used that time. We'll answer that in part two.

## Slide 7 — What Claude can do with the Datadog MCP  ·  0:19

- **Message:** With the MCP server, Claude runs the Datadog queries for you, from everyday questions in plain English.
- **Visual:** A small header, "Some of what Claude can query with the Datadog MCP", over a row of five tool families: Spans (traces and requests), Metrics (request counts and latency), Profiles (flame graphs), Errors (Error Tracking and snapshots), Logpoints (Live Debugger, in dev). Below, six everyday example prompts in a 2x3 grid, each with a short label. A small "Setup guide ↗" link at the bottom right.
- **On-slide text:** "Some of what Claude can query with the Datadog MCP" · "Spans · Metrics · Profiles · Errors · Logpoints" · "Check a fix after a release: Since today's release of resident-api, have errors on GET /api/user/dashboard gone down?" · "A user reports a slow page: A user says the dashboard in resident-api was slow around 2:30 pm today. Find a slow request from then and tell me where the time went." · "Before removing an endpoint: Is anything still calling GET /api/account/accessmedia/waiver in resident-api? Show me the last 30 days of traffic." · "Explain a trace from a bug report: Here's a trace link from a bug ticket: [link]. Explain in plain English what happened in this request." · "Pick a maintenance window: When is resident-api least busy during the week? I need a time for a maintenance window." · "After a Watchdog alert: I just got a Watchdog alert for resident-api. What triggered it, and what caused the problem?" · "Setup guide ↗"
- **Say:** Now let's see how Claude can help with this. Before today, you were asked to set up the Datadog MCP server in Claude Code. MCP is a standard way to give Claude extra tools, and this server gives it tools to query our Datadog account. It signs in with your own Datadog login, so Claude can only see what you can see.

  The row at the top shows some of what Claude can query: spans, metrics, profiles, errors, and Live Debugger logpoints. We'll see each of these during the session.

  You ask your question in normal English, and Claude decides which queries to run. So even if you only know the basics of Datadog, you can still get a detailed answer.

  The bottom of the slide has a few questions you could ask in your normal work. For example, if you fixed a bug and the fix went out in today's release, you can ask Claude whether that error has stopped. Before you remove an old endpoint, you can ask whether anything still calls it. And if you get a Watchdog alert, you can ask Claude what triggered it.

## Slide 8 — DEMO D2: Claude helps analyze the slow request  ·  5 minutes (0:21–0:26)

- **Message:** Hand Claude the trace we just read by hand, ask the same question in one short prompt, and compare its answer with what we saw.
- **Visual:** Demo card: "D2" and "5 min" tags, "Switch to the terminal · Claude Code", three short steps, and the prompt text in the code box so the audience can read it, with a dimmed "[paste the trace link]" line under it.
- **On-slide text:** "Demo · D2 · 5 min" · "1. Paste the prompt below into Claude Code" · "2. From the D1 tab, copy the trace link and paste it after the prompt" · "3. Compare its numbers with what we found by hand" · the prompt · "[paste the trace link]"
- **Say:** Let's ask Claude about the same request we just looked at. I'll give it the link to the trace, and ask it to split the time the same way we did by hand. You can read the prompt on the slide.

  (Click the prompt below once to select it, copy it, and paste it into Claude Code. Don't press Enter yet. Go to the D1 browser tab, where the slow trace is still open, and copy the URL from the address bar. Back in the terminal, paste it after the prompt, and press Enter.)

  `For the trace below, from prod resident-api, answer in three short bullet points, in plain English: how long did the request take, how much of that was SQL, and how much was our own code?`

  (While Claude works, which takes about half a minute, say which tools it calls. It reads the trace from the link and adds up the time in its spans.)

  (When it answers, compare it with what we found: the total time, the time in the stored procedure, and the time in our own code. It's the same request, so the numbers should match what we saw in D1.)

  (If Claude can't open the link, paste only the long number after traceID= in the link.)

  (End of part one. Ask if anyone has a question, and take one or two. Keep it to about two minutes.)

---

## Part 2 — Why is it slow? Profiling  ·  25 minutes with questions (0:28–0:53)

## Slide 9 — Divider: Part 2

- **Message:** Profiling shows what our code was doing during the gap.
- **Visual:** Divider: solid navy, part number and title.
- **On-slide text:** "Part 2 of 4" · "Why is it slow?"
- **Say:** Part two is about slow requests. We'll answer the question we had from the trace: what was our code doing during the gap?

## Slide 10 — Tracing vs profiling  ·  0:29

- **Message:** Tracing shows that a request was slow and what it called. Profiling shows what our code was doing.
- **Visual:** Two rows over the same timeline. Top: trace spans with the empty gap. Bottom: profiler samples as small ticks filling the whole timeline, dense inside the gap.
- **On-slide text:** "Tracing: *that* it was slow, and what it called" · "Profiling: *what our code was doing*"
- **Say:** Tracing records the calls our code makes to other systems, and how long they take. Profiling works in a different way. Many times per second, the profiler checks every thread and records its stack. The stack is the method that is running, and the methods that called it.

  The profiler does this all day, for every request, and it only costs a few percent of CPU. When we turned it on, CPU on the web servers went up by about two and a half percentage points.

  So where the trace has no spans, the profile still has samples. We turned on the profiler for all CiraNet .NET Framework APIs in dev, qa and prod on September 22nd, so traces from that day onward can have profile data.

## Slide 11 — Reading a flame graph  ·  0:30

- **Message:** Width is time, read top-down, and follow our namespaces to the widest leaf.
- **Visual:** A simplified flame graph (inline diagram) with framework frames grayed and `CiraNet` frames in color, the widest `CiraNet` leaf circled.
- **On-slide text:** "Width = time" · "Top calls bottom" · "Follow CiraNet… to the widest leaf"
- **Say:** In the profiler, a flame graph combines thousands of these stack samples into one picture. I'll give you three rules for reading it.

  First, width means time. A wider box used more time, and the order from left to right doesn't mean anything. This is different from the flame graph in a trace. In a trace, left to right is time. In a profile, only the width matters.

  Second, read it from top to bottom. The methods at the top call the methods below them.

  Third, look for our own code. Skip the framework methods, find the methods in our namespaces, the ones that start with CiraNet, and follow them down to the widest one. That's usually where the problem is.

## Slide 12 — CPU Time vs Wall Time  ·  0:32

- **Message:** The Profiles page opens on CPU Time. For slow requests that wait, switch to Wall Time.
- **Visual:** `profiles-cpu-default.png` with the CPU Time card and the Wall Time card circled; the per-endpoint list showing the dashboard low on CPU.
- **On-slide text:** "CPU Time = running" · "Wall Time = running + waiting" · "Slow request? Wall Time (G1)"
- **Say:** Our services collect a few profile types: CPU, wall time, exceptions, lock contention and garbage collection. Today we need two of them. CPU Time only counts the time a thread was actually running on the CPU. Wall Time counts all the time that passed, running or waiting.

  The Profiles page opens with CPU Time selected, and that can be misleading. Look at the dashboard endpoint in this list. It only uses about two tenths of a second of CPU per minute, so it looks unimportant. That's because most dashboard requests are fast, and the slow ones spend a lot of their time waiting for SQL. That's why we switch to Wall Time for slow requests.

  But wall time also counts threads that are just sleeping, so you should filter it to your endpoint and your own code. Also, a time range with little traffic gives a thin profile. If the graph looks empty, make the time range bigger.

## Slide 13 — From one slow trace to its profile  ·  0:34

- **Message:** The span's Profiles tab shows exactly which method filled the gap in that one request.
- **Visual:** `span-profiles-flame-graph.png`: the thread timeline busy after 5 s, and the tooltip "UserBP.GetUserDashboardItems() CPU Time 5.05 s".
- **On-slide text:** "Span → Profiles" · "The gap = GetUserDashboardItems, 5.05 s"
- **Say:** Let's go back to our trace. Click the resident-api span, and then the Profiles tab. Look at the thread timeline. While SQL is running, the thread is not doing anything. Then, from about five seconds until the end, it's busy the whole time. That's the gap from the trace.

  The flame graph below shows which method it was: UserBP.GetUserDashboardItems, with about five seconds of CPU time.

  There are two things to notice here. First, it appears under the user repository and a caching method. This happens because the code is async. Code that runs after an await is shown under the method it waited for. The repository is not slow, so keep reading down to our widest method.

  Second, every frame shows line zero. We need the symbol files to get line numbers, and that work is tracked in DX-1570. For now, use the method name to find the code.

## Slide 14 — The answer: filter the flame graph  ·  0:36

- **Message:** In the Profiles explorer, Wall Time plus a flame graph filter names the exact code: a cross join with reflection on every pair.
- **Visual:** `profiles-wall-filtered.png` with the filter box circled and `EnumExtensions.GetEnumFromDescription` highlighted.
- **On-slide text:** "Filter flame graph → focus_on(method:\"UserBP.GetUserDashboardItems()\")" · "Reflection per pair (G3)"
- **Say:** To look at all requests, not just this one, click Open in Profiling. Choose the resident-api service, Wall Time, and the dashboard endpoint.

  You might try to search for GetUserDashboardItems in the method list on the right, but it isn't there. That list sorts methods by their own time, and this method spends most of its time calling other methods. So use the Filter flame graph box instead. Pick the method and choose focus on.

  Now we can read the stack. GetUserDashboardItems combines every activity feed item with every property, which is a cross join. For each pair, it calls GetEnumFromDescription, and that method calls GetCustomAttribute, which uses reflection, every single time. Then DashboardHelper.GetActivities does the same lookup again for each item.

  In total, about a third of the endpoint's time is in that one helper method. But across the whole service it's less than one percent, so you would not find it by looking at the service as a whole.

## Slide 15 — DEMO D3: trace to profile  ·  6 minutes (0:38–0:44)

- **Message:** Watch the jump from one slow trace to the profile, then the filter.
- **Visual:** Demo card. "D3", "6 min", browser icon.
- **On-slide text:** "Demo · D3 · 6 min" · "Span → Profiles → Open in Profiling → Wall Time → Filter"
- **Say:** (Switch to the browser and follow D3 in the run sheet.) Let me show you this live. (Open the Profiles tab on the span and show the timeline. Then open it in Profiling, switch to Wall Time and add the filter. If the filtered graph is empty, change the time range to one day and explain that there weren't enough samples.)

## Slide 16 — DEMO D4: Claude reads the profile  ·  6 minutes (0:44–0:50)

- **Message:** Watch Claude pull the wall-time profile and name our methods, then look at a different endpoint where the cause is the database.
- **Visual:** Demo card. Terminal icon. "P4 which of our methods?" · "P5 our C# or the database?".
- **On-slide text:** "Demo · D4 · 6 min" · "P4 → P5"
- **Say:** (Switch to the terminal and follow D4 in the run sheet. Paste P4, then P5.) Now let's ask Claude for the profile. (When P4 finishes, say that we asked for Wall Time on purpose, and check that it really used it. Point out that it found the same reflection problem we found with the filter. Then paste P5, which asks about the workflow search endpoint in management-api. When it finishes, explain the difference: here most of the profile is Entity Framework waiting for rows from SQL Server. That's also a useful result, because it tells us that making the C# faster won't help, and we should look at the query instead.)

## Slide 17 — The contrast: when it's the database  ·  0:50

- **Message:** A profile with little of our code in it means the fix is in the database rather than the C#.
- **Visual:** Two mini flame graphs side by side. Left (dashboard): wide `CiraNet` frames. Right (`/api/wf/search`): wide `SqlDataReader.ReadAsync` / Entity Framework frames and a thin sliver of ours.
- **On-slide text:** "Our code is wide → fix the code" · "Waiting is wide → fix the query"
- **Say:** (If you skipped P5, first say that this is the workflow search endpoint in management-api.) Here are the two profiles next to each other. For the dashboard, our own methods take most of the width, so the fix is in our code.

  For the workflow search, our methods are only a thin part. Most of the width is Entity Framework and the SQL reader waiting for data. The workflow search's stored procedure, api_mp_SearchWorkflows, takes most of the time in every slow request. So the same tool can point you to two very different fixes.

  (End of part two. Ask for questions and take one or two.)

---

## Part 3 — Why did it fail? Exception Replay and Live Debugger  ·  26 minutes with questions (0:53–1:19)

## Slide 18 — Divider: Part 3

- **Message:** Two newer features that show the values of variables in running code.
- **Visual:** Divider: solid navy, part number and title.
- **On-slide text:** "Part 3 of 4" · "Why did it fail?"
- **Say:** So far we've looked at time, and where a slow request spends it. Part three is about requests that fail.

  I'll show you two features that show the values of variables in running code. The first is Exception Replay, and the second is Live Debugger. Both are fairly new in Datadog and they have some limits, which I'll point out as we go. At the end of this part, you'll see Claude use both of them.

## Slide 19 — Exception Replay: the values when it failed  ·  0:54

- **Message:** When an exception happens again, Datadog saves the arguments and local variables of each frame in the stack trace, and shows them in Error Tracking.
- **Visual:** Three-step diagram (made-up values): first occurrence (tracked, no snapshot) → second occurrence on the same server (snapshot taken) → Error Tracking issue with values per frame, one shown as `[REDACTED]`. Limit line underneath.
- **On-slide text:** "2nd time it happens → snapshot of every frame's values" · "Error Tracking → issue → Stack Trace" · "1 per exception type, per server, per hour (G13)"
- **Say:** Exception Replay works together with Error Tracking. Error Tracking is the Datadog page that groups the same exception into one issue. Normally, when an exception happens, Error Tracking shows the stack trace, which is the list of methods that were running. With Exception Replay, Datadog also saves the values of the arguments and local variables in each method of that stack trace. This is called a snapshot.

  It's already on for our services through Datadog's remote settings, and we get snapshots from production today, so you don't need to set anything up.

  There are some limits. Datadog doesn't take a snapshot the first time an exception happens. It starts on the second time, on the same server. After that, it takes at most one snapshot for each exception type, on each server, every hour.

  Values with sensitive names, like passwords or tokens, are hidden, and you see the word redacted instead. It also doesn't work for every exception. Sometimes the issue only shows a message saying the data could not be captured.

## Slide 20 — What a real snapshot looks like  ·  0:56

- **Message:** A real production snapshot: the frame, the values under it, and the redaction mode.
- **Visual:** `exception-replay-snapshot.png`: an identity-api `DbEntityValidationException`, with the snapshot panel under the `PortalUserDBEntities.SaveChangesAsync` frame and the **Redaction: Targeted** badge circled.
- **On-slide text:** "Frame → Exception Replay snapshot (on exit)" · "Each line: a variable's value when the exception left the method"
- **Say:** This is what a real snapshot looks like. It's from identity-api in production, and it's a validation error, so there's no customer data in it.

  Under the method in the stack trace, you see the snapshot. Each line is a variable and its value at the moment the exception left this method. For example, entityValidationErrors holds the validation error the code collected, and newExceptionMessage is the message it built from it. The lines with an arrow are objects, and you can open them to see their fields.

  On the right, you can see that this service uses targeted redaction. Targeted means that only values with sensitive names are hidden. Most of our production services use Strict, which is tighter. Now I'll show you one live.

## Slide 21 — DEMO D5: a real Exception Replay snapshot  ·  5 minutes (0:57–1:02)

- **Message:** Watch a real production snapshot: the stack trace, the values in one frame, a redacted value, and what it looks like when nothing was captured.
- **Visual:** Demo card. Browser icon, "D5", "5 min", "live in Datadog · production data, no screenshots".
- **On-slide text:** "Demo · D5 · 5 min" · "Error Tracking → issue → Stack Trace → expand a frame"
- **Say:** (Switch to the browser and follow D5 in the run sheet. Use the production issue you picked the day before.) Let me show you a real snapshot from production. Please don't take screenshots of this part, because it shows production data. (Open the issue, scroll to the stack trace and expand one frame. Read two or three of the values out loud and explain what they tell you about the failure. Point at the redaction badge, and at any value that says redacted. Then open an issue without a snapshot and read its message, so people recognize it when they see it.)

## Slide 22 — Live Debugger: a log line without a deploy  ·  1:02

- **Message:** Pick a line in a running service and log the variable values when it runs, with no code change, no deploy and no restart.
- **Visual:** Code panel with a logpoint marker on one line, a condition tag, and the log message it produces with the values filled in. Three labels: session (about 60 minutes), condition, no deploy.
- **On-slide text:** "Pick a line → log its values" · "No code change · no deploy · no restart" · "Dev only today: management-api (G15, G16)"
- **Say:** Live Debugger is for a different kind of problem. Exception Replay only helps after an exception. With Live Debugger, you choose a line of code in a running service, and Datadog logs the values of variables every time that line runs. You don't change the code, and you don't deploy or restart anything.

  First you start a debug session. Inside the session you add a logpoint. A logpoint has a file and a line number, and a message template that can include variable values in curly braces. You can also add a condition, so it only logs the requests you care about. The session stops by itself after about an hour, so nothing stays on by mistake.

  Today it only works where we've turned it on in the service's own configuration, and at the moment that's management-api in dev. Most of our production services use Strict redaction, and Strict blocks logpoints. Turning it on for more services is tracked in DX-1573.

## Slide 23 — DEMO D6: add a logpoint in dev  ·  7 minutes (1:04–1:11)

- **Message:** Watch a logpoint go from nothing to a captured value in about a minute, on management-api in dev.
- **Visual:** Demo card. Browser and terminal icons, "D6", "7 min", the condition.
- **On-slide text:** "Demo · D6 · 7 min" · "Live Debugger → session → logpoint → send a request → snapshot" · `IDENT != null && contains(IDENT, "dx-training")`
- **Say:** (Switch to the browser and follow D6 in the run sheet.) Now I'll add a logpoint to management-api in dev. This endpoint serves files from links in our emails, for example the logo images inside an email. Line 53 decides which kind of link the request has. If someone reported a broken image in an email, we'd want to see the values the request actually received, and that's what this logpoint will log. (Create the session, then add the logpoint on line 53 of FilesController with the message template and the condition. Explain the condition: it only logs requests that contain the text dx-training, so other people's requests are not logged. Wait until the logpoint is active, which takes about thirty seconds. Then switch to the terminal and send the test request. The request returns an error 500, and that's expected, because the demo value isn't a real encrypted key, and the logpoint line runs before the error. Switch back to the browser. The capture is on the session page. Open it and show the argument values. Explain that the same entry also appears in Logs, but only after a few minutes. At the end, stop the session.)

## Slide 24 — DEMO D7: Claude on an error, then a logpoint  ·  6 minutes (1:11–1:17)

- **Message:** Claude reads an Exception Replay snapshot and explains the failure, then places a Live Debugger logpoint in dev and reads what it captured.
- **Visual:** Demo card. Terminal icon. "P9 explain an error → P10 add a logpoint → curl → P10b read it and disable it".
- **On-slide text:** "Demo · D7 · 6 min" · "P9 → P10 → curl → P10b"
- **Say:** (Switch to the terminal and follow D7 in the run sheet.) Now let's ask Claude to do the same debugging work we just did by hand. (Paste P9. While it works, explain that it's reading the Error Tracking issue and the snapshot. When it answers, point out that it explains the failure from the captured values as well as the stack trace. If it suggests adding a logpoint, say something like: Live Debugger only works on management-api in dev today, so I'll ask Claude to add the same logpoint we added by hand in the last demo. Then paste P10. Claude creates the debug session and the logpoint in dev, with the condition. If Claude says Datadog returned an error while creating the logpoint, that's a known Datadog problem. The logpoint is usually created anyway, so Claude checks that it exists before it tries again. When it says the logpoint is ready, send the curl request from the second terminal tab. Then paste P10b: Claude shows what the logpoint captured and disables it. Say that this is the only demo prompt that changes something, which is why it only runs in dev, with a condition, and ends by disabling the logpoint.)

  (End of part three. Ask for questions and take one or two.)

---

## Part 4 — Checking Claude's work  ·  11 minutes (1:19–1:30)

## Slide 25 — Divider: Part 4

- **Message:** Before you act on Claude's answer, check how it got there.
- **Visual:** Divider: solid navy, part number and title.
- **On-slide text:** "Part 4 of 4" · "Trust, but check"
- **Say:** You've now seen Claude answer questions about slow requests and errors. In the last part, I'll show you how to check those answers before you act on them.

## Slide 26 — Five checking habits  ·  1:20

- **Message:** Ask for the query and a link, spot-check it, distrust zeros and span-count rates, compare to a baseline, and tell Claude what Datadog can't see.
- **Visual:** Five numbered habits, each with an icon. Beside them, three small "seen while building this course" tags.
- **On-slide text:** "C1 Which query? Link?" · "C2 Open it, spot-check" · "C3 Distrust 0 and span-count rates" · "C4 Compare to a baseline" · "C5 Add what Datadog can't see"
- **Say:** There are five things I recommend you always do. One: ask Claude which query it ran, and ask for a link. Two: open the link and check one number in Datadog. Three: be careful with zero results, and with any rate that was calculated from span counts. Some filters return nothing without showing an error, and spans are sampled. Four: before you call something a regression, compare it with the same time the day before. Five: tell Claude about things that Datadog doesn't record.

  All of these examples happened while I was preparing this course. I ran every prompt twice. The same question about the slowest endpoints gave two different numbers, because Claude used two different methods. One answer counted thirty requests in an hour, but the metrics show more than five hundred. And one Jira draft looked at CPU time, which is the default, and said our code was not the problem. The same prompt with wall time found the reflection problem.

  In every case, asking Claude which query it ran showed me what really happened.

## Slide 27 — "Is this real?"  ·  1:22

- **Message:** A real latency alert: one ten-minute spike on every host, the median flat, and the day before normal. Is it a regression? (The cause comes out in D8, after P7b.)
- **Visual:** Line chart of management-api p95, 18:00–19:30 UTC on 2026-09-22 against the same window on 2026-09-21: one spike to 2.27 s at 18:40, otherwise ~0.7 s on both days. p50 flat underneath.
- **On-slide text:** "p95 0.7 s → 2.27 s → 0.68 s" · "Median flat · one window · back to baseline" · "Real regression, or something else?"
- **Say:** This is a real example from the day we rolled out the profiler. Watchdog, which is Datadog's automatic alerting, created latency alerts for management-api. The p95 latency went from about 0.7 seconds to 2.27 seconds for one ten-minute window, and then it went straight back down.

  The median didn't change, and the same time window on the day before was normal. So is this a real regression, or something else? Let's ask Claude.

## Slide 28 — DEMO D8: is it real? then check it  ·  6 minutes (1:24–1:30)

- **Message:** Watch Claude judge the spike, then check its queries and give it the missing context.
- **Visual:** Demo card. Terminal icon. "D8 · P6 → P7 → P7b".
- **On-slide text:** "Demo · D8 · 6 min" · "P6 is it real? → P7 which queries? → P7b here's what you couldn't see"
- **Say:** (Switch to the terminal and follow D8 in the run sheet.) Let's ask Claude about this alert. (Paste P6. When it answers, paste P7 to ask for the queries and links. Open one link in the browser and check one number. Then say the next paragraph, paste P7b, and show how Claude changes its answer.)

  There's one thing Claude couldn't see. Just before the spike, we changed the app pool configuration on all the web servers, and that restarts the app pools. Datadog doesn't record an event when that happens. Let's tell Claude and see if it changes its answer.

  (After P7b answers, explain the cause in your own words.) When an app pool starts, it needs some time to warm up. It compiles code, builds the Entity Framework model and fills its caches. So Claude found the right evidence, but a person had to give it the cause.

---

## Wrap-up  ·  5 minutes, then questions (1:30–1:40)

## Slide 29 — Which tool for which question  ·  1:30

- **Message:** Slow request: trace, then profile. An exception: Exception Replay. A value at a line, right now: Live Debugger. Claude can help with all three.
- **Visual:** Three cards, one question each, with the tool underneath. A line under the cards about Claude.
- **On-slide text:** "Why is it slow? → Trace, then profile" · "Why did it fail? → Exception Replay" · "What is this value, at this line? → Live Debugger" · "Claude can do all three · check its work (C1–C5)"
- **Say:** Here's a short summary of today. If a request is slow, start with the trace, and then look at the profile. If a request fails with an exception, open the issue in Error Tracking and check whether it has an Exception Replay snapshot. If you need to see a value at a specific line and there's no exception, use Live Debugger. For now, that means management-api in dev.

  For all three, you can ask Claude to do the research, as long as you check its work.

  The two debugging features are still new, so if one doesn't work, check the gotchas page before you spend a lot of time on it.

## Slide 30 — Gotchas to keep  ·  1:32

- **Message:** Twenty traps, all hit for real during the rollout or while building this course. They're on the cheat sheet.
- **Visual:** Compact grid of G1–G12 titles, then one row for G13–G20.
- **On-slide text:** "CPU vs Wall · Idle threads · Method list vs filter · Async nesting · Line 0 · Since 9/22 · Thin profiles · Sampled spans · Silent zeros · Operation + env · toolsets=all · Claude can't see recycles" · "G13–G20: Exception Replay and Live Debugger"
- **Say:** Here are the common mistakes from today on one slide. The ones for Exception Replay and Live Debugger are in the last row. You don't need to remember them, because they're all on the cheat sheet.

  If you only want to remember three, remember these: use Wall Time, use the flame graph filter, and don't calculate rates from span counts.

## Slide 31 — Your turn  ·  1:33

- **Message:** Run one investigation on your own service in the next two weeks and post one finding.
- **Visual:** Three items with icons: cheat sheet, Slack thread, feedback form. The P8 prompt shown small as "and when you find something: draft the ticket".
- **On-slide text:** "Cheat sheet" · "Post one finding in the course thread" · "Feedback form (2 min)"
- **Say:** For homework, please do one investigation in the next two weeks. Pick a service you work on, ask Claude for its slowest endpoints, and follow one of them down to a method. Then share what you found in the course thread in the ai-exchange channel on Slack. A screenshot and one sentence is enough.

  If you find something worth fixing, there's a prompt on the cheat sheet, P8, that writes a draft Jira ticket for you. Check the evidence before you create the ticket. The cheat sheet has all the prompts and click paths.

  Please also fill in the feedback form. It takes about two minutes, and it helps me improve the next version.

  OK, does anyone have questions?

---

## Backup slides

These slides follow the wrap-up. Use them only if the live D1 demo doesn't cooperate (no slow trace in range, or Datadog is slow), and jump back to slide 6 afterwards.

## Slide 32 — Backup: finding traces

- **Message:** APM → Traces searches spans by service, resource, env and duration. Anything older than 15 minutes is a sample.
- **Visual:** Screenshot `traces-explorer.png`, scaled to fit and centered. Callout boxes on the search bar, the Spans | Traces switch with "in Indexed Spans", and the Duration column. Caption underneath.
- **On-slide text:** "APM → Traces" · "Older than 15 minutes = indexed spans (a sample)"
- **Say:** (Backup for D1.) To find traces, go to APM and then Traces, and filter by service, resource, environment and duration. Next to the Spans and Traces switch, it says Live Search for the last fifteen minutes and Indexed Spans for older time ranges, like in this screenshot. Indexed spans are only a sample, so the number of rows is not the number of requests.

## Slide 33 — Backup: three views of one trace

- **Message:** Flame Graph, Waterfall and Span List show the same spans three ways.
- **Visual:** `trace-flame-graph-gap.png` large, `trace-waterfall.png` and `trace-span-list.png` as smaller insets. Each view is labeled at its own screenshot.
- **On-slide text:** "Flame Graph · Waterfall · Span List"
- **Say:** (Backup for D1.) The same spans, three ways. The flame graph shows the shape of the request. The waterfall is a list on a timeline; click expand all. The span list groups the time by service, and its Exec Time column shows each service's own time.

## Slide 34 — Backup: the span tabs

- **Message:** Each span has tabs. Know what each one is for, and that Profiles is the one we'll use.
- **Visual:** `span-sql-queries.png` with a box around the tab row. A one-line label for each tab underneath, in tab order.
- **On-slide text:** "Overview · Infrastructure · Metrics · Logs · SQL Queries · Profiles"
- **Say:** (Backup for D1.) When you click a span, these tabs appear under the trace. Overview shows the attributes, Infrastructure the host, Metrics the .NET runtime metrics, Logs the logs for this trace, SQL Queries every database call, and Profiles our methods, which we open in part two.

## Slide 35 — Backup: the Service page

- **Message:** The Service page is one service's dashboard. Check the operation and env selectors before trusting any number.
- **Visual:** `service-page.png` on the left, with a box around the operation / env / version selectors. On the right: "Check these first (G10)", a card for Operation listing aspnet.request (the whole request, as IIS and ASP.NET handle it) and aspnet_webapi.request (the part inside our Web API controller), and a card for Env (prod, qa or dev).
- **On-slide text:** "Check these first (G10)" · "Operation" with one line per operation · "Env: prod, qa or dev"
- **Say:** (Backup for D1.) The Service page is a home page for one service. Before you trust a number, check the environment and operation selectors at the top. Our ASP.NET endpoints report under two operations, aspnet.request and aspnet_webapi.request, so each endpoint appears twice.
