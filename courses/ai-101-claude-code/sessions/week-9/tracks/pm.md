# Product Management Track - Capstone Guidance

## Recommended Option

**Option E: Product Design & Documentation** is designed specifically for Product Managers.

## Why Option E?

This option lets you use Claude Code for what PMs do best--defining products and communicating requirements--without requiring coding skills:

- Create comprehensive product specifications
- Generate user stories with acceptance criteria
- Build stakeholder-ready documentation
- Learn AI-assisted product workflows

**This is a NON-CODING capstone.** Your deliverables are documents, not code.

## What You'll Build

### 1. Product Requirements Document (PRD)

- Problem statement with user research
- Solution overview and scope
- Success metrics and KPIs
- Technical constraints and dependencies
- Timeline and milestones

### 2. User Story Mapping

- Epics aligned to business goals
- User stories following INVEST principles
- Acceptance criteria in Gherkin format
- Priority and effort estimates
- Release planning suggestions

### 3. Stakeholder Documentation

- Executive summary (1-pager)
- Technical specification for engineering
- Training materials outline
- FAQ for support teams
- Presentation deck outline

### 4. Process Documentation

- How you used Claude Code for PM work
- Prompts that worked well
- Iteration and refinement process
- Lessons learned

## Skills You'll Use

| Week | Skill Applied |
| ---- | ------------- |
| Week 2 | Prompt engineering for requirements |
| Week 3 | Plan Mode for structuring documents |
| Week 5 | Custom PM skills (release-notes, user-stories, etc.) |
| Week 8 | Interactive skill workflows and automation design |

### Required: Bring Your Week 5 Skills

Your capstone must include at least one custom PM skill from Week 5:

- `/release-notes` - Generate stakeholder-friendly release notes
- `/meeting-actions` - Extract action items from meeting notes
- `/sprint-summary` - Create executive sprint summaries
- `/user-stories` - Break epics into well-formed stories

**Show how you used these skills in your capstone work.**

## Getting Started

```bash
# Set up your workspace
cd courses/ai-101-claude-code/sessions/week-9
cp -r examples/capstone-templates/option-e-pm-product-design sandbox/
cd sandbox/option-e-pm-product-design

# Start Claude Code
claude
```

### First Steps

1. **Choose your feature**: Pick an HOA capability to specify (suggestions below)
2. **Start with the PRD**: Define the problem and solution
3. **Generate user stories**: Use Claude to create comprehensive stories
4. **Build stakeholder docs**: Create role-specific documentation
5. **Document your process**: Capture how Claude helped

## Suggested Features to Specify

Choose ONE of these (or propose your own):

1. **Resident Self-Service Portal**
   - View account balance and payment history
   - Submit maintenance requests
   - Access community documents
   - Receive notifications

2. **Board Meeting Management**
   - Agenda generation from pending items
   - Voting and decision tracking
   - Minutes auto-generation
   - Action item follow-up

3. **Violation Appeals Process**
   - Online appeal submission
   - Evidence upload and review
   - Hearing scheduling
   - Decision notification

4. **Community Amenity Booking**
   - Pool, clubhouse, tennis court reservations
   - Availability calendar
   - Deposit and fee collection
   - Usage reporting

## Example Prompts

```text
# PRD generation
"Create a comprehensive PRD for a Resident Self-Service Portal
for HOA management. Include problem statement, user personas,
functional requirements, and success metrics."

# User story generation
"Generate user stories for the resident payment feature.
Follow INVEST principles and include acceptance criteria
in Gherkin format (Given/When/Then)."

# Stakeholder documentation
"Create an executive summary for this feature that a
non-technical board member could understand in 2 minutes."

# Acceptance criteria
"Write detailed acceptance criteria for: 'As a resident,
I want to view my payment history so I can track my dues.'"
```

## Custom Skill: `/generate-user-stories`

Create this skill to automate story generation:

```markdown
---
name: generate-user-stories
description: Generate user stories from a feature description
argument-hint: [feature_name]
---

Generate user stories for the $0 feature.

## Output Format
For each story, provide:
1. Story title
2. As a [user], I want [goal] so that [benefit]
3. Acceptance criteria (Given/When/Then)
4. Priority (Must/Should/Could)
5. Estimated complexity (S/M/L)

## Quality Standards
- Stories follow INVEST principles
- Acceptance criteria are testable
- No implementation details in stories
- Clear user benefit stated
```

## Success Criteria Checklist

```text
[ ] PRD covers problem, solution, and success metrics
    - Clear problem statement with evidence
    - User personas defined
    - Functional requirements listed
    - Non-functional requirements noted
    - Success metrics are measurable

[ ] User stories follow INVEST principles
    - Independent: Can be developed separately
    - Negotiable: Details can be discussed
    - Valuable: Delivers user value
    - Estimable: Can be sized
    - Small: Fits in a sprint
    - Testable: Has clear acceptance criteria

[ ] Acceptance criteria are testable and clear
    - Written in Gherkin format
    - Cover happy path and edge cases
    - No ambiguous language
    - QA can write tests from them

[ ] Documentation is stakeholder-ready
    - Executive summary is concise
    - Technical spec has enough detail
    - Training outline is actionable
    - FAQ addresses real questions

[ ] All artifacts generated with Claude assistance
    - Document how Claude helped
    - Note prompts that worked well
    - Capture iteration process

[ ] No coding required for submission
    - All deliverables are documents
    - Markdown or standard doc formats
    - Presentation outline (not slides)
```

## Deliverables Summary

1. **PRD** (`docs/PRD.md`)
   - Complete product requirements document
   - 5-10 pages covering all aspects

2. **User Story Map** (`docs/user-stories.md`)
   - Epics and stories organized
   - Acceptance criteria for each
   - Priority and estimates

3. **Stakeholder Docs** (`docs/stakeholder/`)
   - `executive-summary.md`
   - `technical-spec.md`
   - `training-outline.md`
   - `faq.md`

4. **Custom PM Skill** (`.claude/skills/<skill-name>/SKILL.md`)
   - At least one PM automation skill
   - Used in your capstone workflow
   - Examples: `/user-stories`, `/release-notes`, `/sprint-summary`

5. **Process Documentation** (`docs/process.md`)
   - How you used Claude Code and your custom skills
   - Effective prompts
   - Lessons learned

6. **Presentation Outline** (`docs/presentation-outline.md`)
   - Slide-by-slide outline
   - Key talking points
   - Demo of your custom skill in action

## Tips for Success

- **Be specific in prompts**: The more context you give Claude, the better the output
- **Iterate on outputs**: First drafts are starting points, not final products
- **Add domain knowledge**: Include HOA-specific details Claude might miss
- **Review for accuracy**: Validate Claude's suggestions against real business rules
- **Document your process**: This is valuable for teaching others

## Common Pitfalls

- Accepting Claude's first output without refinement
- Missing HOA-specific requirements (compliance, board approval)
- Writing stories that are too large or too vague
- Forgetting to include non-functional requirements
- Not documenting the Claude-assisted process

---

**Questions?** Ask in `#ai-exchange` on Slack.
