using TestExecutionControl.Exercise.Models;

namespace TestExecutionControl.Exercise.Services;

public class PaymentService
{
    public async Task<PaymentResult> ProcessPaymentAsync(Payment payment, CancellationToken cancellationToken = default)
    {
        if (payment == null) throw new ArgumentNullException(nameof(payment));

        // Simulate external API call delay
        await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);

        // Simulate payment processing
        return new PaymentResult
        {
            Success = true,
            TransactionId = Guid.NewGuid().ToString(),
            Message = "Payment processed successfully"
        };
    }
}
