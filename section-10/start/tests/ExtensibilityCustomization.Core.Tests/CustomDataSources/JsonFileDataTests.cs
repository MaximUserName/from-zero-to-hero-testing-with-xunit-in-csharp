using ExtensibilityCustomization.Core.CustomDataSources;
using Xunit.v3;

namespace ExtensibilityCustomization.Core.Tests.CustomDataSources;

public class JsonFileDataTests
{
    private readonly ValidationService _service = new();

    [Theory]
    [InlineData("user@domain.com", true)]
    [InlineData("invalid-email", false)]
    [InlineData("", false)]
    [InlineData("user@domain", false)]
    public void IsEmailValid_WithInlineData_ReturnsExpectedResult(string email, bool expected)
    {
        // Act
        var result = _service.IsEmailValid(email);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("ExtraTestCase", "Valid")]
    [InlineData("x", "Input too short")]
    public void ValidateInput_WithMultipleDataSources_ReturnsExpectedResult(string input, string expected)
    {
        // Act
        var result = _service.ValidateInput(input);

        // Assert
        Assert.Equal(expected, result);
    }

}
