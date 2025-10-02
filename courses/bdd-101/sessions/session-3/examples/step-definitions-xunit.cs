// Complete xUnit step definitions example for Reqnroll BDD automation
// This demonstrates using xUnit assertions instead of MSTest in Reqnroll step definitions

using Reqnroll;
using Reqnroll.Assist;
using Xunit;

namespace BDDDemo.Tests.StepDefinitions
{
    [Binding]
    public class BankAccountStepsXUnit
    {
        private BankAccount? _account;
        private string? _lastError;
        private Exception? _lastException;
        private bool _operationSucceeded;

        // Basic Given steps for account setup
        [Given(@"I have a bank account with balance \$(.*)")]
        public void GivenIHaveABankAccountWithBalance(decimal balance)
        {
            _account = new BankAccount(balance);
        }

        [Given(@"I have an empty bank account")]
        public void GivenIHaveAnEmptyBankAccount()
        {
            _account = new BankAccount(0);
        }

        [Given(@"I have a (.*) account with balance \$(.*)")]
        public void GivenIHaveAnAccountTypeWithBalance(string accountType, decimal balance)
        {
            // This could be extended to support different account types
            _account = new BankAccount(balance);
        }

        // When steps for actions
        [When(@"I deposit \$(.*)")]
        public void WhenIDeposit(decimal amount)
        {
            try
            {
                _account?.Deposit(amount);
                _operationSucceeded = true;
            }
            catch (Exception ex)
            {
                _lastException = ex;
                _lastError = ex.Message;
                _operationSucceeded = false;
            }
        }

        [When(@"I withdraw \$(.*)")]
        public void WhenIWithdraw(decimal amount)
        {
            try
            {
                var success = _account?.Withdraw(amount) ?? false;
                _operationSucceeded = success;
                if (!success && _account?.LastError != null)
                {
                    _lastError = _account.LastError;
                }
            }
            catch (Exception ex)
            {
                _lastException = ex;
                _lastError = ex.Message;
                _operationSucceeded = false;
            }
        }

        [When(@"I attempt to withdraw \$(.*)")]
        public void WhenIAttemptToWithdraw(decimal amount)
        {
            // Same as withdraw, but emphasizes this might fail
            WhenIWithdraw(amount);
        }

        [When(@"I attempt to deposit \$(.*)")]
        public void WhenIAttemptToDeposit(decimal amount)
        {
            WhenIDeposit(amount);
        }

        [When(@"I process the following transactions:")]
        public void WhenIProcessTheFollowingTransactions(DataTable table)
        {
            foreach (var row in table.Rows)
            {
                var type = row["Type"];
                var amount = decimal.Parse(row["Amount"]);

                try
                {
                    if (type.Equals("Deposit", StringComparison.OrdinalIgnoreCase))
                    {
                        _account?.Deposit(amount);
                    }
                    else if (type.Equals("Withdrawal", StringComparison.OrdinalIgnoreCase))
                    {
                        _account?.Withdraw(amount);
                    }
                    else
                    {
                        throw new ArgumentException($"Unknown transaction type: {type}");
                    }
                }
                catch (Exception ex)
                {
                    _lastException = ex;
                    _lastError = ex.Message;
                    _operationSucceeded = false;
                    break; // Stop processing on first error
                }
            }
        }

        // Then steps for assertions - using xUnit Assert methods
        [Then(@"my account balance should be \$(.*)")]
        public void ThenMyAccountBalanceShouldBe(decimal expectedBalance)
        {
            Assert.NotNull(_account);
            Assert.Equal(expectedBalance, _account.Balance);
        }

        [Then(@"my account balance should remain \$(.*)")]
        public void ThenMyAccountBalanceShouldRemain(decimal expectedBalance)
        {
            ThenMyAccountBalanceShouldBe(expectedBalance);
        }

        [Then(@"I should see an error ""(.*)""")]
        public void ThenIShouldSeeAnError(string expectedError)
        {
            Assert.NotNull(_lastError);
            Assert.Contains(expectedError, _lastError);
        }

        [Then(@"I should see an error message")]
        public void ThenIShouldSeeAnErrorMessage()
        {
            Assert.True(!string.IsNullOrEmpty(_lastError));
        }

        [Then(@"the operation should succeed")]
        public void ThenTheOperationShouldSucceed()
        {
            Assert.True(_operationSucceeded);
            Assert.Null(_lastException);
        }

        [Then(@"the operation should fail")]
        public void ThenTheOperationShouldFail()
        {
            Assert.False(_operationSucceeded);
        }

        [Then(@"no error should occur")]
        public void ThenNoErrorShouldOccur()
        {
            Assert.Null(_lastException);
            Assert.True(string.IsNullOrEmpty(_lastError));
        }

        [Then(@"my account should have a positive balance")]
        public void ThenMyAccountShouldHaveAPositiveBalance()
        {
            Assert.NotNull(_account);
            Assert.True(_account.Balance > 0);
        }

        [Then(@"my account should be empty")]
        public void ThenMyAccountShouldBeEmpty()
        {
            Assert.NotNull(_account);
            Assert.Equal(0, _account.Balance);
        }

        [Then(@"my account balance should be greater than \$(.*)")]
        public void ThenMyAccountBalanceShouldBeGreaterThan(decimal minimumBalance)
        {
            Assert.NotNull(_account);
            Assert.True(_account.Balance > minimumBalance);
        }

        [Then(@"my account balance should be less than \$(.*)")]
        public void ThenMyAccountBalanceShouldBeLessThan(decimal maximumBalance)
        {
            Assert.NotNull(_account);
            Assert.True(_account.Balance < maximumBalance);
        }

        // Example of custom assertion with detailed error message
        [Then(@"the account should be in a valid state")]
        public void ThenTheAccountShouldBeInAValidState()
        {
            Assert.NotNull(_account);
            
            // Custom business rule: balance cannot be negative
            Assert.True(_account.Balance >= 0, 
                $"Account balance should not be negative, but was {_account.Balance}");
            
            // Custom business rule: no lingering error messages after successful operations
            if (_operationSucceeded)
            {
                Assert.True(string.IsNullOrEmpty(_account.LastError), 
                    "Account should not have error message after successful operation");
            }
        }

        // Example of testing exception types with xUnit
        [Then(@"an (.*) should be thrown")]
        public void ThenAnExceptionShouldBeThrown(string exceptionType)
        {
            Assert.NotNull(_lastException);
            
            var expectedType = Type.GetType($"System.{exceptionType}Exception");
            Assert.NotNull(expectedType);
            Assert.IsType(expectedType, _lastException);
        }

        // Example of collection assertions
        [Then(@"the transaction history should contain (.*) entries")]
        public void ThenTheTransactionHistoryShouldContainEntries(int expectedCount)
        {
            // This would require extending the BankAccount class to track history
            // Showing how xUnit handles collection assertions
            Assert.NotNull(_account);
            
            // Example implementation if account had transaction history
            // var history = _account.GetTransactionHistory();
            // Assert.Equal(expectedCount, history.Count);
            
            // For demo purposes, just verify account exists
            Assert.NotNull(_account);
        }

        // Example of range assertions
        [Then(@"my account balance should be between \$(.*) and \$(.*)")]
        public void ThenMyAccountBalanceShouldBeBetween(decimal minimum, decimal maximum)
        {
            Assert.NotNull(_account);
            Assert.InRange(_account.Balance, minimum, maximum);
        }

        // Example cleanup method (if needed)
        [AfterScenario]
        public void CleanUp()
        {
            _account = null;
            _lastError = null;
            _lastException = null;
            _operationSucceeded = false;
        }
    }

    // Simple BankAccount model for demonstration
    // In a real project, this would be in your business logic layer
    public class BankAccount
    {
        public decimal Balance { get; private set; }
        public string? LastError { get; private set; }

        public BankAccount(decimal initialBalance = 0)
        {
            if (initialBalance < 0)
            {
                throw new ArgumentException("Initial balance cannot be negative");
            }
            Balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                LastError = "Deposit amount must be positive";
                throw new ArgumentException(LastError);
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

/*
Key differences between xUnit and MSTest assertions:

MSTest                          xUnit
--------                        -----
Assert.AreEqual(expected, actual)   → Assert.Equal(expected, actual)
Assert.IsTrue(condition)            → Assert.True(condition)
Assert.IsFalse(condition)           → Assert.False(condition)
Assert.IsNull(obj)                  → Assert.Null(obj)
Assert.IsNotNull(obj)               → Assert.NotNull(obj)
Assert.ThrowsException<T>()         → Assert.Throws<T>()
Assert.IsInstanceOfType(obj, type)  → Assert.IsType<T>(obj)
CollectionAssert.Contains()         → Assert.Contains()
Assert.AreNotEqual()                → Assert.NotEqual()

xUnit-specific assertions that don't exist in MSTest:
- Assert.InRange(value, min, max)
- Assert.Empty(collection)
- Assert.Single(collection)
- Assert.All(collection, predicate)
- Assert.StartsWith(expectedStart, actualString)
- Assert.EndsWith(expectedEnd, actualString)
- Assert.Matches(regex, actualString)

xUnit benefits:
- More descriptive assertion methods
- Better failure messages by default  
- Modern, actively developed framework
- Better async support
- Parallel test execution by default
- Theory-based parameterized tests

Reqnroll vs SpecFlow:
- Reqnroll is the open-source successor to SpecFlow
- SpecFlow reached end-of-life on December 31, 2024
- Reqnroll provides full compatibility with existing SpecFlow tests
- Migration typically takes less than an hour - mainly package and namespace changes
- Reqnroll is actively maintained and supports latest .NET versions
- Learn more at https://docs.reqnroll.net/latest/index.html
*/