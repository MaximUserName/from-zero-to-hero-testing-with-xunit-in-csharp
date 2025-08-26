# From Zero to Hero: Testing with xUnit in C# 

Welcome to the ["From Zero to Hero: Testing with xUnit in C#"](https://dometrain.com/course/from-zero-to-hero-testing-with-xunit-in-csharp/?ref=dometrain-github) course on Dometrain! 

This comprehensive course teaches you everything you need to know about testing in .NET using xUnit.net. You'll learn testing fundamentals, advanced assertion techniques, data-driven testing, test lifecycle management, and how to extend it.

Whether you're new to testing or looking to master xUnit.net, this course provides practical, hands-on experience and exercises.

This repository contains the source code for the course, which you can use to follow along.

## Getting Started

The **main branch** contains the most up-to-date version of the code, reflecting the latest improvements, updates, and fixes. 

Each section in the course has folder in the repository. The folder contains the source code for the section, containing both a `/start` and `/end` directory—which aligns with the source code at a point in time as it relates to the course.

## Course Structure

- [Section 2: Getting Started with xUnit.net](./section-02/)
- [Section 3: Test Fundamentals](./section-03/)
- [Section 4: Assertions](./section-04/)
- [Section 5: Data-Driven Testing with Theories](./section-05/)
- [Section 6: Test Lifecycle and Fixtures](./section-06/)
- [Section 7: Test Execution and Control](./section-07/)
- [Section 8: Output and Diagnostics](./section-08/)
- [Section 9: Reporting and Integration](./section-09/)
- [Section 10: Extensibility and Customisation](./section-10/)

## Setup

### .NET
Install .NET 9
https://dotnet.microsoft.com/en-us/download/dotnet/9.0

### xUnit v3 Templates
```shell
dotnet new install xunit.v3.templates
```

## Testing Platform

### xUnit
https://xunit.net/docs/getting-started/v3/microsoft-testing-platform

### How to enable on your IDE

#### Rider
1. Open **Settings** → **Build, Execution, Deployment** → **Unit Testing** → **VSTest**
2. Ensure **Enable Microsoft Testing Platform support** is checked
3. Apply settings and restart IDE if prompted


#### Visual Studio
1. Go to **Tools** → **Manage Preview Features**
2. Enable **Use testing platform server mode**
3. Restart Visual Studio to apply changes

#### VS Code
1. Install the extension Name: C# Dev Kit
2. Go to the C# Dev Kit extension's settings
3. Enable Dotnet > Test Window > Use Testing Platform Protocol

### More about Microsoft Testing Platform:
- [Microsoft Testing Platform Documentation](https://learn.microsoft.com/en-us/dotnet/core/testing/microsoft-testing-platform-intro)
- [Microsoft Testing Platform vs VSTest](https://learn.microsoft.com/en-us/dotnet/core/testing/microsoft-testing-platform-vs-vstest)
- [Microsoft Testing Platform - Why we moved from VSTest](https://youtu.be/nvwyaNucle0?si=dE4G9HkYXGWegFK2)

## Terminal Usage

### Running Specific Tests

**Microsoft Testing Platform:**
```bash
dotnet run -p tests/OutputDiagnostics.Core.Tests --output detailed --filter-query "/OutputDiagnostics.Core.Tests/OutputDiagnostics.Core.Tests.DiagnosticMessages/OrderProcessorTests/ProcessOrder_WithInvalidCustomer_ProvidesContextualFailureInfo"
```

**VSTest:**
```bash
dotnet test --logger "console;verbosity=detailed" --filter "FullyQualifiedName=OutputDiagnostics.Core.Tests.DiagnosticMessages.OrderProcessorTests.ProcessOrder_WithInvalidCustomer_ProvidesContextualFailureInfo"
```

### Running All Tests with Detailed Output

**Microsoft Testing Platform:**
```bash
dotnet run -p tests/OutputDiagnostics.Core.Tests --output detailed 
```

**VSTest:**
```bash
dotnet test --logger "console;verbosity=detailed" 
```

### Common Test Filters

```bash
# Run tests by category
dotnet test --filter "Category=Unit"

# Run tests by trait
dotnet test --filter "Priority=High"

# Run tests by name pattern
dotnet test --filter "Name~Calculator"
```

## Resources

### Test Naming Conventions
- [Effective Test Naming Video](https://www.youtube.com/watch?v=E9_7FaVy0YU)

### Documentation
- [xUnit.net Documentation](https://xunit.net/)
- [xUnit.net v3](https://xunit.net/docs/getting-started/v3/getting-started)
- [.NET Testing Best Practices](https://learn.microsoft.com/en-us/dotnet/core/testing/)