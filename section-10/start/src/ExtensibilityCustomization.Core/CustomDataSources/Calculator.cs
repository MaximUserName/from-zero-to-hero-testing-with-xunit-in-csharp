namespace ExtensibilityCustomization.Core.CustomDataSources;

public class Calculator
{
    public double Calculate(double number1, double number2, Operation operation)
    {
        return operation switch
        {
            Operation.Add => number1 + number2,
            Operation.Subtract => number1 - number2,
            Operation.Multiply => number1 * number2,
            Operation.Divide => number2 != 0 ? number1 / number2 : throw new DivideByZeroException(),
            _ => throw new ArgumentOutOfRangeException(nameof(operation))
        };
    }
}

public enum Operation
{
    Add,
    Subtract,
    Multiply,
    Divide
}
