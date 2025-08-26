namespace OutputDiagnostics.Exercise;

public class PaymentProcessingResult
{
    public bool IsSuccess { get; set; }
    public string? TransactionId { get; set; }
    public string? ErrorMessage { get; set; }
    public int ProcessingTimeMs { get; set; }
    public decimal ProcessedAmount { get; set; }
    public DateTime ProcessedAt { get; set; } = DateTime.UtcNow;
    public string? ExternalServiceResponse { get; set; }
    public Dictionary<string, object> SystemState { get; set; } = new();
}