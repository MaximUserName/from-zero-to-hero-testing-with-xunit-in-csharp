using System.Diagnostics;

namespace OutputDiagnostics.Exercise;

public class PaymentProcessor
{
    private readonly List<string> _processedPayments = new();
    
    public PaymentProcessingResult ProcessPayment(Payment payment)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var result = new PaymentProcessingResult
        {
            ProcessedAmount = payment.Amount
        };
        
        try
        {
            // Simulate external service calls and complex processing
            var externalResponse = SimulateExternalServiceCall(payment);
            result.ExternalServiceResponse = externalResponse;
            
            // Capture system state
            result.SystemState["ThreadId"] = Thread.CurrentThread.ManagedThreadId;
            result.SystemState["ProcessId"] = Environment.ProcessId;
            result.SystemState["MemoryUsage"] = GC.GetTotalMemory(false);
            result.SystemState["ProcessedPaymentsCount"] = _processedPayments.Count;
            
            // Business logic validation
            if (payment.Amount <= 0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Invalid payment amount";
                return result;
            }
            
            if (payment.Amount > 10000)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Payment amount exceeds processing limit";
                return result;
            }
            
            // Simulate payment method-specific processing
            if (payment.PaymentMethod == "CreditCard")
            {
                Thread.Sleep(Random.Shared.Next(100, 200)); // Credit card processing delay
            }
            else if (payment.PaymentMethod == "BankTransfer")
            {
                Thread.Sleep(Random.Shared.Next(200, 400)); // Bank transfer processing delay
            }
            
            // Generate transaction ID and mark as successful
            result.TransactionId = $"TXN-{Guid.NewGuid():N}"[..12];
            _processedPayments.Add(result.TransactionId);
            result.IsSuccess = true;
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.ErrorMessage = $"Processing failed: {ex.Message}";
            result.SystemState["ExceptionType"] = ex.GetType().Name;
            result.SystemState["ExceptionStackTrace"] = ex.StackTrace ?? "";
        }
        finally
        {
            stopwatch.Stop();
            result.ProcessingTimeMs = (int)stopwatch.ElapsedMilliseconds;
        }
        
        return result;
    }
    
    private string SimulateExternalServiceCall(Payment payment)
    {
        // Simulate external service latency
        Thread.Sleep(Random.Shared.Next(50, 150));
        
        // Simulate different responses based on payment method
        return payment.PaymentMethod switch
        {
            "CreditCard" => $"CC_AUTH_SUCCESS_{Random.Shared.Next(1000, 9999)}",
            "BankTransfer" => $"BANK_TRANSFER_PENDING_{Random.Shared.Next(1000, 9999)}",
            _ => $"UNKNOWN_METHOD_{Random.Shared.Next(1000, 9999)}"
        };
    }
    
    public List<string> GetProcessedPayments() => _processedPayments.ToList();
    
    public Dictionary<string, object> GetSystemDiagnostics()
    {
        return new Dictionary<string, object>
        {
            ["TotalProcessedPayments"] = _processedPayments.Count,
            ["CurrentMemory"] = GC.GetTotalMemory(false),
            ["ThreadCount"] = Process.GetCurrentProcess().Threads.Count,
            ["MachineName"] = Environment.MachineName,
            ["ProcessorCount"] = Environment.ProcessorCount
        };
    }
}