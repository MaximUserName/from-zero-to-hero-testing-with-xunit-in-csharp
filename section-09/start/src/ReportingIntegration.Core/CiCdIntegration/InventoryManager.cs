namespace ReportingIntegration.Core.CiCdIntegration;

public class InventoryManager
{
    private readonly Dictionary<string, InventoryItem> _inventory = new();

    public InventoryItem AddItem(InventoryItem item)
    {
        ValidateItem(item);

        if (_inventory.ContainsKey(item.Sku))
        {
            throw new InvalidOperationException($"Item with SKU {item.Sku} already exists");
        }

        item.LastUpdated = DateTime.UtcNow;
        _inventory[item.Sku] = item;
        return item;
    }

    public InventoryItem? GetItem(string sku)
    {
        return _inventory.TryGetValue(sku, out var item) ? item : null;
    }

    public InventoryItem UpdateQuantity(string sku, int quantity)
    {
        var item = GetItem(sku);
        if (item == null)
        {
            throw new ArgumentException($"Item with SKU {sku} not found", nameof(sku));
        }

        if (quantity < 0)
        {
            throw new ArgumentException("Quantity cannot be negative", nameof(quantity));
        }

        item.Quantity = quantity;
        item.LastUpdated = DateTime.UtcNow;
        return item;
    }

    public bool IsInStock(string sku)
    {
        var item = GetItem(sku);
        return item != null && item.IsActive && item.Quantity > 0;
    }

    public IEnumerable<InventoryItem> GetLowStockItems(int threshold = 10)
    {
        return _inventory.Values
            .Where(item => item.IsActive && item.Quantity <= threshold)
            .OrderBy(item => item.Quantity);
    }

    public IEnumerable<InventoryItem> GetItemsByCategory(string category)
    {
        return _inventory.Values
            .Where(item => item.IsActive && 
                   string.Equals(item.Category, category, StringComparison.OrdinalIgnoreCase));
    }

    public void DeactivateItem(string sku)
    {
        var item = GetItem(sku);
        if (item == null)
        {
            throw new ArgumentException($"Item with SKU {sku} not found", nameof(sku));
        }

        item.IsActive = false;
        item.LastUpdated = DateTime.UtcNow;
    }

    private static void ValidateItem(InventoryItem item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        if (string.IsNullOrWhiteSpace(item.Sku))
            throw new ArgumentException("SKU is required", nameof(item));

        if (string.IsNullOrWhiteSpace(item.Name))
            throw new ArgumentException("Name is required", nameof(item));

        if (item.Price < 0)
            throw new ArgumentException("Price cannot be negative", nameof(item));

        if (item.Quantity < 0)
            throw new ArgumentException("Quantity cannot be negative", nameof(item));
    }
}
