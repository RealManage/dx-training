# Week 2 Example: Prompt Lab 🎯

A hands-on tool for learning effective prompt engineering with Claude Code.

## What This Example Teaches

This interactive Prompt Lab helps you:
- Analyze prompt quality and structure
- Compare different prompting approaches
- Use templates for consistent results
- Learn from RealManage-specific examples

## Features

1. **Prompt Analyzer** - Scores your prompts on clarity, context, specificity, and structure
2. **Prompt Comparison** - A/B test different prompt styles
3. **Template Manager** - Use and customize reusable prompt templates
4. **HOA Examples** - Learn from real before/after prompt examples
5. **Refinement Tracker** - Iterate and improve prompts with guidance

## Getting Started

```bash
# Build and run
dotnet build
dotnet run

# Try these exercises:
# 1. Analyze a vague prompt vs specific prompt
# 2. Use the "Service with TDD" template
# 3. Review HOA-specific examples
# 4. Practice iterative refinement
```

## Intentional Learning Opportunities

This example includes some "bugs" for you to discover and fix:
- The prompt analyzer's scoring algorithm could be improved
- Context detection is overly simplistic
- Template variable replacement has edge cases

## Key Concepts to Practice

### 1. Specificity Matters
```
Bad:  "Write a payment service"
Good: "Create a C# PaymentService class using .NET 9 with repository pattern, 
      async/await, and xUnit tests achieving 95% coverage"
```

### 2. Structure Helps
Use XML tags or markdown to organize complex prompts:
```xml
<requirements>
- Language: C# .NET 9
- Pattern: Repository
- Testing: xUnit with 95% coverage
</requirements>
```

### 3. Domain Context is Crucial
Always include RealManage-specific rules:
- 30-day grace period for payments
- 10% monthly compound interest on late fees
- 30/60/90 day violation escalation

## Exercises to Try

1. **Vague to Specific**: Take "make a violation tracker" and refine it 3 times
2. **Template Practice**: Fill in the API Endpoint template for a real feature
3. **Bug Hunt**: Find and fix the scoring algorithm issue in PromptAnalyzer
4. **Create Your Own**: Add a new template to the TemplateManager

## What Makes a Good Prompt?

✅ **Clear objective** - Starts with an action verb
✅ **Specific requirements** - Languages, frameworks, patterns
✅ **Acceptance criteria** - Measurable outcomes like test coverage
✅ **Domain context** - Business rules and constraints
✅ **Structure** - Well-organized with lists or sections

## Next Steps

After mastering prompt engineering:
1. Save your best prompts in your project's CLAUDE.md
2. Build a personal prompt library
3. Share effective prompts with your team
4. Move on to Week 3: Plan Mode & Exploration

Remember: The quality of AI output directly correlates with prompt quality!