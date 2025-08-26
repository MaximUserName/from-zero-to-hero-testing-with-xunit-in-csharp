namespace TestFundamentals.Exercise;

public class Calculator
{
    public int Add(int first, int second)
    {
        return first + second;
    }

    public double Divide(int dividend, int zeroDivisor)
    {
        return dividend / zeroDivisor;
    }

    public int Subtract(int minuend, int subtrahend)
    {
        return minuend - subtrahend;
    }
}