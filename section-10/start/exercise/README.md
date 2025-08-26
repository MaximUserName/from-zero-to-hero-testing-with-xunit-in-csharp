# E2E Test Library Exercise

Create a custom xUnit library that simplifies marking tests as "End-to-End" and automatically skips them in local development environments.

**🎯 Learning Objectives**: Build a practical test library with custom attributes that detect execution environment and manage E2E test execution.

## Scenario: End-to-End Test Management

You're working on a project where End-to-End tests should behave differently based on environment:

- **E2E tests are slow and resource-intensive** - shouldn't run during local development
- **CI/CD pipelines need full E2E coverage** - all E2E tests should run in build environments  
- **Different E2E test categories** - API tests, UI tests, integration flows
- **Clear test organization** - easy to identify and filter E2E tests

**The Problem:** Developers accidentally run expensive E2E tests locally, slowing down their workflow. You need a clean way to manage when E2E tests execute.

## Your Mission

Create an E2E test library that:

1. **Detects execution environment** (local dev vs CI/CD)
2. **Automatically skips E2E tests locally** unless explicitly enabled
3. **Provides different E2E test types** (API, UI, Integration)
4. **Adds meaningful traits** for test organization and filtering
5. **Allows override mechanisms** for local E2E testing when needed

## 💡 Hints

**For Environment Detection:**
- Check for CI/CD environment variables like `CI`, `GITHUB_ACTIONS`, `BUILD_ID`
- Consider a local override mechanism (environment variable like `RUN_E2E_LOCALLY=true`)
- Think about how developers might want to run E2E tests locally for debugging

**For E2E Test Categories:**
- Consider different types: API E2E, UI E2E, Integration E2E
- Think about what traits would be useful for each category
- Consider test complexity levels (Simple, Complex, Full-Flow)

**For Skip Logic:**
- Use xUnit's `Skip` property to conditionally skip tests
- Provide clear skip messages explaining why tests were skipped
- Consider making the skip behavior configurable

## Tasks

**Task 1: Create the Base E2E Attribute**
- Create an `E2ETestAttribute` that inherits from `FactAttribute`
- Implement logic to skip tests when running locally (not in CI/CD)
- Provide a clear skip message: "E2E tests are skipped in local development. Set RUN_E2E_LOCALLY=true to enable."
- Add basic traits for test organization

**Task 2: Create Specialized E2E Attributes**
- Create `ApiE2ETestAttribute` for API end-to-end tests
- Create `UiE2ETestAttribute` for UI end-to-end tests  
- Create `IntegrationE2ETestAttribute` for integration flow tests
- Each should have specific traits and potentially different skip logic

**Task 3: Add Override Mechanism and Testing**
- Implement environment variable override (`RUN_E2E_LOCALLY=true`)
- Add comprehensive traits for filtering (TestType, Environment, Complexity)
- Create sample tests using your attributes
- Test both local skipping and CI/CD execution scenarios

## Example Usage

Your final implementation should allow tests like this:

```csharp
[ApiE2ETest]
public void UserRegistration_EndToEnd_CreatesAccountAndSendsEmail()
{
    // Full API flow test - only runs in CI/CD
    var client = new ApiTestClient();
    // ... full registration flow test
}

[UiE2ETest]
public void ShoppingCart_CompleteCheckout_ProcessesOrderSuccessfully()
{
    // UI automation test - only runs in CI/CD
    var driver = new WebDriver();
    // ... full UI flow test
}

[IntegrationE2ETest]
public void PaymentProcessing_WithExternalServices_HandlesFullFlow()
{
    // Integration with external services - only runs in CI/CD
    var paymentGateway = new PaymentGatewayClient();
    // ... full integration test
}

[Theory]
[InlineData("premium")]
[InlineData("standard")]
[ApiE2ETest]
public void SubscriptionUpgrade_DifferentTiers_UpdatesCorrectly(string tier)
{
    // Parameterized E2E test
    // ... subscription flow test
}
```

## Bonus Challenge

Extend your library to support:
- **Complexity levels** with different skip behaviors
- **Custom skip reasons** for specific test requirements
- **Test duration estimation** in skip messages
- **Environment-specific configuration** via attributes

```csharp
[ApiE2ETest(Complexity.High, "Requires external payment gateway")]
public void Payment_FullFlow_WithRealGateway() { }

[UiE2ETest(skipLocally: false)] // Force run locally for debugging
public void LoginFlow_Debug_WorksCorrectly() { }

[IntegrationE2ETest(EstimatedDuration = "5 minutes")]
public void DataSync_FullPipeline_ProcessesCorrectly() { }
```

## Testing Your Library

Create tests to verify your library works correctly:

```csharp
[Fact]
public void E2ETestAttribute_InLocalEnvironment_SkipsTest()
{
    // Test that E2E tests are skipped locally
}

[Fact] 
public void E2ETestAttribute_WithOverrideEnabled_RunsTest()
{
    // Test that override mechanism works
}

[Fact]
public void E2ETestAttributes_HaveCorrectTraits()
{
    // Test that traits are properly assigned
}
```

## Need help?
Go back and rewatch the previous lectures. It usually helps out. 
If you still need help after that, don't hesitate to reach out (https://guiferreira.me/about)!

## Looking for an accountability partner?
Tag me on X (@gsferreira) or LinkedIn (@gferreira), and I will be there for you.

Let's do it!