namespace Assertions.Core.Collections;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string Role { get; set; } = string.Empty;

    public override bool Equals(object? obj)
    {
        if (obj is not User other) return false;
        return Id == other.Id && Name == other.Name && Email == other.Email;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Email);
    }
}

public class UserService
{
    private readonly List<User> _users = new()
    {
        new User { Id = 1, Name = "John Doe", Email = "john@example.com", IsActive = true, Role = "Admin" },
        new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com", IsActive = true, Role = "User" },
        new User { Id = 3, Name = "Bob Johnson", Email = "bob@example.com", IsActive = false, Role = "User" },
        new User { Id = 4, Name = "Alice Brown", Email = "alice@example.com", IsActive = true, Role = "Manager" }
    };

    public IEnumerable<User> GetAllUsers()
    {
        return _users.AsEnumerable();
    }

    public IEnumerable<User> GetActiveUsers()
    {
        return _users.Where(u => u.IsActive);
    }

    public IEnumerable<User> GetUsersByRole(string role)
    {
        return _users.Where(u => u.Role.Equals(role, StringComparison.OrdinalIgnoreCase));
    }

    public User? FindUserByEmail(string email)
    {
        return _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<string> GetUserEmails()
    {
        return _users.Select(u => u.Email);
    }

    public Dictionary<string, List<User>> GroupUsersByRole()
    {
        return _users.GroupBy(u => u.Role)
                    .ToDictionary(g => g.Key, g => g.ToList());
    }
}
