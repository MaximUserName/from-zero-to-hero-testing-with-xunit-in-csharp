# Output and Diagnostics Exercise

Practice using xUnit diagnostic features to capture detailed test information and improve debugging capabilities.

**🎯 Learning Objectives**: Master ITestOutputHelper, diagnostic messages, and debugging techniques for better test observability.

## Scenario: Payment Processing System

You're testing a payment processing system that handles different types of transactions. Here's an example of the payment data:

```json
{
  "transactionId": "TXN-001",
  "amount": 99.99,
  "currency": "USD",
  "paymentMethod": "CreditCard",
  "customerId": "CUST-123"
}
```

The system has different components with varying diagnostic needs:

- **Payment Validator** - Simple validation logic (basic output needed)
- **Payment Processor** - Complex processing with external services (comprehensive diagnostics needed)

**The Problem:** When tests fail in CI/CD, you need detailed diagnostic information to understand what went wrong without access to a debugger.

## Your Mission

Create these two test classes using appropriate diagnostic techniques:

1. **`PaymentValidatorTests`** - Test payment validation with basic ITestOutputHelper output
2. **`PaymentProcessorTests`** - Test payment processing with comprehensive diagnostics and error capture

## 💡 Hints

**Basic Output (PaymentValidatorTests):**
- Inject `ITestOutputHelper` via constructor
- Use `_output.WriteLine()` to log test steps
- Include input parameters and results in output
- Log timing information for performance insights

**Comprehensive Diagnostics (PaymentProcessorTests):**
- Use `ITestOutputHelper` for structured output
- Capture exceptions with full details (type, message, stack trace)
- Log system state (memory, threads, environment)
- Create diagnostic reports as test attachments
- Use structured logging sections (e.g., "--- SETUP ---", "--- EXECUTION ---")

**Test Data:**
Use the payment example from the scenario:
- Transaction ID: "TXN-001"
- Amount: 99.99
- Currency: "USD"
- Payment Method: "CreditCard"
- Customer ID: "CUST-123"

## Need help?
Go back and rewatch the previous lectures. It usually helps out. 
If you still need help after that, don't hesitate to reach out (https://guiferreira.me/about)!

## Looking for an accountability partner?
Tag me on X (@gsferreira) or LinkedIn (@gferreira), and I will be there for you.

Let's do it!
