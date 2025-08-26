namespace ReportingIntegration.Core.Configuration;

public class PaymentProcessor
{
    private readonly decimal _minimumAmount;
    private readonly decimal _maximumAmount;
    private readonly TimeSpan _processingDelay;

    public PaymentProcessor(decimal minimumAmount = 0.01m, decimal maximumAmount = 10000m, TimeSpan? processingDelay = null)
    {
        _minimumAmount = minimumAmount;
        _maximumAmount = maximumAmount;
        _processingDelay = processingDelay ?? TimeSpan.FromMilliseconds(100);
    }

    public async Task<Payment> ProcessPaymentAsync(Payment payment)
    {
        ValidatePayment(payment);

        payment.Status = PaymentStatus.Processing;
        
        // Simulate processing time
        await Task.Delay(_processingDelay);

        // Simulate different outcomes based on amount
        if (payment.Amount > _maximumAmount)
        {
            payment.Status = PaymentStatus.Failed;
            throw new InvalidOperationException($"Payment amount exceeds maximum allowed: {_maximumAmount}");
        }

        if (payment.Amount < _minimumAmount)
        {
            payment.Status = PaymentStatus.Failed;
            throw new ArgumentException($"Payment amount below minimum required: {_minimumAmount}");
        }

        // Simulate occasional failures for testing
        if (payment.Amount == 13.13m) // Unlucky amount for testing
        {
            payment.Status = PaymentStatus.Failed;
            throw new InvalidOperationException("Payment processing failed due to external service error");
        }

        payment.Status = PaymentStatus.Completed;
        payment.ProcessedAt = DateTime.UtcNow;

        return payment;
    }

    public Payment RefundPayment(Payment payment)
    {
        if (payment.Status != PaymentStatus.Completed)
        {
            throw new InvalidOperationException("Can only refund completed payments");
        }

        payment.Status = PaymentStatus.Refunded;
        return payment;
    }

    private void ValidatePayment(Payment payment)
    {
        if (payment == null)
            throw new ArgumentNullException(nameof(payment));

        if (payment.Amount <= 0)
            throw new ArgumentException("Payment amount must be positive", nameof(payment));

        if (string.IsNullOrWhiteSpace(payment.Currency))
            throw new ArgumentException("Currency is required", nameof(payment));
    }
}
