namespace TestFundamentals.Core.Tests.FactsVsTheories;

using TestFundamentals.Core.FactsVsTheories;

public class CalculatorTests
{
    [Fact]
    public void Add_2And2_Returns4()
    {
        var calc = new Calculator();
        var result = calc.Add(2, 2);
        Assert.Equal(4, result);
    }
    
    [Fact]
    public void Add_5And3_Returns8()
    {
        var calc = new Calculator();
        var result = calc.Add(5, 3);
        Assert.Equal(8, result);
    }

    [Fact]
    public void Add_0And5_Returns5()
    {
        var calc = new Calculator();
        var result = calc.Add(0, 5);
        Assert.Equal(5, result);
    }

    [Theory]
    [InlineData(1,2,3)]
    [InlineData(2,2,4)]
    [InlineData(5,3,8)]
    [InlineData(5,0,5)]
    public void Add_TwoNumbers_ReturnExpectedResult(int a, int b, int expectedResult)
    {
        var calc = new Calculator();
        var result = calc.Add(a, b);
        Assert.Equal(expectedResult, result);
    }


    [Fact]
    public void Divide_ByZero_ThrowsException()
    {
        var calc = new Calculator();
        Assert.Throws<DivideByZeroException>(() => calc.Divide(10, 0));
    }
}
