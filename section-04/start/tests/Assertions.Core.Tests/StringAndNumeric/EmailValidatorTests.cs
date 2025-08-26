using Assertions.Core.StringAndNumeric;

namespace Assertions.Core.Tests.StringAndNumeric;

public class EmailValidatorTests
{
    [Fact]
    public void Validate_WithValidEmail_ReturnsValidResult()
    {
        // Arrange
        var validator = new EmailValidator();
        var validEmail = "user@example.com";

        // Act
        var result = validator.Validate(validEmail);

        // Assert - Basic assertions
    }

    [Fact]
    public void Validate_WithInvalidEmail_ReturnsInvalidResult()
    {
        // Arrange
        var validator = new EmailValidator();
        var invalidEmail = "not-an-email";

        // Act
        var result = validator.Validate(invalidEmail);

        // Assert - String assertions for error messages

    }

    [Fact]
    public void Validate_WithNullEmail_ReturnsAppropriateError()
    {
        // Arrange
        var validator = new EmailValidator();

        // Act
        var result = validator.Validate(null);

        // Assert - String content assertions

    }

    [Fact]
    public void Validate_WithEmailMissingAtSymbol_ReturnsSpecificError()
    {
        // Arrange
        var validator = new EmailValidator();
        var emailWithoutAt = "userexample.com";

        // Act
        var result = validator.Validate(emailWithoutAt);

        // Assert - Specific string matching

    }

    // TODO: Add more test cases to practice string assertions:
    // - Test emails with different cases (Assert with StringComparison options)
    // - Use Assert.Matches with regex patterns for email validation
    // - Practice Assert.DoesNotContain for invalid characters
    // - Test string length limits with Assert.InRange
}
