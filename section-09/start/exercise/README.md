# Test Reporting Exercise

Practice enabling detailed test diagnostics and generating a TRX report for your test results.

**🎯 Learning Objectives**: Enable diagnostic messages in xUnit configuration and generate a TRX report file.

## Scenario: Debugging Test Issues

You're working on an online store system and some tests are failing intermittently. You need to:

1. **Enable diagnostic messages** to see what's happening during test execution
2. **Generate a TRX report** that the QA team can analyze

**The Problem:** The current test configuration doesn't show enough detail to debug issues, and there's no structured report file for analysis.

## Your Mission

Make two simple changes:

1. **Enable diagnostics** in the xUnit configuration
2. **Generate a TRX report** when running tests

> **💡 Note**: Use the existing test project in this section (`tests/ReportingIntegration.Core.Tests/`) for this exercise. It already contains sample tests you can work with.

## 💡 Hints

**For Diagnostic Messages:**
- Look for the `xunit.runner.json` configuration file in the test project
- Find the setting that controls diagnostic output
- Consider what value would enable more detailed information

**For TRX Reports:**
- Research the `dotnet run` command and its report options (for xUnit)
- TRX is a specific report format supported by the Microsoft Testing Platform
- Think about where the generated report file should be saved

## Tasks

**Task 1: Enable Diagnostic Messages**
- Locate and modify the xUnit configuration file to enable detailed diagnostic output
- Run the tests and observe the difference in console output
- You should see more detailed information about test discovery and execution

**Task 2: Generate TRX Report**
- Figure out how to run tests with TRX report generation
- Locate the generated report file
- Verify it contains structured test result data that can be analyzed

## Need help?
Go back and rewatch the previous lectures. It usually helps out. 
If you still need help after that, don't hesitate to reach out (https://guiferreira.me/about)!

## Looking for an accountability partner?
Tag me on X (@gsferreira) or LinkedIn (@gferreira), and I will be there for you.

Let's do it!
