using System.Diagnostics;
using TestExecutionControl.Exercise.Helpers;
using TestExecutionControl.Exercise.Models;
using TestExecutionControl.Exercise.Services;

namespace TestExecutionControl.Exercise;

public class OrderServiceTests
{
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _orderService = new OrderService();
    }

    [Fact(Timeout = 2000)]
    public async Task ProcessOrderAsync_StandardOrder_CompletesUnderTwoSeconds()
    {
        // Arrange
        var order = new Order
        {
            OrderId = Guid.NewGuid(),
            CustomerId = 12345,
            Amount = 99.99m,
            Type = "Standard"
        };

        var stopwatch = Stopwatch.StartNew();
        TestContext.Current.SendDiagnosticMessage($"Starting fast order processing test at {DateTime.Now:HH:mm:ss.fff}");

        // Act
        var result = await _orderService.ProcessOrderAsync(order, TestContext.Current.CancellationToken);
        
        stopwatch.Stop();
        TestContext.Current.SendDiagnosticMessage($"Fast order processing completed in {stopwatch.ElapsedMilliseconds}ms");

        // Assert
        Assert.True(result);
        Assert.True(stopwatch.ElapsedMilliseconds < 2000, 
            $"Processing took {stopwatch.ElapsedMilliseconds}ms, expected under 2000ms");
    }

    [Fact(Timeout = 6000)]
    public async Task ProcessOrderAsync_ExpressOrder_CompletesUnderSixSeconds()
    {
        // Skip slow tests in CI environment
        Assert.SkipWhen(TestEnvironment.IsCI, "Skipping slow test in CI environment");

        // Arrange
        var order = new Order
        {
            OrderId = Guid.NewGuid(),
            CustomerId = 12345,
            Amount = 99.99m,
            Type = "Express"
        };

        var stopwatch = Stopwatch.StartNew();
        TestContext.Current.SendDiagnosticMessage($"Starting slow order processing test at {DateTime.Now:HH:mm:ss.fff}");

        // Act
        var result = await _orderService.ProcessOrderAsync(order, TestContext.Current.CancellationToken);
        
        stopwatch.Stop();
        TestContext.Current.SendDiagnosticMessage($"Slow order processing completed in {stopwatch.ElapsedMilliseconds}ms");

        // Assert
        Assert.True(result);
        Assert.True(stopwatch.ElapsedMilliseconds < 6000, 
            $"Processing took {stopwatch.ElapsedMilliseconds}ms, expected under 6000ms");
        Assert.True(stopwatch.ElapsedMilliseconds > 3000, 
            $"Processing took {stopwatch.ElapsedMilliseconds}ms, expected over 3000ms for Express orders");
    }

    [Fact(Timeout = 2000)]
    public async Task ProcessOrderAsync_WithCancellation_ThrowsOperationCanceledException()
    {
        // Arrange
        var order = new Order
        {
            OrderId = Guid.NewGuid(),
            CustomerId = 12345,
            Amount = 99.99m,
            Type = "Express" // This will take 4 seconds
        };

        using var cts = new CancellationTokenSource(1000); // Cancel after 1 second
        TestContext.Current.SendDiagnosticMessage($"Starting cancellation test at {DateTime.Now:HH:mm:ss.fff}");

        // Act & Assert
        await Assert.ThrowsAsync<TaskCanceledException>(
            () => _orderService.ProcessOrderAsync(order, cts.Token));
        
        TestContext.Current.SendDiagnosticMessage($"Cancellation test completed at {DateTime.Now:HH:mm:ss.fff}");
    }

    [Fact]
    public async Task ProcessOrderAsync_NullOrder_ThrowsArgumentNullException()
    {
        // Arrange
        TestContext.Current.SendDiagnosticMessage("Testing null order handling");

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            () => _orderService.ProcessOrderAsync(null!, TestContext.Current.CancellationToken));
        
        TestContext.Current.SendDiagnosticMessage("Null order test completed successfully");
    }
}
