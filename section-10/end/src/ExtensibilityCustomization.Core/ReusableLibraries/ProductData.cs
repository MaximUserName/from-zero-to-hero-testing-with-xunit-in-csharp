namespace ExtensibilityCustomization.Core.ReusableLibraries;


public class ProductData
{
    public string Name { get; set; } = "Default Product";
    public decimal Price { get; set; } = 10.00m;
    public string Category { get; set; } = "General";
    public bool IsAvailable { get; set; } = true;

    public override string ToString()
    {
        return $"Product: {Name} - ${Price:F2} ({Category}) [{(IsAvailable ? "Available" : "Unavailable")}]";
    }
}