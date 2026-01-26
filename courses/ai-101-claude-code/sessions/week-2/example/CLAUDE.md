# Week 2: Prompt Lab Context

## Prime Directives
- **YOU MUST** follow Red/Green/Refactor TDD Best Practics using xUnit
- **YOU MUST** implement C# code in a Best Practices language idiomatic fashion
- **YOU MUST** help students understand effective prompting techniques
- **YOU MUST** demonstrate the difference between vague and specific prompts
- **YOU MUST** emphasize the importance of including test requirements (80-90% coverage)
- **YOU MUST** show how structure (XML tags, markdown) improves prompt clarity

## About This Example
This is a Prompt Lab application for teaching prompt engineering. It includes:
- PromptAnalyzer: Analyzes prompt quality (has intentional scoring bugs)
- TemplateManager: Manages reusable prompt templates
- HoaPromptExamples: Before/after examples for RealManage scenarios

## Known Issues (For Students to Discover)
1. PromptAnalyzer scoring gives equal weight to all factors (should prioritize specificity)
2. Context detection is too simplistic (just checks length > 50)
3. Template variable replacement doesn't handle edge cases

## Learning Goals
Students should learn to:
- Write clear, specific prompts with measurable outcomes
- Use XML tags and markdown for structure
- Include all necessary context (language, framework, patterns)
- Always request tests with specific coverage requirements
- Iterate and refine prompts based on output quality

## Prompt Engineering Best Practices
1. Start with an action verb (create, implement, fix, refactor)
2. Specify the technology stack explicitly
3. Include acceptance criteria and constraints
4. Request specific output format when needed
5. Always include testing requirements

## RealManage Context
- HOA domain with violations, payments, residents
- 30/60/90 day escalation rules
- 10% monthly compound interest on late fees
- 30-day grace period standard
- 80-90% test coverage requirement (non-negotiable)