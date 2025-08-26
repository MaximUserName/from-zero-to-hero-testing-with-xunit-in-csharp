using LifecycleFeatures.Core.Repositories;
using LifecycleFeatures.Core.Domain;

namespace LifecycleFeatures.Core.Tests.AsyncLifetime;

public class AsyncDatabaseTests : IDisposable
{
    private InMemoryUserRepository _repository = null!;
    private List<User> _testUsers = new();

    public AsyncDatabaseTests()
    {
        // Simulate async database connection and setup
        Task.Delay(100).GetAwaiter().GetResult(); // Simulate connection time
        
        _repository = new InMemoryUserRepository();
        
        // Seed test data asynchronously
        SeedTestDataAsync().GetAwaiter().GetResult();   
    }

    private async Task SeedTestDataAsync()
    {
        // Simulate async data seeding
        var users = new[]
        {
            new User { Name = "Alice Johnson", Email = "alice@test.com", IsActive = true },
            new User { Name = "Bob Smith", Email = "bob@test.com", IsActive = true },
            new User { Name = "Charlie Brown", Email = "charlie@test.com", IsActive = false }
        };

        foreach (var user in users)
        {
            // Simulate async database insert
            await Task.Delay(10);
            var savedUser = _repository.Create(user);
            _testUsers.Add(savedUser);
        }
    }

    [Fact]
    public void SeedData_AfterAsyncInit_IsAvailable()
    {
        // Assert that seeded data is available
        var activeUsers = _repository.GetActiveUsers();
        Assert.Equal(2, activeUsers.Count); // Alice and Bob are active
        Assert.Contains(activeUsers, u => u.Name == "Alice Johnson");
    }

    [Fact]
    public async Task AddUser_WithAsyncOperation_PersistsCorrectly()
    {
        // Simulate async operation
        await Task.Delay(10);
        
        // Act
        var newUser = new User { Name = "David Wilson", Email = "david@test.com", IsActive = true };
        var savedUser = _repository.Create(newUser);

        // Assert
        Assert.True(savedUser.Id > 0);
        Assert.Equal("David Wilson", savedUser.Name);
        
        var retrieved = _repository.GetById(savedUser.Id);
        Assert.NotNull(retrieved);
        Assert.Equal("david@test.com", retrieved.Email);
    }

    public async ValueTask DisposeAsync()
    {
        Console.WriteLine("🧹 AsyncDatabaseTests - Starting async cleanup...");
        
       
        Console.WriteLine("✅ Async cleanup complete");
    }

    public void Dispose()
    {
        // Simulate async cleanup (closing connections, etc.)
        Task.Delay(50).GetAwaiter().GetResult();
        
        _testUsers.Clear();
    }
}
