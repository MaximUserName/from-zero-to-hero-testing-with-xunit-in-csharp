namespace ReportingIntegration.Core.Configuration;

public class Payment
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public PaymentMethod Method { get; set; }
    public PaymentStatus Status { get; set; }
    public DateTime ProcessedAt { get; set; }
}

public enum PaymentMethod
{
    CreditCard,
    DebitCard,
    PayPal,
    BankTransfer,
    DigitalWallet
}

public enum PaymentStatus
{
    Pending,
    Processing,
    Completed,
    Failed,
    Refunded
}
