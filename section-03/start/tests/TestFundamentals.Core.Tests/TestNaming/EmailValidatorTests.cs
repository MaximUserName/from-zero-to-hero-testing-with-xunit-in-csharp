namespace TestFundamentals.Core.Tests.TestNaming;

using TestFundamentals.Core.TestNaming;

public class EmailValidatorTests
{
    [Fact]
    public void Test1()
    {
        var validator = new EmailValidator();
        var result = validator.IsValid("user@domain.com");
        Assert.True(result);
    }

    [Fact]
    public void Test2()
    {
        var validator = new EmailValidator();
        var result = validator.IsValid("");
        Assert.False(result);
    }

    [Fact]
    public void TestValidation()
    {
        var validator = new EmailValidator();
        Assert.False(validator.IsValid("invalid"));
    }
}
