# Week 4: Test-Driven Development with Claude - Trainer Runbook

## Pre-Session Checklist

### For Instructors

- [ ] Test both example projects build without warnings
- [ ] Verify coverage tools work
- [ ] Have backup exercises ready
- [ ] Monitor `#ai-exchange` Slack

---

## Instructor Notes

### Common Issues & Solutions

> **"Build warnings about Test SDK"**

- Both projects include `<IsTestProject>true</IsTestProject>`
- This prevents CS7022 warnings with top-level statements
- Normal for projects mixing app code and tests

> **"Should I test private methods?"**

- No! Test public behavior only
- Private methods are implementation details
- If it's important, it should affect public behavior

> **"My tests are brittle"**

- Test behavior, not implementation
- Use interfaces and dependency injection
- Don't over-specify in assertions

> **"Coverage is stuck at 90%"**

- Check for error handling paths
- Look for early returns
- Consider edge cases

### Time Management Tips

- Part 1-2: Keep conceptual (25 min)
- Part 3: Heart of session (75 min)
- If running late: Skip mocking details
- Always leave 5 min for wrap-up

### Engagement Strategies

- Live code the first test together
- Celebrate first "Green" test
- Share coverage reports on screen
- Use real HOA scenarios

### Assessment

Quick check at end:

1. What comes first: test or code?
2. What are the three phases of TDD?
3. Why do tests prevent AI hallucinations?
4. How do you mock an external service?

---

*End of Week 4 Trainer Runbook*
