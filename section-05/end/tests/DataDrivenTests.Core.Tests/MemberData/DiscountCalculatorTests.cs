using DataDrivenTests.Core.MemberData;
using DataDrivenTests.Core.Tests.MemberData.TestData;

namespace DataDrivenTests.Core.Tests.MemberData;

public class DiscountCalculatorTests
{
    [Theory]
    [ClassData(typeof(DiscountData))]
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
}