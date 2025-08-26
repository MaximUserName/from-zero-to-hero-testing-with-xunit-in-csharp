namespace LifecycleFeatures.Exercise;

/// <summary>
/// Tests for Employee Repository functionality using class fixtures
/// Uses IClassFixture<DatabaseFixture> to share expensive database setup across all tests
/// </summary>
public class EmployeeRepositoryTests : IClassFixture<DatabaseFixture>
{
    private readonly EmployeeRepository _employeeRepository;
    private readonly DatabaseFixture _databaseFixture;

    public EmployeeRepositoryTests(DatabaseFixture databaseFixture)
    {
        // Class fixture injected via constructor parameter
        // The expensive database setup (3 seconds) is done only once for this test class
        _databaseFixture = databaseFixture ?? throw new ArgumentNullException(nameof(databaseFixture));
        _employeeRepository = _databaseFixture.EmployeeRepository;
    }

    [Fact]
    public async Task SaveEmployeeAsync_ValidEmployee_SavesSuccessfully()
    {
        // Arrange
        var employee = new Employee
        {
            EmployeeId = "001",
            Name = "John Smith",
            Department = "Engineering",
            Salary = 75000,
            StartDate = new DateTime(2023, 1, 15)
        };

        // Act
        await _employeeRepository.SaveEmployeeAsync(employee);

        // Assert
        var savedEmployee = await _employeeRepository.GetEmployeeAsync("001");
        Assert.NotNull(savedEmployee);
        Assert.Equal("001", savedEmployee.EmployeeId);
        Assert.Equal("John Smith", savedEmployee.Name);
        Assert.Equal("Engineering", savedEmployee.Department);
        Assert.Equal(75000, savedEmployee.Salary);
        Assert.Equal(new DateTime(2023, 1, 15), savedEmployee.StartDate);
    }

    [Fact]
    public async Task SaveEmployeeAsync_NullEmployee_ThrowsArgumentNullException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
            _employeeRepository.SaveEmployeeAsync(null!));
    }

    [Fact]
    public async Task GetEmployeeAsync_ExistingEmployee_ReturnsEmployee()
    {
        // Arrange
        var employee = new Employee
        {
            EmployeeId = "002",
            Name = "Jane Doe",
            Department = "Marketing",
            Salary = 65000,
            StartDate = new DateTime(2023, 2, 1)
        };
        await _employeeRepository.SaveEmployeeAsync(employee);

        // Act
        var retrievedEmployee = await _employeeRepository.GetEmployeeAsync("002");

        // Assert
        Assert.NotNull(retrievedEmployee);
        Assert.Equal("002", retrievedEmployee.EmployeeId);
        Assert.Equal("Jane Doe", retrievedEmployee.Name);
        Assert.Equal("Marketing", retrievedEmployee.Department);
        Assert.Equal(65000, retrievedEmployee.Salary);
        Assert.Equal(new DateTime(2023, 2, 1), retrievedEmployee.StartDate);
    }

    [Fact]
    public async Task GetEmployeeAsync_NonExistentEmployee_ReturnsNull()
    {
        // Act
        var employee = await _employeeRepository.GetEmployeeAsync("999");

        // Assert
        Assert.Null(employee);
    }

    [Fact]
    public async Task GetEmployeeAsync_NullOrEmptyId_ThrowsArgumentException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => 
            _employeeRepository.GetEmployeeAsync(null!));
        await Assert.ThrowsAsync<ArgumentException>(() => 
            _employeeRepository.GetEmployeeAsync(""));
        await Assert.ThrowsAsync<ArgumentException>(() => 
            _employeeRepository.GetEmployeeAsync("   "));
    }

    [Fact]
    public async Task GetAllEmployeesAsync_WithMultipleEmployees_ReturnsAllEmployees()
    {
        // Arrange
        var employee1 = new Employee
        {
            EmployeeId = "003",
            Name = "Bob Johnson",
            Department = "Engineering",
            Salary = 80000,
            StartDate = new DateTime(2022, 12, 10)
        };
        var employee2 = new Employee
        {
            EmployeeId = "004",
            Name = "Alice Brown",
            Department = "HR",
            Salary = 70000,
            StartDate = new DateTime(2023, 3, 1)
        };

        await _employeeRepository.SaveEmployeeAsync(employee1);
        await _employeeRepository.SaveEmployeeAsync(employee2);

        // Act
        var allEmployees = await _employeeRepository.GetAllEmployeesAsync();

        // Assert
        Assert.NotNull(allEmployees);
        Assert.True(allEmployees.Count >= 2); // At least the two we just added
        
        // Verify our specific employees are in the list
        Assert.Contains(allEmployees, e => e.EmployeeId == "003" && e.Name == "Bob Johnson");
        Assert.Contains(allEmployees, e => e.EmployeeId == "004" && e.Name == "Alice Brown");
    }

    [Fact]
    public void DatabaseConnection_IsConnected_ReturnsTrue()
    {
        // Assert
        // The database connection should be established by the fixture
        Assert.True(_databaseFixture.DatabaseConnection.IsConnected);
    }
}
