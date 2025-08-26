namespace LifecycleFeatures.Exercise;

/// <summary>
/// Employee domain model for CSV processing exercise
/// </summary>
public class Employee
{
    public string EmployeeId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public DateTime StartDate { get; set; }
}
