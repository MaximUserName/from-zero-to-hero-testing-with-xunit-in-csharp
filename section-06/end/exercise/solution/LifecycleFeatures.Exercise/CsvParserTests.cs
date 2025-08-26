namespace LifecycleFeatures.Exercise;

/// <summary>
/// Tests for CSV parser functionality using constructor injection
/// No fixtures needed since CsvParser is lightweight and fast to create
/// </summary>
public class CsvParserTests
{
    private readonly CsvParser _csvParser;

    public CsvParserTests()
    {
        // Constructor injection - object created fresh for each test
        // No expensive setup needed, so no fixtures required
        _csvParser = new CsvParser();
    }

    [Fact]
    public void ParseEmployee_ValidCsvLine_ReturnsCorrectEmployee()
    {
        // Arrange
        var csvLine = "001,John Smith,Engineering,75000,2023-01-15";

        // Act
        var employee = _csvParser.ParseEmployee(csvLine);

        // Assert
        Assert.Equal("001", employee.EmployeeId);
        Assert.Equal("John Smith", employee.Name);
        Assert.Equal("Engineering", employee.Department);
        Assert.Equal(75000, employee.Salary);
        Assert.Equal(new DateTime(2023, 1, 15), employee.StartDate);
    }

    [Fact]
    public void ParseEmployee_ValidCsvLineWithSpaces_TrimsSpacesCorrectly()
    {
        // Arrange
        var csvLine = " 002 , Jane Doe , Marketing , 65000 , 2023-02-01 ";

        // Act
        var employee = _csvParser.ParseEmployee(csvLine);

        // Assert
        Assert.Equal("002", employee.EmployeeId);
        Assert.Equal("Jane Doe", employee.Name);
        Assert.Equal("Marketing", employee.Department);
        Assert.Equal(65000, employee.Salary);
        Assert.Equal(new DateTime(2023, 2, 1), employee.StartDate);
    }

    [Fact]
    public void ParseEmployee_NullOrEmptyLine_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _csvParser.ParseEmployee(null!));
        Assert.Throws<ArgumentException>(() => _csvParser.ParseEmployee(""));
        Assert.Throws<ArgumentException>(() => _csvParser.ParseEmployee("   "));
    }

    [Fact]
    public void ParseEmployee_InvalidNumberOfFields_ThrowsArgumentException()
    {
        // Arrange
        var csvLineWithTooFewFields = "001,John Smith,Engineering";
        var csvLineWithTooManyFields = "001,John Smith,Engineering,75000,2023-01-15,ExtraField";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _csvParser.ParseEmployee(csvLineWithTooFewFields));
        Assert.Throws<ArgumentException>(() => _csvParser.ParseEmployee(csvLineWithTooManyFields));
    }

    [Fact]
    public void ParseEmployee_InvalidSalaryFormat_ThrowsFormatException()
    {
        // Arrange
        var csvLine = "001,John Smith,Engineering,NotANumber,2023-01-15";

        // Act & Assert
        Assert.Throws<FormatException>(() => _csvParser.ParseEmployee(csvLine));
    }

    [Fact]
    public void ParseEmployee_InvalidDateFormat_ThrowsFormatException()
    {
        // Arrange
        var csvLine = "001,John Smith,Engineering,75000,NotADate";

        // Act & Assert
        Assert.Throws<FormatException>(() => _csvParser.ParseEmployee(csvLine));
    }

    [Fact]
    public void IsValidFormat_ValidCsvLine_ReturnsTrue()
    {
        // Arrange
        var csvLine = "001,John Smith,Engineering,75000,2023-01-15";

        // Act
        var isValid = _csvParser.IsValidFormat(csvLine);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void IsValidFormat_NullOrEmptyLine_ReturnsFalse()
    {
        // Act & Assert
        Assert.False(_csvParser.IsValidFormat(null!));
        Assert.False(_csvParser.IsValidFormat(""));
        Assert.False(_csvParser.IsValidFormat("   "));
    }

    [Fact]
    public void IsValidFormat_InvalidNumberOfFields_ReturnsFalse()
    {
        // Arrange
        var csvLineWithTooFewFields = "001,John Smith,Engineering";
        var csvLineWithTooManyFields = "001,John Smith,Engineering,75000,2023-01-15,ExtraField";

        // Act & Assert
        Assert.False(_csvParser.IsValidFormat(csvLineWithTooFewFields));
        Assert.False(_csvParser.IsValidFormat(csvLineWithTooManyFields));
    }

    [Fact]
    public void IsValidFormat_InvalidSalaryFormat_ReturnsFalse()
    {
        // Arrange
        var csvLine = "001,John Smith,Engineering,NotANumber,2023-01-15";

        // Act
        var isValid = _csvParser.IsValidFormat(csvLine);

        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public void IsValidFormat_InvalidDateFormat_ReturnsFalse()
    {
        // Arrange
        var csvLine = "001,John Smith,Engineering,75000,NotADate";

        // Act
        var isValid = _csvParser.IsValidFormat(csvLine);

        // Assert
        Assert.False(isValid);
    }
}
