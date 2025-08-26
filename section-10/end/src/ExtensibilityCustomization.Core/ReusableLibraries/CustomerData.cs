namespace ExtensibilityCustomization.Core.ReusableLibraries;

public class CustomerData
{
    public string Name { get; set; } = "Default Customer";
    public string Email { get; set; } = "customer@example.com";
    public bool IsVip { get; set; } = false;
    public int OrderCount { get; set; } = 0;

    public override string ToString()
    {
        return $"Customer: {Name} ({Email}) - {OrderCount} orders{(IsVip ? " [VIP]" : "")}";
    }
}