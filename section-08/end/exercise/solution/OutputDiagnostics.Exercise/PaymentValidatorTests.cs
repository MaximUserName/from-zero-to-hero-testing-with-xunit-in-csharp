namespace OutputDiagnostics.Exercise;

public class PaymentValidatorTests
{
    private readonly ITestOutputHelper _output;

    public PaymentValidatorTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ValidatePayment_WithValidPayment_ReturnsSuccess()
    {
        // Arrange
        _output.WriteLine("Testing payment validation with valid payment data");
        var validator = new PaymentValidator();
        var payment = new Payment
        {
            TransactionId = "TXN-001",
            Amount = 99.99m,
            Currency = "USD",
            PaymentMethod = "CreditCard",
            CustomerId = "CUST-123"
        };
        
        _output.WriteLine($"Input payment: ID={payment.TransactionId}, Amount={payment.Amount:C}, Currency={payment.Currency}");
        _output.WriteLine($"Payment Method: {payment.PaymentMethod}, Customer: {payment.CustomerId}");

        var startTime = DateTime.UtcNow;

        // Act
        var result = validator.ValidatePayment(payment);
        
        var endTime = DateTime.UtcNow;
        var totalTime = (endTime - startTime).TotalMilliseconds;

        // Assert
        _output.WriteLine($"Validation result: IsValid={result.IsValid}");
        _output.WriteLine($"Validation errors count: {result.ValidationErrors.Count}");
        _output.WriteLine($"Validation time: {result.ValidationTimeMs}ms");
        _output.WriteLine($"Total test execution time: {totalTime:F2}ms");

        Assert.True(result.IsValid, "Payment should be valid");
        Assert.Empty(result.ValidationErrors);
        Assert.True(result.ValidationTimeMs > 0, "Validation time should be recorded");
        
        _output.WriteLine("✓ Payment validation test completed successfully");
    }

    [Theory]
    [InlineData("", 99.99, "USD", "CreditCard", "CUST-123", "Transaction ID is required")]
    [InlineData("TXN-001", -50, "USD", "CreditCard", "CUST-123", "Amount must be greater than zero")]
    [InlineData("TXN-001", 99.99, "", "CreditCard", "CUST-123", "Currency is required")]
    [InlineData("TXN-001", 99.99, "US", "CreditCard", "CUST-123", "Currency must be 3 characters (ISO 4217)")]
    [InlineData("TXN-001", 99.99, "USD", "", "CUST-123", "Payment method is required")]
    [InlineData("TXN-001", 99.99, "USD", "CreditCard", "", "Customer ID is required")]
    public void ValidatePayment_WithInvalidData_ReturnsValidationErrors(
        string transactionId, decimal amount, string currency, string paymentMethod, 
        string customerId, string expectedError)
    {
        // Arrange
        _output.WriteLine($"Testing payment validation with invalid data: {expectedError}");
        var validator = new PaymentValidator();
        var payment = new Payment
        {
            TransactionId = transactionId,
            Amount = amount,
            Currency = currency,
            PaymentMethod = paymentMethod,
            CustomerId = customerId
        };

        _output.WriteLine($"Input parameters: TxnId='{transactionId}', Amount={amount}, Currency='{currency}'");
        _output.WriteLine($"PaymentMethod='{paymentMethod}', CustomerId='{customerId}'");

        var startTime = DateTime.UtcNow;

        // Act
        var result = validator.ValidatePayment(payment);
        
        var endTime = DateTime.UtcNow;
        var totalTime = (endTime - startTime).TotalMilliseconds;

        // Assert
        _output.WriteLine($"Validation result: IsValid={result.IsValid}");
        _output.WriteLine($"Validation errors: [{string.Join(", ", result.ValidationErrors)}]");
        _output.WriteLine($"Validation time: {result.ValidationTimeMs}ms");
        _output.WriteLine($"Total test execution time: {totalTime:F2}ms");

        Assert.False(result.IsValid, "Payment should be invalid");
        Assert.Contains(expectedError, result.ValidationErrors);
        Assert.True(result.ValidationTimeMs > 0, "Validation time should be recorded");
        
        _output.WriteLine($"✓ Expected validation error confirmed: {expectedError}");
    }

    [Theory]
    [InlineData(100.50, "USD", true, "Valid USD amount")]
    [InlineData(24999.99, "USD", true, "Maximum valid USD amount")]
    [InlineData(25000.01, "USD", false, "USD amount exceeds limit")]
    [InlineData(50000.01, "EUR", false, "Amount exceeds global limit")]
    [InlineData(-10.00, "USD", false, "Negative amount")]
    public void ValidateAmount_WithDifferentScenarios_ReturnsExpectedResults(
        decimal amount, string currency, bool expectedValid, string scenario)
    {
        // Arrange
        _output.WriteLine($"Testing amount validation: {scenario}");
        var validator = new PaymentValidator();
        
        _output.WriteLine($"Input: Amount={amount:C}, Currency={currency}");
        _output.WriteLine($"Expected result: {(expectedValid ? "Valid" : "Invalid")}");

        var performanceStart = DateTime.UtcNow;

        // Act
        var result = validator.ValidateAmount(amount, currency);
        
        var performanceEnd = DateTime.UtcNow;
        var totalExecutionTime = (performanceEnd - performanceStart).TotalMilliseconds;

        // Assert
        _output.WriteLine($"Validation result: IsValid={result.IsValid}");
        _output.WriteLine($"Validation errors: [{string.Join(", ", result.ValidationErrors)}]");
        _output.WriteLine($"Processing time: {result.ValidationTimeMs}ms");
        _output.WriteLine($"Total execution time: {totalExecutionTime:F2}ms");
        
        if (!expectedValid && result.ValidationErrors.Any())
        {
            _output.WriteLine($"Error details: {string.Join("; ", result.ValidationErrors)}");
        }

        Assert.Equal(expectedValid, result.IsValid);
        Assert.True(result.ValidationTimeMs > 0, "Processing time should be recorded");
        
        _output.WriteLine($"✓ Amount validation test completed: {scenario}");
    }
}