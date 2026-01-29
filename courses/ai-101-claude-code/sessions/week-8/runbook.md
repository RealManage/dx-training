# Week 8: Real-World Automation - Trainer Runbook

## Pre-Session Checklist

### For Instructors

- [ ] Test all example projects build without warnings
- [ ] Verify automation script examples work
- [ ] Have workflow examples ready
- [ ] Monitor `#ai-exchange` Slack channel

---

## Instructor Notes

### Key Points to Emphasize

- **Cross-functional value** - Claude helps everyone, not just developers
- **Headless automation** - CLI scripting for batch processing
- **Cost awareness** - Use the right model for the job
- **Continuous improvement** - CLAUDE.md evolves over time

### Common Questions

> **"How do we handle API keys in automation scripts?"**

- Use environment variables (never hardcode)
- Store in secure credential managers
- Reference as `$ANTHROPIC_API_KEY` in scripts
- Never commit keys to repository

> **"How do we ensure consistent output from skills?"**

- Include explicit output format in skill
- Test skills with multiple inputs
- Version control skill files
- Review and refine based on results

> **"When should I use Sonnet vs Opus?"**

- Sonnet: Daily work, general development (faster)
- Opus: Complex architecture, nuanced analysis (more capable)
- Start with Sonnet, switch to Opus for hard problems

### Troubleshooting

**Automation scripts not working:**

- Check script has execute permissions (`chmod +x`)
- Verify Claude CLI is installed and authenticated
- Check environment variables are set
- Test with `--verbose` flag for debugging

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
2. What CLI flags enable headless automation?
3. When should you use Opus vs Sonnet?
4. How do you keep Claude's context clean?
