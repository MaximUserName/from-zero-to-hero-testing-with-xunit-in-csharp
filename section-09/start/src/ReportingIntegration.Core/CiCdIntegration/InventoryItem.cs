namespace ReportingIntegration.Core.CiCdIntegration;

public class InventoryItem
{
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; } = string.Empty;
    public DateTime LastUpdated { get; set; }
    public bool IsActive { get; set; } = true;
}
