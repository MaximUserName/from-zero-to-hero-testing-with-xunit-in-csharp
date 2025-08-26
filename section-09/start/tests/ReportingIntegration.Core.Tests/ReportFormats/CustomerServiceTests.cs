using ReportingIntegration.Core.ReportFormats;

namespace ReportingIntegration.Core.Tests.ReportFormats;

public class CustomerServiceTests
{
    private readonly CustomerService _service = new();

    [Fact]
    [Trait("Category", "Unit")]
    [Trait("Priority", "High")]
    public void CreateCustomer_ValidData_ReturnsCustomer()
    {
        // Arrange
        var name = "John Doe";
        var email = "john.doe@example.com";

        // Act
        var customer = _service.CreateCustomer(name, email);

        // Assert
        Assert.NotNull(customer);
        Assert.Equal(name, customer.Name);
        Assert.Equal(email, customer.Email);
        Assert.Equal(CustomerTier.Bronze, customer.Tier);
        Assert.True(customer.IsActive);
    }

    [Theory]
    [Trait("Category", "Unit")]
    [InlineData("", "test@example.com")]
    [InlineData(null, "test@example.com")]
    [InlineData("   ", "test@example.com")]
    public void CreateCustomer_InvalidName_ThrowsArgumentException(string name, string email)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _service.CreateCustomer(name, email));
        Assert.Contains("Name cannot be empty", exception.Message);
    }

    [Theory]
    [Trait("Category", "Unit")]
    [InlineData("John Doe", "")]
    [InlineData("John Doe", "invalid-email")]
    [InlineData("John Doe", "missing-at-sign.com")]
    public void CreateCustomer_InvalidEmail_ThrowsArgumentException(string name, string email)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _service.CreateCustomer(name, email));
        Assert.Contains("Invalid email address", exception.Message);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void CreateCustomer_DuplicateEmail_ThrowsInvalidOperationException()
    {
        // Arrange
        var email = "duplicate@example.com";
        _service.CreateCustomer("First Customer", email);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => 
            _service.CreateCustomer("Second Customer", email));
        Assert.Contains("Customer with this email already exists", exception.Message);
    }

    [Fact]
    [Trait("Category", "Unit")]
    [Trait("Priority", "Medium")]
    public void GetCustomerById_ExistingCustomer_ReturnsCustomer()
    {
        // Arrange
        var customer = _service.CreateCustomer("Jane Smith", "jane@example.com");

        // Act
        var result = _service.GetCustomerById(customer.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customer.Id, result.Id);
        Assert.Equal(customer.Name, result.Name);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void GetCustomerById_NonExistentCustomer_ReturnsNull()
    {
        // Act
        var result = _service.GetCustomerById(999);

        // Assert
        Assert.Null(result);
    }

    [Theory]
    [Trait("Category", "Unit")]
    [InlineData(CustomerTier.Bronze, CustomerTier.Silver)]
    [InlineData(CustomerTier.Silver, CustomerTier.Gold)]
    [InlineData(CustomerTier.Gold, CustomerTier.Platinum)]
    [InlineData(CustomerTier.Platinum, CustomerTier.Platinum)]
    public void UpgradeTier_ValidCustomer_UpgradesTier(CustomerTier currentTier, CustomerTier expectedTier)
    {
        // Arrange
        var customer = _service.CreateCustomer("Test User", "test@example.com");
        customer.Tier = currentTier;

        // Act
        var result = _service.UpgradeTier(customer.Id);

        // Assert
        Assert.Equal(expectedTier, result.Tier);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void UpgradeTier_NonExistentCustomer_ThrowsArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _service.UpgradeTier(999));
        Assert.Contains("Customer not found", exception.Message);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void DeactivateCustomer_ValidCustomer_SetsIsActiveFalse()
    {
        // Arrange
        var customer = _service.CreateCustomer("Active User", "active@example.com");
        Assert.True(customer.IsActive);

        // Act
        _service.DeactivateCustomer(customer.Id);

        // Assert
        Assert.False(customer.IsActive);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void DeactivateCustomer_NonExistentCustomer_ThrowsArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _service.DeactivateCustomer(999));
        Assert.Contains("Customer not found", exception.Message);
    }
}
