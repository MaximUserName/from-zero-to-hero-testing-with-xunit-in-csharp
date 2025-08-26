namespace TestFundamentals.Core.FactsVsTheories;

public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public decimal Divide(decimal a, decimal b)
    {
        if (b == 0)
            throw new DivideByZeroException();
        
        return a / b;
    }
}
