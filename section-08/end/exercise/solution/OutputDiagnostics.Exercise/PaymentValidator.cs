namespace OutputDiagnostics.Exercise;

public class PaymentValidator
{
    public PaymentValidationResult ValidatePayment(Payment payment)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var result = new PaymentValidationResult();
        
        // Basic validation rules
        if (string.IsNullOrWhiteSpace(payment.TransactionId))
            result.ValidationErrors.Add("Transaction ID is required");
            
        if (payment.Amount <= 0)
            result.ValidationErrors.Add("Amount must be greater than zero");
            
        if (string.IsNullOrWhiteSpace(payment.Currency))
            result.ValidationErrors.Add("Currency is required");
        else if (payment.Currency.Length != 3)
            result.ValidationErrors.Add("Currency must be 3 characters (ISO 4217)");
            
        if (string.IsNullOrWhiteSpace(payment.PaymentMethod))
            result.ValidationErrors.Add("Payment method is required");
            
        if (string.IsNullOrWhiteSpace(payment.CustomerId))
            result.ValidationErrors.Add("Customer ID is required");
        
        // Simulate validation processing time
        Thread.Sleep(Random.Shared.Next(10, 50));
        
        stopwatch.Stop();
        result.ValidationTimeMs = (int)stopwatch.ElapsedMilliseconds;
        result.IsValid = result.ValidationErrors.Count == 0;
        
        return result;
    }
    
    public PaymentValidationResult ValidateAmount(decimal amount, string currency)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var result = new PaymentValidationResult();
        
        if (amount <= 0)
            result.ValidationErrors.Add("Amount must be positive");
            
        if (amount > 50000)
            result.ValidationErrors.Add("Amount exceeds maximum limit");
            
        // Currency-specific validation
        if (currency == "USD" && amount > 25000)
            result.ValidationErrors.Add("USD payments cannot exceed $25,000");
            
        Thread.Sleep(Random.Shared.Next(5, 25));
        
        stopwatch.Stop();
        result.ValidationTimeMs = (int)stopwatch.ElapsedMilliseconds;
        result.IsValid = result.ValidationErrors.Count == 0;
        
        return result;
    }
}