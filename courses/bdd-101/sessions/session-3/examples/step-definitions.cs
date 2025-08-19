// Step Definitions Examples for BDD Training
// This file shows various patterns and techniques for writing SpecFlow step definitions

using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using System.Collections.Generic;
using System.Linq;

namespace BDDDemo.Tests.StepDefinitions
{
    // ===================================================================
    // BASIC STEP DEFINITIONS
    // ===================================================================

    [Binding]
    public class BasicStepDefinitions
    {
        private decimal _accountBalance;
        private string? _lastErrorMessage;
        private bool _operationSuccess;

        // Simple Given step with parameter extraction
        [Given(@"I have \$(.*) in my account")]
        public void GivenIHaveMoneyInMyAccount(decimal amount)
        {
            _accountBalance = amount;
        }

        // When step that performs an action
        [When(@"I withdraw \$(.*)")]
        public void WhenIWithdraw(decimal amount)
        {
            if (amount <= _accountBalance)
            {
                _accountBalance -= amount;
                _operationSuccess = true;
            }
            else
            {
                _lastErrorMessage = "Insufficient funds";
                _operationSuccess = false;
            }
        }

        // Then step that verifies outcome
        [Then(@"my balance should be \$(.*)")]
        public void ThenMyBalanceShouldBe(decimal expectedBalance)
        {
            Assert.AreEqual(expectedBalance, _accountBalance);
        }

        // Then step for error verification
        [Then(@"I should see the error ""(.*)""")]
        public void ThenIShouldSeeTheError(string expectedError)
        {
            Assert.AreEqual(expectedError, _lastErrorMessage);
        }
    }

    // ===================================================================
    // DATA TABLE EXAMPLES
    // ===================================================================

    [Binding]
    public class DataTableStepDefinitions
    {
        private List<Product> _cart = new List<Product>();
        private decimal _cartTotal;

        // Step that accepts a data table
        [When(@"I add the following products to my cart:")]
        public void WhenIAddTheFollowingProductsToMyCart(Table table)
        {
            foreach (var row in table.Rows)
            {
                var product = new Product
                {
                    Name = row["Product"],
                    Price = decimal.Parse(row["Price"]),
                    Quantity = int.Parse(row["Quantity"])
                };
                
                _cart.Add(product);
            }
            
            _cartTotal = _cart.Sum(p => p.Price * p.Quantity);
        }

        // Alternative: Use SpecFlow.Assist for automatic object creation
        [When(@"I add these items to my cart:")]
        public void WhenIAddTheseItemsToMyCart(Table table)
        {
            var products = table.CreateSet<Product>();
            _cart.AddRange(products);
            _cartTotal = _cart.Sum(p => p.Price * p.Quantity);
        }

        [Then(@"my cart total should be \$(.*)")]
        public void ThenMyCartTotalShouldBe(decimal expectedTotal)
        {
            Assert.AreEqual(expectedTotal, _cartTotal);
        }

        // Helper class for data table binding
        public class Product
        {
            public string Name { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }
    }

    // ===================================================================
    // SCENARIO CONTEXT EXAMPLE
    // ===================================================================

    [Binding]
    public class ScenarioContextStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        // Dependency injection of ScenarioContext
        public ScenarioContextStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I have a customer named ""(.*)""")]
        public void GivenIHaveACustomerNamed(string customerName)
        {
            var customer = new Customer { Name = customerName };
            _scenarioContext.Set(customer, "CurrentCustomer");
        }

        [When(@"the customer places an order for \$(.*)")]
        public void WhenTheCustomerPlacesAnOrder(decimal amount)
        {
            var customer = _scenarioContext.Get<Customer>("CurrentCustomer");
            var order = new Order 
            { 
                CustomerId = customer.Id, 
                Amount = amount,
                Status = "Placed" 
            };
            
            _scenarioContext.Set(order, "CurrentOrder");
        }

        [Then(@"the order should be confirmed")]
        public void ThenTheOrderShouldBeConfirmed()
        {
            var order = _scenarioContext.Get<Order>("CurrentOrder");
            Assert.AreEqual("Confirmed", order.Status);
        }

        // Helper classes
        public class Customer
        {
            public int Id { get; set; } = 1;
            public string Name { get; set; } = string.Empty;
        }

        public class Order
        {
            public int CustomerId { get; set; }
            public decimal Amount { get; set; }
            public string Status { get; set; } = string.Empty;
        }
    }

    // ===================================================================
    // ADVANCED PARAMETER HANDLING
    // ===================================================================

    [Binding]
    public class AdvancedParameterStepDefinitions
    {
        private User? _currentUser;
        private List<string> _permissions = new List<string>();

        // Multiple parameter extraction
        [Given(@"I am a (.*) user with (.*) access level")]
        public void GivenIAmAUserWithAccessLevel(string userType, string accessLevel)
        {
            _currentUser = new User 
            { 
                Type = userType, 
                AccessLevel = accessLevel 
            };
        }

        // Optional parameters using regex groups
        [Given(@"I have(?: (\d+))? items in my cart")]
        public void GivenIHaveItemsInMyCart(int? itemCount = null)
        {
            var count = itemCount ?? 1; // Default to 1 if not specified
            // Setup cart with specified number of items
        }

        // Enum parameter parsing
        [When(@"I set my notification preference to (.*)")]
        public void WhenISetMyNotificationPreferenceTo(NotificationPreference preference)
        {
            _currentUser!.NotificationPreference = preference;
        }

        // Custom parameter transformation
        [StepArgumentTransformation]
        public NotificationPreference TransformNotificationPreference(string preference)
        {
            return preference.ToLower() switch
            {
                "email" => NotificationPreference.Email,
                "sms" => NotificationPreference.SMS,
                "push" => NotificationPreference.Push,
                "none" => NotificationPreference.None,
                _ => throw new ArgumentException($"Unknown preference: {preference}")
            };
        }

        // Helper classes and enums
        public class User
        {
            public string Type { get; set; } = string.Empty;
            public string AccessLevel { get; set; } = string.Empty;
            public NotificationPreference NotificationPreference { get; set; }
        }

        public enum NotificationPreference
        {
            Email,
            SMS, 
            Push,
            None
        }
    }

    // ===================================================================
    // HOOKS EXAMPLE
    // ===================================================================

    [Binding]
    public class TestHooks
    {
        private static TestDatabase? _database;
        private readonly ScenarioContext _scenarioContext;

        public TestHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        // Runs once before all scenarios in the test run
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            // Initialize test infrastructure
            _database = new TestDatabase();
            _database.Initialize();
        }

        // Runs once after all scenarios in the test run
        [AfterTestRun]
        public static void AfterTestRun()
        {
            // Cleanup test infrastructure
            _database?.Dispose();
        }

        // Runs before each feature
        [BeforeFeature]
        public static void BeforeFeature()
        {
            // Feature-level setup
            _database?.SeedFeatureData();
        }

        // Runs after each feature
        [AfterFeature]
        public static void AfterFeature()
        {
            // Feature-level cleanup
            _database?.CleanupFeatureData();
        }

        // Runs before each scenario
        [BeforeScenario]
        public void BeforeScenario()
        {
            // Scenario-level setup
            var testData = new TestDataBuilder().Build();
            _scenarioContext.Set(testData, "TestData");
        }

        // Runs after each scenario
        [AfterScenario]
        public void AfterScenario()
        {
            // Scenario-level cleanup
            if (_scenarioContext.ContainsKey("TestData"))
            {
                var testData = _scenarioContext.Get<TestData>("TestData");
                testData.Cleanup();
            }
        }

        // Runs only for scenarios with specific tags
        [BeforeScenario("@database")]
        public void BeforeDatabaseScenario()
        {
            // Setup specific to database scenarios
            _database?.BeginTransaction();
        }

        [AfterScenario("@database")]
        public void AfterDatabaseScenario()
        {
            // Cleanup specific to database scenarios
            _database?.RollbackTransaction();
        }

        // Helper classes
        public class TestDatabase
        {
            public void Initialize() { /* Initialize test database */ }
            public void SeedFeatureData() { /* Add feature test data */ }
            public void CleanupFeatureData() { /* Remove feature test data */ }
            public void BeginTransaction() { /* Start database transaction */ }
            public void RollbackTransaction() { /* Rollback database transaction */ }
            public void Dispose() { /* Cleanup database */ }
        }

        public class TestData
        {
            public void Cleanup() { /* Cleanup test data */ }
        }

        public class TestDataBuilder
        {
            public TestData Build() => new TestData();
        }
    }

    // ===================================================================
    // PAGE OBJECT PATTERN INTEGRATION
    // ===================================================================

    [Binding]
    public class WebStepDefinitions
    {
        private readonly LoginPage _loginPage;
        private readonly DashboardPage _dashboardPage;
        private readonly ScenarioContext _scenarioContext;

        public WebStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            // In real implementation, you'd inject these or create based on driver
            _loginPage = new LoginPage();
            _dashboardPage = new DashboardPage();
        }

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            _loginPage.Navigate();
        }

        [When(@"I login as ""(.*)"" with password ""(.*)""")]
        public void WhenILoginAsWithPassword(string username, string password)
        {
            _loginPage.Login(username, password);
        }

        [Then(@"I should see the dashboard")]
        public void ThenIShouldSeeTheDashboard()
        {
            Assert.IsTrue(_dashboardPage.IsDisplayed());
        }

        [Then(@"I should see welcome message for ""(.*)""")]
        public void ThenIShouldSeeWelcomeMessageFor(string username)
        {
            var welcomeMessage = _dashboardPage.GetWelcomeMessage();
            Assert.IsTrue(welcomeMessage.Contains(username));
        }

        // Mock page objects for example
        public class LoginPage
        {
            public void Navigate() { /* Navigate to login page */ }
            public void Login(string username, string password) { /* Perform login */ }
        }

        public class DashboardPage
        {
            public bool IsDisplayed() => true; // Mock implementation
            public string GetWelcomeMessage() => "Welcome John!"; // Mock implementation
        }
    }

    // ===================================================================
    // BUSINESS SERVICE INTEGRATION
    // ===================================================================

    [Binding]
    public class BusinessServiceStepDefinitions
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly ScenarioContext _scenarioContext;

        // In real implementation, use dependency injection
        public BusinessServiceStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _orderService = new MockOrderService(); // Would be injected
            _customerService = new MockCustomerService(); // Would be injected
        }

        [Given(@"customer ""(.*)"" exists in the system")]
        public void GivenCustomerExistsInTheSystem(string customerName)
        {
            var customer = _customerService.CreateCustomer(customerName);
            _scenarioContext.Set(customer, "Customer");
        }

        [When(@"the customer places an order for product ""(.*)""")]
        public void WhenTheCustomerPlacesAnOrderForProduct(string productName)
        {
            var customer = _scenarioContext.Get<Customer>("Customer");
            var order = _orderService.CreateOrder(customer.Id, productName);
            _scenarioContext.Set(order, "Order");
        }

        [Then(@"the order should be in ""(.*)"" status")]
        public void ThenTheOrderShouldBeInStatus(string expectedStatus)
        {
            var order = _scenarioContext.Get<Order>("Order");
            Assert.AreEqual(expectedStatus, order.Status);
        }

        // Mock services for example
        public interface IOrderService
        {
            Order CreateOrder(int customerId, string productName);
        }

        public interface ICustomerService
        {
            Customer CreateCustomer(string name);
        }

        public class MockOrderService : IOrderService
        {
            public Order CreateOrder(int customerId, string productName)
            {
                return new Order { CustomerId = customerId, Status = "Created" };
            }
        }

        public class MockCustomerService : ICustomerService
        {
            public Customer CreateCustomer(string name)
            {
                return new Customer { Id = 1, Name = name };
            }
        }

        public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class Order
        {
            public int CustomerId { get; set; }
            public string Status { get; set; } = string.Empty;
        }
    }

    // ===================================================================
    // ERROR HANDLING EXAMPLES
    // ===================================================================

    [Binding]
    public class ErrorHandlingStepDefinitions
    {
        private Exception? _lastException;
        private string? _lastErrorMessage;

        [When(@"I attempt to (.*) with invalid data")]
        public void WhenIAttemptToWithInvalidData(string action)
        {
            try
            {
                // Simulate operation that might fail
                switch (action.ToLower())
                {
                    case "register":
                        throw new ValidationException("Email address is required");
                    case "login":
                        throw new AuthenticationException("Invalid credentials");
                    default:
                        throw new InvalidOperationException($"Unknown action: {action}");
                }
            }
            catch (Exception ex)
            {
                _lastException = ex;
                _lastErrorMessage = ex.Message;
            }
        }

        [Then(@"I should receive a validation error")]
        public void ThenIShouldReceiveAValidationError()
        {
            Assert.IsNotNull(_lastException);
            Assert.IsInstanceOfType(_lastException, typeof(ValidationException));
        }

        [Then(@"the error message should be ""(.*)""")]
        public void ThenTheErrorMessageShouldBe(string expectedMessage)
        {
            Assert.AreEqual(expectedMessage, _lastErrorMessage);
        }

        // Custom exceptions for example
        public class ValidationException : Exception
        {
            public ValidationException(string message) : base(message) { }
        }

        public class AuthenticationException : Exception
        {
            public AuthenticationException(string message) : base(message) { }
        }
    }
}

// ===================================================================
// BEST PRACTICES SUMMARY
// ===================================================================

/*
1. Keep step definitions simple and focused
2. Use meaningful parameter names and types
3. Leverage ScenarioContext for sharing data between steps
4. Use hooks for setup and teardown
5. Integrate with Page Objects for UI automation
6. Work with business services, not UI when possible
7. Handle errors gracefully and provide clear assertions
8. Use data tables for complex input data
9. Make step definitions reusable across scenarios
10. Follow consistent naming conventions

Common Patterns:
- Given: Setup initial state
- When: Perform action
- Then: Verify outcome
- And/But: Continue previous step type

Remember: Step definitions should be thin wrappers around your business logic,
not contain complex implementation details.
*/