namespace ReportingIntegration.Core.ReportFormats;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public CustomerTier Tier { get; set; }
    public DateTime JoinDate { get; set; }
    public bool IsActive { get; set; }
}

public enum CustomerTier
{
    Bronze,
    Silver,
    Gold,
    Platinum
}
