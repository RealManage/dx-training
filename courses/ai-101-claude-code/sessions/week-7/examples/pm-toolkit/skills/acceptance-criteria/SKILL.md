---
name: acceptance-criteria
description: Generate detailed acceptance criteria from a feature description
argument-hint: <feature_description>
---

Generate comprehensive acceptance criteria for this feature:

$ARGUMENTS

## Output Format

### Feature: [Feature Name]

#### Functional Criteria

**FC-1: [Criterion Title]**
- Given: [Precondition]
- When: [Action]
- Then: [Expected Result]
- Verification: [How to test]

**FC-2: [Criterion Title]**
(repeat format for 4-6 functional criteria)

#### Non-Functional Criteria

**Performance**
- [ ] Page load time < 2 seconds
- [ ] API response time < 500ms
- [ ] Supports 100 concurrent users

**Security**
- [ ] Input validation on all fields
- [ ] No PII in logs
- [ ] Authentication required

**Accessibility**
- [ ] WCAG 2.1 AA compliant
- [ ] Screen reader compatible
- [ ] Keyboard navigable

#### Edge Cases

| Scenario | Input | Expected Behavior |
|----------|-------|-------------------|
| Empty input | "" | Show validation error |
| Max length | 255+ chars | Truncate or reject |
| Special chars | <script> | Sanitize input |

#### Definition of Done

- [ ] Code complete and reviewed
- [ ] Unit tests passing (80%+ coverage)
- [ ] Integration tests passing
- [ ] Documentation updated
- [ ] QA sign-off received
- [ ] Product owner accepted

## Guidelines
- Every criterion must be testable
- Include happy path AND failure cases
- Consider security implications
- Don't forget accessibility
