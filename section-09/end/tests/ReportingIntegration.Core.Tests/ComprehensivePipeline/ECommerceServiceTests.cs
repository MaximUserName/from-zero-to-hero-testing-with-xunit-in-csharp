using ReportingIntegration.Core.ComprehensivePipeline;

namespace ReportingIntegration.Core.Tests.ComprehensivePipeline;

public class ECommerceServiceTests
{
    private readonly ECommerceService _service = new();

    [Fact]
    [Trait("Category", "Unit")]
    [Trait("Feature", "ProductManagement")]
    [Trait("Priority", "High")]
    public void AddProduct_ValidProduct_AddsSuccessfully()
    {
        // Arrange
        var product = new Product
        {
            Name = "Laptop",
            Description = "High-performance laptop",
            Price = 999.99m,
            Category = "Electronics",
            StockQuantity = 10
        };

        // Act
        var result = _service.AddProduct(product);

        // Assert
        Assert.True(result.Id > 0);
        Assert.True(result.CreatedAt > DateTime.MinValue);
        Assert.Equal(product.Name, result.Name);
    }

    [Fact]
    [Trait("Category", "Unit")]
    [Trait("Feature", "ProductManagement")]
    public void AddProduct_DuplicateName_ThrowsInvalidOperationException()
    {
        // Arrange
        var product1 = new Product { Name = "Duplicate Product", Price = 50m, Category = "Test", StockQuantity = 5 };
        var product2 = new Product { Name = "Duplicate Product", Price = 75m, Category = "Test", StockQuantity = 3 };

        _service.AddProduct(product1);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _service.AddProduct(product2));
        Assert.Contains("already exists", exception.Message);
    }

    [Theory]
    [Trait("Category", "Unit")]
    [InlineData("", "Description", 10.0, "Category", 5)]
    [InlineData("Product", "Description", -1.0, "Category", 5)]
    [InlineData("Product", "Description", 10.0, "", 5)]
    [InlineData("Product", "Description", 10.0, "Category", -1)]
    public void AddProduct_InvalidProduct_ThrowsArgumentException(string name, string description, double price, string category, int stock)
    {
        // Arrange
        var product = new Product
        {
            Name = name,
            Description = description,
            Price = (decimal)price,
            Category = category,
            StockQuantity = stock
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _service.AddProduct(product));
    }

    [Fact]
    [Trait("Category", "Unit")]
    [Trait("Feature", "ProductManagement")]
    public void GetProduct_ExistingProduct_ReturnsProduct()
    {
        // Arrange
        var product = new Product { Name = "Test Product", Price = 25m, Category = "Test", StockQuantity = 10 };
        var addedProduct = _service.AddProduct(product);

        // Act
        var result = _service.GetProduct(addedProduct.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(addedProduct.Id, result.Id);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void GetProduct_NonExistentProduct_ReturnsNull()
    {
        // Act
        var result = _service.GetProduct(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    [Trait("Category", "Unit")]
    [Trait("Feature", "ProductCatalog")]
    public void GetProductsByCategory_MultipleCategories_ReturnsCorrectProducts()
    {
        // Arrange
        _service.AddProduct(new Product { Name = "Phone", Price = 500m, Category = "Electronics", StockQuantity = 5 });
        _service.AddProduct(new Product { Name = "Tablet", Price = 300m, Category = "Electronics", StockQuantity = 8 });
        _service.AddProduct(new Product { Name = "Novel", Price = 15m, Category = "Books", StockQuantity = 20 });

        // Act
        var result = _service.GetProductsByCategory("Electronics");

        // Assert
        Assert.Equal(2, result.Count());
        Assert.All(result, product => Assert.Equal("Electronics", product.Category));
    }

    [Fact]
    [Trait("Category", "Unit")]
    [Trait("Feature", "ShoppingCart")]
    [Trait("Priority", "High")]
    public void CreateCart_ValidCustomerId_CreatesCart()
    {
        // Arrange
        var customerId = "customer123";

        // Act
        var result = _service.CreateCart(customerId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customerId, result.CustomerId);
        Assert.NotEmpty(result.Id);
        Assert.Empty(result.Items);
    }

    [Theory]
    [Trait("Category", "Unit")]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void CreateCart_InvalidCustomerId_ThrowsArgumentException(string customerId)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _service.CreateCart(customerId));
        Assert.Contains("Customer ID is required", exception.Message);
    }

    [Fact]
    [Trait("Category", "Unit")]
    [Trait("Feature", "ShoppingCart")]
    public void AddToCart_ValidItem_AddsSuccessfully()
    {
        // Arrange
        var product = _service.AddProduct(new Product { Name = "Test Item", Price = 10m, Category = "Test", StockQuantity = 5 });
        var cart = _service.CreateCart("customer123");

        // Act
        var result = _service.AddToCart(cart.Id, product.Id, 2);

        // Assert
        Assert.Single(result.Items);
        Assert.Equal(2, result.Items.First().Quantity);
        Assert.Equal(20m, result.Total);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void AddToCart_ExistingProduct_UpdatesQuantity()
    {
        // Arrange
        var product = _service.AddProduct(new Product { Name = "Test Item", Price = 10m, Category = "Test", StockQuantity = 10 });
        var cart = _service.CreateCart("customer123");
        _service.AddToCart(cart.Id, product.Id, 2);

        // Act
        var result = _service.AddToCart(cart.Id, product.Id, 3);

        // Assert
        Assert.Single(result.Items);
        Assert.Equal(5, result.Items.First().Quantity);
        Assert.Equal(50m, result.Total);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void AddToCart_InsufficientStock_ThrowsInvalidOperationException()
    {
        // Arrange
        var product = _service.AddProduct(new Product { Name = "Limited Item", Price = 10m, Category = "Test", StockQuantity = 3 });
        var cart = _service.CreateCart("customer123");

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _service.AddToCart(cart.Id, product.Id, 5));
        Assert.Contains("Not enough stock", exception.Message);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void AddToCart_UnavailableProduct_ThrowsInvalidOperationException()
    {
        // Arrange
        var product = _service.AddProduct(new Product { Name = "Unavailable Item", Price = 10m, Category = "Test", StockQuantity = 5 });
        product.IsAvailable = false; // Make product unavailable
        var cart = _service.CreateCart("customer123");

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _service.AddToCart(cart.Id, product.Id, 1));
        Assert.Contains("is not available", exception.Message);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void CalculateCartTotal_MultipleItems_ReturnsCorrectTotal()
    {
        // Arrange
        var product1 = _service.AddProduct(new Product { Name = "Item 1", Price = 10m, Category = "Test", StockQuantity = 10 });
        var product2 = _service.AddProduct(new Product { Name = "Item 2", Price = 15m, Category = "Test", StockQuantity = 10 });
        var cart = _service.CreateCart("customer123");

        _service.AddToCart(cart.Id, product1.Id, 2); // 20
        _service.AddToCart(cart.Id, product2.Id, 3); // 45

        // Act
        var result = _service.CalculateCartTotal(cart.Id);

        // Assert
        Assert.Equal(65m, result);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void IsProductAvailable_AvailableProduct_ReturnsTrue()
    {
        // Arrange
        var product = _service.AddProduct(new Product { Name = "Available Item", Price = 10m, Category = "Test", StockQuantity = 10 });

        // Act
        var result = _service.IsProductAvailable(product.Id, 5);

        // Assert
        Assert.True(result);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void IsProductAvailable_InsufficientStock_ReturnsFalse()
    {
        // Arrange
        var product = _service.AddProduct(new Product { Name = "Low Stock Item", Price = 10m, Category = "Test", StockQuantity = 3 });

        // Act
        var result = _service.IsProductAvailable(product.Id, 5);

        // Assert
        Assert.False(result);
    }

    [Fact]
    [Trait("Category", "Integration")]
    [Trait("Feature", "ECommerceWorkflow")]
    [Trait("Priority", "Critical")]
    public void ECommerceWorkflow_CompleteScenario_WorksEndToEnd()
    {
        // Arrange & Act - Complete e-commerce workflow
        
        // 1. Add products
        var laptop = _service.AddProduct(new Product 
        { 
            Name = "Gaming Laptop", 
            Price = 1299.99m, 
            Category = "Electronics", 
            StockQuantity = 5 
        });
        
        var mouse = _service.AddProduct(new Product 
        { 
            Name = "Gaming Mouse", 
            Price = 79.99m, 
            Category = "Electronics", 
            StockQuantity = 15 
        });

        // 2. Create shopping cart
        var cart = _service.CreateCart("workflow-customer");

        // 3. Add items to cart
        _service.AddToCart(cart.Id, laptop.Id, 1);
        _service.AddToCart(cart.Id, mouse.Id, 2);

        // 4. Check availability
        var laptopAvailable = _service.IsProductAvailable(laptop.Id, 1);
        var mouseAvailable = _service.IsProductAvailable(mouse.Id, 2);

        // 5. Calculate total
        var total = _service.CalculateCartTotal(cart.Id);

        // 6. Get products by category
        var electronicsProducts = _service.GetProductsByCategory("Electronics");

        // Assert - Verify complete workflow
        Assert.True(laptopAvailable);
        Assert.True(mouseAvailable);
        Assert.Equal(1459.97m, total); // 1299.99 + (79.99 * 2)
        Assert.Equal(2, cart.Items.Count);
        Assert.Equal(2, electronicsProducts.Count());
    }

    [Fact(Skip = "Long running integration test - enable for full test suite")]
    [Trait("Category", "Integration")]
    [Trait("Performance", "Slow")]
    public void ECommerceService_LargeDataSet_PerformsWell()
    {
        // This test would be skipped in normal runs but included in comprehensive reporting
        // to demonstrate how different test categories are handled in reports

        // Arrange - Add many products
        for (int i = 1; i <= 1000; i++)
        {
            _service.AddProduct(new Product
            {
                Name = $"Product {i}",
                Price = i * 10m,
                Category = i % 2 == 0 ? "Electronics" : "Books",
                StockQuantity = i % 10 + 1
            });
        }

        // Act & Assert - Performance test
        var startTime = DateTime.UtcNow;
        var electronicsProducts = _service.GetProductsByCategory("Electronics");
        var endTime = DateTime.UtcNow;

        Assert.Equal(500, electronicsProducts.Count());
        Assert.True((endTime - startTime).TotalMilliseconds < 1000); // Should complete within 1 second
    }
}
