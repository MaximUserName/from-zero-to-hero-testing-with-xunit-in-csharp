namespace TestFundamentals.Core.Tests.TestStructure;

using TestFundamentals.Core.TestStructure;

public class OrderServiceTests
{
    [Fact]
    public void TestCreateOrder()
    {
        var service = new OrderService();
        var customer = new Customer()
        {
            Id = Guid.NewGuid(),
            CustomerName = "Gui",
            Email = "gui@guiferreira.me"
        };
        Assert.NotNull(service.Create(customer, 100));
        var order = service.Create(customer, 100);
        Assert.Equal("Gui", order.CustomerName);
        Assert.Equal(100, order.Amount);
    }

    [Fact]
    public void TestException()
    {
        Assert.Throws<ArgumentException>(() => new OrderService().Create(null!, 100));
    }
}
