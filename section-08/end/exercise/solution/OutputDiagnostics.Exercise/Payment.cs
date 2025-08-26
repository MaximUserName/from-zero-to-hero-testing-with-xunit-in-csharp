namespace OutputDiagnostics.Exercise;

// Payment model classes
public class Payment
{
    public string TransactionId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

// PaymentValidator - Simple validation logic (basic output needed)

// PaymentProcessor - Complex processing with external services (comprehensive diagnostics needed)

// PaymentValidatorTests - Test payment validation with basic ITestOutputHelper output

// PaymentProcessorTests - Test payment processing with comprehensive diagnostics and error capture