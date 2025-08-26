using ExtensibilityCustomization.Core.ReusableLibraries;

namespace ExtensibilityCustomization.Core.Tests.ReusableLibraries;

public class EnvironmentSkipTests
{
    [SkipOnEnvironment("Production", "This test should not run in production")]
    public void DangerousOperation_SkippedInProduction()
    {
        // This test will be skipped if ENVIRONMENT=Production
        Console.WriteLine("Running dangerous operation that should not run in production");
        Assert.True(true);
    }

    [RunOnlyInEnvironment("Development")]
    public void DebugOperation_OnlyInDevelopment()
    {
        // This test only runs in Development environment
        Console.WriteLine("Running debug operation only available in development");
        Assert.True(true);
    }

    [RunOnlyInEnvironment("Testing", "Integration tests only run in testing environment")]
    public void IntegrationTest_OnlyInTesting()
    {
        // This test only runs in Testing environment
        Console.WriteLine("Running integration test");
        Assert.True(true);
    }

    [SkipWhenEnvironmentVariableExists("CI", "Skipped in CI environment")]
    public void LocalOnlyTest_SkippedInCI()
    {
        // This test is skipped when CI environment variable exists
        Console.WriteLine("Running test that should only run locally");
        Assert.True(true);
    }

    [SkipWhenEnvironmentVariableEquals("BUILD_TYPE", "Release", "Skipped in release builds")]
    public void DebugOnlyTest_SkippedInRelease()
    {
        // This test is skipped when BUILD_TYPE=Release
        Console.WriteLine("Running debug-only test");
        Assert.True(true);
    }

    [Fact]
    public void EnvironmentTestHelper_GetCurrentEnvironment_ReturnsExpectedValue()
    {
        var environment = EnvironmentTestHelper.GetCurrentEnvironment();
        
        // Should default to "Development" if no environment variables are set
        Assert.NotNull(environment);
        Assert.NotEmpty(environment);
        
        Console.WriteLine($"Current environment: {environment}");
    }

    [Fact]
    public void EnvironmentTestHelper_IsEnvironment_WorksCorrectly()
    {
        var currentEnv = EnvironmentTestHelper.GetCurrentEnvironment();
        
        // Should return true for current environment
        Assert.True(EnvironmentTestHelper.IsEnvironment(currentEnv));
        
        // Should return false for different environment
        Assert.False(EnvironmentTestHelper.IsEnvironment("NonExistentEnvironment"));
    }

    [Fact]
    public void EnvironmentTestHelper_SkipIfEnvironment_WorksWithCurrentEnvironment()
    {
        var currentEnv = EnvironmentTestHelper.GetCurrentEnvironment();
        
        // This should skip the test if we try to skip current environment
        // We can't easily test this since it would skip the test itself
        // Instead, test with a different environment
        EnvironmentTestHelper.SkipIfEnvironment("NonExistentEnvironment", "Should not skip");
        
        // If we get here, the method worked correctly (didn't skip)
        Assert.True(true);
    }

    [Fact]
    public void EnvironmentTestHelper_SkipUnlessEnvironment_WorksWithCurrentEnvironment()
    {
        var currentEnv = EnvironmentTestHelper.GetCurrentEnvironment();
        
        // This should NOT skip since we're testing with current environment
        EnvironmentTestHelper.SkipUnlessEnvironment(currentEnv, "Should not skip");
        
        // If we get here, the method worked correctly (didn't skip)
        Assert.True(true);
    }

    // Example of using helper methods directly in tests
    [Fact]
    public void ManualEnvironmentCheck_UsingHelper()
    {
        // Skip manually based on environment
        EnvironmentTestHelper.SkipIfEnvironment("Production", "Manual skip for production");
        
        // This code only runs if not in Production
        Console.WriteLine("Test running - not in Production environment");
        Assert.True(true);
    }

    // Example combining multiple environment checks
    [Fact]
    public void ComplexEnvironmentLogic_UsingHelper()
    {
        var currentEnv = EnvironmentTestHelper.GetCurrentEnvironment();
        
        if (EnvironmentTestHelper.IsEnvironment("Production"))
        {
            Assert.Skip("Skipping complex test in Production");
        }
        
        if (EnvironmentTestHelper.IsEnvironment("Staging"))
        {
            // Different behavior in staging
            Console.WriteLine("Running with staging-specific logic");
        }
        else
        {
            // Default behavior for other environments
            Console.WriteLine($"Running with default logic in {currentEnv}");
        }
        
        Assert.True(true);
    }
}
