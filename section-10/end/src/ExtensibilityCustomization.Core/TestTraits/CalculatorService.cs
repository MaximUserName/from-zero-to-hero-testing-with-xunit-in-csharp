namespace ExtensibilityCustomization.Core.TestTraits;

public class CalculatorService
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Subtract(int a, int b)
    {
        return a - b;
    }

    public int Multiply(int a, int b)
    {
        return a * b;
    }

    public double Divide(int a, int b)
    {
        if (b == 0)
            throw new DivideByZeroException("Cannot divide by zero");

        return (double)a / b;
    }

    public double PerformComplexCalculation(int iterations)
    {
        // Simulate a complex calculation that takes some time
        double result = 0;
        for (int i = 0; i < iterations; i++)
        {
            result += Math.Sqrt(i) * Math.Log(i + 1);
        }
        return result;
    }
}
