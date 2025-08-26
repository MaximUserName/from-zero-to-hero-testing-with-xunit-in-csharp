namespace LifecycleFeatures.Exercise;

/// <summary>
/// Repository for employee database operations
/// </summary>
public class EmployeeRepository
{
    private readonly List<Employee> _employees = new();
    private readonly DatabaseConnection _databaseConnection;

    public EmployeeRepository(DatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection ?? throw new ArgumentNullException(nameof(databaseConnection));
    }

    /// <summary>
    /// Saves an employee to the database
    /// </summary>
    /// <param name="employee">Employee to save</param>
    /// <returns>Task representing the async operation</returns>
    public async Task SaveEmployeeAsync(Employee employee)
    {
        if (employee == null)
            throw new ArgumentNullException(nameof(employee));

        if (!_databaseConnection.IsConnected)
            throw new InvalidOperationException("Database connection is not established");

        // Simulate database save operation
        await Task.Delay(100);
        _employees.Add(employee);
    }

    /// <summary>
    /// Retrieves an employee by ID
    /// </summary>
    /// <param name="employeeId">Employee ID to search for</param>
    /// <returns>Employee if found, null otherwise</returns>
    public async Task<Employee?> GetEmployeeAsync(string employeeId)
    {
        if (string.IsNullOrWhiteSpace(employeeId))
            throw new ArgumentException("Employee ID cannot be null or empty", nameof(employeeId));

        if (!_databaseConnection.IsConnected)
            throw new InvalidOperationException("Database connection is not established");

        // Simulate database query operation
        await Task.Delay(50);
        return _employees.FirstOrDefault(e => e.EmployeeId == employeeId);
    }

    /// <summary>
    /// Gets all employees from the database
    /// </summary>
    /// <returns>List of all employees</returns>
    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        if (!_databaseConnection.IsConnected)
            throw new InvalidOperationException("Database connection is not established");

        // Simulate database query operation
        await Task.Delay(75);
        return new List<Employee>(_employees);
    }
}

/// <summary>
/// Simulates a database connection
/// </summary>
public class DatabaseConnection
{
    public bool IsConnected { get; private set; }

    /// <summary>
    /// Establishes database connection (expensive operation)
    /// </summary>
    /// <returns>Task representing the async operation</returns>
    public async Task ConnectAsync()
    {
        // Simulate expensive database setup - 3 seconds as per exercise requirements
        await Task.Delay(3000);
        IsConnected = true;
    }

    /// <summary>
    /// Closes database connection
    /// </summary>
    /// <returns>Task representing the async operation</returns>
    public async Task DisconnectAsync()
    {
        await Task.Delay(100);
        IsConnected = false;
    }
}
