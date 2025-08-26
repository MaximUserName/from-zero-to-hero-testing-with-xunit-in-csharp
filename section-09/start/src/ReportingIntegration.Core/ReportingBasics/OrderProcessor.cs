namespace ReportingIntegration.Core.ReportingBasics;

public class OrderProcessor
{
    public Order ProcessOrder(Order order)
    {
        if (string.IsNullOrWhiteSpace(order.CustomerName))
        {
            throw new ArgumentException("Customer name is required", nameof(order));
        }

        if (order.Total <= 0)
        {
            throw new ArgumentException("Order total must be positive", nameof(order));
        }

        order.Status = OrderStatus.Confirmed;
        order.OrderDate = DateTime.UtcNow;

        return order;
    }

    public decimal CalculateTotal(IEnumerable<decimal> itemPrices)
    {
        return itemPrices.Sum();
    }

    public bool ValidateOrder(Order order)
    {
        return !string.IsNullOrWhiteSpace(order.CustomerName) && order.Total > 0;
    }
}
