namespace Assertions.Exercise;

public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public double Divide(double a, double b)
    {
        if (b == 0)
            throw new DivideByZeroException("Cannot divide by zero");
        
        return a / b;
    }
}
