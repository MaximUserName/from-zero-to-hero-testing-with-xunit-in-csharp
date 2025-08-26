using ExtensibilityCustomization.Core.ReusableLibraries;

namespace ExtensibilityCustomization.Core.Tests.ReusableLibraries;

public static class CustomAssert
{
    public static void ProductIsValidForSale(ProductData product)
    {
        Assert.NotNull(product);
        Assert.False(string.IsNullOrEmpty(product.Name), "Product name should not be empty");
        Assert.True(product.Price > 0, $"Product price should be positive, but was: {product.Price}");
        Assert.True(product.IsAvailable, "Product should be available for sale");
    }

    public static void CustomerIsVipEligible(CustomerData customer)
    {
        Assert.NotNull(customer);
        Assert.True(customer.IsVip, $"Customer {customer.Name} should be VIP");
        Assert.True(customer.OrderCount >= 5, $"VIP customer should have at least 5 orders, but has: {customer.OrderCount}");
    }

    public static void EmailIsValid(string email)
    {
        Assert.NotNull(email);
        Assert.Contains("@", email);
        Assert.Contains(".", email);
        Assert.True(email.Length > 5, "Email should be longer than 5 characters");
    }
}

public static class AssertionExtensions
{
    public static ProductAssertions Should(this ProductData product)
    {
        return new ProductAssertions(product);
    }

    public static CustomerAssertions Should(this CustomerData customer)
    {
        return new CustomerAssertions(customer);
    }
}

public class ProductAssertions
{
    private readonly ProductData _product;

    public ProductAssertions(ProductData product)
    {
        _product = product ?? throw new ArgumentNullException(nameof(product));
    }

    public ProductAssertions BeAvailable(string because = "")
    {
        Assert.True(_product.IsAvailable, 
            $"Expected product to be available{FormatBecause(because)}, but it was unavailable");
        return this;
    }

    public ProductAssertions HavePrice(decimal expectedPrice, string because = "")
    {
        Assert.Equal(expectedPrice, _product.Price);
        return this;
    }

    public ProductAssertions BeInCategory(string expectedCategory, string because = "")
    {
        Assert.Equal(expectedCategory, _product.Category);
        return this;
    }

    private static string FormatBecause(string because) => 
        string.IsNullOrEmpty(because) ? "" : $" because {because}";
}

public class CustomerAssertions
{
    private readonly CustomerData _customer;

    public CustomerAssertions(CustomerData customer)
    {
        _customer = customer ?? throw new ArgumentNullException(nameof(customer));
    }

    public CustomerAssertions BeVip(string because = "")
    {
        Assert.True(_customer.IsVip, 
            $"Expected customer to be VIP{FormatBecause(because)}");
        return this;
    }

    public CustomerAssertions HaveOrderCount(int expectedCount, string because = "")
    {
        Assert.Equal(expectedCount, _customer.OrderCount);
        return this;
    }

    private static string FormatBecause(string because) => 
        string.IsNullOrEmpty(because) ? "" : $" because {because}";
}
