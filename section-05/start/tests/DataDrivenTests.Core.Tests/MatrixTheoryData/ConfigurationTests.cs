using DataDrivenTests.Core.MatrixTheoryData;

namespace DataDrivenTests.Core.Tests.MatrixTheoryData;

public class ConfigurationTests
{
    [Theory]
    [MemberData(nameof(ConfigurationMatrix))]
    public void ProcessRequest_ShouldHandleAllConfigurationCombinations(
        bool isHttps, 
        string environment, 
        int timeout)
    {
        // Arrange
        var config = new Configuration(isHttps, environment, timeout);
        var processor = new RequestProcessor(config);

        // Act & Assert
        if (environment == "Production" && !isHttps)
        {
            // Production must use HTTPS
            var result = processor.ProcessRequest("GET", true);
            Assert.False(result.Success);
            Assert.Contains("Invalid configuration", result.Message);
        }
        else if (timeout <= 0)
        {
            // Invalid timeout
            var result = processor.ProcessRequest("GET", true);
            Assert.False(result.Success);
        }
        else
        {
            // Valid configuration
            var result = processor.ProcessRequest("GET", true);
            Assert.True(result.Success);
        }
    }

    [Theory]
    [MemberData(nameof(RequestProcessingMatrix))]
    public void ProcessRequest_ShouldHandleAllRequestCombinations(
        string requestType, 
        bool hasAuth, 
        string environment)
    {
        // Arrange
        var config = new Configuration(true, environment, 30); // Valid config
        var processor = new RequestProcessor(config);

        // Act
        var result = processor.ProcessRequest(requestType, hasAuth);

        // Assert
        if (requestType == "DELETE" && !hasAuth)
        {
            Assert.False(result.Success);
            Assert.Contains("Authentication required", result.Message);
        }
        else if (requestType == "DEBUG" && environment == "Production")
        {
            Assert.False(result.Success);
            Assert.Contains("not allowed in Production", result.Message);
        }
        else
        {
            Assert.True(result.Success);
        }
    }

    // Creates all combinations: 2 × 3 × 3 = 18 test cases using MemberData
    public static IEnumerable<object[]> ConfigurationMatrix()
    {
        var httpsOptions = new[] { true, false };
        var environments = new[] { "Development", "Staging", "Production" };
        var timeouts = new[] { 30, 60, 120 };

        foreach (var https in httpsOptions)
        {
            foreach (var env in environments)
            {
                foreach (var timeout in timeouts)
                {
                    yield return new object[] { https, env, timeout };
                }
            }
        }
    }

    // Creates request processing combinations: 4 × 2 × 3 = 24 test cases
    public static IEnumerable<object[]> RequestProcessingMatrix()
    {
        var requestTypes = new[] { "GET", "POST", "DELETE", "DEBUG" };
        var authOptions = new[] { true, false };
        var environments = new[] { "Development", "Staging", "Production" };

        foreach (var requestType in requestTypes)
        {
            foreach (var hasAuth in authOptions)
            {
                foreach (var env in environments)
                {
                    yield return new object[] { requestType, hasAuth, env };
                }
            }
        }
    }
}
