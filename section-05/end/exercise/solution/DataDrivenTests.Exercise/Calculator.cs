namespace DataDrivenTests.Exercise;

public class Calculator
{
    public int Add(int a, int b) => a + b;
    
    public bool IsEven(int number) => number % 2 == 0;
    
    public decimal Divide(decimal a, decimal b)
    {
        if (b == 0) throw new DivideByZeroException();
        return a / b;
    }
    
    public decimal CalculatePercentage(decimal value, decimal percentage)
    {
        return (value * percentage) / 100;
    }
}
