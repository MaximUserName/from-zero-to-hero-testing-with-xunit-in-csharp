using Assertions.Core.StringAndNumeric;

namespace Assertions.Core.Tests.StringAndNumeric;

public class PriceCalculatorTests
{
    [Fact]
    public void CalculateTotal_WithValidPrices_ReturnsCorrectSum()
    {
        // Arrange
        var calculator = new PriceCalculator();
        var prices = new decimal[] { 10.50m, 25.99m, 5.00m };

        // Act
        var total = calculator.CalculateTotal(prices);

        // Assert - Decimal precision
        
    }

    [Fact]
    public void ApplyDiscount_WithValidDiscount_ReturnsDiscountedPrice()
    {
        // Arrange
        var calculator = new PriceCalculator();
        var originalPrice = 100.00m;
        var discount = 20m; // 20%

        // Act
        var discountedPrice = calculator.ApplyDiscount(originalPrice, discount);

        // Assert - Numeric precision

    }

    [Fact]
    public void FormatCurrency_WithUSD_ReturnsCorrectFormat()
    {
        // Arrange
        var calculator = new PriceCalculator();
        var amount = 123.45m;

        // Act
        var formatted = calculator.FormatCurrency(amount, "USD");

        // Assert - String formatting assertions

    }

    [Fact]
    public void IsValidPrice_WithDifferentValues_ReturnsExpectedResults()
    {
        // Arrange
        var calculator = new PriceCalculator();

        // Act & Assert - Range testing

    }

    [Fact]
    public void GetRandomScore_GeneratesScoreInValidRange()
    {
        // Arrange
        var calculator = new PriceCalculator();

        // Act & Assert - Range testing for random values
        for (int i = 0; i < 10; i++) // Test multiple times for randomness
        {
            var score = calculator.GetRandomScore();
            
        }
    }

    // TODO: Add more test cases to practice numeric assertions:
    // - Test floating-point calculations with Assert.Equal(expected, actual, precision)
    // - Use Assert.InRange for boundary testing
    // - Practice currency formatting with different cultures
    // - Test numeric edge cases (zero, negative, very large numbers)
}
