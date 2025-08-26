namespace OutputDiagnostics.Exercise;

public class PaymentValidationResult
{
    public bool IsValid { get; set; }
    public List<string> ValidationErrors { get; set; } = new();
    public int ValidationTimeMs { get; set; }
}