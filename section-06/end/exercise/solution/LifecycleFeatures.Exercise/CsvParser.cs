namespace LifecycleFeatures.Exercise;

/// <summary>
/// Simple CSV parser for employee data
/// </summary>
public class CsvParser
{
    /// <summary>
    /// Parses a CSV line into an Employee object
    /// </summary>
    /// <param name="csvLine">CSV line with employee data</param>
    /// <returns>Parsed Employee object</returns>
    /// <exception cref="ArgumentException">Thrown when CSV format is invalid</exception>
    public Employee ParseEmployee(string csvLine)
    {
        if (string.IsNullOrWhiteSpace(csvLine))
            throw new ArgumentException("CSV line cannot be null or empty", nameof(csvLine));

        var parts = csvLine.Split(',');
        if (parts.Length != 5)
            throw new ArgumentException("CSV line must have exactly 5 fields", nameof(csvLine));

        return new Employee
        {
            EmployeeId = parts[0].Trim(),
            Name = parts[1].Trim(),
            Department = parts[2].Trim(),
            Salary = decimal.Parse(parts[3].Trim()),
            StartDate = DateTime.Parse(parts[4].Trim())
        };
    }

    /// <summary>
    /// Validates if a CSV line has the correct format
    /// </summary>
    /// <param name="csvLine">CSV line to validate</param>
    /// <returns>True if valid, false otherwise</returns>
    public bool IsValidFormat(string csvLine)
    {
        if (string.IsNullOrWhiteSpace(csvLine))
            return false;

        var parts = csvLine.Split(',');
        if (parts.Length != 5)
            return false;

        // Check if salary can be parsed as decimal
        if (!decimal.TryParse(parts[3].Trim(), out _))
            return false;

        // Check if start date can be parsed as DateTime
        if (!DateTime.TryParse(parts[4].Trim(), out _))
            return false;

        return true;
    }
}
