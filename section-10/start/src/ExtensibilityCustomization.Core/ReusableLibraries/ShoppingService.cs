namespace ExtensibilityCustomization.Core.ReusableLibraries;

public class ShoppingService
{
    public decimal CalculateTotal(ProductData[] products)
    {
        if (products == null || products.Length == 0)
            return 0;

        return products.Where(p => p.IsAvailable).Sum(p => p.Price);
    }

    public bool CanPurchase(ProductData product, CustomerData customer)
    {
        if (product == null || customer == null)
            return false;

        if (!product.IsAvailable)
            return false;

        // VIP customers can purchase any available product
        if (customer.IsVip)
            return true;

        // Regular customers have a price limit
        return product.Price <= 100.00m;
    }

    public string ProcessOrder(ProductData[] products, CustomerData customer)
    {
        if (products == null || products.Length == 0)
            return "No products in order";

        if (customer == null)
            return "Customer information required";

        var availableProducts = products.Where(p => p.IsAvailable).ToArray();
        if (availableProducts.Length == 0)
            return "No available products";

        var total = CalculateTotal(availableProducts);
        var discount = customer.IsVip ? 0.1m : 0.0m;
        var finalTotal = total * (1 - discount);

        return $"Order processed for {customer.Name}: {availableProducts.Length} items, total: ${finalTotal:F2}";
    }
}
