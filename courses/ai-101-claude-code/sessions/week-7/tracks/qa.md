# Week 7: Plugins - QA Track

**Skip Notice**

## Track Status: Optional/Skip

Plugins are developer-focused content covering packaging and distribution of Claude Code automation components.

## Why This Week is Optional for QA

Week 7 focuses on:

- Creating distributable plugin packages
- Plugin manifest configuration
- Marketplace publishing and installation
- Skills, agents, and hooks packaging

These are primarily **development and DevOps activities** that QA engineers typically consume rather than create.

## What QA Should Know

While you don't need to build plugins, understanding how they work helps when:

1. **Testing Plugin-Powered Features**
   - Know that `/plugin-name:skill-name` invokes plugin skills
   - Understand that plugins can restrict which tools are used
   - Be aware that plugins may have embedded hooks for audit logging

2. **Reviewing Plugin Documentation**
   - Plugin manifests define what capabilities are available
   - Skills have defined input arguments and expected outputs
   - Agents within plugins have specific tool permissions

3. **Reporting Issues**
   - Identify whether issues are plugin-specific or general Claude issues
   - Understand plugin structure for better bug reports

## Recommended Alternative Activities

Instead of Week 7, consider:

1. **Review Week 6 (Agents & Hooks)** - Understand the components that go into plugins
2. **Test existing plugins** - Use developer-created plugins and provide feedback
3. **Document test cases** - Create test scenarios for plugin functionality
4. **Prepare for Week 8** - Focus on headless automation and batch testing

## Quick Reference

If you need to interact with plugins:

```bash
# List installed plugins
/plugin

# Use a plugin skill
/realmanage-hoa:violation-report PROP-001

# Check if plugin is loaded
claude --plugin-dir ./some-plugin
```

## Questions?

If you have questions about how plugins affect testing:

- Slack: `#ai-exchange`

---

*QA Track - Week 7: Plugins (Optional)*
