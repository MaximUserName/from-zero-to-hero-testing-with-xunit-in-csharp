# Assertions Practice Exercise

Practice using different types of xUnit assertions with a simple calculator and user management system.

**🎯 Learning Objectives**: Master basic, string, numeric, collection, and exception assertions.

## Part 1: Basic Calculator Tests

Write tests for a simple calculator that demonstrate different assertion types:

```csharp
[Fact]
public void Calculator_Add_ReturnsCorrectSum()
{
    // Arrange
    var calculator = new Calculator();
    
    // Act
    var result = calculator.Add(5, 3);
    
    // Assert
    // TODO: Add your assertions here
    // Verify the result is exactly 8
}

[Fact]
public void Calculator_Divide_ReturnsCorrectQuotient()
{
    // Arrange
    var calculator = new Calculator();
    
    // Act
    var result = calculator.Divide(10, 3);
    
    // Assert
    // TODO: Add your assertions here
    // Verify the result is approximately 3.33 (with precision)
}
```

## Part 2: User Management Tests

Test string operations and collections:

```csharp
[Fact]
public void UserService_CreateUser_GeneratesValidEmail()
{
    // Arrange
    var userService = new UserService();
    
    // Act
    var user = userService.CreateUser("John", "Doe");
    
    // Assert
    // TODO: Add your assertions here
    // - Verify user is not null
    // - Check email format (should be john.doe@company.com)
    // - Verify full name contains both first and last name
}

[Fact]
public void UserService_GetActiveUsers_ReturnsFilteredList()
{
    // Arrange
    var userService = new UserService();
    userService.AddUser(new User { Name = "Alice", IsActive = true });
    userService.AddUser(new User { Name = "Bob", IsActive = false });
    userService.AddUser(new User { Name = "Charlie", IsActive = true });
    
    // Act
    var activeUsers = userService.GetActiveUsers();
    
    // Assert
    // TODO: Add your assertions here
    // - Verify collection has exactly 2 users
    // - Check that all users in the collection are active
    // - Verify specific users are present
}
```

## Part 3: Exception Handling

Test exception scenarios:

```csharp
[Fact]
public void Calculator_DivideByZero_ThrowsException()
{
    // Arrange
    var calculator = new Calculator();
    
    // Act & Assert
    // TODO: Add your exception assertion here
    // Verify that dividing by zero throws DivideByZeroException
}

[Fact]
public async Task UserService_CreateDuplicateUser_ThrowsException()
{
    // Arrange
    var userService = new UserService();
    await userService.CreateUserAsync("john@test.com");
    
    // Act & Assert
    // TODO: Add your async exception assertion here
    // Verify that creating a duplicate user throws InvalidOperationException
}
```

## 💡 Hints

**Basic Assertions:**
- Use `Assert.Equal()` for exact value comparisons
- Use `Assert.NotNull()` to verify objects exist
- Use `Assert.True()` and `Assert.False()` for boolean conditions

**String Assertions:**
- Use `Assert.Contains()` to check if a string contains another string
- Use `Assert.StartsWith()` and `Assert.EndsWith()` for prefix/suffix checks
- Use `Assert.Matches()` with regex for pattern validation

**Numeric Assertions:**
- Use `Assert.Equal(expected, actual, precision)` for decimal comparisons
- Use `Assert.InRange()` to verify values are within bounds

**Collection Assertions:**
- Use `Assert.Equal()` for count comparisons
- Use `Assert.All()` to verify conditions on all collection items
- Use `Assert.Contains()` to check for specific items

**Exception Assertions:**
- Use `Assert.Throws<ExceptionType>()` for synchronous exceptions
- Use `Assert.ThrowsAsync<ExceptionType>()` for asynchronous exceptions

## Need help?
Go back and rewatch the previous lectures. It usually helps out. 
If you still need help after that, don't hesitate to reach out (https://guiferreira.me/about)!

## Looking for an accountability partner?
Tag me on X (@gsferreira) or LinkedIn (@gferreira), and I will be there for you.

Let's do it!