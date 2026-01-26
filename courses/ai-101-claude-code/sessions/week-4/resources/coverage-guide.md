# Code Coverage Guide 📊

## What is Code Coverage?

Code coverage measures how much of your code is executed during tests. It's a metric that helps identify untested code paths.

## Coverage Metrics

### Line Coverage
Percentage of lines executed by tests
```
✅ Tested:    if (value > 0)
✅ Tested:        return true;
❌ Untested:  else
❌ Untested:      return false;
```
**Line Coverage: 50%**

### Branch Coverage
Percentage of decision paths taken
```
if (age >= 18 && hasLicense)  // 4 possible branches
    // Branch 1: true && true   ✅
    // Branch 2: true && false  ❌
    // Branch 3: false && true  ❌
    // Branch 4: false && false ✅
```
**Branch Coverage: 50%**

### Method Coverage
Percentage of methods executed
```csharp
public class Service
{
    public void Method1() { }  // ✅ Called in tests
    public void Method2() { }  // ✅ Called in tests
    public void Method3() { }  // ❌ Never called
}
```
**Method Coverage: 66%**

## Running Coverage in .NET

### Basic Coverage Collection
```bash
# Simple coverage run
dotnet test /p:CollectCoverage=true

# With format specification
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# With threshold enforcement (fails if below 80%)
dotnet test /p:CollectCoverage=true /p:Threshold=80
```

### Coverage Output Formats
```bash
# JSON format (default)
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=json

# Cobertura (for CI/CD tools)
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura

# OpenCover (for Visual Studio)
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# Multiple formats
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=\"opencover,json\"
```

## Generating HTML Reports

### Install Report Generator
```bash
# Install globally
dotnet tool install -g dotnet-reportgenerator-globaltool

# Or locally
dotnet tool install dotnet-reportgenerator-globaltool
```

### Generate Report
```bash
# Basic report
reportgenerator -reports:coverage.opencover.xml -targetdir:coveragereport

# With specific report types
reportgenerator -reports:coverage.opencover.xml \
                -targetdir:coveragereport \
                -reporttypes:Html

# Open report
open coveragereport/index.html  # Mac/Linux
start coveragereport/index.html # Windows
```

## Coverage in Practice

### Good Coverage (95%+)
```csharp
public class Calculator
{
    public int Add(int a, int b)
    {
        if (a == 0) return b;      // ✅ Tested
        if (b == 0) return a;      // ✅ Tested
        return a + b;               // ✅ Tested
    }
    
    public int Divide(int a, int b)
    {
        if (b == 0)                 // ✅ Tested
            throw new ArgumentException("Cannot divide by zero");
        return a / b;               // ✅ Tested
    }
}
```

### Poor Coverage (60%)
```csharp
public class UserService
{
    public User GetUser(int id)
    {
        if (id <= 0)                // ❌ Not tested
            throw new ArgumentException("Invalid ID");
            
        var user = _db.Find(id);    // ✅ Tested
        
        if (user == null)           // ❌ Not tested
            return null;
            
        if (user.IsDeleted)         // ❌ Not tested
            return null;
            
        return user;                // ✅ Tested
    }
}
```

## Excluding Code from Coverage

### Class/Method Level
```csharp
[ExcludeFromCodeCoverage]
public class MigrationHelper
{
    // This class won't be included in coverage
}

public class Service
{
    [ExcludeFromCodeCoverage]
    public void DebugMethod()
    {
        // This method won't be included
    }
}
```

### File Level
```xml
<!-- In .csproj -->
<ItemGroup>
  <ExcludeFromCodeCoverage Include="**/Migrations/**/*.cs" />
  <ExcludeFromCodeCoverage Include="**/Program.cs" />
</ItemGroup>
```

### Using Coverlet Settings
```json
// coverlet.runsettings.json
{
  "exclude": [
    "[*]*.Migrations.*",
    "[*]*.Program",
    "[xunit.*]*"
  ],
  "includeTestAssembly": false
}
```

## CI/CD Integration

### GitLab CI/CD
```yaml
test-with-coverage:
  stage: test
  script:
    - dotnet test /p:CollectCoverage=true
                  /p:CoverletOutputFormat=cobertura
                  /p:Threshold=95
                  /p:ThresholdType=line
  artifacts:
    reports:
      coverage_report:
        coverage_format: cobertura
        path: "**/coverage.cobertura.xml"
```

### Azure DevOps
```yaml
- task: DotNetCoreCLI@2
  displayName: 'Run tests with coverage'
  inputs:
    command: test
    arguments: '/p:CollectCoverage=true /p:CoverletOutputFormat=cobertura'
    
- task: PublishCodeCoverageResults@1
  inputs:
    codeCoverageTool: 'Cobertura'
    summaryFileLocation: '$(System.DefaultWorkingDirectory)/**/coverage.cobertura.xml'
```

## Coverage Best Practices

### ✅ DO
- Aim for 80-95% coverage
- Focus on critical business logic
- Test edge cases and error paths
- Use coverage to find untested code
- Exclude generated code from metrics

### ❌ DON'T
- Chase 100% coverage blindly
- Write meaningless tests for coverage
- Include UI code in coverage
- Test framework code
- Sacrifice test quality for numbers

## TDD and Coverage

With proper TDD, coverage emerges naturally:

1. **Red Phase**: Write test first (0% coverage)
2. **Green Phase**: Implement minimum code (new code 100% covered)
3. **Refactor Phase**: Improve code (coverage maintained)

```bash
# After each TDD cycle
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=console

# Output shows incremental coverage growth:
# Cycle 1: 25% coverage
# Cycle 2: 50% coverage
# Cycle 3: 75% coverage
# Cycle 4: 80-90% coverage ✅
```

## Troubleshooting Low Coverage

### Common Issues

1. **Missing Error Path Tests**
```csharp
// Code
if (input == null)
    throw new ArgumentNullException(nameof(input));

// Missing Test
[Fact]
public void Method_NullInput_ThrowsException()
{
    var action = () => service.Method(null);
    action.Should().Throw<ArgumentNullException>();
}
```

2. **Untested Catch Blocks**
```csharp
// Code
try
{
    return await CallExternalService();
}
catch (HttpException ex)  // Often untested
{
    _logger.LogError(ex, "Service call failed");
    return null;
}

// Add Test with Mock
mock.Setup(x => x.CallExternalService())
    .ThrowsAsync(new HttpException());
```

3. **Complex Conditionals**
```csharp
// Code with multiple conditions
if (user.Age >= 18 && user.HasLicense && !user.IsSuspended)

// Need tests for all combinations:
// - Adult with license, not suspended ✅
// - Adult with license, suspended
// - Adult without license
// - Minor with license
// etc.
```

## Coverage Visualization

### Console Output
```
+--------+--------+--------+
| Module | Line   | Branch |
+--------+--------+--------+
| Core   | 95.2%  | 88.1%  |
| Data   | 92.8%  | 85.5%  |
| API    | 88.5%  | 79.2%  |
+--------+--------+--------+
| Total  | 92.1%  | 84.3%  |
+--------+--------+--------+
```

### HTML Report Features
- **File Browser**: Navigate through your code
- **Line Highlighting**: 
  - 🟢 Green = Covered
  - 🔴 Red = Not Covered
  - 🟡 Yellow = Partially Covered
- **Metrics Summary**: Overall and per-file statistics
- **Trend Graphs**: Coverage over time
- **Risk Hotspots**: Identifies complex, untested code

## Quick Commands Reference

```bash
# Run with coverage
dotnet test /p:CollectCoverage=true

# With threshold
dotnet test /p:CollectCoverage=true /p:Threshold=95

# Generate report
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
reportgenerator -reports:coverage.opencover.xml -targetdir:report

# Open report (Mac/Linux)
open report/index.html

# Open report (Windows)
start report/index.html

# Watch mode with coverage
dotnet watch test /p:CollectCoverage=true /p:CoverletOutputFormat=console
```

---

*Remember: Coverage is a tool, not a goal. High coverage with poor tests is worse than moderate coverage with excellent tests.* 🎯