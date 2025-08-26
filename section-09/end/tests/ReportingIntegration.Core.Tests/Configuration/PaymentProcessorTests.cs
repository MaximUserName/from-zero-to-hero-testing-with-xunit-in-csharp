using ReportingIntegration.Core.Configuration;

namespace ReportingIntegration.Core.Tests.Configuration;

public class PaymentProcessorTests
{
    private readonly PaymentProcessor _processor;

    public PaymentProcessorTests()
    {
        // Use fast processing for tests
        _processor = new PaymentProcessor(processingDelay: TimeSpan.FromMilliseconds(1));
    }

    [Fact]
    [Trait("Category", "Unit")]
    [Trait("Performance", "Fast")]
    public async Task ProcessPaymentAsync_ValidPayment_CompletesSuccessfully()
    {
        // Arrange
        var payment = new Payment
        {
            Amount = 100.00m,
            Currency = "USD",
            Method = PaymentMethod.CreditCard,
            Status = PaymentStatus.Pending
        };

        // Act
        var result = await _processor.ProcessPaymentAsync(payment);

        // Assert
        Assert.Equal(PaymentStatus.Completed, result.Status);
        Assert.True(result.ProcessedAt > DateTime.MinValue);
    }

    [Theory]
    [Trait("Category", "Unit")]
    [InlineData(0)]
    [InlineData(-50.00)]
    public async Task ProcessPaymentAsync_InvalidAmount_ThrowsArgumentException(decimal amount)
    {
        // Arrange
        var payment = new Payment
        {
            Amount = amount,
            Currency = "USD",
            Method = PaymentMethod.CreditCard
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => 
            _processor.ProcessPaymentAsync(payment));
        Assert.Contains("Payment amount must be positive", exception.Message);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task ProcessPaymentAsync_NullPayment_ThrowsArgumentNullException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
            _processor.ProcessPaymentAsync(null!));
    }

    [Theory]
    [Trait("Category", "Unit")]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public async Task ProcessPaymentAsync_InvalidCurrency_ThrowsArgumentException(string currency)
    {
        // Arrange
        var payment = new Payment
        {
            Amount = 50.00m,
            Currency = currency,
            Method = PaymentMethod.CreditCard
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => 
            _processor.ProcessPaymentAsync(payment));
        Assert.Contains("Currency is required", exception.Message);
    }

    [Fact]
    [Trait("Category", "Unit")]
    [Trait("Performance", "Slow")]
    public async Task ProcessPaymentAsync_UnluckyAmount_FailsWithException()
    {
        // Arrange
        var payment = new Payment
        {
            Amount = 13.13m, // Special amount that triggers failure
            Currency = "USD",
            Method = PaymentMethod.CreditCard
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => 
            _processor.ProcessPaymentAsync(payment));
        Assert.Contains("Payment processing failed due to external service error", exception.Message);
        Assert.Equal(PaymentStatus.Failed, payment.Status);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void RefundPayment_CompletedPayment_RefundsSuccessfully()
    {
        // Arrange
        var payment = new Payment
        {
            Amount = 75.00m,
            Status = PaymentStatus.Completed
        };

        // Act
        var result = _processor.RefundPayment(payment);

        // Assert
        Assert.Equal(PaymentStatus.Refunded, result.Status);
    }

    [Theory]
    [Trait("Category", "Unit")]
    [InlineData(PaymentStatus.Pending)]
    [InlineData(PaymentStatus.Processing)]
    [InlineData(PaymentStatus.Failed)]
    public void RefundPayment_NonCompletedPayment_ThrowsInvalidOperationException(PaymentStatus status)
    {
        // Arrange
        var payment = new Payment
        {
            Amount = 75.00m,
            Status = status
        };

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => 
            _processor.RefundPayment(payment));
        Assert.Contains("Can only refund completed payments", exception.Message);
    }

    [Fact]
    [Trait("Category", "Integration")]
    [Trait("Performance", "Slow")]
    public async Task ProcessPaymentAsync_MultiplePayments_ProcessesSequentially()
    {
        // Arrange
        var payments = new[]
        {
            new Payment { Amount = 10.00m, Currency = "USD", Method = PaymentMethod.CreditCard },
            new Payment { Amount = 20.00m, Currency = "EUR", Method = PaymentMethod.PayPal },
            new Payment { Amount = 30.00m, Currency = "GBP", Method = PaymentMethod.DebitCard }
        };

        // Act
        var results = new List<Payment>();
        foreach (var payment in payments)
        {
            var result = await _processor.ProcessPaymentAsync(payment);
            results.Add(result);
        }

        // Assert
        Assert.All(results, payment => Assert.Equal(PaymentStatus.Completed, payment.Status));
        Assert.True(results.All(p => p.ProcessedAt > DateTime.MinValue));
    }

    [Fact]
    public void Culture()
    {
        Assert.Equal("en-US", Thread.CurrentThread.CurrentCulture.Name);
    }
}
