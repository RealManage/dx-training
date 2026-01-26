namespace RealManage.PromptLab;

public class TemplateManager
{
    private readonly Dictionary<string, PromptTemplate> _templates;
    
    public TemplateManager()
    {
        _templates = InitializeTemplates();
    }
    
    public List<string> GetAvailableTemplates()
    {
        return _templates.Keys.ToList();
    }
    
    public PromptTemplate GetTemplate(string name)
    {
        return _templates.TryGetValue(name, out var template) 
            ? template 
            : new PromptTemplate { Name = "Empty", Content = "", Variables = new() };
    }
    
    public string FillTemplate(string templateName, Dictionary<string, string> values)
    {
        var template = GetTemplate(templateName);
        var result = template.Content;
        
        foreach (var kvp in values)
        {
            // Bug: Simple string replacement can cause issues with nested variables
            result = result.Replace($"<{kvp.Key.ToUpper()}>", kvp.Value);
        }
        
        return result;
    }
    
    private Dictionary<string, PromptTemplate> InitializeTemplates()
    {
        return new Dictionary<string, PromptTemplate>
        {
            ["Service with TDD"] = new PromptTemplate
            {
                Name = "Service with TDD",
                Content = @"Create a C# service class for <FUNCTIONALITY> following these requirements:
- Use .NET 10 with nullable reference types enabled
- Follow Red/Green/Refactor TDD pattern
- Write xUnit tests FIRST with 80-90% coverage minimum
- Use FluentAssertions for test assertions
- Mock dependencies with Moq
- Follow repository pattern
- Include async/await for all I/O operations
- Add comprehensive XML documentation
- Follow RealManage naming conventions",
                Variables = new List<string> { "FUNCTIONALITY" }
            },
            
            ["Angular Component"] = new PromptTemplate
            {
                Name = "Angular Component",
                Content = @"Create an Angular 17 component for <FEATURE> with:
- Component name: <COMPONENT_NAME>
- OnPush change detection strategy
- Standalone component (no modules)
- RxJS for state management
- Signals where appropriate
- Jasmine/Karma tests with 80-90% coverage
- Responsive design with CSS Grid
- WCAG 2.1 AA accessibility
- Error handling with user feedback",
                Variables = new List<string> { "FEATURE", "COMPONENT_NAME" }
            },
            
            ["HOA Violation Handler"] = new PromptTemplate
            {
                Name = "HOA Violation Handler",
                Content = @"Implement a violation <ACTION> system that:
- Handles violation type: <VIOLATION_TYPE>
- Tracks lifecycle stages
- Calculates fines/penalties as needed
- Sends notifications to: <RECIPIENTS>
- Maintains audit trail
- Integrates with existing ViolationService
- Includes comprehensive unit tests
- Follows 30/60/90 day escalation rules",
                Variables = new List<string> { "ACTION", "VIOLATION_TYPE", "RECIPIENTS" }
            },
            
            ["API Endpoint"] = new PromptTemplate
            {
                Name = "API Endpoint",
                Content = @"Create an ASP.NET Core Web API endpoint for <RESOURCE>:
- HTTP Method: <METHOD>
- Route: /api/<ROUTE>
- Request model with validation
- Response model with proper status codes
- Error handling and logging
- Authorization with Azure AD B2C
- Swagger documentation
- Integration tests with 80-90% coverage",
                Variables = new List<string> { "RESOURCE", "METHOD", "ROUTE" }
            },
            
            ["Unit Test Suite"] = new PromptTemplate
            {
                Name = "Unit Test Suite",
                Content = @"Write comprehensive xUnit tests for <CLASS_NAME> that:
- Test method: <METHOD_NAME>
- Follow AAA pattern (Arrange, Act, Assert)
- Test happy path and edge cases
- Mock dependencies with Moq
- Use FluentAssertions
- Include parameterized tests where applicable
- Achieve 80-90% code coverage minimum
- Test exception scenarios
- Verify async behavior if applicable",
                Variables = new List<string> { "CLASS_NAME", "METHOD_NAME" }
            },
            
            ["Bug Fix Request"] = new PromptTemplate
            {
                Name = "Bug Fix Request",
                Content = @"Fix the following bug:

Location: <FILE_PATH>
Method/Component: <METHOD_NAME>
Issue: <BUG_DESCRIPTION>
Expected behavior: <EXPECTED>
Actual behavior: <ACTUAL>

Requirements:
- Write a failing test that reproduces the bug
- Fix the implementation
- Ensure all existing tests still pass
- Add additional tests to prevent regression
- Document the fix with comments",
                Variables = new List<string> { "FILE_PATH", "METHOD_NAME", "BUG_DESCRIPTION", "EXPECTED", "ACTUAL" }
            }
        };
    }
}

public class PromptTemplate
{
    public string Name { get; set; } = "";
    public string Content { get; set; } = "";
    public List<string> Variables { get; set; } = new();
}