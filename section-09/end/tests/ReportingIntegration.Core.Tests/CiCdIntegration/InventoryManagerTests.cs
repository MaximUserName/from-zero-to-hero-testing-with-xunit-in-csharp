using ReportingIntegration.Core.CiCdIntegration;

namespace ReportingIntegration.Core.Tests.CiCdIntegration;

public class InventoryManagerTests
{
    private readonly InventoryManager _manager = new();

    [Fact]
    [Trait("Category", "Unit")]
    [Trait("Feature", "Inventory")]
    public void AddItem_ValidItem_AddsSuccessfully()
    {
        // Arrange
        var item = new InventoryItem
        {
            Sku = "TEST-001",
            Name = "Test Product",
            Quantity = 50,
            Price = 29.99m,
            Category = "Electronics"
        };

        // Act
        var result = _manager.AddItem(item);

        // Assert
        Assert.Equal(item.Sku, result.Sku);
        Assert.True(result.LastUpdated > DateTime.MinValue);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void AddItem_DuplicateSku_ThrowsInvalidOperationException()
    {
        // Arrange
        var item1 = new InventoryItem { Sku = "DUPLICATE", Name = "Item 1", Price = 10m };
        var item2 = new InventoryItem { Sku = "DUPLICATE", Name = "Item 2", Price = 20m };

        _manager.AddItem(item1);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _manager.AddItem(item2));
        Assert.Contains("already exists", exception.Message);
    }

    [Theory]
    [Trait("Category", "Unit")]
    [InlineData("", "Valid Name", 10, 29.99)]
    [InlineData("VALID-SKU", "", 10, 29.99)]
    [InlineData("VALID-SKU", "Valid Name", -1, 29.99)]
    [InlineData("VALID-SKU", "Valid Name", 10, -1)]
    public void AddItem_InvalidItem_ThrowsArgumentException(string sku, string name, int quantity, decimal price)
    {
        // Arrange
        var item = new InventoryItem
        {
            Sku = sku,
            Name = name,
            Quantity = quantity,
            Price = price,
            Category = "Test"
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _manager.AddItem(item));
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void GetItem_ExistingItem_ReturnsItem()
    {
        // Arrange
        var item = new InventoryItem { Sku = "GET-TEST", Name = "Get Test", Price = 15m };
        _manager.AddItem(item);

        // Act
        var result = _manager.GetItem("GET-TEST");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("GET-TEST", result.Sku);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void GetItem_NonExistentItem_ReturnsNull()
    {
        // Act
        var result = _manager.GetItem("NON-EXISTENT");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void UpdateQuantity_ValidItem_UpdatesSuccessfully()
    {
        // Arrange
        var item = new InventoryItem { Sku = "UPDATE-TEST", Name = "Update Test", Quantity = 10, Price = 25m };
        var addedItem = _manager.AddItem(item);
        var originalTimestamp = addedItem.LastUpdated;

        // Add small delay to ensure timestamp difference
        Thread.Sleep(10);

        // Act
        var result = _manager.UpdateQuantity("UPDATE-TEST", 20);

        // Assert
        Assert.Equal(20, result.Quantity);
        Assert.True(result.LastUpdated >= originalTimestamp);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void UpdateQuantity_NonExistentItem_ThrowsArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _manager.UpdateQuantity("NON-EXISTENT", 10));
        Assert.Contains("not found", exception.Message);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void UpdateQuantity_NegativeQuantity_ThrowsArgumentException()
    {
        // Arrange
        var item = new InventoryItem { Sku = "NEGATIVE-TEST", Name = "Negative Test", Price = 10m };
        _manager.AddItem(item);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _manager.UpdateQuantity("NEGATIVE-TEST", -5));
        Assert.Contains("cannot be negative", exception.Message);
    }

    [Theory]
    [Trait("Category", "Unit")]
    [InlineData(10, true)]
    [InlineData(0, false)]
    [InlineData(-1, false)] // This case won't happen due to validation, but tests the logic
    public void IsInStock_DifferentQuantities_ReturnsExpectedResult(int quantity, bool expectedInStock)
    {
        // Arrange
        var item = new InventoryItem { Sku = "STOCK-TEST", Name = "Stock Test", Quantity = Math.Max(0, quantity), Price = 10m };
        _manager.AddItem(item);

        if (quantity < 0) // Simulate edge case
        {
            item.Quantity = quantity; // Direct assignment to bypass validation
        }

        // Act
        var result = _manager.IsInStock("STOCK-TEST");

        // Assert
        Assert.Equal(expectedInStock, result);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void IsInStock_DeactivatedItem_ReturnsFalse()
    {
        // Arrange
        var item = new InventoryItem { Sku = "DEACTIVATED-TEST", Name = "Deactivated Test", Quantity = 10, Price = 10m };
        _manager.AddItem(item);
        _manager.DeactivateItem("DEACTIVATED-TEST");

        // Act
        var result = _manager.IsInStock("DEACTIVATED-TEST");

        // Assert
        Assert.False(result);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void GetLowStockItems_MultipleItems_ReturnsCorrectItems()
    {
        // Arrange
        _manager.AddItem(new InventoryItem { Sku = "LOW-1", Name = "Low Stock 1", Quantity = 5, Price = 10m });
        _manager.AddItem(new InventoryItem { Sku = "LOW-2", Name = "Low Stock 2", Quantity = 8, Price = 15m });
        _manager.AddItem(new InventoryItem { Sku = "HIGH-1", Name = "High Stock 1", Quantity = 50, Price = 20m });

        // Act
        var result = _manager.GetLowStockItems(threshold: 10);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(result, item => item.Sku == "LOW-1");
        Assert.Contains(result, item => item.Sku == "LOW-2");
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void GetItemsByCategory_MultipleCategories_ReturnsCorrectItems()
    {
        // Arrange
        _manager.AddItem(new InventoryItem { Sku = "ELEC-1", Name = "Electronics 1", Category = "Electronics", Price = 10m });
        _manager.AddItem(new InventoryItem { Sku = "ELEC-2", Name = "Electronics 2", Category = "Electronics", Price = 15m });
        _manager.AddItem(new InventoryItem { Sku = "BOOK-1", Name = "Book 1", Category = "Books", Price = 20m });

        // Act
        var result = _manager.GetItemsByCategory("Electronics");

        // Assert
        Assert.Equal(2, result.Count());
        Assert.All(result, item => Assert.Equal("Electronics", item.Category));
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void DeactivateItem_ValidItem_DeactivatesSuccessfully()
    {
        // Arrange
        var item = new InventoryItem { Sku = "DEACTIVATE-TEST", Name = "Deactivate Test", Price = 10m };
        _manager.AddItem(item);

        // Act
        _manager.DeactivateItem("DEACTIVATE-TEST");

        // Assert
        var result = _manager.GetItem("DEACTIVATE-TEST");
        Assert.NotNull(result);
        Assert.False(result.IsActive);
    }

    [Fact]
    [Trait("Category", "Integration")]
    [Trait("Feature", "Workflow")]
    public void InventoryWorkflow_CompleteScenario_WorksCorrectly()
    {
        // Arrange & Act - Complete inventory workflow
        var item = new InventoryItem
        {
            Sku = "WORKFLOW-001",
            Name = "Workflow Test Product",
            Quantity = 100,
            Price = 49.99m,
            Category = "Test"
        };

        // Add item
        _manager.AddItem(item);

        // Check if in stock
        var inStock = _manager.IsInStock("WORKFLOW-001");

        // Update quantity to low stock level
        _manager.UpdateQuantity("WORKFLOW-001", 5);

        // Check low stock (threshold 10, quantity 5 should be included)
        var lowStockItems = _manager.GetLowStockItems(10).ToList();

        // Verify item is in low stock before deactivation
        var workflowItemBeforeDeactivation = _manager.GetItem("WORKFLOW-001");
        Assert.NotNull(workflowItemBeforeDeactivation);
        Assert.Equal(5, workflowItemBeforeDeactivation.Quantity);
        Assert.True(workflowItemBeforeDeactivation.IsActive);

        // Deactivate item
        _manager.DeactivateItem("WORKFLOW-001");

        // Final stock check
        var finalStockCheck = _manager.IsInStock("WORKFLOW-001");

        // Assert
        Assert.True(inStock); // Initially in stock
        Assert.Contains(lowStockItems, i => i.Sku == "WORKFLOW-001"); // Should be in low stock
        Assert.False(finalStockCheck); // Should not be in stock after deactivation
    }
}
