namespace TestExecutionControl.Exercise.Models;

public class Payment
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
}

public class PaymentResult
{
    public bool Success { get; set; }
    public string TransactionId { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
