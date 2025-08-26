using ExtensibilityCustomization.Core.TestTraits;

namespace ExtensibilityCustomization.Core.Tests.TestTraits;

public class CategoryTraitTests
{
    [Fact]
    [Trait("Priority", "High")]
    public void Add_WithTwoNumbers_ReturnsSum()
    {
        var calculator = new CalculatorService();
        var result = calculator.Add(2, 3);

        Assert.Equal(5, result);
    }

    [Fact]
    [Trait("Priority", "Medium")]
    public void Subtract_WithTwoNumbers_ReturnsDifference()
    {
        var calculator = new CalculatorService();
        var result = calculator.Subtract(5, 3);

        Assert.Equal(2, result);
    }

    [Fact]
    [Trait("Priority", "High")]
    public void Divide_WithZero_ThrowsException()
    {
        var calculator = new CalculatorService();

        Assert.Throws<DivideByZeroException>(() => calculator.Divide(10, 0));
    }

    [Fact]
    [Trait("Priority", "Low")]
    public void PerformComplexCalculation_WithManyIterations_CompletesSuccessfully()
    {
        var calculator = new CalculatorService();
        var result = calculator.PerformComplexCalculation(1000);

        Assert.True(result > 0);
    }

    [Fact]
    [Trait("Priority", "Critical")]
    public void Multiply_WithLargeNumbers_HandlesCorrectly()
    {
        var calculator = new CalculatorService();
        var result = calculator.Multiply(1000, 2000);

        Assert.Equal(2000000, result);
    }
}
