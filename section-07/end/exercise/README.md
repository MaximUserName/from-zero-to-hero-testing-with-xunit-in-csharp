# Test Execution Control Exercise

Practice using xUnit's test execution control features to manage test timeouts, skipping, and environment-specific testing.

**🎯 Learning Objectives**: Master test timeouts, conditional skipping, test context, and parallel execution control.

## Scenario: Order Processing System

You're testing an order processing system that handles different types of orders with varying processing times. The system needs to:

- Process orders within specific time limits
- Skip tests when external services are unavailable
- Run different tests based on environment settings
- Provide debug information during test execution

## Your Mission

Create these two test classes using appropriate execution control features:

1. **`OrderServiceTests`** - Test order processing with timeouts and conditional skipping
2. **`PaymentServiceTests`** - Test payment processing with environment-based skipping

## 💡 Hints

**Test Timeouts:**
- Use `[Fact(Timeout = milliseconds)]` for time-limited tests
- Use `CancellationToken` in your service methods
- Simulate processing delays with `Task.Delay()`

**Conditional Skipping:**
- Use `Skip.If(condition, reason)` to skip tests conditionally
- Use `Skip.IfNot(condition, reason)` for inverse conditions
- Check environment variables or service availability

**Test Context:**
- Use `ITestOutputHelper` for debug output
- Access test metadata with `TestContext.Current`
- Log important test information

**Environment Detection:**
```csharp
public static class TestEnvironment
{
    public static bool IsCI => 
        !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI"));
    
    public static bool HasExternalAccess =>
        !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("EXTERNAL_API_KEY"));
}
```

## Test Requirements

**OrderServiceTests:**
- Test fast order processing (should complete under 2 seconds)
- Test slow order processing (should complete under 5 seconds, timeout at 6 seconds)
- Skip slow tests in CI environment
- Log processing times using `ITestOutputHelper`

**PaymentServiceTests:**
- Test payment processing (requires external API access)
- Skip payment tests when `EXTERNAL_API_KEY` environment variable is not set
- Use timeout of 10 seconds for payment operations
- Log payment attempts and results

## Sample Data

**Order:**
- Order ID: Generate new Guid
- Customer ID: 12345
- Amount: $99.99
- Type: "Standard" or "Express"

**Payment:**
- Amount: $99.99
- Currency: "USD"
- Card Number: "4111111111111111" (test card)

## Need help?
Go back and rewatch the previous lectures. It usually helps out. 
If you still need help after that, don't hesitate to reach out (https://guiferreira.me/about)!

## Looking for an accountability partner?
Tag me on X (@gsferreira) or LinkedIn (@gferreira), and I will be there for you.

Let's do it!