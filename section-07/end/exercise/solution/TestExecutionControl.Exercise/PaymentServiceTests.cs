using System.Diagnostics;
using TestExecutionControl.Exercise.Helpers;
using TestExecutionControl.Exercise.Models;
using TestExecutionControl.Exercise.Services;

namespace TestExecutionControl.Exercise;

public class PaymentServiceTests
{
    private readonly PaymentService _paymentService;

    public PaymentServiceTests()
    {
        _paymentService = new PaymentService();
    }

    [Fact(Timeout = 10000)]
    public async Task ProcessPaymentAsync_ValidPayment_ReturnsSuccessResult()
    {
        // Skip if external API access is not available
        Assert.SkipUnless(TestEnvironment.HasExternalAccess, 
            "Skipping payment test - EXTERNAL_API_KEY environment variable not set");

        // Arrange
        var payment = new Payment
        {
            Amount = 99.99m,
            Currency = "USD",
            CardNumber = "4111111111111111"
        };

        var stopwatch = Stopwatch.StartNew();
        TestContext.Current.SendDiagnosticMessage($"Starting payment processing test at {DateTime.Now:HH:mm:ss.fff}");
        TestContext.Current.SendDiagnosticMessage($"Processing payment: Amount={payment.Amount} {payment.Currency}, Card=****{payment.CardNumber[^4..]}");

        // Act
        var result = await _paymentService.ProcessPaymentAsync(payment, TestContext.Current.CancellationToken);
        
        stopwatch.Stop();
        TestContext.Current.SendDiagnosticMessage($"Payment processing completed in {stopwatch.ElapsedMilliseconds}ms");
        TestContext.Current.SendDiagnosticMessage($"Payment result: Success={result.Success}, TransactionId={result.TransactionId}, Message={result.Message}");

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.NotEmpty(result.TransactionId);
        Assert.NotEmpty(result.Message);
        Assert.True(stopwatch.ElapsedMilliseconds < 10000, 
            $"Payment processing took {stopwatch.ElapsedMilliseconds}ms, expected under 10000ms");
    }

    [Fact(Timeout = 10000)]
    public async Task ProcessPaymentAsync_WithCancellation_ThrowsOperationCanceledException()
    {
        // Skip if external API access is not available
        Assert.SkipUnless(TestEnvironment.HasExternalAccess, 
            "Skipping payment cancellation test - EXTERNAL_API_KEY environment variable not set");

        // Arrange
        var payment = new Payment
        {
            Amount = 99.99m,
            Currency = "USD",
            CardNumber = "4111111111111111"
        };

        using var cts = new CancellationTokenSource(1000); // Cancel after 1 second
        TestContext.Current.SendDiagnosticMessage($"Starting payment cancellation test at {DateTime.Now:HH:mm:ss.fff}");
        TestContext.Current.SendDiagnosticMessage("Payment will be cancelled after 1 second");

        // Act & Assert
        await Assert.ThrowsAsync<TaskCanceledException>(
            () => _paymentService.ProcessPaymentAsync(payment, cts.Token));
        
        TestContext.Current.SendDiagnosticMessage($"Payment cancellation test completed at {DateTime.Now:HH:mm:ss.fff}");
    }

    [Fact]
    public async Task ProcessPaymentAsync_NullPayment_ThrowsArgumentNullException()
    {
        // This test doesn't require external access as it tests argument validation
        TestContext.Current.SendDiagnosticMessage("Testing null payment handling");

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            () => _paymentService.ProcessPaymentAsync(null!, TestContext.Current.CancellationToken));
        
        TestContext.Current.SendDiagnosticMessage("Null payment test completed successfully");
    }

    [Fact(Timeout = 10000)]
    public async Task ProcessPaymentAsync_DifferentCurrencies_ProcessesSuccessfully()
    {
        // Skip if external API access is not available
        Assert.SkipUnless(TestEnvironment.HasExternalAccess, 
            "Skipping multi-currency payment test - EXTERNAL_API_KEY environment variable not set");

        // Arrange
        var payments = new[]
        {
            new Payment { Amount = 99.99m, Currency = "USD", CardNumber = "4111111111111111" },
            new Payment { Amount = 75.50m, Currency = "EUR", CardNumber = "4111111111111111" },
            new Payment { Amount = 120.00m, Currency = "GBP", CardNumber = "4111111111111111" }
        };

        TestContext.Current.SendDiagnosticMessage($"Starting multi-currency payment test at {DateTime.Now:HH:mm:ss.fff}");

        // Act & Assert
        foreach (var payment in payments)
        {
            TestContext.Current.SendDiagnosticMessage($"Processing {payment.Currency} payment for {payment.Amount}");
            
            var result = await _paymentService.ProcessPaymentAsync(payment, TestContext.Current.CancellationToken);
            
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotEmpty(result.TransactionId);
            
            TestContext.Current.SendDiagnosticMessage($"✓ {payment.Currency} payment processed successfully - TransactionId: {result.TransactionId}");
        }
        
        TestContext.Current.SendDiagnosticMessage("Multi-currency payment test completed successfully");
    }
}
