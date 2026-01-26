# Coverage Strategies - Answer Key

This document provides instructors with strategies for helping students identify coverage gaps, understand common edge cases, and differentiate between coverage types.

---

## Part 1: Identifying Coverage Gaps

### Strategy 1: Follow the Code Paths

Every `if`, `switch`, `try-catch`, or `??` creates a branch. Each branch needs testing.

**Example from PropertyService:**

```csharp
public async Task<List<Property>> SearchPropertiesAsync(string searchTerm)
{
    // Branch 1: searchTerm is null or whitespace
    if (string.IsNullOrWhiteSpace(searchTerm))
        return new List<Property>();

    // Branch 2: searchTerm has value
    var term = searchTerm.ToLower();
    return await _context.Properties
        .Where(p => p.Address.ToLower().Contains(term) ||  // Sub-branch: Address match
                   p.City.ToLower().Contains(term) ||      // Sub-branch: City match
                   p.State.ToLower().Contains(term))       // Sub-branch: State match
        .ToListAsync();
}
```

**Required Tests for Full Coverage:**

| Test Case | Branch Covered | Why It's Needed |
|-----------|----------------|-----------------|
| `SearchProperties_EmptyTerm_ReturnsEmpty` | Branch 1 (true) | Empty string handling |
| `SearchProperties_NullTerm_ReturnsEmpty` | Branch 1 (true) | Null handling |
| `SearchProperties_WhitespaceTerm_ReturnsEmpty` | Branch 1 (true) | Whitespace handling |
| `SearchProperties_MatchingAddress_ReturnsResults` | Branch 2 + Address | Address search works |
| `SearchProperties_MatchingCity_ReturnsResults` | Branch 2 + City | City search works |
| `SearchProperties_MatchingState_ReturnsResults` | Branch 2 + State | State search works |
| `SearchProperties_NoMatches_ReturnsEmpty` | Branch 2 (no match) | No results case |
| `SearchProperties_CaseInsensitive_ReturnsResults` | Branch 2 | Case handling |

---

### Strategy 2: The "What Could Go Wrong?" Checklist

For each method, ask these questions:

**Input Validation Gaps:**
- [ ] What if the input is null?
- [ ] What if the input is empty?
- [ ] What if the input is whitespace?
- [ ] What if the input is at boundary values (0, -1, MAX_INT)?
- [ ] What if the input format is invalid?

**State Gaps:**
- [ ] What if the object doesn't exist?
- [ ] What if the object is in an invalid state?
- [ ] What if there's a duplicate?
- [ ] What if there's a concurrent modification?

**Integration Gaps:**
- [ ] What if the database operation fails?
- [ ] What if the external service is unavailable?
- [ ] What if the network times out?

---

### Strategy 3: Read the TODO Comments

The example projects include deliberate TODO comments marking untested areas:

**From `PropertyServiceTests.cs`:**
```csharp
// TODO: Missing test coverage for:
// - GetPropertyById with non-existent ID
// - SearchProperties with empty/null term
// - SearchProperties with no matches
// - UpdateProperty (not tested at all!)
// - Error handling scenarios
// - Edge cases
```

**From `PropertyService.cs`:**
```csharp
// TODO: This needs better testing - edge cases not covered
// TODO: Not tested at all!
// TODO: Add these methods via TDD:
// - AddValuationAsync(...)
// - GetValuationHistoryAsync(...)
```

These TODOs represent real coverage gaps students should address.

---

### Strategy 4: The Coverage Report Deep Dive

**Running Coverage:**
```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
reportgenerator -reports:coverage.opencover.xml -targetdir:coveragereport
```

**Reading the Report:**

The HTML report shows:
- Red lines = Never executed
- Yellow lines = Partially executed (branches not fully covered)
- Green lines = Fully executed

**Common Red Flags in Reports:**

| Report Shows | What It Means | How to Fix |
|--------------|---------------|------------|
| Entire method red | No tests call this method | Write test that calls it |
| `catch` block red | Exception path not tested | Mock to throw exception |
| `else` branch red | Only happy path tested | Add test for negative case |
| Return statement red | Early return not tested | Add test for that condition |

---

## Part 2: Edge Cases Students Commonly Miss

### Category 1: Null and Empty Handling

**The Trap:**
Students test happy path but forget null checks.

```csharp
// Students often write this:
[Fact]
public void AddHomeowner_ValidInput_ReturnsTrue()
{
    var result = service.AddHomeowner(validHomeowner);
    result.Should().BeTrue();
}

// But forget these:
[Fact]
public void AddHomeowner_NullHomeowner_ThrowsArgumentNullException()
{
    var action = () => service.AddHomeowner(null!);
    action.Should().Throw<ArgumentNullException>();
}

[Fact]
public void AddHomeowner_EmptyEmail_ThrowsArgumentException()
{
    var homeowner = new Homeowner { Email = "", FirstName = "John", LastName = "Doe" };
    var action = () => service.AddHomeowner(homeowner);
    action.Should().Throw<ArgumentException>();
}
```

**Complete Null/Empty Test Matrix:**

| Property | Null Test | Empty Test | Whitespace Test |
|----------|-----------|------------|-----------------|
| Email | AddHomeowner_NullEmail_Throws | AddHomeowner_EmptyEmail_Throws | AddHomeowner_WhitespaceEmail_Throws |
| FirstName | AddHomeowner_NullFirstName_Throws | AddHomeowner_EmptyFirstName_Throws | AddHomeowner_WhitespaceFirstName_Throws |
| LastName | AddHomeowner_NullLastName_Throws | AddHomeowner_EmptyLastName_Throws | AddHomeowner_WhitespaceLastName_Throws |

---

### Category 2: Boundary Values

**The Trap:**
Students test with "normal" values but miss boundaries.

```csharp
// Payment plan day of month boundaries
[Theory]
[InlineData(0)]   // Below minimum
[InlineData(-1)]  // Negative
[InlineData(29)]  // Above maximum (Feb issues)
[InlineData(30)]  // Above maximum
[InlineData(31)]  // Above maximum
public void SetupPaymentPlan_InvalidDayOfMonth_ThrowsArgumentException(int invalidDay)
{
    var action = () => service.SetupPaymentPlan("test@example.com", 250m, invalidDay);
    action.Should().Throw<ArgumentException>();
}

[Theory]
[InlineData(1)]   // Minimum valid
[InlineData(15)]  // Middle
[InlineData(28)]  // Maximum valid (safe for all months)
public void SetupPaymentPlan_ValidDayOfMonth_Succeeds(int validDay)
{
    var plan = service.SetupPaymentPlan("test@example.com", 250m, validDay);
    plan.DayOfMonth.Should().Be(validDay);
}
```

**Boundary Testing Guide:**

| Type | Test Cases Needed |
|------|-------------------|
| Integer | 0, 1, -1, MAX, MIN, typical |
| String | null, "", " ", very long, special chars |
| Decimal | 0, negative, very small, very large |
| Date | past, future, today, leap year, DST transition |
| Collection | empty, one item, many items, null |

---

### Category 3: Error Handling and Exceptions

**The Trap:**
Students don't test exception paths because they require setup.

```csharp
// Test that database failures are handled
[Fact]
public async Task UpdateProperty_DatabaseFailure_ReturnsFalse()
{
    // Arrange - Use a context that will fail
    var failingContext = CreateFailingContext();
    var service = new PropertyService(failingContext);

    // Act
    var result = await service.UpdatePropertyAsync(property);

    // Assert
    result.Should().BeFalse();
}

// Test external service failures with mocks
[Fact]
public async Task GenerateWelcomePacket_EmailServiceFails_ThrowsOrHandlesGracefully()
{
    // Arrange
    var mockEmail = new Mock<IEmailService>();
    mockEmail.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
        .ThrowsAsync(new SmtpException("Connection failed"));

    var service = new HomeownerService(mockEmail.Object);

    // Act & Assert - depends on expected behavior
    var action = () => service.GenerateWelcomePacketAsync("test@example.com");
    action.Should().ThrowAsync<SmtpException>();  // If bubbling up
    // OR
    // result.Should().BeFalse();  // If handling gracefully
}
```

---

### Category 4: Concurrency and State

**The Trap:**
Students test methods in isolation but miss state-dependent behavior.

```csharp
// Test duplicate handling
[Fact]
public void AddHomeowner_SameEmailTwice_SecondReturnsFalse()
{
    // Arrange
    var homeowner1 = new Homeowner { Email = "john@example.com", FirstName = "John", LastName = "Doe" };
    var homeowner2 = new Homeowner { Email = "john@example.com", FirstName = "Jane", LastName = "Doe" };

    // Act
    var first = service.AddHomeowner(homeowner1);
    var second = service.AddHomeowner(homeowner2);

    // Assert
    first.Should().BeTrue();
    second.Should().BeFalse();
}

// Test case insensitivity for duplicates
[Fact]
public void AddHomeowner_SameEmailDifferentCase_TreatedAsDuplicate()
{
    // Arrange
    var homeowner1 = new Homeowner { Email = "john@example.com", FirstName = "John", LastName = "Doe" };
    var homeowner2 = new Homeowner { Email = "JOHN@EXAMPLE.COM", FirstName = "Jane", LastName = "Doe" };

    // Act
    service.AddHomeowner(homeowner1);
    var result = service.AddHomeowner(homeowner2);

    // Assert
    result.Should().BeFalse();  // Should be treated as duplicate
}
```

---

### Category 5: HOA-Specific Business Rules

Students often miss domain-specific edge cases:

```csharp
// Late fee calculation - compound interest
[Fact]
public void CalculateLateFee_After30Days_Applies10PercentCompoundMonthly()
{
    // Arrange
    var originalAmount = 100.00m;
    var daysLate = 60;  // 2 months

    // Act
    var fee = calculator.CalculateLateFee(originalAmount, daysLate);

    // Assert
    // 10% monthly compound: 100 * 1.1 * 1.1 = 121
    fee.Should().Be(21.00m);  // Just the fee portion
}

// Grace period handling
[Fact]
public void CalculateLateFee_Within30DayGrace_ReturnsZero()
{
    // Arrange
    var originalAmount = 100.00m;
    var daysLate = 29;

    // Act
    var fee = calculator.CalculateLateFee(originalAmount, daysLate);

    // Assert
    fee.Should().Be(0m);
}

// Exactly at boundary
[Fact]
public void CalculateLateFee_Exactly30Days_ReturnsZero()
{
    // This tests the boundary - is 30 days still grace period?
    var fee = calculator.CalculateLateFee(100m, 30);
    fee.Should().Be(0m);  // 30 days is still in grace
}

[Fact]
public void CalculateLateFee_31Days_AppliesFirstMonthFee()
{
    // First month late
    var fee = calculator.CalculateLateFee(100m, 31);
    fee.Should().Be(10m);  // 10% of 100
}
```

---

## Part 3: Branch Coverage vs Line Coverage

### Understanding the Difference

**Line Coverage:**
Measures which lines of code were executed during tests.

**Branch Coverage:**
Measures which decision paths were taken at branch points.

### The Critical Difference

Consider this code:

```csharp
public string GetStatus(int age, bool hasLicense)
{
    if (age >= 18 && hasLicense)
        return "Can Drive";
    return "Cannot Drive";
}
```

**With Only This Test:**
```csharp
[Fact]
public void GetStatus_AdultWithLicense_CanDrive()
{
    var result = GetStatus(25, true);
    result.Should().Be("Can Drive");
}
```

| Metric | Value | Why |
|--------|-------|-----|
| Line Coverage | 100% | Both return statements touched |
| Branch Coverage | 25% | Only 1 of 4 branch combinations tested |

**The Four Branches:**

| age >= 18 | hasLicense | Result | Tested? |
|-----------|------------|--------|---------|
| true | true | "Can Drive" | Yes |
| true | false | "Cannot Drive" | No |
| false | true | "Cannot Drive" | No |
| false | false | "Cannot Drive" | No |

**Complete Branch Coverage:**
```csharp
[Theory]
[InlineData(25, true, "Can Drive")]
[InlineData(25, false, "Cannot Drive")]
[InlineData(16, true, "Cannot Drive")]
[InlineData(16, false, "Cannot Drive")]
public void GetStatus_AllCombinations_ReturnsCorrectStatus(int age, bool hasLicense, string expected)
{
    var result = GetStatus(age, hasLicense);
    result.Should().Be(expected);
}
```

---

### Visual Example: The Coverage Trap

```csharp
public decimal CalculateFee(Homeowner owner)
{
    decimal fee = 250m;  // Line 1 - covered if method called

    if (owner.IsVeteran)  // Line 2 - branch point
        fee *= 0.9m;      // Line 3 - only covered if veteran

    if (owner.IsSenior)   // Line 4 - branch point
        fee *= 0.85m;     // Line 5 - only covered if senior

    return fee;           // Line 6 - covered if method called
}
```

**Test That Achieves High Line Coverage But Low Branch Coverage:**
```csharp
[Fact]
public void CalculateFee_VeteranSenior_AppliesBothDiscounts()
{
    var owner = new Homeowner { IsVeteran = true, IsSenior = true };
    var fee = service.CalculateFee(owner);
    fee.Should().Be(250m * 0.9m * 0.85m);
}
```

**Coverage Analysis:**

| Metric | With Above Test |
|--------|-----------------|
| Line Coverage | 100% (all 6 lines touched) |
| Branch Coverage | 25% (1 of 4 paths) |

**The Four Paths:**

```
Path 1: IsVeteran=true,  IsSenior=true   -> 250 * 0.9 * 0.85 = 191.25 [TESTED]
Path 2: IsVeteran=true,  IsSenior=false  -> 250 * 0.9 = 225        [NOT TESTED]
Path 3: IsVeteran=false, IsSenior=true   -> 250 * 0.85 = 212.50    [NOT TESTED]
Path 4: IsVeteran=false, IsSenior=false  -> 250                     [NOT TESTED]
```

**Complete Test Suite:**
```csharp
[Theory]
[InlineData(true, true, 191.25)]    // Path 1
[InlineData(true, false, 225.00)]   // Path 2
[InlineData(false, true, 212.50)]   // Path 3
[InlineData(false, false, 250.00)]  // Path 4
public void CalculateFee_AllDiscountCombinations_ReturnsCorrectFee(
    bool isVeteran, bool isSenior, decimal expectedFee)
{
    var owner = new Homeowner { IsVeteran = isVeteran, IsSenior = isSenior };
    var fee = service.CalculateFee(owner);
    fee.Should().Be(expectedFee);
}
```

---

### When Branch Coverage Matters Most

| Scenario | Why Branch Coverage is Critical |
|----------|--------------------------------|
| Financial calculations | Wrong branch = wrong money |
| Access control | Wrong branch = security hole |
| Data validation | Wrong branch = corrupt data |
| Business rules | Wrong branch = compliance failure |
| Error handling | Untested exception = production crash |

### When Line Coverage is Sufficient

| Scenario | Why Line Coverage May Be OK |
|----------|----------------------------|
| Simple getters/setters | No logic to branch |
| Logging statements | Side effects only |
| Configuration loading | Straightforward |
| Data transfer objects | No behavior |

---

## Part 4: Quick Reference - Coverage Gap Detection

### The 5-Minute Coverage Audit

1. **Run coverage report:**
   ```bash
   dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
   reportgenerator -reports:coverage.opencover.xml -targetdir:report
   ```

2. **Open report and look for:**
   - Any red lines (uncovered)
   - Any yellow lines (partial branches)
   - Any methods with < 80% coverage

3. **For each gap, ask:**
   - What input would make this line execute?
   - What state would trigger this branch?
   - What failure would hit this catch block?

4. **Write the test that fills the gap**

5. **Re-run and verify improvement**

### The Branch Coverage Formula

For `if (A && B && C)`:
- Number of branches = 2^n where n = number of conditions
- 3 conditions = 8 branches to test

For `if (A || B || C)`:
- Same formula applies
- 3 conditions = 8 branches

**Practical approach:** Test the true path, the fully false path, and each condition being the deciding factor.

---

## Summary Table: What Students Miss and Why

| Gap Category | Why Missed | Impact if Untested |
|--------------|------------|-------------------|
| Null inputs | "Won't happen in practice" | NullReferenceException in prod |
| Empty strings | "Same as null" | Subtly different behavior |
| Boundary values | "Any number works" | Off-by-one errors |
| Exception paths | "Hard to trigger" | Unhandled exceptions |
| Negative cases | "Focus on happy path" | False positives in validation |
| State changes | "Test one method at a time" | Race conditions, duplicates |
| Case sensitivity | "Didn't think of it" | User frustration, data issues |

---

*Remember: Coverage is a means to an end. The goal is confidence in your code, not a percentage.*
