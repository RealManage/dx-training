# Session 3: From Scenarios to Automation

**Duration:** 2 hours  
**Format:** Hands-on workshop with coding exercises  
**Prerequisites:** Completed Sessions 1 & 2, basic programming knowledge

## 🎯 Learning Objectives

By the end of this session, participants will be able to:

- ✅ Understand how BDD scenarios become executable tests
- ✅ Set up a basic BDD automation framework
- ✅ Write step definitions that implement scenario steps
- ✅ Run automated BDD tests and interpret results
- ✅ Plan BDD automation strategy for their team
- ✅ Integrate BDD tests into development workflow

## 📚 Session Agenda

### 1. Welcome & Automation Overview (15 minutes)

#### Recap: The BDD Journey So Far

- **Session 1:** Learn to write scenarios that business stakeholders understand
- **Session 2:** Write high-quality scenarios that serve as living documentation  
- **Session 3:** Make scenarios executable to verify system behavior

#### BDD Automation Benefits

- **Living Documentation:** Scenarios stay current with code
- **Regression Testing:** Automated verification of business rules
- **Collaboration:** Shared understanding between business and technical teams
- **Confidence:** Quick feedback when business logic changes

---

### 2. BDD Automation Landscape (20 minutes)

#### 2.1 Popular BDD Tools by Platform

##### For .NET (C#)

- **SpecFlow** - Most popular .NET BDD framework
- **LightBDD** - Lightweight alternative
- **NBehave** - Older framework, less active

##### For JavaScript/TypeScript

- **Cucumber-JS** - JavaScript implementation of Cucumber
- **CodeceptJS** - BDD framework with multiple backends

##### For Python

- **Behave** - Python BDD framework
- **Pytest-BDD** - pytest plugin for BDD

#### 2.2 SpecFlow Architecture (Focus for this session)

```text
Feature Files (.feature)
         ↓
   SpecFlow Runtime
         ↓  
   Step Definitions (.cs)
         ↓
   Application Under Test
```

**Key Components:**

- **Feature Files:** Gherkin scenarios (.feature files)
- **Step Definitions:** C# methods that implement scenario steps
- **Test Runner:** Executes scenarios and reports results
- **Application Bindings:** Interface with your application code

#### 2.3 The Test Pyramid and BDD

```text
    /\
   /UI\      ← Few BDD scenarios for critical user journeys
  /____\
 / API  \    ← Some BDD scenarios for business logic
/________\
|  Unit  |   ← Many unit tests for technical details
|________|
```

##### BDD Sweet Spot

- Critical business workflows
- Complex business rules
- Integration between system components
- User acceptance criteria

---

### 3. Setting Up SpecFlow (30 minutes)

#### 3.1 Project Setup (10 minutes)

**Create a new test project:**

```bash
# Create solution and projects
dotnet new sln -n BDDDemo
dotnet new classlib -n BDDDemo.Library
dotnet new mstest -n BDDDemo.Tests

# Add projects to solution
dotnet sln add BDDDemo.Library
dotnet sln add BDDDemo.Tests

# Add project reference
dotnet add BDDDemo.Tests reference BDDDemo.Library
```

**Add SpecFlow packages:**

```bash
cd BDDDemo.Tests
dotnet add package SpecFlow
dotnet add package SpecFlow.Tools.MsBuild.Generation
dotnet add package SpecFlow.MsTest
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package MSTest.TestAdapter
dotnet add package MSTest.TestFramework
```

#### 3.2 Configuration (10 minutes)

**Create specflow.json:**

```json
{
  "language": {
    "feature": "en-us"
  },
  "unitTestProvider": {
    "name": "mstest"
  },
  "runtime": {
    "dependencies": []
  }
}
```

**Project Structure:**

```text
BDDDemo.sln
├── BDDDemo.Library/
│   ├── Models/
│   │   └── BankAccount.cs
│   └── Services/
│       └── BankingService.cs
└── BDDDemo.Tests/
    ├── Features/
    │   └── BankAccount.feature
    ├── StepDefinitions/
    │   └── BankAccountSteps.cs
    └── specflow.json
```

#### 3.3 First Feature File (10 minutes)

**Create Features/BankAccount.feature:**

```gherkin
Feature: Bank Account Management
  As a bank customer
  I want to manage my bank account
  So that I can deposit, withdraw, and check my balance

Scenario: Deposit money into account
  Given I have a bank account with balance $100
  When I deposit $50
  Then my account balance should be $150

Scenario: Withdraw money from account with sufficient funds
  Given I have a bank account with balance $100
  When I withdraw $30
  Then my account balance should be $70

Scenario: Cannot withdraw more than account balance
  Given I have a bank account with balance $50
  When I attempt to withdraw $100
  Then I should see an error "Insufficient funds"
  And my account balance should remain $50
```

---

### 4. Writing Step Definitions (45 minutes)

#### 4.1 Understanding Step Definitions (15 minutes)

**Step definitions are methods that:**

- Implement the behavior described in Gherkin steps
- Use attributes to match step text patterns
- Can access shared context between steps
- Interact with your application code

**Step Attributes:**

```csharp
[Given(@"...")] // Sets up initial context
[When(@"...")]  // Performs an action
[Then(@"...")]  // Verifies the outcome
```

#### 4.2 Basic Application Code (10 minutes)

**Create BDDDemo.Library/Models/BankAccount.cs:**

```csharp
namespace BDDDemo.Library.Models
{
    public class BankAccount
    {
        public decimal Balance { get; private set; }
        public string? LastError { get; private set; }

        public BankAccount(decimal initialBalance = 0)
        {
            Balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                LastError = "Deposit amount must be positive";
                return;
            }

            Balance += amount;
            LastError = null;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                LastError = "Withdrawal amount must be positive";
                return false;
            }

            if (amount > Balance)
            {
                LastError = "Insufficient funds";
                return false;
            }

            Balance -= amount;
            LastError = null;
            return true;
        }
    }
}
```

#### 4.3 Implementing Step Definitions (20 minutes)

**Create BDDDemo.Tests/StepDefinitions/BankAccountSteps.cs:**

```csharp
using BDDDemo.Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace BDDDemo.Tests.StepDefinitions
{
    [Binding]
    public class BankAccountSteps
    {
        private BankAccount? _account;
        private string? _lastError;

        [Given(@"I have a bank account with balance \$(.*)")]
        public void GivenIHaveABankAccountWithBalance(decimal balance)
        {
            _account = new BankAccount(balance);
        }

        [When(@"I deposit \$(.*)")]
        public void WhenIDeposit(decimal amount)
        {
            _account?.Deposit(amount);
        }

        [When(@"I withdraw \$(.*)")]
        public void WhenIWithdraw(decimal amount)
        {
            _account?.Withdraw(amount);
        }

        [When(@"I attempt to withdraw \$(.*)")]
        public void WhenIAttemptToWithdraw(decimal amount)
        {
            var success = _account?.Withdraw(amount);
            if (!success)
            {
                _lastError = _account?.LastError;
            }
        }

        [Then(@"my account balance should be \$(.*)")]
        public void ThenMyAccountBalanceShouldBe(decimal expectedBalance)
        {
            Assert.AreEqual(expectedBalance, _account?.Balance);
        }

        [Then(@"I should see an error ""(.*)""")]
        public void ThenIShouldSeeAnError(string expectedError)
        {
            Assert.AreEqual(expectedError, _lastError ?? _account?.LastError);
        }

        [Then(@"my account balance should remain \$(.*)")]
        public void ThenMyAccountBalanceShouldRemain(decimal expectedBalance)
        {
            Assert.AreEqual(expectedBalance, _account?.Balance);
        }
    }
}
```

**Key Concepts:**

- **[Binding]** attribute marks the class as containing step definitions
- **Regular expressions** in step attributes match step text
- **Capture groups** extract parameters from step text
- **Context sharing** via private fields between steps in same scenario

---

### 5. Running and Debugging BDD Tests (15 minutes)

#### 5.1 Running Tests

**Command line:**

```bash
dotnet test
```

**Expected output:**

```text
Test run for BDDDemo.Tests.dll(.NETCoreApp,Version=v6.0)
Microsoft (R) Test Execution Command Line Tool Version 17.0.0

Starting test execution, please wait...
A total of 1 files were found that matched the given pattern.

Passed! - Failed: 0, Passed: 3, Skipped: 0, Total: 3
```

#### 5.2 Understanding Test Results

**When tests pass:**

- All assertions in Then steps succeed
- No exceptions thrown during execution
- Scenarios execute successfully start to finish

**When tests fail:**

- Clear error messages indicate which step failed
- Stack trace points to specific assertion or code issue
- SpecFlow provides context about which scenario failed

#### 5.3 Debugging Failed Tests

**Common issues:**

1. **Missing step definitions** - SpecFlow provides code templates
1. **Parameter parsing errors** - Check regex patterns
1. **Assertion failures** - Verify expected vs actual values
1. **Application logic bugs** - Debug your business code

---

### 6. Advanced Step Definition Techniques (20 minutes)

#### 6.1 Data Tables

**Scenario with data table:**

```gherkin
Scenario: Process multiple transactions
  Given I have a bank account with balance $1000
  When I process the following transactions:
    | Type | Amount |
    | Deposit | 200 |
    | Withdrawal | 150 |
    | Deposit | 75 |
  Then my account balance should be $1125
```

**Step definition with data table:**

```csharp
[When(@"I process the following transactions:")]
public void WhenIProcessTheFollowingTransactions(Table table)
{
    foreach (var row in table.Rows)
    {
        var type = row["Type"];
        var amount = decimal.Parse(row["Amount"]);
        
        if (type == "Deposit")
        {
            _account?.Deposit(amount);
        }
        else if (type == "Withdrawal")
        {
            _account?.Withdraw(amount);
        }
    }
}
```

#### 6.2 Scenario Context

**Sharing data between step definition classes:**

```csharp
[Binding]
public class BankAccountSteps
{
    private readonly ScenarioContext _scenarioContext;

    public BankAccountSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Given(@"I have a bank account with balance \$(.*)")]
    public void GivenIHaveABankAccountWithBalance(decimal balance)
    {
        var account = new BankAccount(balance);
        _scenarioContext.Set(account, "BankAccount");
    }

    [Then(@"my account balance should be \$(.*)")]
    public void ThenMyAccountBalanceShouldBe(decimal expectedBalance)
    {
        var account = _scenarioContext.Get<BankAccount>("BankAccount");
        Assert.AreEqual(expectedBalance, account.Balance);
    }
}
```

#### 6.3 Hooks for Setup and Teardown

**Before and after scenario execution:**

```csharp
[Binding]
public class TestHooks
{
    [BeforeScenario]
    public void BeforeScenario()
    {
        // Setup before each scenario
        // Initialize test data, reset state, etc.
    }

    [AfterScenario]
    public void AfterScenario()
    {
        // Cleanup after each scenario
        // Close connections, delete test data, etc.
    }

    [BeforeFeature]
    public static void BeforeFeature()
    {
        // Setup before all scenarios in a feature
    }

    [AfterFeature] 
    public static void AfterFeature()
    {
        // Cleanup after all scenarios in a feature
    }
}
```

---

### 7. Hands-on Exercise: E-commerce Shopping Cart (30 minutes)

#### 7.1 Exercise Setup (5 minutes)

**Your task:** Implement BDD automation for this feature:

```gherkin
Feature: Shopping Cart Management
  As an online shopper
  I want to manage items in my shopping cart
  So that I can control my purchases

Scenario: Add product to empty cart
  Given I have an empty shopping cart
  When I add a "Laptop" priced at $999.99
  Then my cart should contain 1 item
  And my cart total should be $999.99

Scenario: Add multiple quantities of same product
  Given I have an empty shopping cart
  When I add 3 "Books" priced at $15.00 each
  Then my cart should contain 3 items
  And my cart total should be $45.00

Scenario: Remove item from cart
  Given I have a "Laptop" priced at $999.99 in my cart
  When I remove the "Laptop" from my cart
  Then my cart should be empty
  And my cart total should be $0.00
```

#### 7.2 Individual Work (20 minutes)

**Tasks:**

1. Create the shopping cart model classes
1. Implement the step definitions
1. Run the tests and make them pass
1. Add one additional scenario of your choice

**Starter code structure:**

```csharp
// Models/Product.cs
public class Product
{
    public string Name { get; }
    public decimal Price { get; }
    // Constructor and other members
}

// Models/ShoppingCart.cs  
public class ShoppingCart
{
    public int ItemCount { get; }
    public decimal Total { get; }
    
    public void AddProduct(Product product, int quantity = 1)
    {
        // Implement
    }
    
    public void RemoveProduct(string productName)
    {
        // Implement
    }
}
```

#### 7.3 Group Review (5 minutes)

- Share solutions and approaches
- Discuss challenges encountered
- Compare different implementation strategies

---

### 8. BDD in the Development Workflow (10 minutes)

#### 8.1 Integration with CI/CD

**Typical workflow:**

1. **Write scenarios** during feature planning (Three Amigos)
1. **Implement step definitions** (developers)
1. **Run tests locally** before committing
1. **Automated testing** in CI pipeline
1. **Living documentation** generation

**Sample CI configuration (.github/workflows/bdd-tests.yml):**

```yaml
name: BDD Tests
on: [push, pull_request]

jobs:
  bdd-tests:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v2
    - name: Setup .NET
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: 6.0.x
    - name: Restore dependencies
      run: dotnet restore
    - name: Run BDD tests
      run: dotnet test --logger trx --results-directory TestResults
    - name: Generate test report
      uses: dorny/test-reporter@v1
      if: success() || failure()
      with:
        name: BDD Test Results
        path: TestResults/*.trx
        reporter: dotnet-trx
```

#### 8.2 Test Organization Strategies

**By Feature Area:**

```text
Features/
├── UserManagement/
│   ├── Registration.feature
│   └── Authentication.feature
├── Shopping/
│   ├── ProductCatalog.feature
│   └── ShoppingCart.feature
└── Orders/
    ├── Checkout.feature
    └── OrderTracking.feature
```

**By User Journey:**

```text
Features/
├── CustomerJourney.feature
├── AdminWorkflow.feature
└── SupportProcesses.feature
```

---

### 9. Best Practices and Pitfalls (10 minutes)

#### 9.1 Best Practices

**Step Definition Guidelines:**

```csharp
// ✅ Good - Reusable, clear parameter handling
[Given(@"I have a (.*) account with balance \$(.*)")]
public void GivenIHaveAnAccountWithBalance(string accountType, decimal balance)
{
    _account = AccountFactory.Create(accountType, balance);
}

// ❌ Avoid - Too specific, not reusable
[Given(@"I have a premium checking account with balance $1000.00")]
public void GivenIHaveAPremiumCheckingAccountWithBalance1000()
{
    _account = new PremiumCheckingAccount(1000.00m);
}
```

**Keep step definitions simple:**

```csharp
// ✅ Good - Delegates to business logic
[When(@"I transfer \$(.*) to account (.*)")]
public void WhenITransferMoneyToAccount(decimal amount, string accountNumber)
{
    _transferResult = _bankingService.Transfer(_account, accountNumber, amount);
}

// ❌ Avoid - Business logic in step definition
[When(@"I transfer \$(.*) to account (.*)")]
public void WhenITransferMoneyToAccount(decimal amount, string accountNumber)
{
    // Don't put complex business logic here
    if (_account.Balance >= amount && IsValidAccount(accountNumber))
    {
        _account.Balance -= amount;
        var targetAccount = FindAccount(accountNumber);
        targetAccount.Balance += amount;
        LogTransaction(amount, accountNumber);
        _transferResult = "Success";
    }
    else
    {
        _transferResult = "Failed";
    }
}
```

#### 9.2 Common Pitfalls

1. Brittle tests tied to UI

   ```csharp
   // ❌ Avoid - Tied to UI elements
   [When(@"I click the blue submit button in the top right corner")]
   public void WhenIClickTheBlueSubmitButton()
   {
       driver.FindElement(By.ClassName("blue-submit-btn")).Click();
   }

   // ✅ Better - Business action
   [When(@"I submit my application")]
   public void WhenISubmitMyApplication()
   {
       _applicationService.Submit(_currentApplication);
   }
   ```text

1. Over-engineering step definitions

   ```csharp
   // ❌ Avoid - Too complex
   [Given(@"I have a (.*) customer with (.*) credit rating and (.*) account history")]
   public void ComplexCustomerSetup(string type, string rating, string history)
   {
       // Lots of complex setup logic
   }

   // ✅ Better - Simple, focused steps  
   [Given(@"I am a premium customer")]
   public void GivenIAmAPremiumCustomer()
   {
       _customer = CustomerFactory.CreatePremium();
   }
   ```

---

### 10. Team Implementation Planning (10 minutes)

#### 10.1 Getting Started with BDD Automation

##### Week 1: Foundation

- [ ] Choose BDD tool (SpecFlow for .NET teams)
- [ ] Set up basic project structure
- [ ] Create first feature file
- [ ] Implement basic step definitions

##### Week 2: Expansion

- [ ] Add 2-3 more features
- [ ] Establish step definition patterns
- [ ] Create shared utilities
- [ ] Integrate with CI pipeline

##### Month 1: Adoption

- [ ] Train all team members
- [ ] Establish BDD workflow
- [ ] Create style guide
- [ ] Regular scenario reviews

#### 10.2 Success Metrics

##### Technical Metrics

- Test execution time
- Test reliability (pass/fail consistency)
- Code coverage of business logic
- Defect detection rate

##### Collaboration Metrics

- Business stakeholder participation in scenario writing
- Frequency of scenario updates
- Time from requirement to automated test
- Shared understanding of requirements

#### 10.3 Common Implementation Challenges

1. Challenge: Business stakeholders don't engage

   - **Solution:** Start with high-value features, show quick wins
   - **Solution:** Make scenarios easily accessible and readable
   - **Solution:** Regular demo sessions showing living documentation

1. Challenge: Tests are slow or unreliable

   - **Solution:** Focus on business logic, not UI automation
   - **Solution:** Use test doubles for external dependencies
   - **Solution:** Implement proper test data management

1. Challenge: Maintenance overhead

   - **Solution:** Keep step definitions simple and reusable
   - **Solution:** Regular refactoring of test code
   - **Solution:** Clear naming conventions and organization

---

### 11. Wrap-up and Next Steps (5 minutes)

#### 11.1 Course Completion Checklist

**Knowledge gained:**

- [ ] Understand BDD principles and collaboration benefits
- [ ] Can write clear, business-readable Gherkin scenarios
- [ ] Know how to avoid common BDD anti-patterns  
- [ ] Can set up basic BDD automation framework
- [ ] Can implement step definitions for scenarios
- [ ] Understand how to integrate BDD into development workflow

#### 11.2 Immediate Next Steps

**This week:**

1. Set up BDD framework for one feature in your project
2. Convert 2-3 existing user stories to Gherkin scenarios
3. Implement step definitions and get tests passing
4. Share results with your team

**Next month:**

1. Expand BDD to 5-10 scenarios covering critical business logic
2. Integrate BDD tests into your CI/CD pipeline
3. Establish regular Three Amigos sessions
4. Train other team members on BDD practices

#### 11.3 Continued Learning Resources

**Books:**

- "Specification by Example" by Gojko Adzic
- "The Cucumber Book" by Matt Wynne & Aslak Hellesøy
- "BDD in Action" by John Ferguson Smart

**Online Resources:**

- [SpecFlow Documentation](https://docs.specflow.org/)
- [Cucumber School](https://school.cucumber.io/)
- [BDD Community](https://cucumber.io/community/)

**Practice:**

- Implement BDD for your current project features
- Join BDD communities and forums
- Attend local meetups or conferences
- Share your BDD experiences with other teams

## 🤝 Getting Help

**After the course:**

- **Technical issues:** Check [troubleshooting guide](../../resources/troubleshooting.md)
- **Best practices:** Review [BDD patterns guide](../../resources/bdd-patterns.md)
- **Community:** Join SpecFlow or Cucumber communities
- **Internal support:** Create internal BDD community of practice

## Final Thoughts

BDD is ultimately about **communication and collaboration**. The tools and automation are important, but the real value comes from:

- **Shared understanding** between business and technical team members
- **Living documentation** that stays current with your software
- **Confidence** that your software delivers the intended business value
- **Quality** through early detection of misunderstood requirements

Start small, focus on high-value scenarios, and gradually expand your BDD practices. The investment in collaboration and shared understanding will pay dividends in reduced defects, better requirements, and improved team communication.

**Good luck with your BDD journey!**

---

**Course Complete!** 🎉

You now have the foundation to implement BDD practices in your team. Remember: start with collaboration, add scenarios gradually, and automate selectively. The goal is better software through better communication.
