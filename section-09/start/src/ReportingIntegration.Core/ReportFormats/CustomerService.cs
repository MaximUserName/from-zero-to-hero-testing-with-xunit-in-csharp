namespace ReportingIntegration.Core.ReportFormats;

public class CustomerService
{
    private readonly List<Customer> _customers = new();

    public Customer CreateCustomer(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));

        if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            throw new ArgumentException("Invalid email address", nameof(email));

        if (_customers.Any(c => c.Email == email))
            throw new InvalidOperationException("Customer with this email already exists");

        var customer = new Customer
        {
            Id = _customers.Count + 1,
            Name = name,
            Email = email,
            Tier = CustomerTier.Bronze,
            JoinDate = DateTime.UtcNow,
            IsActive = true
        };

        _customers.Add(customer);
        return customer;
    }

    public Customer? GetCustomerById(int id)
    {
        return _customers.FirstOrDefault(c => c.Id == id);
    }

    public Customer UpgradeTier(int customerId)
    {
        var customer = GetCustomerById(customerId);
        if (customer == null)
            throw new ArgumentException("Customer not found", nameof(customerId));

        customer.Tier = customer.Tier switch
        {
            CustomerTier.Bronze => CustomerTier.Silver,
            CustomerTier.Silver => CustomerTier.Gold,
            CustomerTier.Gold => CustomerTier.Platinum,
            CustomerTier.Platinum => CustomerTier.Platinum,
            _ => throw new InvalidOperationException("Invalid tier")
        };

        return customer;
    }

    public void DeactivateCustomer(int customerId)
    {
        var customer = GetCustomerById(customerId);
        if (customer == null)
            throw new ArgumentException("Customer not found", nameof(customerId));

        customer.IsActive = false;
    }

    private static bool IsValidEmail(string email)
    {
        return email.Contains('@') && email.Contains('.');
    }
}
