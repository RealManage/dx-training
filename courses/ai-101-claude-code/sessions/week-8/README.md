# Week 8: Real-World Automation & CI/CD

**Duration:** 2 hours
**Format:** In-person or virtual
**Audience:** RealManage cross-functional team (engineers, PMs, support staff)

## Learning Objectives

By the end of this session, participants will be able to:
- Apply Claude Code to cross-functional RealManage workflows
- Integrate Claude Code with GitLab CI/CD pipelines
- Implement efficiency and context management strategies
- Build continuous improvement workflows
- Create production-ready automation pipelines
- Measure and track productivity gains

## Pre-Session Checklist

### For Participants
- [ ] Completed Weeks 1-7 (especially skills from Week 7)
- [ ] GitLab account with repository access
- [ ] Understanding of GitLab CI/CD basics
- [ ] Access to a test repository for exercises
- [ ] Ready for 2-hour session

### For Instructors
- [ ] Test all example projects build without warnings
- [ ] Verify GitLab CI/CD pipeline examples are correct
- [ ] Prepare cost monitoring examples
- [ ] Have production workflow examples ready
- [ ] Monitor `#dx-training` Slack channel

## Session Plan

### Part 1: Cross-Functional Use Cases (25 min)

#### 1.1 Support Team Workflows (10 min)

**Ticket Summarization Skill (.claude/skills/ticket-summary/SKILL.md):**
```markdown
---
name: ticket-summary
description: Summarize support ticket for escalation
argument-hint: [ticket_id]
disable-model-invocation: true
---

Analyze support ticket $ARGUMENTS and create an executive summary.

## Summary Format:
1. **Issue Type:** [Category]
2. **Severity:** [High/Medium/Low]
3. **Affected Property:** [Property ID]
4. **Summary:** [2-3 sentence description]
5. **Root Cause:** [If identifiable]
6. **Recommended Action:** [Next steps]
7. **Estimated Resolution:** [Timeline]

## Guidelines:
- Extract key facts from ticket history
- Identify if related to existing violations
- Note any recurring issues for this property
- Flag if board escalation needed
```

**Response Draft Skill (.claude/skills/draft-response/SKILL.md):**
```markdown
---
name: draft-response
description: Draft professional homeowner response
argument-hint: [ticket_id] [response_type]
disable-model-invocation: true
---

Draft a $2 response for ticket $1.

## Response Types:
- acknowledgment: Initial receipt of inquiry
- status_update: Progress update on issue
- resolution: Issue resolved notification
- escalation: Escalating to board/management

## Tone Guidelines:
- Professional but warm
- Empathetic to homeowner concerns
- Clear on next steps
- Include relevant policy references
- Provide contact for follow-up

## Template Elements:
1. Greeting with owner name
2. Reference to original inquiry
3. Status/information update
4. Next steps (if any)
5. Closing with contact info
```

#### 1.2 Product Manager Workflows (10 min)

**Release Notes Generator (.claude/skills/release-notes/SKILL.md):**
```markdown
---
name: release-notes
description: Generate release notes from git history
argument-hint: [from_tag] [to_tag]
disable-model-invocation: true
---

Generate release notes for changes between $1 and $2.

## Steps:
1. Get commits between tags: git log $1..$2
2. Categorize by type (feat, fix, refactor, docs)
3. Extract ticket references (DX-###)
4. Group by area (API, UI, Database, etc.)

## Output Format:
### Release Notes: [Version]
**Date:** [Today's date]

#### New Features
- [Feature description] (DX-###)

#### Bug Fixes
- [Bug description] (DX-###)

#### Improvements
- [Improvement description]

#### Breaking Changes
- [If any, with migration notes]

#### Known Issues
- [Ongoing issues if any]
```

**Sprint Planning Assistant (.claude/skills/sprint-plan/SKILL.md):**
```markdown
---
name: sprint-plan
description: Assist with sprint planning from backlog
argument-hint: [sprint_number] [capacity_points]
disable-model-invocation: true
---

Plan sprint $1 with team capacity of $2 story points.

## Process:
1. Review prioritized backlog items
2. Estimate complexity for unpointed items
3. Select items fitting capacity ($2 points)
4. Identify dependencies between items
5. Flag risks or blockers

## Output:
### Sprint $1 Plan

**Capacity:** $2 points
**Selected Items:** [List with points]
**Total Points:** [Sum]
**Buffer:** [Remaining capacity]

**Dependencies:**
- [Item A] blocks [Item B]

**Risks:**
- [Identified risks]

**Recommended Assignments:**
- [Item] -> [Team member rationale]
```

#### 1.3 Engineering Workflows (5 min)

**Code Documentation Generator (.claude/skills/api-docs/SKILL.md):**
```markdown
---
name: api-docs
description: Generate API documentation from code
argument-hint: [controller_path]
disable-model-invocation: true
---

Generate OpenAPI-compatible documentation for $ARGUMENTS.

## Process:
1. Parse controller/endpoint files
2. Extract route definitions
3. Identify request/response models
4. Document authentication requirements
5. Include example payloads

## Output Format:
### [Controller Name] API

#### Endpoints

##### [METHOD] /api/[route]
**Description:** [Purpose]
**Authentication:** [Required/Optional]

**Request:**
\`\`\`json
{ "example": "payload" }
\`\`\`

**Response (200):**
\`\`\`json
{ "example": "response" }
\`\`\`

**Errors:**
- 400: [When/why]
- 401: [When/why]
- 404: [When/why]
```

**Refactoring Assistant (.claude/skills/refactor/SKILL.md):**
```markdown
---
name: refactor
description: Safely refactor code with test preservation
argument-hint: [file_path] [refactor_type]
disable-model-invocation: true
---

Refactor $1 applying $2 pattern.

## Supported Refactor Types:
- extract_method: Extract repeated code into method
- extract_interface: Create interface from class
- simplify_conditionals: Reduce nesting and complexity
- async_conversion: Convert sync to async/await

## Process:
1. Read existing code and tests
2. Identify refactoring opportunities
3. Apply refactoring incrementally
4. Run tests after each change
5. Document changes made

## Safety Rules:
- Never break existing tests
- Preserve public API surface
- Add tests for new extracted methods
- Create rollback commit point first
```

### Part 2: GitLab CI/CD Integration (30 min)

#### 2.1 Claude Code in CI/CD (10 min)

**Understanding CI/CD Integration:**

Claude Code can be integrated into GitLab CI/CD pipelines for:
- Automated code review on merge requests
- MR description generation
- Test generation for new code
- Documentation updates
- Release automation

**Basic Pipeline Structure:**
```yaml
# .gitlab-ci.yml
stages:
  - build
  - test
  - review

variables:
  DOTNET_VERSION: "10.0"

# Required CI/CD Variables (Settings > CI/CD > Variables):
#   - ANTHROPIC_API_KEY: Your Anthropic API key (masked)
#   - GITLAB_TOKEN: GitLab personal access token with api scope

build:
  stage: build
  image: mcr.microsoft.com/dotnet/sdk:10.0
  script:
    - dotnet restore
    - dotnet build --no-restore --warnaserror

test:
  stage: test
  image: mcr.microsoft.com/dotnet/sdk:10.0
  script:
    - dotnet test --verbosity normal --collect:"XPlat Code Coverage"
  coverage: '/Total\s*\|\s*(\d+(?:\.\d+)?%)/'

claude-review:
  stage: review
  image: node:22
  rules:
    - if: $CI_PIPELINE_SOURCE == "merge_request_event"
  before_script:
    - npm install -g @anthropic-ai/claude-code
  script:
    - |
      claude --print "Review the changes in this MR for:
      1. Code quality issues
      2. Security concerns
      3. Test coverage gaps
      4. Documentation needs

      Output findings as markdown."
```

#### 2.2 Automated Code Review Workflow (10 min)

**MR Review Pipeline:**
```yaml
# Add to .gitlab-ci.yml
claude-review:
  stage: review
  image: node:22
  rules:
    - if: $CI_PIPELINE_SOURCE == "merge_request_event"
  before_script:
    - npm install -g @anthropic-ai/claude-code
  script:
    - |
      # Get list of changed files in this merge request
      CHANGED_FILES=$(git diff --name-only $CI_MERGE_REQUEST_DIFF_BASE_SHA...HEAD | tr '\n' ' ')

      # Run Claude Code to review the changes
      claude --print "Review these changed files for:
      1. Logic errors or bugs
      2. Security vulnerabilities (OWASP Top 10)
      3. Performance issues
      4. Code style consistency
      5. Missing error handling

      Files: $CHANGED_FILES

      Format as markdown with file:line references." > review.md

      # Post the review as a comment on the merge request
      REVIEW_BODY=$(cat review.md | jq -Rs .)
      curl --request POST \
        --header "PRIVATE-TOKEN: $GITLAB_TOKEN" \
        --header "Content-Type: application/json" \
        --data "{\"body\": $REVIEW_BODY}" \
        "$CI_API_V4_URL/projects/$CI_PROJECT_ID/merge_requests/$CI_MERGE_REQUEST_IID/notes"
```

**Key GitLab CI/CD Variables:**
| Variable | Description | Where to Set |
|----------|-------------|--------------|
| `CI_MERGE_REQUEST_DIFF_BASE_SHA` | Base commit of MR | Auto-provided |
| `CI_MERGE_REQUEST_IID` | MR internal ID | Auto-provided |
| `CI_API_V4_URL` | GitLab API URL | Auto-provided |
| `ANTHROPIC_API_KEY` | Claude API key | Settings > CI/CD > Variables |
| `GITLAB_TOKEN` | PAT with api scope | Settings > CI/CD > Variables |

#### 2.3 Documentation Generation Pipeline (10 min)

**Auto-Update Docs Pipeline:**
```yaml
# Add to .gitlab-ci.yml
doc-generation:
  stage: deploy
  image: node:22
  rules:
    - if: $CI_COMMIT_BRANCH == "main"
      changes:
        - "src/**/*.cs"
  before_script:
    - npm install -g @anthropic-ai/claude-code
    - apt-get update && apt-get install -y git
  script:
    - |
      claude --print "Scan src/Controllers/*.cs and generate:
      1. API endpoint documentation in docs/api/
      2. Update README.md API section
      3. Create changelog entry for new endpoints

      Use existing documentation style and format."

      # Commit and push documentation updates
      git config user.name "GitLab CI"
      git config user.email "ci@realmanage.com"
      git add docs/ README.md CHANGELOG.md
      git diff --staged --quiet || git commit -m "docs: Auto-update documentation

      Co-Authored-By: Claude <noreply@anthropic.com>"
      git push https://oauth2:$GITLAB_TOKEN@$CI_SERVER_HOST/$CI_PROJECT_PATH.git HEAD:main
```

**Setting Up Scheduled Pipelines:**
1. Go to **CI/CD > Schedules** in your GitLab project
2. Click **New schedule**
3. Set interval (e.g., daily at 9am)
4. Select target branch
5. Add any schedule-specific variables

### Part 3: Model Selection & Context Management (20 min)

#### 3.1 Choosing the Right Model (10 min)

**Available Models:**

| Model | Capability | Speed | Best For |
|-------|------------|-------|----------|
| **Sonnet** | High | Fast | Daily driver, general development (default) |
| **Opus** | Highest | Moderate | Complex analysis, architecture, nuanced tasks |

**Model Selection Commands:**
```bash
# Check current model
/model

# Switch models mid-session
/model opus    # For complex architectural decisions
/model sonnet  # Switch back for implementation
```

**When to Use Opus:**
- Complex architectural decisions
- Multi-file refactoring with many dependencies
- Nuanced code review requiring deep understanding
- Debugging subtle, hard-to-find issues
- Tasks requiring strong reasoning across large contexts

**Sonnet as Your Default:**
Sonnet handles 90%+ of development tasks excellently. Use it for:
- Feature implementation
- Writing tests
- Code generation
- Standard refactoring
- Documentation

#### 3.2 Context Management Strategies (10 min)

**Strategy 1: Model Selection for Task Type**
```bash
# Sonnet for implementation (default)
claude --model sonnet
> "Implement the payment processing service with TDD"

# Opus for complex analysis
claude --model opus
> "Review the system architecture for scalability issues"
/model sonnet  # Switch back for implementation
```

**Strategy 2: Context Management**
```bash
# Compact conversation to save tokens
/compact

# Clear and start fresh for new task
/clear

# Use focused context
claude --add-dir src/Services  # Only relevant code
```

**Strategy 3: Efficient Prompting**
```markdown
# INEFFICIENT (verbose):
"I have a C# service that handles HOA violations. The service needs to
calculate fines based on how long the violation has been open. We use
30/60/90 day escalation. Can you help me write this method? I want it
to use compound interest at 10% monthly after 60 days."

# EFFICIENT (concise):
"Write C# method: CalculateFine(Violation v)
- 0-30d: $0
- 31-60d: $50
- 61-90d: $100 + 10% monthly compound
- 90+d: Same formula, flag for board"
```

**Strategy 4: Caching and Reuse**
```markdown
# Create reusable skills instead of repeating prompts
# Save to: .claude/skills/fine-calc/SKILL.md

---
name: fine-calc
description: Calculate violation fine
argument-hint: [days_since_report]
disable-model-invocation: true
---

Calculate fine for violation reported $ARGUMENTS days ago:
- 0-30d: $0 (warning)
- 31-60d: $50 (first notice)
- 61-90d: $100 + 10% compound (second notice)
- 90+d: Continue compound, flag board review

Show calculation breakdown.
```

**Productivity Dashboard Skill (.claude/skills/productivity-report/SKILL.md):**
```markdown
---
name: productivity-report
description: Generate productivity analysis report
argument-hint: [time_period]
disable-model-invocation: true
---

Analyze development productivity for $ARGUMENTS.

## Metrics to Include:
1. PRs merged and review turnaround
2. Test coverage trends
3. Bug escape rate
4. Common patterns in commits
5. Skill usage frequency

## Recommendations:
- Repeated tasks that should become skills
- Patterns that could be automated
- Context management improvements
```

### Part 4: Continuous Improvement Workflows (20 min)

#### 4.1 Building a Team Knowledge Base (10 min)

**Evolving Your CLAUDE.md:**

```markdown
# Project: HOA Management System
# Last Updated: 2025-01-22

## Quick Reference
- API: ASP.NET Core 10 Web API
- UI: Angular 17 standalone
- Tests: xUnit, FluentAssertions, Moq

## Lessons Learned

### 2025-01 Discoveries
- Use `[FromQuery]` not `[FromBody]` for GET params
- FluentAssertions `.Should().BeEquivalentTo()` ignores order
- EF Core 9 requires explicit `Include()` for navigation

### Common Pitfalls
1. Forgetting async/await on SaveChangesAsync
2. Not disposing DbContext in tests
3. Missing CORS configuration for Angular dev

## Effective Prompts (Tested)

### For Bug Fixes
"Find the bug causing [symptom]. Check:
1. Input validation
2. Null handling
3. Async issues
Show fix with test."

### For New Features
"Implement [feature] following:
1. Existing patterns in [similar file]
2. TDD approach
3. 80-90% coverage target"

## Team Conventions
- PR titles: type(scope): description
- Commits: conventional commits format
- Reviews: Always run /code-review skill first
```

**Knowledge Capture Skill (.claude/skills/capture-learning/SKILL.md):**
```markdown
---
name: capture-learning
description: Capture learning from current session
argument-hint: [topic]
disable-model-invocation: true
---

Document what we learned about $ARGUMENTS in this session.

## Format:
### Learning: $ARGUMENTS
**Date:** [Today]
**Context:** [What we were trying to do]

**Discovery:**
[Key insight or pattern discovered]

**Example:**
[Code snippet or command]

**When to Apply:**
[Future scenarios where this helps]

**Gotchas:**
[What to watch out for]

Append to CLAUDE.md under "## Lessons Learned"
```

#### 4.2 Measuring Productivity Gains (10 min)

**Metrics to Track:**

| Metric | How to Measure | Target |
|--------|----------------|--------|
| Time to PR | Commit to PR creation | -30% |
| Test Coverage | Coverlet reports | 80-90% |
| Bug Escape Rate | Production issues/sprint | -40% |
| Documentation | Pages auto-generated | +200% |
| Code Review Time | PR open to approve | -50% |

**Productivity Dashboard Skill (.claude/skills/productivity-report/SKILL.md):**
```markdown
---
name: productivity-report
description: Generate productivity metrics
argument-hint: [sprint_number]
disable-model-invocation: true
---

Analyze productivity for Sprint $ARGUMENTS.

## Metrics to Calculate:
1. PRs created with Claude assistance
2. Lines of code + test coverage
3. Bugs found during review vs production
4. Documentation pages generated
5. Time saved estimates

## Data Sources:
- Git history for code metrics
- GitLab MR data
- Jira tickets for bug tracking
- Coverage reports for trends

## Output Format:
### Sprint $ARGUMENTS Productivity Report

**Code Output:**
- Features delivered: X
- Tests written: X (Y% coverage)
- Documentation: X pages

**Quality:**
- Bugs caught in review: X
- Bugs in production: X
- Review feedback addressed: X%

**Efficiency:**
- Estimated hours saved: X
- Claude Code cost: $X
- ROI: X hours/$1 spent
```

### Part 5: Hands-On Workshop (20 min)

#### 5.1 Set Up Your Sandbox

```bash
# Copy example to sandbox
cd courses/ai-101-claude-code/sessions/week-8
cp -r examples sandbox
cd sandbox/hoa-workflow-automation

# Build and verify
dotnet build
dotnet test

# Start Claude
claude
```

#### 5.2 Workshop Exercises

**Exercise 1: Create Cross-Functional Skill (10 min)**
```
Create a skill that generates a weekly status report:
1. Takes sprint_number as argument
2. Summarizes completed work from git commits
3. Lists open violations requiring attention
4. Calculates late fee revenue this week
5. Notes any escalations to board

Save to: .claude/skills/weekly-status/SKILL.md
```

**Exercise 2: Build GitLab CI Job (5 min)**
```
Create a pipeline job that:
1. Triggers on MR to main branch
2. Runs dotnet test
3. Uses Claude to review changed files
4. Posts review as MR comment

Add to: .gitlab-ci.yml
```

**Exercise 3: Efficiency Audit (5 min)**
```
Analyze the example project for efficiency improvements:
1. Identify repeated prompts that could be skills
2. Find tasks that could use more focused context
3. Create a streamlined version of the project CLAUDE.md
```

### Part 6: Q&A and Reflection (5 min)

**Discussion Questions:**
- Which workflow will have the biggest impact for your team?
- What metrics should we track for ROI?
- How can we share learnings across teams?

**Quick Poll:**
1. Most valuable topic from today (1-5)?
2. Will you set up GitLab CI/CD integration?
3. What will you automate first?

## Key Takeaways

### Cross-Functional Skills Quick Reference
```
SUPPORT:
/ticket-summary <id>           -> Summarize for escalation
/draft-response <id> <type>    -> Write professional reply

PM:
/release-notes <from> <to>     -> Generate changelog
/sprint-plan <num> <pts>       -> Plan sprint items

ENGINEERING:
/api-docs <path>               -> Generate API documentation
/refactor <file> <type>        -> Safe refactoring
```

### GitLab CI/CD Patterns
```
CODE REVIEW:
- Trigger: merge_request_event
- Action: Review changed files
- Output: MR comment via API

DOCUMENTATION:
- Trigger: push to main + file changes
- Action: Update docs from code
- Output: Commit to main

COST MONITORING:
- Trigger: schedule (daily)
- Action: Check usage vs budget
- Output: Slack alert if needed
```

### Efficiency Checklist
```
[ ] Use /compact for context management
[ ] Create skills for repeated tasks
[ ] Clear context between unrelated tasks
[ ] Match model to task complexity (Sonnet vs Opus)
[ ] Track productivity metrics monthly
```

## Homework (Before Week 9)

### Required Tasks:
1. Create three cross-functional skills for your team
2. Set up a GitLab CI/CD pipeline with Claude
3. Implement auto-run-tests hook for your projects
4. Update your project CLAUDE.md with lessons learned
5. Share your best workflow in `#dx-training`

### Stretch Goals:
1. Build automated PR review pipeline
2. Create team productivity dashboard
3. Document team efficiency gains with metrics

### Skill Check:
Design a complete automation workflow that:
```
1. Triggers on new violation created
2. Auto-generates first notice letter
3. Schedules follow-up for 30 days
4. Logs all actions for SOC 2 compliance
5. Notifies property manager via Slack
```

## Resources

### Official Documentation
- [Claude Code CI/CD Integration](https://docs.anthropic.com/en/docs/claude-code/ci-cd)
- [Claude Code Best Practices](https://docs.anthropic.com/en/docs/claude-code/best-practices)
- [Context Management](https://docs.anthropic.com/en/docs/claude-code/context)

### RealManage Resources
- [Week 8 Examples](./examples/) - HOA Workflows project
- Slack: `#dx-training` for workflow help

### Additional Reading
- [GitLab CI/CD Documentation](https://docs.gitlab.com/ee/ci/)
- [GitLab CI/CD Variables](https://docs.gitlab.com/ee/ci/variables/)
- [Measuring Developer Productivity](https://www.microsoft.com/research/group/dev-analytics/)

## Success Metrics

### You're ready for Week 9 when you can:
- [ ] Create cross-functional skills for different team roles
- [ ] Set up GitLab CI/CD integration with Claude
- [ ] Manage context effectively for quality responses
- [ ] Build continuous improvement into your workflow
- [ ] Measure productivity gains objectively
- [ ] Share learnings through CLAUDE.md updates

### Red Flags (seek help if):
- [ ] GitLab CI/CD pipelines failing
- [ ] Claude responses degrading in quality
- [ ] Skills not providing consistent results
- [ ] Team not adopting new workflows
- [ ] Metrics showing no improvement

## Next Week Preview

**Week 9: Capstone Hackerspace**
- Choose from three capstone options
- Apply all skills learned in Weeks 1-8
- Build production-ready automation
- Present to team and get certified!

**Pre-work:** Review all weeks and identify your capstone project preference

---

## Instructor Notes

### Session Timing (2 hours)
- Part 1: Cross-Functional Use Cases - 25 min
- Part 2: GitLab CI/CD Integration - 30 min
- Part 3: Cost Optimization - 20 min
- Part 4: Continuous Improvement - 20 min
- Part 5: Hands-On Workshop - 20 min
- Part 6: Q&A and Reflection - 5 min
- **Total: 2h 00min**

### Key Points to Emphasize
- **Cross-functional value** - Claude helps everyone, not just developers
- **CI/CD integration** - Automation at scale
- **Cost awareness** - Use the right model for the job
- **Continuous improvement** - CLAUDE.md evolves over time

### Common Questions

**"How do we handle API keys in GitLab CI/CD?"**
- Store in CI/CD Variables (Settings > CI/CD > Variables)
- Mark as "Masked" and "Protected" for security
- Reference as `$ANTHROPIC_API_KEY` in scripts
- Never commit keys to repository

**"How do we ensure consistent output from skills?"**
- Include explicit output format in skill
- Test skills with multiple inputs
- Version control skill files
- Review and refine based on results

**"When should I use Sonnet vs Opus?"**
- Sonnet: Daily work, general development (faster)
- Opus: Complex architecture, nuanced analysis (more capable)
- Start with Sonnet, switch to Opus for hard problems

### Troubleshooting

**GitLab CI/CD pipelines not running:**
- Check `.gitlab-ci.yml` syntax (use CI Lint in GitLab)
- Verify rules/triggers are correct
- Check CI/CD settings are enabled for project
- Review pipeline logs for errors

**Response quality degrading:**
- Use `/compact` to clean up context
- Use `/clear` when switching unrelated tasks
- Check if using right model for task complexity
- Look for repeated similar prompts (create skills instead)

**Skills producing inconsistent results:**
- Make prompts more explicit
- Add example outputs
- Reduce ambiguity
- Test with edge cases

### Assessment
Quick check at end of session:
1. Name two cross-functional use cases
2. What triggers a GitLab CI/CD pipeline?
3. When should you use Opus vs Sonnet?
4. How do you keep Claude's context clean?

---

## 📚 Quick Resources

- [Glossary](../../resources/glossary.md) - Key terms and definitions
- [Decision Trees](../../resources/decision-trees.md) - When to use what
- [Troubleshooting](../../resources/troubleshooting.md) - Common issues and fixes
- [CLI Commands](../../resources/cli-commands.md) - Command cheatsheet

---

*End of Week 8 Session Plan*
*Next Session: Week 9 - Capstone Hackerspace*
