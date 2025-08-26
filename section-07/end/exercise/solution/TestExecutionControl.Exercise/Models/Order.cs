namespace TestExecutionControl.Exercise.Models;

public class Order
{
    public Guid OrderId { get; set; }
    public int CustomerId { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = string.Empty;
}
