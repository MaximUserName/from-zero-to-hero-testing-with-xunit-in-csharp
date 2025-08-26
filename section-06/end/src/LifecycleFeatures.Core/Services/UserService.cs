using LifecycleFeatures.Core.Domain;
using LifecycleFeatures.Core.Repositories;

namespace LifecycleFeatures.Core.Services;

/// <summary>
/// User service for business logic demos
/// </summary>
public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public UserService(IUserRepository userRepository, IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<User> CreateUserAsync(string name, string email)
    {
        var user = new User { Name = name, Email = email };
        var createdUser = _userRepository.Create(user);
        
        await _emailService.SendEmailAsync(
            email, 
            "Welcome!", 
            $"Welcome {name}!");

        return createdUser;
    }

    public IList<User> GetActiveUsers() => _userRepository.GetActiveUsers();
}
