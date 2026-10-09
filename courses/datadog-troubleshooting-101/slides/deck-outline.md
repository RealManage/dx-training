# Troubleshooting with Datadog 101 — Deck Outline

> **How to use this.** This outline is the source of truth for the deck. Edit here first, then update the deck. Each slide has four parts:
> **Message**, the one idea the slide carries. **Visual**, what's on screen. **On-slide text**, the few words the audience reads. **Say**, the presenter's talking track: it goes into the speaker notes, **never on the slide**.
>
> If a slide needs more than one sentence of on-slide text, it's doing too much. Push it into *Say* or split it.
>
> **How Say is written:** plain, conversational English, the way you'd explain something to teammates, many of whom speak English as a second language. Use full sentences and common words. It should sound like something the presenter would actually say out loud. Don't open with short dramatic fragments ("Four words."), don't use metaphors or give software human traits ("people live in APM", "tracing sees a blank"), and don't use ALL CAPS for emphasis. Avoid phrasing that sounds machine-written: "it's not X, it's Y" contrasts, "here's the thing", "the key is", "that's where X comes in", neat groups of three, a question you answer yourself straight away, and colons or dashes used for effect. Stage directions go in parentheses. Split the Say text into short paragraphs, one idea each (indent continuation paragraphs two spaces under the bullet); each becomes its own paragraph in the speaker notes.
>
> **Timing:** about 85 minutes, plus questions (two minutes at the end of parts 1 to 3, and the rest at the end), in a two-hour slot. 24 slides plus 1 backup slide, 4 part dividers and 7 live demos (**D1–D7**). Parts 1 and 2 are taught mostly live in Datadog, on one trace. Each part shows a task by hand in Datadog first, then a demo of Claude doing the same work. The minute budget per part is in each part heading, and per demo in the demo slides. The [run sheet](../sessions/session-1/run-sheet.md) has the clock times, window switches and fallbacks; the [demo prompts](../resources/demo-prompts.md) have the exact text to paste.
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

- **Message:** Four skills: read a trace, find out why a request is slow, find the cause of an error, and get Claude to do the research. Claude shows up in every part.
- **Visual:** Four tall numbered cards across the slide. No agenda or minutes: the timings live in the run sheet.
- **On-slide text:** "1 · Find and understand a trace" · "2 · Why is it slow?" · "3 · Find the cause of an error" · "4 · Investigate with Claude"
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

  (Open the time picker again and choose LIVE 15 Minutes, at the top of the list.) Now it says Live Search, with a green Live label. Live Search searches every span we sent, but only for the last fifteen minutes. (Point at the Ok count under Status, on the left.) It's the same fifteen minutes, and now we see every request. (Set the time range to Past 1 Day.) For older data, we're back to Historical Search. That's fine for finding slow requests, as long as you remember it's only a sample.

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
- **Say:** Part two is about slow requests. We'll answer the question we had from the trace: what was our code doing during the gap? Most of this part is a live demo again, on the same trace from part one.

## Slide 10 — Tracing vs profiling  ·  0:29

- **Message:** Tracing shows that a request was slow and what it called. Profiling shows what our code was doing.
- **Visual:** Two rows over the same timeline. Top: trace spans with the empty gap. Bottom: profiler samples as small ticks filling the whole timeline, dense inside the gap.
- **On-slide text:** "Tracing: *that* it was slow, and what it called" · "Profiling: *what our code was doing*"
- **Say:** So far we've used traces. A trace records the calls our code makes, like the SQL calls, and how long each one takes. (Point at the top row.) But it doesn't record what our own code does between those calls. That's why our trace had a gap.

  The profiler works differently. Many times every second, it samples every thread and records which method is running, and which methods called it. (Point at the small marks on the bottom row.) Each of these marks is one sample. There are a lot of them during the gap, so the profile can tell us what our code was doing there.

## Slide 11 — DEMO D3: from the trace to the profile  ·  12 minutes (0:31–0:43)

- **Message:** On the same trace as D1, the span's profile shows what our code did during the gap; the Profiles page shows every dashboard request does the same.
- **Visual:** Demo card: "D3" and "12 min" tags, "Switch to the browser · the D1 trace", six numbered steps, and the filter text. Step 5 carries an "All requests ↗" link to APM → Profiles, filtered to resident-api in prod and the dashboard endpoint, on CPU Time (the right view here: the gap is CPU work, and Wall Time adds the waiting branches, which hide the helper); it opens in a new tab so the trace stays open for D4.
- **On-slide text:** "Demo · D3 · 12 min" · "1. Back to the D1 trace: click the resident-api span → Profiles" · "2. Thread timeline: idle during SQL, busy in the gap" · "3. Flame Graph tab: gray is framework, blue is our code" · "4. Read down to GetEnumFromDescription" · "5. APM → Profiles: every dashboard request, on CPU Time [All requests ↗]" · "6. Filter flame graph: focus on GetUserDashboardItems" · `focus_on(method:"UserBP.GetUserDashboardItems()")`
- **Say:** In this demo, we'll go back to the same trace from part one and open its profile. We'll see what our code was doing during the gap, and then check if the same thing happens in every dashboard request.

  (Go to the D1 browser tab, where the slow trace is still open. Click the resident-api span, then Profiles, the last of the span tabs. If the panel is small, drag its top edge up.) This is the profile for just this span. (Point at the thread timeline.) This is the thread timeline. Like the trace, time goes from left to right. While SQL is running, the thread isn't doing anything. Then, after the SQL finishes, it's busy until the end. That's the gap from the trace.

  (Click Flame Graph, under the timeline.) This is a flame graph again, but in a profile you read it a little differently. In the trace, left to right was time. Here, only the width matters: a wider box means more time, and the order from left to right doesn't mean anything. We read it from top to bottom. Each method calls the methods under it.

  (Point at the gray Standard Library rows, then the blue rows.) The gray rows that say Standard Library are .NET framework code, folded up so they don't get in the way. Our own code is in blue, so we skip the gray rows and follow the blue ones down. (Point at CPU Time at the top.) Also notice that it says CPU Time. The thread was actually running during the gap, not waiting for something, so this is work our own code was doing.

  (Point at CCCaching.AddOrGetCacheAsync and UserRepository.GetUserActivityFeed.) At the top there's a caching method and the user repository. They aren't the slow part. Our code is async, and code that runs after an await shows up under the method it was waiting for. So we keep reading down. (Point at UserBP.GetUserDashboardItems.) Here's the method that builds the dashboard, GetUserDashboardItems. (Point at DashboardHelper.GetActivities and the GetEnumFromDescription boxes.) Under it, most of the width is GetActivities and a small helper called GetEnumFromDescription, which shows up in several places. (Hover one of the GetEnumFromDescription boxes and read its time off the screen.)

  So that's one request. Is it the same in every dashboard request? (Click All requests on the slide. It opens APM, then Profiles, in a new tab, already filtered to resident-api in prod and the dashboard endpoint, so the trace stays open in the D1 tab.) This is the Profiles page. It combines the profiles from all requests. You can get here yourself from APM, then Profiles, and pick the service and the endpoint. (Set the time range to Past 1 Day.) I'll use a full day, so there are enough requests to see the pattern.

  (Point at the CPU Time and Wall Time cards.) There are two ways to measure time here. CPU Time only counts the time a thread was actually running. Wall Time counts all the time that passed, running or waiting. In our trace, the profile said CPU Time, so during the gap our code was running, not waiting. That means CPU Time is the right one to look at. If a request were slow because it was waiting, for example for another API, you would switch to Wall Time.

  (Click the Filter flame graph box and choose focus_on from the list. Inside its brackets, type GetUserDashboardItems, and choose the UserBP.GetUserDashboardItems suggestion.) There's still a lot on this page, so I'll focus on our method. Focus on hides everything that isn't under GetUserDashboardItems. (Point at GetEnumFromDescription.) And it looks like our one trace: most of the CPU time goes to GetEnumFromDescription. So this isn't one unlucky request. Other dashboard requests spend their time in the same helper.

  One more thing before we move on. Sometimes a profile looks very different: most of the width is Entity Framework or the SQL reader waiting for rows, and our code is only a thin part. Then the problem is in the database, and making the C# faster won't help. Here, our own code is the wide part, so the fix is in our code.

## Slide 12 — What we just saw: a slow lookup, many times  ·  0:43

- **Message:** The gap is one small helper doing reflection, uncached, once for every activity and property pair. Knowing this first makes Claude's answer in D4 easy to follow.
- **Visual:** Left: our code as a small top-down flame graph: GetUserDashboardItems, then a row of repeated GetEnumFromDescription boxes labeled "for every activity × every property", each over a gray reflection box. Right: "What it does", "Why it's slow", "Why so often". Bottom: "The fix is in our code. Next, Claude reads the profile and the code."
- **On-slide text:** "UserBP.GetUserDashboardItems()" · "for every activity × every property" · "EnumExtensions.GetEnumFromDescription(), called again each time" · "Attribute.GetCustomAttribute: reflection, nothing cached" · the three right-hand points · the bottom line
- **Say:** Let's sum up what we found. During the gap, our code was running GetUserDashboardItems, and most of that time went to a small helper called GetEnumFromDescription.

  That helper turns an activity's category text into an enum value. It does that with reflection, which is slow, and it never saves the answer, so it does the same slow lookup again on every call.

  It's also called many more times than it needs to be. The code pairs every activity with every property the user has, and does the lookup for each pair. So a user with many properties pays for it many times.

  The profiler showed us where the time goes. To understand why, we have to read the code, and that's what we'll ask Claude to do next.

## Slide 13 — DEMO D4: Claude reads the profile and the code  ·  7 minutes (0:44–0:51)

- **Message:** Hand Claude the same trace link; it reads this request's profile, then the code, and explains the slow method and how to fix it.
- **Visual:** Demo card: "D4" and "7 min" tags, "Switch to the terminal · Claude Code", three short steps, and the prompt in the code box, with a dimmed "[paste the trace link]" line under it.
- **On-slide text:** "Demo · D4 · 7 min" · "1. Paste the prompt below into Claude Code" · "2. From the D1 tab, copy the trace link and paste it after the prompt" · "3. Compare with what we found: the method, why it's slow, the fix" · the prompt · "[paste the trace link]"
- **Say:** Now let's ask Claude to do the same thing, and then go one step further. Claude Code can read our code, so after it finds the slow method in the profile, it can tell us what the method does and how to make it faster. We'll give it the same trace link again.

  (Click the prompt below once to select it, copy it, and paste it into Claude Code. Don't press Enter yet. Go to the D1 browser tab and copy the trace link from the address bar again. Back in the terminal, paste it after the prompt, and press Enter.)

  `For the trace below, from prod resident-api, use the profiler to find which of our own methods took the most time during this request. Then read that code and answer in a few short bullet points, in plain English: what does it do, why is it slow, and how could we make it faster?`

  (Claude takes about two minutes. Scroll slowly so the room can see its steps.) While it works, you can follow its steps on the screen. First it gets the profile for this one request from Datadog. Then it searches our code for the methods in that profile, and opens and reads those files. That's the part we didn't do by hand, so it takes a couple of minutes.

  (When it answers, read its plain-language recap out loud. Then point at the method it names and the fixes it suggests. They should match D3: GetEnumFromDescription, reflection on every call, and a lookup that runs once for every activity and property pair. If it mentions the dashboard stored procedure, say that's the database half of the request, which is a separate problem.)

  (If Claude can't open the link, paste only the long number after traceID= in the link.)

  So that's part two. We started with a gap in the trace, and the profile told us which of our methods was running during that gap. Then Claude read that method in our code, explained why it's slow, and suggested a fix. You can do the same with your own slow endpoints.

  Before we move on, does anyone have a question? (Take one or two. Keep it to about two minutes.)

---

## Part 3 — Finding the cause of an error  ·  26 minutes with questions (0:53–1:19)

> This part is a show and tell. Most of the room won't use Exception Replay or Live Debugger often; the goal is that they know these tools exist and think of them the next time an error doesn't make sense. Keep the talking short.

## Slide 14 — Divider: Part 3

- **Message:** Part 3 follows one error from our email links, with two newer Datadog features that show the values of variables in running code.
- **Visual:** Divider: solid navy, part number and title.
- **On-slide text:** "Part 3 of 4" · "Finding the Cause of an Error"
- **Say:** In part three we'll look at errors, and we'll use a new example. Many of our emails show images, like a logo, and each image is a link to management-api. In production, some of those links fail with an error 500.

  This part is more of a show and tell. These are newer Datadog features, and you may not need them often. But I want you to know they exist, so you think of them the next time you have an error you can't explain.

## Slide 15 — Exception Replay: the values behind an error  ·  0:54

- **Message:** When an exception happens again, Datadog saves the values of the variables in each method of the stack trace and shows them in Error Tracking. Sensitive values are hidden.
- **Visual:** Three steps: 1st time (tracked, no snapshot) → 2nd time on the same server (a snapshot) → Error Tracking → Stack Trace, drawn on our example with type names only: `TripleDesEncryption.Decrypt(…)`, `cipherString = [REDACTED]`, `useHashing = True`, `@exception = FormatException`, `GetFileByEmailResource(…)`. Caption underneath.
- **On-slide text:** "1st time: tracked, no snapshot yet" · "2nd time, same server: the arguments and locals of every frame = a snapshot" · "Error Tracking → Stack Trace" · "At most 1 snapshot per exception type, per server, per hour (G13) · not every exception gets one (G14)"
- **Say:** Exception Replay works together with Error Tracking. Error Tracking is the Datadog page that groups the same exception into one issue, with its stack trace. With Exception Replay, Datadog also saves the values of the variables in each method of that stack trace. That's called a snapshot. Exception Replay is already on for our services, so there's nothing to set up.

  (Point at the first two boxes.) Datadog doesn't take a snapshot the first time an exception happens on a server, only from the second time, and then at most once an hour. So a rare error may only have a few snapshots.

  (Point at the third box.) Values with sensitive names, like keys or encrypted text, are hidden. You'll see that on our real error in a moment.

## Slide 16 — DEMO D5: an error from our email links  ·  5 minutes (0:56–1:01)

- **Message:** A real production snapshot of our example: where it failed, the exception message, and the value Datadog hid.
- **Visual:** Demo card: "D5" and "5 min" tags, "Switch to the browser · production data, no screenshots", four numbered steps, and the search text. Step 1 carries an "Error Tracking ↗" link (Error Tracking filtered to management-api in prod and System.FormatException; no IDs, no time range).
- **On-slide text:** "Demo · D5 · 5 min" · "1. Errors → Error Tracking: management-api FormatException [Error Tracking ↗]" · "2. Set Past 1 Week, open the issue in System.Convert.FromBase64_Decode" · "3. Error sample → Stack Trace: the snapshot under Decrypt" · "4. Expand cipherString: redacted" · `service:management-api env:prod error.type:System.FormatException`
- **Say:** In this demo, I'll open Error Tracking in production and look at what Exception Replay saved for one of these email link errors. Please don't take screenshots of this part, because it's production data.

  (Click Error Tracking on the slide. Set the time range to Past 1 Week.) This is Error Tracking. You can get here from Errors in the left menu. Each row is an issue, which means the same exception from the same place in the code, grouped together. (Open the issue whose location is System.Convert.FromBase64_Decode. The other one with the same message, in TripleDesEncryption.Decrypt, has no snapshot.) Here's our error. (Point at the count.) It's happened hundreds of times this week.

  (Scroll down to Error sample. The Stack Trace tab is open.) This is the stack trace, the list of methods that were running when the exception happened. (Point at the Decrypt method and Exception Replay snapshot under it.) And here's the snapshot. Each line is a variable in the Decrypt method, with its value at the moment of the error. (Point at the red message at the top.) The message says the input isn't a valid Base-64 string, so .NET couldn't even start to decrypt it.

  (Expand cipherString.) But the value we'd most like to see is hidden. Datadog hides values based on the variable's name, so anything that looks like a key or encrypted text is redacted. That's good for security, but it means we can't see what was wrong with the link.

  (If the sample says Exception Replay data was not captured, click Previous until one has a snapshot. If none has one, open the identity-api issue in browser tab 4, and say that this error had no snapshot this week.)

## Slide 17 — Live Debugger: a log line without a deploy  ·  1:01

- **Message:** Pick a line in a running service and log the values of its variables when it runs, with no code change, no deploy and no restart.
- **Visual:** Unchanged: FilesController.cs line 53 with a logpoint, its template and condition, and the log line it produces. Caption: "Set up in dev today (G15) · prod would need Targeted redaction (G16)".
- **On-slide text:** "Pick a line → log its values" · "No code change · no deploy · no restart" · "Set up in dev today (G15) · prod would need Targeted redaction (G16)"
- **Say:** Exception Replay showed us where the request failed and why, but it hid the values. When you need to see more, there's Live Debugger.

  With Live Debugger, you pick a line of code in a running service, and Datadog logs the values of its variables every time that line runs. You don't change the code, and you don't deploy or restart anything. You can add a condition, so it only logs the requests you care about, and the session stops by itself after an hour.

  Today we'll use it in dev. In production we keep tighter security settings for this feature, because it can show sensitive values.

## Slide 18 — DEMO D6: add a logpoint in dev  ·  6 minutes (1:03–1:09)

- **Message:** A logpoint on the email link endpoint goes from nothing to a captured value in about a minute, on management-api in dev, with no code change.
- **Visual:** Demo card: "D6" and "6 min" tags, "Switch to the browser · management-api in dev", five numbered steps, and the condition. Step 1 carries a "Live Debugger ↗" link (APM → Debugger), step 4 a "Test request ↗" link (the dev test URL, opens in a new tab), and step 5 a "Backup" link to the backup slide.
- **On-slide text:** "Demo · D6 · 6 min" · "1. APM → Debugger → New Session → Start debugging manually [Live Debugger ↗]" · "2. management-api, dev · search FilesController → click line 53" · "3. Template, + Condition → Start Debug Session" · "4. Send the test request [Test request ↗]" · "5. Open the capture, then stop the session [Backup]" · `IDENT != null && contains(IDENT, "dx-training")`
- **Say:** In this demo, I'll add a logpoint to the email link endpoint in dev, send it one test request, and look at what it captured.

  (Click Live Debugger on the slide. It opens APM, then Debugger. Skip the Bits Live Debugger box at the top, and click New Session under Start debugging manually.) There's an AI helper at the top, but we'll do it by hand.

  (Choose management-api and dev. Type FilesController in the search, pick the CLASS result, then click in the code to close the method list. Don't press Escape, because it closes the whole window. Scroll to line 53 and click just left of the line number.) This is the endpoint our email images call. Line 53 checks the two values from the link, before anything is decrypted.

  (Copy the template below and paste it over the text in with template.)

  `Course demo: ERES={ERES} IDENT={IDENT} resource={emailResourceKey}`

  (Click + Condition, then copy the condition below and paste it into when.)

  `IDENT != null && contains(IDENT, "dx-training")`

  The template is the log message, and the names in curly braces are replaced with the real values. The condition makes it log only my test request, not everyone else's. (Click Start Debug Session. Wait until the logpoint says Waiting for events, about thirty seconds.) And remember, we didn't change any code or deploy anything.

  (Click Test request on the slide. A new tab opens and says Error retrieving file by link.) This is a test request to the same endpoint, with a made-up value, so it fails the same way as in production. (Go back to the Datadog tab. The capture shows up in the list under the code within about fifteen seconds. If it doesn't, switch the time range above the list from LIVE to Past 15 Minutes and back to LIVE. Open it.) Here's what the logpoint captured, the values of the variables at line 53. Here we can see them, because these variable names aren't hidden by our redaction settings.

  (Close the panel, and click the pause icon next to Active at the top right.) When you're done, stop the session, so the logpoint doesn't keep running.

  (If management-api isn't in the list, or nothing is captured within a minute, click Backup on the slide, and say that Live Debugger needs a setting on the service that isn't working today. Then click Back to D7.)

## Slide 19 — DEMO D7: Claude explains the error  ·  8 minutes (1:09–1:17)

- **Message:** Give Claude the same Error Tracking issue: it explains the error from the snapshot, then reads our code and finds that a broken link returns an error 500 instead of Not Found.
- **Visual:** Demo card: "D7" and "8 min" tags, "Switch to the terminal · Claude Code", three short steps, and the prompt in the code box, with a dimmed "[paste the issue link]" line under it.
- **On-slide text:** "Demo · D7 · 8 min" · "1. Paste the prompt below into Claude Code" · "2. From the D5 tab, copy the issue link and paste it after the prompt" · "3. Compare with what we found, then look at the fix" · the prompt · "[paste the issue link]"
- **Say:** Now let's ask Claude about the same error. I'll give it the link to the issue we opened in D5. Like with the slow request, Claude Code can also read our code, so it can tell us what the user gets back and how we could handle this better.

  (Click the prompt below once to select it, copy it, and paste it into Claude Code. Don't press Enter yet. Go to the D5 browser tab, where the issue is still open, and copy the URL from the address bar. Back in the terminal, paste it after the prompt, and press Enter.)

  `For the Error Tracking issue below, from prod management-api, find a prod sample from the last week that has an Exception Replay snapshot, and then read our code. Answer in four short bullet points, in plain English: why did the request fail, what can't the snapshot show us, what does the user get back, and how could we handle this better? Don't quote any values from the request or the snapshot.`

  (Claude takes about two minutes. Scroll slowly so the room can see its steps.) While it works, you can follow its steps on the screen. It reads the issue and its snapshot from Datadog, then searches our code for the methods in the stack trace and reads them.

  (When it answers, point at each bullet. It should match D5: the link value wasn't valid Base-64, and the snapshot hid the values. Then point at what it found in the code: the code was meant to return Not Found for a bad link, but the exception happens first, so the user gets an error 500.)

  (If Claude says the newest sample is a dev request, that's our test request from D6. It lands in the same issue, which is why the prompt asks for a prod sample.)

  (If Claude can't open the link, paste only the issue ID, the long value after issueId in the link.)

  So that's part three. These tools are newer, and you probably won't need them every day. But the next time you have an error you can't explain, remember that Datadog can show you the values from the moment it happened, and that Claude can read the error and our code for you.

  Does anyone have a question before the last part? (Take one or two. Keep it to about two minutes.)

---

## Part 4 — Checking Claude's work  ·  6 minutes (1:19–1:25)

## Slide 20 — Divider: Part 4

- **Message:** Before you act on Claude's answer, check how it got there.
- **Visual:** Divider: solid navy, part number and title.
- **On-slide text:** "Part 4 of 4" · "Trust, but check"
- **Say:** You've now seen Claude answer questions about slow requests and errors. In the last part, I'll show you how to check those answers before you act on them.

## Slide 21 — Three checking habits  ·  1:20

- **Message:** Ask for the query and a link and check it yourself, be careful with zeros and span-count rates, and give Claude what Datadog can't see.
- **Visual:** Three numbered habits, C1–C3.
- **On-slide text:** "C1 Ask which query it ran and for a link, then check the results yourself" · "C2 Be careful with zero results and rates from span counts" · "C3 Give Claude the context that Datadog can't see"
- **Say:** There are three habits I recommend for any answer from Claude that you plan to act on.

  Ask Claude which query it ran, and ask for a link. Then open the link and look at the results yourself, to make sure they match what Claude told you.

  Be careful with two kinds of numbers. A result of zero isn't always true, because some filters match nothing without showing an error. And don't trust a rate that Claude worked out by counting spans, because Datadog only keeps a sample of them. Ask it to use the metrics instead.

  And give Claude the context it can't get from Datadog. For example, a deploy or a config change, what users told you, or what you already ruled out. Claude can only research what it can see, so this kind of context helps it a lot.

## Slide 22 — "Is this real?"  ·  1:22

- **Message:** A real latency alert that Claude got half right: not a code regression, but the wrong cause, until a person told it about the app pool recycle. That kind of spike should now be gone.
- **Visual:** Line chart of management-api p95, 18:00–19:30 UTC on 2026-09-22 against the same window on 2026-09-21: one spike to 2.27 s at 18:40, otherwise ~0.7 s on both days. p50 flat underneath. Caption: "Median flat · one window · every host · back to baseline → app pools warming up, not a regression".
- **On-slide text:** "p95 0.7 s → 2.27 s → 0.68 s" · "Median flat · one window · every host · back to baseline → app pools warming up, not a regression"
- **Say:** Here's a real example of why that last habit matters. Watchdog, which is Datadog's automatic alerting, created a latency alert for management-api. The p95 latency went from about 0.7 seconds to more than 2 seconds for one ten-minute window, and then it went straight back down. The median didn't change, and the same time the day before was normal.

  When we asked Claude, it said this wasn't a code regression, which was right. But it guessed that the database was the cause. What it couldn't see is that the app pools on every web server had just been recycled, because Datadog doesn't record that. Once we told Claude, it changed its answer: the servers were warming up after the restart.

  This can happen any time the app pools recycle, whether it's scheduled or not. As of this week, we've fixed the warm-up in IIS with Application Initialization, so alerts like this one should be a thing of the past.

---

## Wrap-up  ·  4 minutes, then questions (1:25–1:40)

## Slide 23 — Which tool for which question  ·  1:25

- **Message:** Slow request: trace, then profile. An exception: Exception Replay. A value at a line, right now: Live Debugger. Claude can help with all three.
- **Visual:** Three cards, one question each, with the tool underneath. A line under the cards about Claude.
- **On-slide text:** "Why is it slow? → Trace, then profile" · "What caused this error? → Exception Replay" · "What is this value, at this line? → Live Debugger" · "Claude can do all three · check its work (C1–C3)"
- **Say:** Here's a short summary of today. If a request is slow, start with the trace, and then look at the profile. If a request fails with an exception, open the issue in Error Tracking and check whether it has an Exception Replay snapshot. If you need to see a value at a specific line and there's no exception, use Live Debugger.

## Slide 24 — Your turn  ·  1:27

- **Message:** Run one investigation on your own service in the next two weeks and post one finding; if it's worth fixing, turn it into a ticket with the sdlc plugin.
- **Visual:** Three cards: keep the cheat sheet, post one finding, turn it into a ticket (the sdlc plugin).
- **On-slide text:** "Keep the cheat sheet" · "Post one finding" · "Turn it into a ticket: the sdlc plugin creates it and starts the work"
- **Say:** For homework, please do one investigation in the next two weeks. Pick a service you work on, ask Claude for its slowest endpoints, and follow one of them down to a method. Then share what you found in the course thread in the ai-exchange channel on Slack. A screenshot and one sentence is enough.

  If you find something worth fixing, you can use the sdlc plugin in Claude Code to create the Jira ticket and start working on it.

  OK, does anyone have questions?

  (After the questions.) Thanks, everyone, for joining today. I hope this helps the next time you're troubleshooting something. Have a great rest of your day.

---

## Backup slides

After the wrap-up. Reach them from the Backup link on a demo card; each has a link back.

## Slide 25 — Backup: what a logpoint captures

- **Message:** What D6 would have shown, if Live Debugger isn't working on the day.
- **Visual:** `live-debugger-session.png`: the session page for management-api in dev, the logpoint form on line 53, and one captured log line. Caption with a "Back to D7" link.
- **On-slide text:** "management-api in dev · the logpoint on line 53 and one capture" · "Back to D7"
- **Say:** (Backup for D6.) Live Debugger needs a setting on the service, and it isn't working today, so here's a capture from an earlier test. (Point at the logpoint form, then at the log line.) This is the logpoint on line 53, and this is the log line it wrote, with the values filled in. (Click Back to D7.)
