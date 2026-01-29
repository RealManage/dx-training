# Week 6: Agents & Hooks - PM Track

**Duration:** N/A
**Audience:** Product Managers, Project Managers, Business Analysts

---

## Skip Notice

**This week is optional for PMs.**

Week 6 covers Agents and Hooks, which are technical implementation details primarily used by developers and QA engineers for:

- Custom AI agents with specific tools and permissions
- Automated hooks that run before/after Claude operations
- Test automation and audit logging

---

## What You Should Know

Even though this content is optional, here's a quick summary of what your team will be learning:

### Agents

Agents are specialized AI assistants that developers create for specific tasks:

- **Code reviewer** - Automatically reviews code for quality issues
- **Security auditor** - Scans code for vulnerabilities
- **Test writer** - Generates unit tests

**Why it matters:** Your dev team can create agents that enforce coding standards and catch issues earlier in the development process.

### Hooks

Hooks are automated actions that run when Claude performs operations:

- **Pre-operation hooks** - Run before Claude does something (can block dangerous actions)
- **Post-operation hooks** - Run after Claude completes (can trigger tests, logging)

**Why it matters:** Hooks enable SOC 2 compliance by automatically logging all AI operations. They also ensure tests run after every code change.

---

## Recommended Path

Instead of attending Week 6, you can:

1. **Review the summary above** - Understand what agents and hooks do at a high level
2. **Attend Week 7** - Plugins are more relevant to understanding how teams share automation
3. **Skip to Week 8** - The capstone project will demonstrate these concepts in context

---

## Questions for Your Team

If you want to understand how agents and hooks affect your projects, ask developers:

1. "What agents have you created for code review and testing?"
2. "What dangerous operations are blocked by hooks?"
3. "How are we logging AI operations for SOC 2 compliance?"
4. "Do tests run automatically after every edit?"

---

## Resources

If you want to learn more despite the skip notice:

- [Week 6 Full README](../README.md) - Complete technical content
- [Developer Track](./developer.md) - Full developer content
- [Glossary: Agent](../../../resources/glossary.md#agent-claude-code) - Term definition
- [Glossary: Hook](../../../resources/glossary.md#hook) - Term definition

---

*Next relevant session: Week 7 - Plugins (recommended for PMs)*
