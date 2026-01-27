# Week 7: Plugins - PM Track

**Skip Notice**

## Track Status: Optional/Skip

Plugins are developer-focused content covering the technical packaging and distribution of Claude Code automation components.

## Why This Week is Optional for PMs

Week 7 focuses on:
- Creating plugin directory structures
- Writing plugin manifests (JSON configuration)
- Packaging skills, agents, and hooks
- Marketplace distribution and versioning

These are **technical implementation details** that PMs typically don't need to perform directly.

## What PMs Should Know

While you don't need to build plugins, understanding the concept helps when:

1. **Planning Features**
   - Plugins enable teams to share automation across projects
   - A plugin can bundle related functionality (e.g., "violation-management" plugin)
   - Plugins can be versioned and distributed via marketplace

2. **Evaluating Team Productivity**
   - Well-designed plugins reduce repeated work
   - Teams can share domain-specific automation
   - Plugin adoption indicates automation maturity

3. **Communicating with Engineering**
   - Understand that plugins contain skills, agents, and hooks
   - Know that plugins enable cross-team sharing
   - Recognize that plugin development has overhead (worth it for reusable tools)

## Key Concepts for PMs

### What is a Plugin?
A distributable package containing:
- **Skills** - Enhanced commands with templates and automation
- **Agents** - Specialized AI assistants for specific tasks
- **Hooks** - Automated actions triggered by events

### Why Plugins Matter
| Without Plugins | With Plugins |
|-----------------|--------------|
| Each dev creates their own commands | Team shares standardized tools |
| Inconsistent automation | Consistent, versioned automation |
| Hard to maintain | Centralized updates |
| Knowledge silos | Shared best practices |

### Example Use Case
The "realmanage-hoa" plugin could include:
- `/violation-workflow` skill for processing violations
- `/board-report` skill for generating meeting documents
- Security-auditor agent for compliance checks
- Audit hooks for SOC 2 logging

## Recommended Alternative Activities

Instead of Week 7, consider:

1. **Review Week 8 Preview** - Headless automation and real-world workflows are more PM-relevant
2. **Identify plugin opportunities** - What repeated tasks could be automated?
3. **Document requirements** - What should a "violation-management" plugin do?
4. **Plan team adoption** - How would plugins improve your team's workflow?

## Questions for Engineering

When discussing plugins with your engineering team:

1. "What plugins has the team built?"
2. "Which automation should we package as a plugin?"
3. "Are we using any shared plugins from other teams?"
4. "What's the ROI on plugin development vs. ad-hoc automation?"

## Questions?

If you have questions about how plugins affect product strategy:
- Slack: `#dx-training`
- Email: dx-team@realmanage.com

---

*PM Track - Week 7: Plugins (Optional)*
