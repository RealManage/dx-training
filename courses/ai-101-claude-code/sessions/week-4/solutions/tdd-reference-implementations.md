# TDD Reference Implementations - Answer Key

This document shows the **complete Red-Green-Refactor cycle** for the Week 4 exercises. Use this to verify student understanding and provide guidance.

---

## Exercise 1: Homeowner Setup - Building from Scratch

### Feature 1: Add Homeowner with Email Validation

This demonstrates the full TDD dance for the most fundamental feature.

#### Cycle 1: Basic AddHomeowner - Returns True for Valid Input

**Step 1: RED - Write the Failing Test**

```csharp
using Xunit;
using FluentAssertions;

namespace RealManage.HomeownerSetup.Tests;

public class HomeownerServiceTests
{
    [Fact]
    public void AddHomeowner_ValidInput_ReturnsTrue()
    {
        // Arrange
        var service = new HomeownerService();
        var homeowner = new Homeowner
        {
            Email = "john@example.com",
            FirstName = "John",
            LastName = "Doe"
        };

        // Act
        var result = service.AddHomeowner(homeowner);

        // Assert
        result.Should().BeTrue();
    }
}
```

**Run Test - See Failure:**

```
error CS0246: The type or namespace name 'HomeownerService' could not be found
error CS0246: The type or namespace name 'Homeowner' could not be found
```

**Why this fails:** The classes don't exist yet. This is a *compilation failure* - the first type of "red" in TDD.

---

**Step 2: GREEN - Write Minimal Code to Pass**

Create `Models/Homeowner.cs`:

```csharp
namespace RealManage.HomeownerSetup.Models;

public class Homeowner
{
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
```

Create `Services/HomeownerService.cs`:

```csharp
using RealManage.HomeownerSetup.Models;

namespace RealManage.HomeownerSetup.Services;

public class HomeownerService
{
    public bool AddHomeowner(Homeowner homeowner)
    {
        return true;  // MINIMAL code to pass!
    }
}
```

**Run Test - See Pass:**

```
Passed!  - 1 test
```

**Key Insight:** We return `true` unconditionally. This seems "wrong" but follows TDD Law #3: *Write no more production code than sufficient to pass the test.*

---

**Step 3: REFACTOR - None Needed Yet**

The code is already minimal. Nothing to refactor.

---

#### Cycle 2: Validation - Missing Email Throws Exception

**Step 1: RED - Write the Failing Test**

```csharp
[Fact]
public void AddHomeowner_NullEmail_ThrowsArgumentException()
{
    // Arrange
    var service = new HomeownerService();
    var homeowner = new Homeowner
    {
        Email = null!,  // Force null
        FirstName = "John",
        LastName = "Doe"
    };

    // Act
    var action = () => service.AddHomeowner(homeowner);

    // Assert
    action.Should().Throw<ArgumentException>()
        .WithMessage("*Email*");
}
```

**Run Test - See Failure:**

```
Expected a <System.ArgumentException> to be thrown, but no exception was thrown.
```

**Why this fails:** Our implementation just returns `true` - it doesn't validate anything.

---

**Step 2: GREEN - Add Minimal Validation**

Update `Services/HomeownerService.cs`:

```csharp
public bool AddHomeowner(Homeowner homeowner)
{
    if (string.IsNullOrWhiteSpace(homeowner.Email))
        throw new ArgumentException("Email is required");

    return true;
}
```

**Run Test - See Pass:**

```
Passed!  - 2 tests
```

---

**Step 3: REFACTOR - Extract Validation Method**

```csharp
public bool AddHomeowner(Homeowner homeowner)
{
    ValidateHomeowner(homeowner);
    return true;
}

private void ValidateHomeowner(Homeowner homeowner)
{
    if (string.IsNullOrWhiteSpace(homeowner.Email))
        throw new ArgumentException("Email is required");
}
```

**Run Tests - Still Green:**

```
Passed!  - 2 tests
```

---

#### Cycle 3: Email Format Validation

**Step 1: RED - Write the Failing Test**

```csharp
[Theory]
[InlineData("not-an-email")]
[InlineData("missing@domain")]
[InlineData("@nodomain.com")]
[InlineData("spaces in@email.com")]
public void AddHomeowner_InvalidEmailFormat_ThrowsArgumentException(string invalidEmail)
{
    // Arrange
    var service = new HomeownerService();
    var homeowner = new Homeowner
    {
        Email = invalidEmail,
        FirstName = "John",
        LastName = "Doe"
    };

    // Act
    var action = () => service.AddHomeowner(homeowner);

    // Assert
    action.Should().Throw<ArgumentException>()
        .WithMessage("*Email*format*");
}
```

**Run Test - See Failure:**

```
Expected exception but none was thrown (for all invalid emails)
```

---

**Step 2: GREEN - Add Email Format Validation**

```csharp
private void ValidateHomeowner(Homeowner homeowner)
{
    if (string.IsNullOrWhiteSpace(homeowner.Email))
        throw new ArgumentException("Email is required");

    if (!IsValidEmailFormat(homeowner.Email))
        throw new ArgumentException("Email format is invalid");
}

private static bool IsValidEmailFormat(string email)
{
    // Minimal regex - just enough to pass tests
    return email.Contains('@') &&
           email.Contains('.') &&
           !email.Contains(' ') &&
           email.IndexOf('@') > 0 &&
           email.LastIndexOf('.') > email.IndexOf('@');
}
```

**Run Test - See Pass:**

```
Passed!  - 6 tests (2 facts + 4 theory cases)
```

---

**Step 3: REFACTOR - Use Built-in Email Validation**

```csharp
using System.Net.Mail;

private static bool IsValidEmailFormat(string email)
{
    try
    {
        var addr = new MailAddress(email);
        return addr.Address == email;
    }
    catch
    {
        return false;
    }
}
```

**Run Tests - Still Green:**

```
Passed!  - 6 tests
```

---

#### Cycle 4: Prevent Duplicate Registrations

**Step 1: RED - Write the Failing Test**

```csharp
[Fact]
public void AddHomeowner_DuplicateEmail_ReturnsFalse()
{
    // Arrange
    var service = new HomeownerService();
    var homeowner1 = new Homeowner
    {
        Email = "john@example.com",
        FirstName = "John",
        LastName = "Doe"
    };
    var homeowner2 = new Homeowner
    {
        Email = "john@example.com",  // Same email
        FirstName = "Jane",
        LastName = "Doe"
    };

    // Act
    service.AddHomeowner(homeowner1);
    var result = service.AddHomeowner(homeowner2);

    // Assert
    result.Should().BeFalse();
}
```

**Run Test - See Failure:**

```
Expected result to be False, but found True.
```

---

**Step 2: GREEN - Track Added Homeowners**

```csharp
public class HomeownerService
{
    private readonly List<Homeowner> _homeowners = new();

    public bool AddHomeowner(Homeowner homeowner)
    {
        ValidateHomeowner(homeowner);

        // Check for duplicate
        if (_homeowners.Any(h => h.Email.Equals(homeowner.Email, StringComparison.OrdinalIgnoreCase)))
            return false;

        _homeowners.Add(homeowner);
        return true;
    }

    // ... rest of implementation
}
```

**Run Test - See Pass:**

```
Passed!  - 7 tests
```

---

### Feature 2: Payment Plan Setup

#### Full Cycle: Create Payment Plan

**Step 1: RED - Write the Failing Test**

```csharp
[Fact]
public void SetupPaymentPlan_ValidInput_ReturnsPaymentPlan()
{
    // Arrange
    var service = new HomeownerService();
    var homeowner = new Homeowner
    {
        Email = "john@example.com",
        FirstName = "John",
        LastName = "Doe"
    };
    service.AddHomeowner(homeowner);

    // Act
    var plan = service.SetupPaymentPlan(
        "john@example.com",
        monthlyAmount: 250.00m,
        dayOfMonth: 1);

    // Assert
    plan.Should().NotBeNull();
    plan.MonthlyAmount.Should().Be(250.00m);
    plan.DayOfMonth.Should().Be(1);
    plan.HomeownerEmail.Should().Be("john@example.com");
}
```

**Run Test - See Failure:**

```
error CS0246: The type or namespace name 'PaymentPlan' could not be found
```

---

**Step 2: GREEN - Create PaymentPlan and Implement**

Create `Models/PaymentPlan.cs`:

```csharp
namespace RealManage.HomeownerSetup.Models;

public class PaymentPlan
{
    public string HomeownerEmail { get; set; } = string.Empty;
    public decimal MonthlyAmount { get; set; }
    public int DayOfMonth { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
```

Add to `HomeownerService.cs`:

```csharp
public PaymentPlan SetupPaymentPlan(string email, decimal monthlyAmount, int dayOfMonth)
{
    return new PaymentPlan
    {
        HomeownerEmail = email,
        MonthlyAmount = monthlyAmount,
        DayOfMonth = dayOfMonth
    };
}
```

**Run Test - See Pass:**

```
Passed!  - 8 tests
```

---

**Step 3: RED - Validate Homeowner Exists**

```csharp
[Fact]
public void SetupPaymentPlan_NonexistentHomeowner_ThrowsException()
{
    // Arrange
    var service = new HomeownerService();

    // Act
    var action = () => service.SetupPaymentPlan(
        "nobody@example.com",
        monthlyAmount: 250.00m,
        dayOfMonth: 1);

    // Assert
    action.Should().Throw<InvalidOperationException>()
        .WithMessage("*Homeowner*not found*");
}
```

---

**Step 4: GREEN - Add Validation**

```csharp
public PaymentPlan SetupPaymentPlan(string email, decimal monthlyAmount, int dayOfMonth)
{
    var homeowner = _homeowners.FirstOrDefault(h =>
        h.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

    if (homeowner == null)
        throw new InvalidOperationException($"Homeowner with email {email} not found");

    return new PaymentPlan
    {
        HomeownerEmail = email,
        MonthlyAmount = monthlyAmount,
        DayOfMonth = dayOfMonth
    };
}
```

---

### Feature 3: Welcome Packet Generation (With Mocking)

This demonstrates mocking external dependencies.

**Step 1: RED - Write the Failing Test with Mock**

```csharp
using Moq;

public class WelcomePacketTests
{
    [Fact]
    public async Task GenerateWelcomePacket_SendsEmailToHomeowner()
    {
        // Arrange
        var mockEmailService = new Mock<IEmailService>();
        mockEmailService
            .Setup(x => x.SendEmailAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
            .ReturnsAsync(true);

        var service = new HomeownerService(mockEmailService.Object);
        var homeowner = new Homeowner
        {
            Email = "john@example.com",
            FirstName = "John",
            LastName = "Doe"
        };
        service.AddHomeowner(homeowner);

        // Act
        await service.GenerateWelcomePacketAsync("john@example.com");

        // Assert
        mockEmailService.Verify(
            x => x.SendEmailAsync(
                "john@example.com",
                It.Is<string>(s => s.Contains("Welcome")),
                It.Is<string>(s => s.Contains("John"))),
            Times.Once);
    }
}
```

**Run Test - See Failure:**

```
error CS0246: The type or namespace name 'IEmailService' could not be found
```

---

**Step 2: GREEN - Create Interface and Implement**

Create `Services/IEmailService.cs`:

```csharp
namespace RealManage.HomeownerSetup.Services;

public interface IEmailService
{
    Task<bool> SendEmailAsync(string to, string subject, string body);
}
```

Update `HomeownerService.cs`:

```csharp
public class HomeownerService
{
    private readonly List<Homeowner> _homeowners = new();
    private readonly IEmailService? _emailService;

    public HomeownerService() { }

    public HomeownerService(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task GenerateWelcomePacketAsync(string email)
    {
        var homeowner = _homeowners.FirstOrDefault(h =>
            h.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
            ?? throw new InvalidOperationException("Homeowner not found");

        if (_emailService != null)
        {
            await _emailService.SendEmailAsync(
                homeowner.Email,
                $"Welcome to Your HOA, {homeowner.FirstName}!",
                $"Dear {homeowner.FirstName},\n\nWelcome to our community...");
        }
    }
}
```

**Run Test - See Pass:**

```
Passed!  - 9 tests
```

---

## Exercise 2: Property Manager Enhancement

This exercise focuses on **enhancing existing code** with TDD, starting from 60% coverage.

### Finding Coverage Gaps

The existing `PropertyServiceTests.cs` has TODO comments showing what's missing:

- `GetPropertyById` with non-existent ID
- `SearchProperties` with empty/null term
- `SearchProperties` with no matches
- `UpdateProperty` (not tested at all!)

### Cycle 1: GetPropertyById - Non-existent ID

**Step 1: RED - Write the Failing Test**

```csharp
[Fact]
public async Task GetPropertyById_NonExistentId_ReturnsNull()
{
    // Arrange - nothing to add, using seeded data

    // Act
    var result = await _service.GetPropertyByIdAsync(99999);

    // Assert
    result.Should().BeNull();
}
```

**Run Test - See Pass?**

Wait - this actually passes! The existing implementation already handles this case correctly. This is an example of "triangulation" - we discovered the code works, but we still keep the test as documentation.

---

### Cycle 2: SearchProperties - Empty Term

**Step 1: RED - Write the Failing Test**

```csharp
[Fact]
public async Task SearchProperties_EmptyTerm_ReturnsEmptyList()
{
    // Act
    var results = await _service.SearchPropertiesAsync("");

    // Assert
    results.Should().BeEmpty();
}

[Fact]
public async Task SearchProperties_NullTerm_ReturnsEmptyList()
{
    // Act
    var results = await _service.SearchPropertiesAsync(null!);

    // Assert
    results.Should().BeEmpty();
}
```

**Run Tests - See Result:**

The empty string test passes (existing code handles it), but the null test may throw a NullReferenceException.

---

**Step 2: GREEN - Fix Null Handling (if needed)**

```csharp
public async Task<List<Property>> SearchPropertiesAsync(string searchTerm)
{
    if (string.IsNullOrWhiteSpace(searchTerm))
        return new List<Property>();

    // ... rest of implementation
}
```

---

### Cycle 3: UpdateProperty - Full TDD

**Step 1: RED - Write the Failing Test**

```csharp
[Fact]
public async Task UpdateProperty_ExistingProperty_ReturnsTrue()
{
    // Arrange
    var property = new Property
    {
        Address = "Original Address",
        City = "Dallas",
        State = "TX",
        ZipCode = "75201"
    };
    var added = await _service.AddPropertyAsync(property);

    // Act
    added.Address = "Updated Address";
    var result = await _service.UpdatePropertyAsync(added);

    // Assert
    result.Should().BeTrue();

    // Verify the change persisted
    var updated = await _service.GetPropertyByIdAsync(added.Id);
    updated!.Address.Should().Be("Updated Address");
}
```

**Run Test - See Result:**

This should pass with the existing implementation.

---

**Step 2: RED - Test Failure Case**

```csharp
[Fact]
public async Task UpdateProperty_NonExistentProperty_ReturnsFalse()
{
    // Arrange
    var nonExistentProperty = new Property
    {
        Id = 99999,
        Address = "Doesn't Exist",
        City = "Nowhere",
        State = "XX",
        ZipCode = "00000"
    };

    // Act
    var result = await _service.UpdatePropertyAsync(nonExistentProperty);

    // Assert
    result.Should().BeFalse();
}
```

This may fail depending on EF Core behavior. The existing code has a try-catch that should handle this.

---

### Cycle 4: Adding Valuation Tracking (New Feature)

**Step 1: RED - Write the Failing Test**

```csharp
[Fact]
public async Task AddValuation_ValidInput_ReturnsValuation()
{
    // Arrange
    var property = new Property
    {
        Address = "100 Test St",
        City = "Dallas",
        State = "TX",
        ZipCode = "75201"
    };
    var added = await _service.AddPropertyAsync(property);

    // Act
    var valuation = await _service.AddValuationAsync(
        added.Id,
        amount: 500000m,
        date: DateTime.UtcNow,
        source: "Appraisal");

    // Assert
    valuation.Should().NotBeNull();
    valuation.PropertyId.Should().Be(added.Id);
    valuation.Amount.Should().Be(500000m);
    valuation.Source.Should().Be("Appraisal");
}
```

**Run Test - See Failure:**

```
error CS0246: The type or namespace name 'Valuation' could not be found
error CS1061: 'PropertyService' does not contain a definition for 'AddValuationAsync'
```

---

**Step 2: GREEN - Create Valuation Model and Method**

Create/update `Models/Valuation.cs`:

```csharp
namespace RealManage.PropertyManager.Models;

public class Valuation
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public decimal Amount { get; set; }
    public DateTime ValuationDate { get; set; }
    public string Source { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public Property? Property { get; set; }
}
```

Update `PropertyContext.cs`:

```csharp
public DbSet<Valuation> Valuations { get; set; } = null!;
```

Add to `PropertyService.cs`:

```csharp
public async Task<Valuation> AddValuationAsync(int propertyId, decimal amount, DateTime date, string source)
{
    var property = await _context.Properties.FindAsync(propertyId)
        ?? throw new InvalidOperationException("Property not found");

    var valuation = new Valuation
    {
        PropertyId = propertyId,
        Amount = amount,
        ValuationDate = date,
        Source = source
    };

    _context.Valuations.Add(valuation);
    await _context.SaveChangesAsync();
    return valuation;
}
```

**Run Test - See Pass:**

```
Passed!
```

---

### Cycle 5: Mocking INotificationService

**Step 1: RED - Write the Failing Test with Mock**

```csharp
[Fact]
public async Task NotifyOwnerOfMaintenance_SendsEmailAndSms()
{
    // Arrange
    var mockNotifier = new Mock<INotificationService>();
    mockNotifier
        .Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
        .ReturnsAsync(true);
    mockNotifier
        .Setup(x => x.SendSmsAsync(It.IsAny<string>(), It.IsAny<string>()))
        .ReturnsAsync(true);

    // Need a way to inject the mock - service needs constructor update
    var options = new DbContextOptionsBuilder<PropertyContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;
    var context = new PropertyContext(options);

    var service = new PropertyService(context, mockNotifier.Object);

    var property = await service.AddPropertyAsync(new Property
    {
        Address = "123 Test St",
        City = "Dallas",
        State = "TX",
        ZipCode = "75201"
    });

    var request = new MaintenanceRequest
    {
        PropertyId = property.Id,
        Description = "Fix leak",
        Priority = Priority.High
    };

    // Act
    await service.NotifyOwnerOfMaintenanceAsync("owner@example.com", request);

    // Assert
    mockNotifier.Verify(
        x => x.SendEmailAsync(
            "owner@example.com",
            It.Is<string>(s => s.Contains("Maintenance")),
            It.Is<string>(s => s.Contains("Fix leak"))),
        Times.Once);

    mockNotifier.Verify(
        x => x.SendSmsAsync(
            It.IsAny<string>(),
            It.Is<string>(s => s.Contains("maintenance"))),
        Times.Once);
}
```

---

**Step 2: GREEN - Update Service with DI**

```csharp
public class PropertyService
{
    private readonly PropertyContext _context;
    private readonly INotificationService? _notificationService;

    public PropertyService(PropertyContext context)
    {
        _context = context;
    }

    public PropertyService(PropertyContext context, INotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }

    public async Task NotifyOwnerOfMaintenanceAsync(string ownerEmail, MaintenanceRequest request)
    {
        if (_notificationService == null)
            throw new InvalidOperationException("Notification service not configured");

        await _notificationService.SendEmailAsync(
            ownerEmail,
            $"Maintenance Request - Priority: {request.Priority}",
            $"A maintenance request has been submitted:\n\n{request.Description}");

        await _notificationService.SendSmsAsync(
            ownerEmail,  // Would be phone in real implementation
            $"New maintenance request submitted for your property.");
    }
}
```

---

## Complete HomeownerService - Final Refactored Version

```csharp
using System.Net.Mail;
using RealManage.HomeownerSetup.Models;

namespace RealManage.HomeownerSetup.Services;

public class HomeownerService
{
    private readonly List<Homeowner> _homeowners = new();
    private readonly List<PaymentPlan> _paymentPlans = new();
    private readonly IEmailService? _emailService;

    public HomeownerService() { }

    public HomeownerService(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public bool AddHomeowner(Homeowner homeowner)
    {
        ValidateHomeowner(homeowner);

        if (IsDuplicateEmail(homeowner.Email))
            return false;

        _homeowners.Add(homeowner);
        return true;
    }

    public Homeowner? GetHomeowner(string email)
    {
        return _homeowners.FirstOrDefault(h =>
            h.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public PaymentPlan SetupPaymentPlan(string email, decimal monthlyAmount, int dayOfMonth)
    {
        ValidatePaymentPlanInput(email, monthlyAmount, dayOfMonth);

        var homeowner = GetHomeowner(email)
            ?? throw new InvalidOperationException($"Homeowner with email {email} not found");

        var plan = new PaymentPlan
        {
            HomeownerEmail = email,
            MonthlyAmount = monthlyAmount,
            DayOfMonth = dayOfMonth
        };

        _paymentPlans.Add(plan);
        return plan;
    }

    public async Task GenerateWelcomePacketAsync(string email)
    {
        var homeowner = GetHomeowner(email)
            ?? throw new InvalidOperationException("Homeowner not found");

        if (_emailService != null)
        {
            await _emailService.SendEmailAsync(
                homeowner.Email,
                $"Welcome to Your HOA, {homeowner.FirstName}!",
                GenerateWelcomeEmailBody(homeowner));
        }
    }

    private void ValidateHomeowner(Homeowner homeowner)
    {
        if (string.IsNullOrWhiteSpace(homeowner.Email))
            throw new ArgumentException("Email is required");

        if (!IsValidEmailFormat(homeowner.Email))
            throw new ArgumentException("Email format is invalid");

        if (string.IsNullOrWhiteSpace(homeowner.FirstName))
            throw new ArgumentException("FirstName is required");

        if (string.IsNullOrWhiteSpace(homeowner.LastName))
            throw new ArgumentException("LastName is required");
    }

    private void ValidatePaymentPlanInput(string email, decimal monthlyAmount, int dayOfMonth)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required");

        if (monthlyAmount <= 0)
            throw new ArgumentException("Monthly amount must be positive");

        if (dayOfMonth < 1 || dayOfMonth > 28)
            throw new ArgumentException("Day of month must be between 1 and 28");
    }

    private bool IsDuplicateEmail(string email)
    {
        return _homeowners.Any(h =>
            h.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    private static bool IsValidEmailFormat(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private static string GenerateWelcomeEmailBody(Homeowner homeowner)
    {
        return $@"Dear {homeowner.FirstName},

Welcome to our community! We're excited to have you as a homeowner.

Your account has been set up with the email: {homeowner.Email}

Next steps:
1. Set up your payment plan
2. Review community guidelines
3. Attend the next HOA meeting

Best regards,
Your HOA Management Team";
    }
}
```

---

## Key Takeaways for Instructors

### The TDD Process - What to Watch For

1. **Students writing implementation first** - Redirect them to write the test first
2. **Students writing too much in one test** - Break it into smaller tests
3. **Students modifying tests to match implementation** - Tests should remain stable
4. **Students adding "extra" code** - Only code needed to pass the current test

### Common Student Mistakes

| Mistake | What They Do | What to Do Instead |
| ------- | ------------ | ------------------ |
| Big Bang TDD | Write 10 tests, then implement all | One test at a time |
| Test After | Write code, then write tests | Write test first, see red |
| Gold Plating | Add features not required by tests | Minimal code only |
| Test Modification | Change test when it fails | Fix implementation instead |
| Over-Mocking | Mock everything | Only mock external dependencies |

### Assessment Criteria

Students should be able to:

1. **Articulate the cycle**: Describe Red-Green-Refactor
2. **Write failing tests first**: Show compilation or assertion failure
3. **Write minimal code**: No extra functionality
4. **Refactor safely**: Tests stay green
5. **Use mocks appropriately**: For external dependencies only
6. **Achieve coverage naturally**: 80-90% without chasing it

---

*Remember: TDD is a discipline, not just a technique. The process matters as much as the result.*
