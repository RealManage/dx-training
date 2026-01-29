# AI Code Review Artifact Guide

## Overview

Code review artifacts document AI-assisted reviews. They capture learnings, ensure reviews happened, and help us measure whether AI assistance improves code quality.

**Why bother?** Artifacts create a knowledge base of patterns and issues. Over time, they help the whole team learn from each other's reviews.

## Artifact Levels

Not every change needs a full review document. Use the appropriate level:

| Change Type | Artifact Level | Example |
| ----------- | -------------- | ------- |
| Major feature | Full | New payment processing flow |
| Bug fix | Standard | Fix null reference in validation |
| Config/dependency update | Lightweight | Bump package version |
| Typo/formatting | None | Fix spelling in comment |

### When in Doubt

Ask yourself: "Did AI help me catch something I might have missed?"
- **Yes** → Document it (at least lightweight)
- **No** → Skip the artifact

## File Location

```
<repo-root>/
└── docs/
    └── code-reviews/
        └── <JIRA-ID>-code-review-summary.md
```

**Example:** `docs/code-reviews/HOA-1234-code-review-summary.md`

## Full Artifact Template

For major features and significant changes:

```markdown
# Code Review Summary: <JIRA-ID>

## Issue
**Jira:** [<JIRA-ID>](https://realmanage.atlassian.net/browse/<JIRA-ID>)
**Title:** <Issue title>
**Author:** <Your name>
**Date:** <YYYY-MM-DD>

## AI Tool Used
- **Tool:** Claude Code
- **Model:** <sonnet/opus>
- **Command:** `/review` or manual prompting

## Files Reviewed
- `path/to/file1.cs`
- `path/to/file2.cs`

## Findings

### Critical
<!-- Must fix before merge -->
- None (or list issues)

### High
<!-- Should fix -->
1. **[File:Line]** - Description
   - **Fix:** What was done
   - **Status:** Fixed in commit <hash>

### Medium
<!-- Should address -->
- None (or list issues)

### Low
<!-- Minor suggestions -->
- None (or list issues)

## Test Coverage
- **Before:** X%
- **After:** Y%
- **New tests:** List of test methods added

## Summary
Brief summary of the review - what was learned, patterns discovered, etc.

---
*Generated with Claude Code*
```

## Standard Artifact Template

For bug fixes and moderate changes:

```markdown
# Code Review Summary: <JIRA-ID>

**Author:** <Your name>
**Date:** <YYYY-MM-DD>
**Tool:** Claude Code

## Files Reviewed
- `path/to/file.cs`

## Findings
1. **[File:Line]** - Description
   - **Fix:** What was done

## Summary
Brief note on what AI caught or helped with.

---
*Generated with Claude Code*
```

## Lightweight Artifact Template

For config changes, dependency updates, trivial fixes:

```markdown
# Code Review: <JIRA-ID>

**Tool:** Claude Code
**Files:** <list files>
**Findings:** None (trivial change)
```

That's it. Three lines. Proves the review happened without wasting time on documentation.

## How to Generate

### Using /review Command (Recommended)
```bash
# In your repo with changes staged
/review HOA-1234
```

The skill will:
1. Analyze your changes
2. Generate the appropriate artifact
3. Save to `docs/code-reviews/`

### Manual Prompting
```
Review the changes for HOA-1234. Focus on:
- Code quality and patterns
- Test coverage
- Security concerns
- Performance implications

Save the review to docs/code-reviews/HOA-1234-code-review-summary.md
```

## Workflow

### Before Creating MR
1. Complete your implementation
2. Run `/review <JIRA-ID>` (or prompt manually)
3. Address any findings
4. Commit the artifact WITH your changes

### Commit Message
```
HOA-1234: Implement violation late fee calculation

- Add CalculateLateFee method
- Add unit tests
- Include AI review artifact

Co-Authored-By: Claude <noreply@anthropic.com>
```

### MR Description
```markdown
## Code Review
AI-assisted review completed. See: `docs/code-reviews/HOA-1234-code-review-summary.md`
```

## FAQ

**Q: Do I need an artifact for every single MR?**
A: No. Use your judgment. If AI helped you catch something, document it. If it was a trivial change, skip it or use the lightweight template.

**Q: What if there are no findings?**
A: Use the lightweight template. "Findings: None" is valid and useful - it shows the review happened.

**Q: Can I use a different AI tool?**
A: Yes. Update the "Tool" field accordingly.

**Q: What if I forget to include the artifact?**
A: Add it in a follow-up commit. No big deal.

**Q: Who reads these artifacts?**
A: Your future self (when debugging), teammates (when learning patterns), and the DX team (when measuring impact).

## Why This Matters

We're not asking for artifacts to check up on you. We want to:

1. **Build a knowledge base** - Artifacts capture patterns and gotchas
2. **Measure impact** - Do AI reviews actually catch bugs?
3. **Share learnings** - Your findings help teammates avoid the same issues
4. **Improve the process** - Your feedback makes the tools better

If artifacts ever feel like busywork, tell us. We want them to be useful, not bureaucratic.
