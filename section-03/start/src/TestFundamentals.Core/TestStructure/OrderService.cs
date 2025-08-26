namespace TestFundamentals.Core.TestStructure;
public class OrderService
{
    public Order Create(string customerName, decimal amount)
    {
        if (string.IsNullOrEmpty(customerName))
            throw new ArgumentException("Customer name required");

        return new Order
        {
            Id = 123,
            CustomerName = customerName,
            Amount = amount
        };
    }
}

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
