using Assertions.Core.Collections;

namespace Assertions.Core.Tests.Collections;

public class UserServiceTests
{
    [Fact]
    public void GetAllUsers_ReturnsNonEmptyCollection()
    {
        // Arrange
        var service = new UserService();

        // Act
        var users = service.GetAllUsers();

        // Assert - Collection structure

    }

    [Fact]
    public void GetActiveUsers_ReturnsOnlyActiveUsers()
    {
        // Arrange
        var service = new UserService();

        // Act
        var activeUsers = service.GetActiveUsers();

        // Assert - Universal conditions with Assert.All

    }

    [Fact]
    public void GetUsersByRole_WithAdminRole_ReturnsAdminUsers()
    {
        // Arrange
        var service = new UserService();

        // Act
        var adminUsers = service.GetUsersByRole("Admin");

        // Assert - Collection membership

    }

    [Fact]
    public void FindUserByEmail_WithValidEmail_ReturnsSingleUser()
    {
        // Arrange
        var service = new UserService();
        var email = "john@example.com";

        // Act
        var user = service.FindUserByEmail(email);

        // Assert - Single item validation

    }

    [Fact]
    public void GetUserEmails_ReturnsAllEmailAddresses()
    {
        // Arrange
        var service = new UserService();

        // Act
        var emails = service.GetUserEmails();

        // Assert - Collection content testing

    }

    [Fact]
    public void GroupUsersByRole_ReturnsCorrectGroupings()
    {
        // Arrange
        var service = new UserService();

        // Act
        var groupedUsers = service.GroupUsersByRole();

        // Assert - Dictionary/grouped collection testing

    }

    // TODO: Add more test cases to practice collection assertions:
    // - Test Assert.Empty with filtered collections that return no results
    // - Use Assert.Single when expecting exactly one result
    // - Practice order-independent collection comparison
    // - Test collection equality with custom comparers
    // - Use Assert.All for complex validation scenarios
}
