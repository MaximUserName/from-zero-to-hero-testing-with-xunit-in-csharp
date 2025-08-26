namespace TestFundamentals.Exercise;

// Problems Identified:
// ❌ Poor naming: "Test1", "Test2", "Test3" tell you nothing  
// ❌ Multiple assertions: Test1 tests multiple scenarios  
// ❌ Inconsistent patterns: Mix of Fact/Theory without clear reasoning  
// ❌ Poor organization: Generic class name, no logical grouping  
// ❌ Missing AAA structure: No clear separation of phases

public class CalculatorTests
{
    [Theory]
    [InlineData(2, 2, 4)]
    [InlineData(2, -2, 0)]
    [InlineData(-2, -2, -4)]
    [InlineData(0, 5, 5)]
    public void Add_WithVariousNumbers_ReturnsCorrectSum(int first, int second, int expected)
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Add(first, second);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(5, 3, 2)]
    [InlineData(10, 7, 3)]
    [InlineData(-5, -3, -2)]
    public void Subtract_WithVariousNumbers_ReturnsCorrectDifference(
        int minuend, int subtrahend, int expected)
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Subtract(minuend, subtrahend);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Divide_WithZeroDivisor_ThrowsDivideByZeroException()
    {
        // Arrange
        var calculator = new Calculator();
        const int dividend = 10;
        const int zeroDivisor = 0;

        // Act & Assert
        Assert.Throws<DivideByZeroException>(() => calculator.Divide(dividend, zeroDivisor));
    }
}