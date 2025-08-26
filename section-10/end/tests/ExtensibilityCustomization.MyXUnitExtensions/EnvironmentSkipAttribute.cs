using Xunit;

namespace ExtensibilityCustomization.MyXUnitExtensions;

[AttributeUsage(AttributeTargets.Method)]
public class SkipOnEnvironmentAttribute : FactAttribute
{
    public SkipOnEnvironmentAttribute(string environmentName, string? reason = null)
    {
        var currentEnvironment = Environment.GetEnvironmentVariable("ENVIRONMENT") 
                                ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                                ?? "Development";

        if (string.Equals(currentEnvironment, environmentName, StringComparison.OrdinalIgnoreCase))
        {
            Skip = reason ?? $"Test skipped in {environmentName} environment";
        }
    }
}

[AttributeUsage(AttributeTargets.Method)]
public class RunOnlyInEnvironmentAttribute : FactAttribute
{
    public RunOnlyInEnvironmentAttribute(string environmentName, string? reason = null)
    {
        var currentEnvironment = Environment.GetEnvironmentVariable("ENVIRONMENT") 
                                ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                                ?? "Development";

        if (!string.Equals(currentEnvironment, environmentName, StringComparison.OrdinalIgnoreCase))
        {
            Skip = reason ?? $"Test only runs in {environmentName} environment (current: {currentEnvironment})";
        }
    }
}

[AttributeUsage(AttributeTargets.Method)]
public class SkipWhenEnvironmentVariableExistsAttribute : FactAttribute
{
    public SkipWhenEnvironmentVariableExistsAttribute(string variableName, string? reason = null)
    {
        var variableValue = Environment.GetEnvironmentVariable(variableName);
        
        if (!string.IsNullOrEmpty(variableValue))
        {
            Skip = reason ?? $"Test skipped because environment variable '{variableName}' is set to '{variableValue}'";
        }
    }
}

[AttributeUsage(AttributeTargets.Method)]
public class SkipWhenEnvironmentVariableEqualsAttribute : FactAttribute
{
    public SkipWhenEnvironmentVariableEqualsAttribute(string variableName, string expectedValue, string? reason = null)
    {
        var variableValue = Environment.GetEnvironmentVariable(variableName);
        
        if (string.Equals(variableValue, expectedValue, StringComparison.OrdinalIgnoreCase))
        {
            Skip = reason ?? $"Test skipped because environment variable '{variableName}' equals '{expectedValue}'";
        }
    }
}

public static class EnvironmentTestHelper
{
    public static string GetCurrentEnvironment()
    {
        return Environment.GetEnvironmentVariable("ENVIRONMENT") 
               ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
               ?? "Development";
    }

    public static bool IsEnvironment(string environmentName)
    {
        return string.Equals(GetCurrentEnvironment(), environmentName, StringComparison.OrdinalIgnoreCase);
    }

    public static void SkipIfEnvironment(string environmentName, string? reason = null)
    {
        if (IsEnvironment(environmentName))
        {
            Assert.Skip(reason ?? $"Test skipped in {environmentName} environment");
        }
    }

    public static void SkipUnlessEnvironment(string environmentName, string? reason = null)
    {
        if (!IsEnvironment(environmentName))
        {
            var current = GetCurrentEnvironment();
            Assert.Skip(reason ?? $"Test only runs in {environmentName} environment (current: {current})");
        }
    }
}
