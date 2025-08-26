namespace ExtensibilityCustomization.Core.CustomAttributes;

public class UserService
{
    public User? GetUserById(int id)
    {
        // Simulate database lookup
        if (id <= 0) return null;
        
        return new User
        {
            Id = id,
            Name = $"User {id}",
            Email = $"user{id}@example.com"
        };
    }

    public void PerformSlowOperation()
    {
        // Simulate a potentially slow operation
        Thread.Sleep(100); // This should pass the default 5s time limit
    }

    public void PerformVerySlowOperation()
    {
        // Simulate an operation that might exceed time limits
        Thread.Sleep(6000); // This will exceed the default 5s limit
    }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
