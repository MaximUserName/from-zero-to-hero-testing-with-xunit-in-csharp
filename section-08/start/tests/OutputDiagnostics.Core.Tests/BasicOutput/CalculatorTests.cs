using OutputDiagnostics.Core.BasicOutput;

namespace OutputDiagnostics.Core.Tests.BasicOutput;

public class CalculatorTests
{
    [Fact]
    public void Add_WithTwoNumbers_ReturnsSum()
    {
        // Arrange
        var calculator = new Calculator();
        var a = 5;
        var b = 3;

        // Act
        var result = calculator.Add(a, b);

        // Assert
        Assert.Equal(8, result);
    }

    [Fact]
    public void ProcessNumbers_WithMultipleValues_LogsProgress()
    {
        // Arrange
        var calculator = new Calculator();
        var numbers = new[] { 1, 2, 3, 4, 5 };

        // Act
        var result = calculator.ProcessNumbers(numbers);

        // Assert
        Assert.Equal(numbers.Length, result.Count);
        Assert.Equal(new[] { 2, 4, 6, 8, 10 }, result);
    }
}
