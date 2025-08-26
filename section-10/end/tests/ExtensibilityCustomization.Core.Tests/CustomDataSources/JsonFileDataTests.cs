using ExtensibilityCustomization.Core.CustomDataSources;
using Xunit.v3;

namespace ExtensibilityCustomization.Core.Tests.CustomDataSources;

public class JsonFileDataTests
{
    private readonly ValidationService _service = new();
    
    [Theory]
    [JsonFileData("CustomDataSources/validation-test-data.json", "ValidationData")]
    public void ValidateInput_WithJsonFileDataAttribute_ReturnsExpectedResult(string input, string expected)
    {
        // Act
        var result = _service.ValidateInput(input);

        // Assert
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [JsonFileData("CustomDataSources/all-validation-data.json")]
    public void ValidateInput_WithEntireJsonFile_ReturnsExpectedResult(string input, string expected)
    {
        // Act
        var result = _service.ValidateInput(input);

        // Assert
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [JsonFileData("CustomDataSources/email-test-data.json", "EmailData")]
    public void IsEmailValid_WithJsonFileDataAttribute_ReturnsExpectedResult(string email, bool expected)
    {
        // Act
        var result = _service.IsEmailValid(email);

        // Assert
        Assert.Equal(expected, result);
    }
    
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
    [JsonFileData("CustomDataSources/validation-test-data.json", "ValidationData")]
    public void ValidateInput_WithMultipleDataSources_ReturnsExpectedResult(string input, string expected)
    {
        // Act
        var result = _service.ValidateInput(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
