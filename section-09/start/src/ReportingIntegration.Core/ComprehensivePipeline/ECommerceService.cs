namespace ReportingIntegration.Core.ComprehensivePipeline;

public class ECommerceService
{
    private readonly List<Product> _products = new();
    private readonly Dictionary<string, ShoppingCart> _carts = new();

    public Product AddProduct(Product product)
    {
        ValidateProduct(product);

        if (_products.Any(p => p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException($"Product with name '{product.Name}' already exists");
        }

        product.Id = _products.Count + 1;
        product.CreatedAt = DateTime.UtcNow;
        product.UpdatedAt = DateTime.UtcNow;

        _products.Add(product);
        return product;
    }

    public Product? GetProduct(int productId)
    {
        return _products.FirstOrDefault(p => p.Id == productId);
    }

    public IEnumerable<Product> GetProductsByCategory(string category)
    {
        return _products.Where(p => p.IsAvailable && 
            string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
    }

    public ShoppingCart CreateCart(string customerId)
    {
        if (string.IsNullOrWhiteSpace(customerId))
        {
            throw new ArgumentException("Customer ID is required", nameof(customerId));
        }

        var cart = new ShoppingCart
        {
            CustomerId = customerId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _carts[cart.Id] = cart;
        return cart;
    }

    public ShoppingCart AddToCart(string cartId, int productId, int quantity)
    {
        var cart = GetCart(cartId);
        if (cart == null)
        {
            throw new ArgumentException($"Cart with ID '{cartId}' not found", nameof(cartId));
        }

        var product = GetProduct(productId);
        if (product == null)
        {
            throw new ArgumentException($"Product with ID '{productId}' not found", nameof(productId));
        }

        if (!product.IsAvailable)
        {
            throw new InvalidOperationException($"Product '{product.Name}' is not available");
        }

        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be positive", nameof(quantity));
        }

        if (quantity > product.StockQuantity)
        {
            throw new InvalidOperationException($"Not enough stock. Available: {product.StockQuantity}, Requested: {quantity}");
        }

        var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == productId);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            cart.Items.Add(new CartItem
            {
                ProductId = productId,
                ProductName = product.Name,
                UnitPrice = product.Price,
                Quantity = quantity
            });
        }

        cart.UpdatedAt = DateTime.UtcNow;
        return cart;
    }

    public ShoppingCart? GetCart(string cartId)
    {
        return _carts.TryGetValue(cartId, out var cart) ? cart : null;
    }

    public decimal CalculateCartTotal(string cartId)
    {
        var cart = GetCart(cartId);
        return cart?.Total ?? 0;
    }

    public bool IsProductAvailable(int productId, int requestedQuantity)
    {
        var product = GetProduct(productId);
        return product != null && product.IsAvailable && product.StockQuantity >= requestedQuantity;
    }

    private static void ValidateProduct(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Product name is required", nameof(product));

        if (product.Price < 0)
            throw new ArgumentException("Product price cannot be negative", nameof(product));

        if (product.StockQuantity < 0)
            throw new ArgumentException("Stock quantity cannot be negative", nameof(product));

        if (string.IsNullOrWhiteSpace(product.Category))
            throw new ArgumentException("Product category is required", nameof(product));
    }
}
