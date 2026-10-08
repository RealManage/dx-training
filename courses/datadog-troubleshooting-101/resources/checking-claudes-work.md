---
order: 50
---
# Checking Claude's Work

> Claude is only as good as the queries it runs, and some Datadog queries return misleading answers with no error. These five habits catch it. Every example below happened while this course was being tested on 2026-09-25.

## The five habits

### C1. Ask which query it ran, and for a link

Follow any answer you'll act on with:

```text
Which exact queries, metrics and time windows did you use for that answer? Give me Datadog links so I can check the numbers myself.
```

**Seen live:** the same "slowest endpoints" prompt (P2), run twice, came back with two different aggregation methods: one averaged short-interval p95s over 24 hours, the other used buckets starting at midnight UTC. The rankings were close and the numbers weren't. Neither was wrong, but you need to know which you're looking at.

**Seen live:** asked C1 after the "is it real?" prompt (P6), both runs corrected themselves unprompted. They had used sampled spans instead of the `trace.*` metrics, and one had mixed request spans with SQL spans in a service-wide percentile.

### C2. Open the link and spot-check

Open one link Claude gives you and check one number in the UI. Check the service, env, time range and profile type in the page header. Claude's profiler links go through an org-switch redirect first; that's normal.

**Seen live:** a profiler query for `GET /api/wf/search` also matched `/api/wf/search/vendors` and `/api/wf/search/defaultbegindate`, because the endpoint filter is a pattern, not an exact match. The answer read as one endpoint; the endpoint filter in the returned link listed all three.

### C3. Be suspicious of "0 results" and span-count rates

- "No results" from a span query can mean the filter was silently ignored ([G9](gotchas.md#g9-some-span-filters-silently-return-nothing)). Ask Claude to group by env or service instead of filtering, or check in the UI.
- Spans are sampled, and errors are kept at a higher rate than successes, so a rate worked out from span counts is biased ([G8](gotchas.md#g8-span-counts-are-sampled)). Ask for the `trace.*` metrics.

**Seen live:** explaining one slow dashboard request (P3), Claude reported "30 requests in the hour" for the endpoint, counted from kept spans. The metrics put it at about 558.

### C4. Compare against a baseline

Before calling anything a regression, compare the same window the day before (or a week before, to rule out weekday effects):

```text
Compare that against the same time yesterday. Is the median different, or only the slow tail? Does it last more than one window?
```

A spike that lasts one window, hits every host, leaves the median flat and returns to baseline is usually warm-up, not a regression.

### C5. Tell it what Datadog can't see

App pool recycles, config changes, manual fixes and data loads often leave no event in Datadog. Claude can't know about them, so it guesses. Tell it:

```text
Context you could not see in Datadog: <what changed, where, and when, in UTC>. Does that change your conclusion?
```

**Seen live:** on the 2026-09-22 management-api Watchdog story (P6), one run called it "noise" and guessed at a stalled shared dependency. Neither run could know that app pools on every web host had just been recycled. Given that context (P7b), both revised to a cold start after the recycle. The evidence they'd already gathered (every host at once, time spent inside the app before the first database call, back to normal minutes later) fit it.

## One more: which profile type?

**Seen live:** asked to draft a Jira ticket (P8), one run pulled the **CPU Time** profile, the default, and wrote that our code "isn't the bottleneck". The other used **Wall Time** and found the reflection hotspot. Any claim about "our code" should say which profile type it came from ([G1](gotchas.md#g1-the-profiles-page-opens-on-cpu-time)).

## The live example in the session

Demo D8 runs P6 → P7 → P7b in front of everyone: the answer, the self-correction, then the revision with context. Open one of the P7 links in the browser and check a number live. See the [run sheet](../sessions/session-1/run-sheet.md#d8-is-it-real-then-check-it-6-min).
