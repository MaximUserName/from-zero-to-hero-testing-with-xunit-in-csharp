using Assertions.Core.Collections;

namespace Assertions.Core.Tests.Collections;

public class ShoppingCartTests
{
    [Fact]
    public void NewCart_IsEmpty()
    {
        // Arrange & Act
        var cart = new ShoppingCart();

        // Assert - Empty collection testing
        
    }

    [Fact]
    public void AddItem_WithValidItem_AddsToCart()
    {
        // Arrange
        var cart = new ShoppingCart();
        var item = new CartItem 
        { 
            ProductId = 1, 
            ProductName = "Laptop", 
            Price = 999.99m, 
            Quantity = 1 
        };

        // Act
        cart.AddItem(item);

        // Assert - Collection membership and structure
    }

    [Fact]
    public void AddItem_WithSameProduct_IncreasesQuantity()
    {
        // Arrange
        var cart = new ShoppingCart();
        var item1 = new CartItem { ProductId = 1, ProductName = "Mouse", Price = 29.99m, Quantity = 1 };
        var item2 = new CartItem { ProductId = 1, ProductName = "Mouse", Price = 29.99m, Quantity = 2 };

        // Act
        cart.AddItem(item1);
        cart.AddItem(item2);

        // Assert - Single item with combined quantity

    }

    [Fact]
    public void GetExpensiveItems_WithThreshold_ReturnsFilteredItems()
    {
        // Arrange
        var cart = new ShoppingCart();
        cart.AddItem(new CartItem { ProductId = 1, ProductName = "Laptop", Price = 999.99m, Quantity = 1 });
        cart.AddItem(new CartItem { ProductId = 2, ProductName = "Mouse", Price = 29.99m, Quantity = 1 });
        cart.AddItem(new CartItem { ProductId = 3, ProductName = "Keyboard", Price = 79.99m, Quantity = 1 });

        // Act
        var expensiveItems = cart.GetExpensiveItems(100m);

        // Assert - Filtered collection testing

    }

    [Fact]
    public void RemoveItem_WithValidProductId_RemovesFromCart()
    {
        // Arrange
        var cart = new ShoppingCart();
        cart.AddItem(new CartItem { ProductId = 1, ProductName = "Laptop", Price = 999.99m, Quantity = 1 });
        cart.AddItem(new CartItem { ProductId = 2, ProductName = "Mouse", Price = 29.99m, Quantity = 1 });

        // Act
        cart.RemoveItem(1);

        // Assert - Collection after removal

    }

    [Fact]
    public void Clear_RemovesAllItems()
    {
        // Arrange
        var cart = new ShoppingCart();
        cart.AddItem(new CartItem { ProductId = 1, ProductName = "Laptop", Price = 999.99m, Quantity = 1 });
        cart.AddItem(new CartItem { ProductId = 2, ProductName = "Mouse", Price = 29.99m, Quantity = 1 });

        // Act
        cart.Clear();

        // Assert - Empty collection after clear

    }

    // TODO: Add more test cases to practice collection assertions:
    // - Test collection equality with expected vs actual item lists
    // - Use Assert.All to validate all items have positive prices and quantities
    // - Practice Assert.Contains with predicate functions
    // - Test dictionary-style operations if you add product lookup features
    // - Use Assert.InRange for testing collection sizes within expected bounds
}
