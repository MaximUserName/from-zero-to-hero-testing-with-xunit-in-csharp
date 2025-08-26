using DataDrivenTests.Core.MemberData;

namespace DataDrivenTests.Core.Tests.MemberData;

public class DiscountCalculatorTests
{
    [Theory]
    [MemberData(nameof(GetDiscountScenarios))]
    public void CalculateDiscount_ShouldApplyCorrectRates(
        decimal orderAmount, 
        string customerType, 
        decimal expectedDiscount,
        string scenario)
    {
        // Arrange
        var calculator = new DiscountCalculator();

        // Act
        var result = calculator.CalculateDiscount(orderAmount, customerType);

        // Assert
        Assert.Equal(expectedDiscount, result, precision: 2);
    }

    [Theory]
    [MemberData(nameof(GetDiscountScenarios))]
    public void CalculateDiscount_WithClassData_AppliesRulesCorrectly(
        decimal orderAmount,
        string customerType,
        decimal expectedDiscount,
        string description)
    {
        // Arrange
        var calculator = new DiscountCalculator();

        // Act
        var result = calculator.CalculateDiscount(orderAmount, customerType);

        // Assert
        Assert.Equal(expectedDiscount, result, precision: 2);
    }

    public static IEnumerable<object[]> GetDiscountScenarios()
    {
        // No discount scenarios
        yield return new object[] { 25.00m, "STANDARD", 0.00m, "Below minimum order" };
        yield return new object[] { 75.00m, "STANDARD", 0.00m, "Standard customer below discount threshold" };

        // Standard customer discounts
        yield return new object[] { 100.00m, "STANDARD", 0.05m, "Standard customer - basic discount" };
        yield return new object[] { 200.00m, "STANDARD", 0.05m, "Standard customer - higher amount" };

        // VIP customer discounts
        yield return new object[] { 100.00m, "VIP", 0.10m, "VIP customer - basic discount" };
        yield return new object[] { 500.00m, "VIP", 0.15m, "VIP customer - premium discount" };

        // Unknown customer type
        yield return new object[] { 100.00m, "UNKNOWN", 0.00m, "Unknown customer type" };
    }
}
