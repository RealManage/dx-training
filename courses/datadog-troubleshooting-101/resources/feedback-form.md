---
order: 70
---
# Feedback Form and Success Measure

> The presenter builds this form in Microsoft Forms from the questions below and shares the link in the meeting chat at the end of the session. The form link is not kept in this repo.

## Feedback form (2 minutes)

**Title:** Troubleshooting with Datadog 101: feedback

1. **Before today, how confident were you investigating a slow endpoint in Datadog?** *(Rating 1–5, 1 = not at all, 5 = very)*
2. **Now, how confident are you?** *(Rating 1–5)*
3. **Before today, had you used Claude with the Datadog MCP server?** *(Choice: Never · Once or twice · Regularly)*
4. **Did the setup guide work for you before the session?** *(Choice: Yes, first time · Yes, after troubleshooting · No)*
5. **What was most useful?** *(Choice, pick up to two: APM tour · Reading a flame graph · CPU vs Wall Time · Trace to profile · Claude demos · Checking Claude's work · Cheat sheet)*
6. **What was confusing, or went too fast?** *(Text)*
7. **What should we add or cover in more depth next time?** *(Text)*
8. **Will you run an investigation with Claude on a service you own in the next two weeks?** *(Choice: Yes · Maybe · No)*
9. **Anything else?** *(Text, optional)*

Questions 1 and 2 give a before/after confidence change without a separate pre-survey. Question 4 tells us whether the setup guide needs work.

## Success measure

**Goal:** attendees investigate on their own after the session, not just watch.

**Measure:** the share of attendees who post at least one finding from their own Claude + Datadog investigation in the course Slack thread **within two weeks** of the session.

- **Target:** at least half of attendees.
- **How to measure:** the presenter starts a thread in **#ai-exchange** at the end of the session and pins it ("Post one thing you found with Claude + Datadog: a slow endpoint, a method, a 'it's the database' result, anything"). Two weeks later, count distinct posters against the attendee list from the Teams meeting.
- **What counts:** any finding from a real investigation of a service, with a sentence on what it showed. A screenshot is welcome but optional. Posts must not include customer data.

**Supporting signals** (read alongside, not instead):

- The median confidence change between questions 1 and 2 of the form (hoped for: +1 or more).
- Jira tickets in the following month whose evidence cites a Claude + Datadog investigation.
- Setup-guide failures reported in question 4 (hoped for: none that lasted past troubleshooting).

Record the result in the [run sheet](../sessions/session-1/run-sheet.md#after-the-session).
