using TestExecutionControl.Exercise.Models;

namespace TestExecutionControl.Exercise.Services;

public class OrderService
{
    public async Task<bool> ProcessOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        if (order == null) throw new ArgumentNullException(nameof(order));

        // Simulate processing time based on order type
        var processingTime = order.Type switch
        {
            "Standard" => TimeSpan.FromMilliseconds(1500), // Fast processing
            "Express" => TimeSpan.FromMilliseconds(4000),   // Slow processing
            _ => TimeSpan.FromMilliseconds(1000)
        };

        await Task.Delay(processingTime, cancellationToken);
        
        // Simulate successful processing
        return true;
    }
}
