using LifecycleFeatures.Core.Domain;
using LifecycleFeatures.Core.Repositories;

namespace LifecycleFeatures.Core.Tests.ClassFixtures;

public class UserRepositoryTests : IDisposable
{
    private readonly List<User> _seedUsers;
    private IUserRepository _userRepository;

    public UserRepositoryTests()
    {
        // Simulate expensive database setup (this runs only ONCE per test class)
        Thread.Sleep(200); // Simulate database creation time
        _seedUsers = new();
        _userRepository = new InMemoryUserRepository();
        SeedTestData();
        
    }
    private void SeedTestData()
    {
        var usersToCreate = new[]
        {
            new User { Name = "Alice Johnson", Email = "alice@fixture.com", IsActive = true },
            new User { Name = "Bob Smith", Email = "bob@fixture.com", IsActive = true },
            new User { Name = "Charlie Brown", Email = "charlie@fixture.com", IsActive = false }
        };

        foreach (var user in usersToCreate)
        {
            var createdUser = _userRepository.Create(user);
            _seedUsers.Add(createdUser);
        }
    }
    
    [Fact]
    public void GetActiveUsers_WithSeededData_ReturnsActiveUsers()
    {
        // Act
        var activeUsers = _userRepository.GetActiveUsers();

        // Assert
        Assert.Equal(2, activeUsers.Count); // Alice and Bob are active
        Assert.Contains(activeUsers, u => u.Name == "Alice Johnson");
        Assert.Contains(activeUsers, u => u.Name == "Bob Smith");
        Assert.All(activeUsers, user => Assert.True(user.IsActive));
    }

    [Fact]
    public void GetActiveUsers_WithSeededData_ReturnsOnlyActive()
    {
        // Act
        var activeUsers = _userRepository.GetActiveUsers().ToList();

        // Assert
        Assert.Equal(2, activeUsers.Count); // Alice and Bob are active
        Assert.All(activeUsers, user => Assert.True(user.IsActive));
    }

    [Fact]
    public void Create_NewUser_AddsToSharedRepository()
    {
        // Arrange
        var initialCount = _userRepository.GetActiveUsers().Count;
        var newUser = new User { Name = "David Wilson", Email = "david@test.com", IsActive = true };

        // Act
        var createdUser = _userRepository.Create(newUser);

        // Assert
        Assert.NotNull(createdUser);
        Assert.True(createdUser.Id > 0);
        Assert.Equal("David Wilson", createdUser.Name);
        
        // Verify it was added to shared repository (affects other tests in this class!)
        var currentActiveCount = _userRepository.GetActiveUsers().Count;
        Assert.Equal(initialCount + 1, currentActiveCount);
    }

    public void Dispose()
    {
        _seedUsers.Clear();
    }
}