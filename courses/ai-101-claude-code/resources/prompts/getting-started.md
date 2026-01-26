# Getting Started Prompts 🚀

Essential prompts for your first week with Claude Code at RealManage.

## 🎯 First Session Prompts

### Understanding a Codebase
```
Can you explain the architecture of this project?
What are the main components and how do they interact?
```

### Finding Specific Code
```
Where is the payment processing logic implemented?
Show me all the API endpoints related to violations.
```

### Understanding Business Logic
```
How does the late fee calculation work?
Explain the violation escalation workflow.
```

## 📝 Basic Code Generation

### Simple Function
```
Write a C# method to calculate compound interest monthly.
Include unit tests with edge cases.
```

### Basic CRUD
```
Create a repository for managing HOA board members.
Include async methods and proper error handling.
```

### Simple Component
```
Create an Angular component to display a list of violations.
Use OnPush change detection and include tests.
```

## 🔍 Code Review Prompts

### Quick Review
```
Review this code for obvious issues.
```

### Detailed Review
```
Review this code for:
- Logic errors
- Performance issues  
- Security concerns
- Test coverage
```

### Standards Check
```
Does this code follow C# best practices?
Check for RealManage coding standards compliance.
```

## 🐛 Debugging Prompts

### Error Analysis
```
I'm getting this error: [paste error]
What's causing it and how do I fix it?
```

### Test Failure
```
This test is failing: [paste test output]
Help me understand why and fix the implementation.
```

### Performance Issue
```
This query is running slowly.
How can I optimize it?
```

## 📚 Learning Prompts

### Concept Explanation
```
Explain dependency injection in ASP.NET Core.
How does Angular's change detection work?
```

### Best Practices
```
What's the best way to handle errors in async C# code?
How should I structure an Angular service?
```

### Pattern Examples
```
Show me an example of the repository pattern in C#.
Demonstrate proper use of RxJS in Angular.
```

## 🎨 Refactoring Prompts

### Simple Refactor
```
Make this code more readable.
Extract this logic into a separate method.
```

### Pattern Application
```
Refactor this to use the repository pattern.
Convert this to use async/await.
```

### Code Cleanup
```
Remove code duplication from these methods.
Simplify this complex conditional logic.
```

## 📖 Documentation Prompts

### Code Comments
```
Add XML documentation to this C# class.
Add JSDoc comments to this TypeScript file.
```

### README Creation
```
Write a README for this project.
Include setup instructions and usage examples.
```

### API Documentation
```
Document this API endpoint.
Include request/response examples.
```

## 🧪 Testing Prompts

### Unit Tests
```
Write unit tests for this method.
Aim for 80-90% coverage with edge cases.
```

### Integration Tests
```
Create integration tests for this API endpoint.
Include authentication and error scenarios.
```

### Test Data
```
Generate test data for this scenario.
Create both valid and invalid examples.
```

## 💡 Tips for New Users

### Start Simple
Begin with small, specific requests:
- ❌ "Fix everything"
- ✅ "Fix the null reference error on line 42"

### Provide Context
Include relevant information:
- ❌ "Write a function"
- ✅ "Write a C# function to calculate HOA late fees with 10% monthly compound interest"

### Iterate
Refine your prompts based on results:
1. Try initial prompt
2. If output isn't quite right, add constraints
3. Save successful prompts for reuse

### Be Specific About Requirements
- ❌ "Add tests"
- ✅ "Add xUnit tests with 80-90% coverage using FluentAssertions"

## 🎯 Quick Wins

Try these in your first session:

1. **Code Understanding**
   ```
   What does this file do?
   ```

2. **Bug Fix**
   ```
   There's a bug in the CalculateFine method. Can you fix it?
   ```

3. **Test Generation**
   ```
   Write tests for the ViolationService class.
   ```

4. **Documentation**
   ```
   Add comments explaining this complex logic.
   ```

5. **Refactoring**
   ```
   Make this code follow C# conventions.
   ```

---

**Remember:** The quality of Claude's output depends on the quality of your prompts. Be clear, specific, and iterative!