namespace LifecycleFeatures.Exercise;

/// <summary>
/// Database fixture for expensive database setup operations
/// Implements IAsyncLifetime for async setup/cleanup
/// </summary>
public class DatabaseFixture : IAsyncLifetime
{
    public DatabaseConnection DatabaseConnection { get; private set; } = null!;
    public EmployeeRepository EmployeeRepository { get; private set; } = null!;

    /// <summary>
    /// Async initialization - called once per test class
    /// Simulates expensive database setup (3 seconds as per requirements)
    /// </summary>
    public async ValueTask InitializeAsync()
    {
        DatabaseConnection = new DatabaseConnection();
        
        // This is the expensive operation that takes 3 seconds
        // Without proper fixture management, this would run for every test
        await DatabaseConnection.ConnectAsync();
        
        EmployeeRepository = new EmployeeRepository(DatabaseConnection);
    }

    /// <summary>
    /// Async cleanup - called once per test class
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        if (DatabaseConnection != null)
        {
            await DatabaseConnection.DisconnectAsync();
        }
    }
}
