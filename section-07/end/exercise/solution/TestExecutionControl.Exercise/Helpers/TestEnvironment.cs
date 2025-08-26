namespace TestExecutionControl.Exercise.Helpers;

public static class TestEnvironment
{
    public static bool IsCI => 
        !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI"));
    
    public static bool HasExternalAccess =>
        !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("EXTERNAL_API_KEY"));
}
