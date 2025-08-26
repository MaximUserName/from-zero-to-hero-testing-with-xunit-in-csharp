namespace DataDrivenTests.Exercise;

public class CalculatorTests
{
    private readonly Calculator _calculator = new();

    // InlineData Tests - Simple and direct
    [Theory]
    [InlineData(2, 3, 5)]
    [InlineData(0, 5, 5)]
    [InlineData(-1, 1, 0)]
    [InlineData(-5, -3, -8)]
    [InlineData(100, 200, 300)]
    public void Add_ShouldReturnCorrectSum(int a, int b, int expected)
    {
        // Act
        var result = _calculator.Add(a, b);
        
        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(2, true)]
    [InlineData(3, false)]
    [InlineData(0, true)]
    [InlineData(-2, true)]
    [InlineData(-3, false)]
    [InlineData(100, true)]
    [InlineData(101, false)]
    public void IsEven_ShouldReturnCorrectResult(int number, bool expected)
    {
        // Act
        var result = _calculator.IsEven(number);
        
        // Assert
        Assert.Equal(expected, result);
    }

    // MemberData Tests - More complex test cases with yield return
    [Theory]
    [MemberData(nameof(GetDivisionTestCases))]
    public void Divide_ShouldCalculateCorrectly(decimal a, decimal b, decimal expected)
    {
        // Act
        var result = _calculator.Divide(a, b);
        
        // Assert
        Assert.Equal(expected, result, precision: 2);
    }

    public static IEnumerable<object[]> GetDivisionTestCases()
    {
        yield return new object[] { 10m, 2m, 5m };
        yield return new object[] { 7m, 3m, 2.33m }; // rounded to 2 decimal places
        yield return new object[] { 15m, 5m, 3m };
        yield return new object[] { 100m, 4m, 25m };
        yield return new object[] { -10m, 2m, -5m };
        yield return new object[] { 10m, -2m, -5m };
        yield return new object[] { -10m, -2m, 5m };
        yield return new object[] { 1m, 3m, 0.33m }; // rounded to 2 decimal places
    }

    // TheoryData Tests - Type-safe test data
    public static TheoryData<decimal, decimal, decimal> PercentageData =>
        new()
        {
            { 100m, 10m, 10m },    // 10% of 100 = 10
            { 50m, 20m, 10m },     // 20% of 50 = 10
            { 200m, 0m, 0m },      // 0% of 200 = 0
            { 75m, 50m, 37.5m },   // 50% of 75 = 37.5
            { 1000m, 5m, 50m },    // 5% of 1000 = 50
            { 25m, 100m, 25m },    // 100% of 25 = 25
            { 150m, 33.33m, 49.995m } // 33.33% of 150 = 49.995
        };

    [Theory]
    [MemberData(nameof(PercentageData))]
    public void CalculatePercentage_ShouldReturnCorrectResult(decimal value, decimal percentage, decimal expected)
    {
        // Act
        var result = _calculator.CalculatePercentage(value, percentage);
        
        // Assert
        Assert.Equal(expected, result, precision: 3);
    }

    // Bonus Challenge - Exception Testing
    [Fact]
    public void Divide_ByZero_ShouldThrowDivideByZeroException()
    {
        // Arrange
        decimal a = 10m;
        decimal b = 0m;
        
        // Act & Assert
        Assert.Throws<DivideByZeroException>(() => _calculator.Divide(a, b));
    }

    [Theory]
    [InlineData(5, 0)]
    [InlineData(-10, 0)]
    [InlineData(0, 0)]
    public void Divide_ByZero_WithDifferentNumerators_ShouldThrowDivideByZeroException(decimal a, decimal b)
    {
        // Act & Assert
        Assert.Throws<DivideByZeroException>(() => _calculator.Divide(a, b));
    }
}
