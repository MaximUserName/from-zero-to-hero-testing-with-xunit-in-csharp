using ReportingIntegration.Core.ReportingBasics;

namespace ReportingIntegration.Core.Tests.ReportingBasics;

public class OrderProcessorTests
{
    private readonly OrderProcessor _processor = new();

    [Fact]
    public void ProcessOrder_ValidOrder_SetsStatusToConfirmed()
    {
        // Arrange
        var order = new Order
        {
            Id = 1,
            CustomerName = "John Doe",
            Total = 99.99m
        };

        // Act
        var result = _processor.ProcessOrder(order);

        // Assert
        Assert.Equal(OrderStatus.Confirmed, result.Status);
        Assert.True(result.OrderDate > DateTime.MinValue);
    }

    [Fact]
    public void ProcessOrder_EmptyCustomerName_ThrowsArgumentException()
    {
        // Arrange
        var order = new Order
        {
            Id = 1,
            CustomerName = "",
            Total = 99.99m
        };

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _processor.ProcessOrder(order));
        Assert.Contains("Customer name is required", exception.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10.50)]
    public void ProcessOrder_InvalidTotal_ThrowsArgumentException(decimal total)
    {
        // Arrange
        var order = new Order
        {
            Id = 1,
            CustomerName = "Jane Smith",
            Total = total
        };

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _processor.ProcessOrder(order));
        Assert.Contains("Order total must be positive", exception.Message);
    }

    [Fact]
    public void CalculateTotal_MultipleItems_ReturnsCorrectSum()
    {
        // Arrange
        var itemPrices = new[] { 10.50m, 25.99m, 5.00m };

        // Act
        var result = _processor.CalculateTotal(itemPrices);

        // Assert
        Assert.Equal(41.49m, result);
    }

    [Fact]
    public void ValidateOrder_ValidOrder_ReturnsTrue()
    {
        // Arrange
        var order = new Order
        {
            CustomerName = "Alice Johnson",
            Total = 50.00m
        };

        // Act
        var result = _processor.ValidateOrder(order);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ValidateOrder_InvalidOrder_ReturnsFalse()
    {
        // Arrange
        var order = new Order
        {
            CustomerName = "",
            Total = 0
        };

        // Act
        var result = _processor.ValidateOrder(order);

        // Assert
        Assert.False(result);
    }
}
