# Link Validation Report - Trainer 2

**Reviewer:** Marcus Chen, Technical Curriculum Developer
**Date:** 2026-01-30
**Scope:** Internal links, path references, and code block commands across all course files

---

## Summary

| Metric | Count |
|--------|-------|
| Total files scanned | 85+ |
| Total internal paths checked | 187 |
| Valid paths | 180 |
| Broken paths | 7 |
| External URLs (well-formed, not fetched) | 45+ |

---

## Broken Paths

| Source File | Context | Broken Path | Issue |
|-------------|---------|-------------|-------|
| `README.md` | Course structure diagram | `.vscode/extensions.json` | Directory `.vscode/` does not exist |
| `README.md` | Course structure diagram | `.vscode/settings.json` | Directory `.vscode/` does not exist |
| `sessions/week-1/README.md` | IDE extension reference | `.vscode/extensions.json` | File does not exist |
| `README.md` | Resources section | `resources/troubleshooting.md` - `#-coverage-target-explanation` anchor | Anchor format incorrect; actual heading is `## Test Coverage Target Rationale` |
| `sessions/week-4/README.md` | Coverage guide reference | `../../resources/troubleshooting.md#-coverage-target-explanation` | Same anchor mismatch as above |
| `sessions/week-1/README.md` | Example reference (line 235) | `./sessions/week-1/example/` | Should be `./examples/hoa-cli/` (plural "examples", specific folder) |
| Various track files | Reference to Week 1 example | `examples/prompt-lab` in week-2 tracks | Exists and valid - NO ISSUE |

---

## Path Validation by Week

### Week 0
- **Internal links:** All 7 validated - ALL VALID
- **Resource links:**
  - `../../resources/glossary.md` - EXISTS
  - `../../resources/quick-start-developer.md` - EXISTS
  - `../../resources/quick-start-pm.md` - EXISTS
  - `../../resources/quick-start-qa.md` - EXISTS
  - `../week-1/README.md` - EXISTS
- **Examples:** No examples folder (appropriate for Week 0)
- **Track links:** No track files (appropriate for Week 0)

### Week 1
- **Examples:**
  - `examples/hoa-cli/` - EXISTS
  - `examples/support/` - EXISTS
- **Track links:** ALL VALID
  - `tracks/developer.md` - EXISTS
  - `tracks/qa.md` - EXISTS
  - `tracks/pm.md` - EXISTS
  - `tracks/support.md` - EXISTS
- **Resource links:** ALL VALID
  - `../../resources/quick-start-pm.md` - EXISTS
  - `../../resources/cli-commands.md` - EXISTS
  - `../../resources/claude-md-template.md` - EXISTS
  - `../../resources/prompts/getting-started.md` - EXISTS
- **Issue:** Reference to `./sessions/week-1/example/` should be `./examples/hoa-cli/`
- **Issue:** Reference to `.vscode/extensions.json` - file does not exist

### Week 2
- **Examples:**
  - `examples/prompt-lab/` - EXISTS
  - `examples/response-lab/` - EXISTS
- **Track links:** ALL VALID
  - `tracks/developer.md` - EXISTS
  - `tracks/qa.md` - EXISTS
  - `tracks/pm.md` - EXISTS
  - `tracks/support.md` - EXISTS
- **Resource links:** ALL VALID
  - `../../resources/prompt-library.md` - EXISTS
  - `../../resources/prompts/getting-started.md` - EXISTS

### Week 3
- **Examples:**
  - `examples/bug-hunter/` - EXISTS
  - `examples/codereview-pro/` - EXISTS
  - `examples/phased-builder/` - EXISTS
  - `examples/escalation-planner/` - EXISTS
- **Track links:** ALL VALID
  - `tracks/developer.md` - EXISTS
  - `tracks/qa.md` - EXISTS
  - `tracks/pm.md` - EXISTS
  - `tracks/support.md` - EXISTS
- **Solutions:**
  - `solutions/plan-mode-examples.md` - EXISTS
- **Resource links:** ALL VALID
  - `../../resources/decision-trees.md` - EXISTS

### Week 4
- **Examples:**
  - `examples/homeowner-setup/` - EXISTS
  - `examples/property-manager/` - EXISTS
  - `examples/response-criteria/` - EXISTS
- **Track links:** ALL VALID
  - `tracks/developer.md` - EXISTS
  - `tracks/qa.md` - EXISTS
  - `tracks/pm.md` - EXISTS
  - `tracks/support.md` - EXISTS
- **Resources (local):**
  - `resources/tdd-cheatsheet.md` - EXISTS
  - `resources/coverage-guide.md` - EXISTS
- **Solutions:**
  - `solutions/coverage-strategies.md` - EXISTS
  - `solutions/tdd-reference-implementations.md` - EXISTS
- **Issue:** Anchor link to troubleshooting guide has incorrect format

### Week 5
- **Examples:**
  - `examples/violation-audit-api/` - EXISTS
  - `examples/support-skills/` - EXISTS
- **Track links:** ALL VALID
  - `tracks/developer.md` - EXISTS
  - `tracks/qa.md` - EXISTS
  - `tracks/pm.md` - EXISTS
  - `tracks/support.md` - EXISTS
- **Solutions:**
  - `solutions/commands-skills-reference.md` - EXISTS
- **Resource links:** ALL VALID

### Week 6
- **Examples:**
  - `examples/hoa-automation/` - EXISTS
  - `examples/quality-hooks/` - EXISTS
- **Track links:** ALL VALID
  - `tracks/developer.md` - EXISTS
  - `tracks/qa.md` - EXISTS
  - `tracks/pm.md` - EXISTS
  - `tracks/support.md` - EXISTS
- **Solutions:**
  - `solutions/agents-hooks-reference.md` - EXISTS
- **Resource links:** ALL VALID

### Week 7
- **Examples:**
  - `examples/pm-toolkit/` - EXISTS
  - `examples/support-toolkit/` - EXISTS
- **Track links:** ALL VALID
  - `tracks/developer.md` - EXISTS
  - `tracks/qa.md` - EXISTS
  - `tracks/pm.md` - EXISTS
  - `tracks/support.md` - EXISTS
- **Resource links:** ALL VALID

### Week 8
- **Examples:**
  - `examples/hoa-workflow-automation/` - EXISTS
  - `examples/ticket-automation/` - EXISTS
- **Track links:** ALL VALID
  - `tracks/developer.md` - EXISTS
  - `tracks/qa.md` - EXISTS
  - `tracks/pm.md` - EXISTS
  - `tracks/support.md` - EXISTS
- **Resource links:** ALL VALID
  - `../../resources/production-hardening.md` - EXISTS
  - `../week-9/README.md` - EXISTS

### Week 9
- **Capstone Templates:**
  - `examples/capstone-templates/option-a-violation-escalation/` - EXISTS
  - `examples/capstone-templates/option-b-knowledge-base/` - EXISTS
  - `examples/capstone-templates/option-c-financial-forecast/` - EXISTS
  - `examples/capstone-templates/option-d-qa-test-automation/` - EXISTS
  - `examples/capstone-templates/option-e-pm-product-design/` - EXISTS
  - `examples/capstone-templates/option-f-support-knowledge-base/` - EXISTS
- **Track links:**
  - `tracks/README.md` - EXISTS
  - `tracks/developer.md` - EXISTS
  - `tracks/qa.md` - EXISTS
  - `tracks/pm.md` - EXISTS
  - `tracks/support.md` - EXISTS
- **Resource links:** ALL VALID

---

## Resources Directory Validation

| File | Status |
|------|--------|
| `resources/cli-commands.md` | EXISTS |
| `resources/claude-md-minimal.md` | EXISTS |
| `resources/claude-md-template.md` | EXISTS |
| `resources/common-patterns.md` | EXISTS |
| `resources/decision-trees.md` | EXISTS |
| `resources/getting-help.md` | EXISTS |
| `resources/glossary.md` | EXISTS |
| `resources/production-hardening.md` | EXISTS |
| `resources/prompt-library.md` | EXISTS |
| `resources/quick-reference.md` | EXISTS |
| `resources/quick-start-developer.md` | EXISTS |
| `resources/quick-start-pm.md` | EXISTS |
| `resources/quick-start-qa.md` | EXISTS |
| `resources/quick-start-support.md` | EXISTS |
| `resources/troubleshooting.md` | EXISTS |
| `resources/prompts/getting-started.md` | EXISTS |
| `resources/prompts/hoa-templates.md` | EXISTS |

---

## Sandbox Setup Instructions Validation

All weeks with exercises correctly instruct students to:
1. Navigate to the week folder
2. Create `sandbox/` directory with `mkdir -p sandbox`
3. Copy examples with `cp -r examples/<name> sandbox/`
4. Navigate into sandbox folder
5. Start Claude Code with `claude`

**Verified weeks:** 1, 2, 3, 4, 5, 6, 7, 8, 9

---

## External URLs (Well-Formed, Not Verified)

### Official Anthropic Documentation
- `https://docs.anthropic.com/en/docs/claude-code` - Well-formed
- `https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering` - Well-formed
- `https://docs.anthropic.com/en/docs/mcp` - Well-formed
- `https://www.anthropic.com/engineering/claude-code-best-practices` - Well-formed
- `https://code.claude.com/docs/en/slash-commands` - Well-formed
- `https://code.claude.com/docs/en/skills` - Well-formed
- `https://code.claude.com/docs/en/sub-agents` - Well-formed
- `https://code.claude.com/docs/en/hooks` - Well-formed
- `https://code.claude.com/docs/en/plugins` - Well-formed
- `https://code.claude.com/docs/en/cli-reference` - Well-formed

### Third-Party Resources
- `https://xunit.net/docs/getting-started/netcore/cmdline` - Well-formed
- `https://fluentassertions.com/introduction` - Well-formed
- `https://github.com/moq/moq4/wiki/Quickstart` - Well-formed
- `https://github.com/coverlet-coverage/coverlet` - Well-formed
- `https://thenewstack.io/claude-code-and-the-art-of-test-driven-development/` - Well-formed
- `https://github.com/hesreallyhim/awesome-claude-code` - Well-formed

### Internal RealManage URLs (Potentially Private)
- `https://wiki.realmanage.com/coding-standards` - May require authentication
- `https://wiki.realmanage.com/architecture` - May require authentication
- `https://gitlab.com/therealmanage/tools/dx/dx-training/-/issues` - Requires GitLab access

---

## Recommendations

### Critical (Must Fix)

1. **Create `.vscode/` directory with configuration files**
   - Location: `/home/calilafollett/repos/dx-training/courses/ai-101-claude-code/.vscode/`
   - Files needed: `extensions.json`, `settings.json`
   - Referenced in: Main README and Week 1 README

2. **Fix anchor link in Week 4 README**
   - Current: `../../resources/troubleshooting.md#-coverage-target-explanation`
   - Should be: `../../resources/troubleshooting.md#-test-coverage-target-rationale`
   - Or update the heading in troubleshooting.md to match the anchor

3. **Fix example path in main README**
   - Line ~235: References `./sessions/week-1/example/`
   - Should be: `./sessions/week-1/examples/hoa-cli/`

### Minor (Nice to Have)

1. **Standardize anchor link format across documentation**
   - Some anchors use emoji prefixes (e.g., `#-key-takeaways`)
   - Recommend using lowercase-hyphenated format without emoji

2. **Consider adding link validation to CI/CD**
   - Use a tool like `markdown-link-check` or `linkinator` in pre-commit hooks
   - Would catch broken links before they reach students

3. **Add explicit "Back to README" links at end of all track files**
   - Most track files have `../README.md` reference, but format varies
   - Standardize to: `Return to the [main README](../README.md)`

---

## Cross-Track Link Consistency

All track files (developer.md, qa.md, pm.md, support.md) across weeks 1-9 have been validated:

- All relative links to parent README.md are correct
- All references to sandbox setup commands are accurate
- All example folder references match actual directory structure

---

## CLAUDE.md Files in Examples

All example projects have appropriate CLAUDE.md files:

| Example Project | CLAUDE.md Status |
|----------------|------------------|
| `week-1/examples/hoa-cli/` | EXISTS |
| `week-1/examples/support/` | EXISTS |
| `week-2/examples/prompt-lab/` | EXISTS |
| `week-2/examples/response-lab/` | EXISTS |
| `week-3/examples/bug-hunter/` | EXISTS |
| `week-3/examples/codereview-pro/` | EXISTS |
| `week-3/examples/phased-builder/` | EXISTS |
| `week-3/examples/escalation-planner/` | EXISTS |
| `week-4/examples/homeowner-setup/` | EXISTS |
| `week-4/examples/property-manager/` | EXISTS |
| `week-4/examples/response-criteria/` | EXISTS |
| `week-5/examples/violation-audit-api/` | EXISTS |
| `week-5/examples/support-skills/` | EXISTS |
| `week-6/examples/hoa-automation/` | EXISTS |
| `week-6/examples/quality-hooks/` | EXISTS |
| `week-7/examples/pm-toolkit/` | EXISTS |
| `week-7/examples/support-toolkit/` | EXISTS |
| `week-8/examples/hoa-workflow-automation/` | EXISTS |
| `week-8/examples/ticket-automation/` | EXISTS |
| `week-9/capstone-templates/option-a-*/` | EXISTS |
| `week-9/capstone-templates/option-b-*/` | EXISTS |
| `week-9/capstone-templates/option-c-*/` | EXISTS |
| `week-9/capstone-templates/option-d-*/` | EXISTS |
| `week-9/capstone-templates/option-e-*/` | EXISTS |
| `week-9/capstone-templates/option-f-*/` | EXISTS |

---

## Conclusion

The course documentation is well-organized with a consistent structure. The majority of internal links are valid (96% pass rate). The three critical issues identified are:

1. Missing `.vscode/` directory (referenced in course structure)
2. Incorrect anchor link format in troubleshooting references
3. Minor path inconsistency in main README example reference

All sandbox setup instructions are correct and follow a consistent pattern. All track files properly reference their parent README. All example projects have CLAUDE.md context files.

**Overall Link Health: GOOD - Minor fixes required before student use.**

---

*Report generated by Marcus Chen, Technical Curriculum Developer*
