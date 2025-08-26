using ExtensibilityCustomization.Core.ReusableLibraries;

namespace ExtensibilityCustomization.Core.Tests.ReusableLibraries;

public class ReusableLibraryTests
{
    [Fact]
    public void TestDataBuilder_WithFluentAPI_CreatesExpectedObjects()
    {
        // Fluent API makes test data creation readable and maintainable
        var product = TestDataBuilder.Product()
            .WithName("Laptop")
            .WithPrice(999.99m)
            .InCategory("Electronics")
            .AsAvailable();

        var customer = TestDataBuilder.Customer()
            .WithName("John Doe")
            .WithEmail("john@example.com")
            .AsVip()
            .WithOrderCount(10);

        // Using custom assertions for domain-specific validation
        CustomAssert.ProductIsValidForSale(product);
        CustomAssert.CustomerIsVipEligible(customer);
        CustomAssert.EmailIsValid(customer.Build().Email);
    }

    [Fact]
    public void FluentAssertions_WithShouldSyntax_ProvidesReadableTests()
    {
        var product = TestDataBuilder.Product()
            .WithName("Gaming Mouse")
            .WithPrice(79.99m)
            .InCategory("Gaming")
            .AsAvailable();

        // Fluent assertions are more readable than traditional Assert calls
        var productData = product.Build();
        productData.Should()
            .BeAvailable("it's a new product")
            .HavePrice(79.99m, "that's the retail price")
            .BeInCategory("Gaming");
    }

    [Fact]
    public void ShoppingService_CalculateTotal_WithMultipleProducts_ReturnsCorrectSum()
    {
        var products = new[]
        {
            TestDataBuilder.Product().WithPrice(10.00m).AsAvailable().Build(),
            TestDataBuilder.Product().WithPrice(20.00m).AsAvailable().Build(),
            TestDataBuilder.Product().WithPrice(30.00m).AsUnavailable().Build() // Should be excluded
        };

        var service = new ShoppingService();
        var total = service.CalculateTotal(products);

        Assert.Equal(30.00m, total); // Only available products counted
    }

    [Theory]
    [InlineData(50.00, false, true)]  // Regular customer, affordable product
    [InlineData(150.00, false, false)] // Regular customer, expensive product
    [InlineData(150.00, true, true)]   // VIP customer, any price
    public void ShoppingService_CanPurchase_WithDifferentScenarios_ReturnsExpectedResult(
        decimal productPrice, bool isVip, bool expectedCanPurchase)
    {
        var product = TestDataBuilder.Product()
            .WithPrice(productPrice)
            .AsAvailable();

        var customerBuilder = TestDataBuilder.Customer()
            .WithName("Test Customer");

        if (isVip)
            customerBuilder.AsVip();

        var customer = customerBuilder.Build();

        var service = new ShoppingService();
        var canPurchase = service.CanPurchase(product, customer);

        Assert.Equal(expectedCanPurchase, canPurchase);
    }
}
