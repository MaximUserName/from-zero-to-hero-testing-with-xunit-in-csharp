using ExtensibilityCustomization.Core.ReusableLibraries;

namespace ExtensibilityCustomization.Core.Tests.ReusableLibraries;

public static class TestDataBuilder
{

    public static ProductTestDataBuilder Product() => new ProductTestDataBuilder();


    public static CustomerTestDataBuilder Customer() => new CustomerTestDataBuilder();
}

public class ProductTestDataBuilder
{
    private readonly ProductData _product = new ProductData();


    public ProductTestDataBuilder WithName(string name)
    {
        _product.Name = name;
        return this;
    }


    public ProductTestDataBuilder WithPrice(decimal price)
    {
        _product.Price = price;
        return this;
    }


    public ProductTestDataBuilder InCategory(string category)
    {
        _product.Category = category;
        return this;
    }


    public ProductTestDataBuilder AsAvailable()
    {
        _product.IsAvailable = true;
        return this;
    }


    public ProductTestDataBuilder AsUnavailable()
    {
        _product.IsAvailable = false;
        return this;
    }


    public ProductData Build() => _product;


    public static implicit operator ProductData(ProductTestDataBuilder builder) => builder.Build();
}

public class CustomerTestDataBuilder
{
    private readonly CustomerData _customer = new CustomerData();

    public CustomerTestDataBuilder WithName(string name)
    {
        _customer.Name = name;
        return this;
    }

    public CustomerTestDataBuilder WithEmail(string email)
    {
        _customer.Email = email;
        return this;
    }

    public CustomerTestDataBuilder AsVip()
    {
        _customer.IsVip = true;
        return this;
    }

    public CustomerTestDataBuilder WithOrderCount(int orderCount)
    {
        _customer.OrderCount = orderCount;
        return this;
    }

    public CustomerData Build() => _customer;

    public static implicit operator CustomerData(CustomerTestDataBuilder builder) => builder.Build();
}
