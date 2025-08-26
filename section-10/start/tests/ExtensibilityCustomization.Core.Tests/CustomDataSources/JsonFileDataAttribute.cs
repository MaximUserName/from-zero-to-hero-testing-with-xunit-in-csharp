using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit.Sdk;
using Xunit.v3;

namespace ExtensibilityCustomization.Core.Tests.CustomDataSources;

public class JsonFileDataAttribute : DataAttribute
{
    private readonly string _filePath;
    private readonly string? _propertyName;

    public JsonFileDataAttribute(string filePath)
        : this(filePath, null) { }

    public JsonFileDataAttribute(string filePath, string? propertyName)
    {
        _filePath = filePath;
        _propertyName = propertyName;
    }


    public override ValueTask<IReadOnlyCollection<ITheoryDataRow>> GetData(MethodInfo testMethod, DisposalTracker disposalTracker)
    {
        if (testMethod == null) { throw new ArgumentNullException(nameof(testMethod)); }

        try
        {
            // Get the absolute path to the JSON file
            var path = Path.IsPathRooted(_filePath)
                ? _filePath
                : Path.GetRelativePath(Directory.GetCurrentDirectory(), _filePath);

            if (!File.Exists(path))
            {
                throw new ArgumentException($"Could not find file at path: {path}");
            }

            // Load the file
            var fileData = File.ReadAllText(_filePath);
            List<object[]> rawData;

            if (string.IsNullOrEmpty(_propertyName))
            {
                // Whole file is the data
                rawData = JsonConvert.DeserializeObject<List<object[]>>(fileData) ?? new List<object[]>();
            }
            else
            {
                // Only use the specified property as the data
                var allData = JObject.Parse(fileData);
                var data = allData[_propertyName];
                rawData = data?.ToObject<List<object[]>>() ?? new List<object[]>();
            }

            // Convert to ITheoryDataRow format for xUnit v3
            var theoryDataRows = rawData.Select(row => new TheoryDataRow(row)).ToList();
            return ValueTask.FromResult<IReadOnlyCollection<ITheoryDataRow>>(theoryDataRows);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to load test data from {_filePath}: {ex.Message}", ex);
        }
    }

    public override bool SupportsDiscoveryEnumeration()
    {
        return true;
    }
}
