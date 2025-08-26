namespace Assertions.Exercise;

public class UserService
{
    private readonly List<User> _users = new();
    private readonly HashSet<string> _existingEmails = new();

    public User CreateUser(string firstName, string lastName)
    {
        var email = $"{firstName.ToLower()}.{lastName.ToLower()}@company.com";
        var fullName = $"{firstName} {lastName}";
        
        var user = new User
        {
            Name = firstName,
            Email = email,
            FullName = fullName,
            IsActive = true
        };
        
        return user;
    }

    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public List<User> GetActiveUsers()
    {
        return _users.Where(u => u.IsActive).ToList();
    }

    public async Task CreateUserAsync(string email)
    {
        // Simulate async operation
        await Task.Delay(10);
        
        if (_existingEmails.Contains(email))
        {
            throw new InvalidOperationException($"User with email {email} already exists");
        }
        
        _existingEmails.Add(email);
        
        var user = new User
        {
            Email = email,
            Name = email.Split('@')[0],
            IsActive = true
        };
        
        _users.Add(user);
    }
}
