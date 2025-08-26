# Fixture Management Exercise

Practice using different xUnit fixture patterns to optimize test performance when dealing with expensive setup operations.

**🎯 Learning Objectives**: Master constructor injection, class fixtures, and collection fixtures for efficient test resource management.

## Scenario: Employee CSV Processing System

You're testing a system that processes employee CSV files. Here's an example of the CSV data:

```csv
EmployeeId,Name,Department,Salary,StartDate
001,John Smith,Engineering,75000,2023-01-15
002,Jane Doe,Marketing,65000,2023-02-01
003,Bob Johnson,Engineering,80000,2022-12-10
```

The system has two main components with different setup requirements:

- **CSV Parser** - Simple validation and parsing (no expensive setup needed)
- **Employee Repository** - Database operations for storing/retrieving employees (requires database setup - takes 3 seconds)

**The Problem:** Without proper fixture management, each test class recreates the expensive database setup, leading to slow test execution.

## Your Mission

Create these two test classes using appropriate fixture patterns:

1. **`CsvParserTests`** - Test CSV parsing and validation (no fixtures needed)
2. **`EmployeeRepositoryTests`** - Test database operations (use class fixture for database setup)


## 💡 Hints

**Constructor Injection (CsvParserTests):**
- Use for fast, simple object creation
- No special attributes needed
- Object created fresh for each test

**Class Fixtures (EmployeeRepositoryTests):**
- Use `IClassFixture<DatabaseFixture>` interface on your test class
- Fixture created once per test class
- Inject fixture via constructor parameter
- Implement `IAsyncLifetime` on the fixture for async setup/cleanup
- Use `Task.Delay(3000)` to simulate expensive database setup

**Test Data:**
Use the CSV example from the scenario:
- Employee ID: "001"
- Name: "John Smith"  
- Department: "Engineering"
- Salary: 75000
- Start Date: "2023-01-15"

## Need help?
Go back and rewatch the previous lectures. It usually helps out. 
If you still need help after that, don't hesitate to reach out (https://guiferreira.me/about)!

## Looking for an accountability partner?
Tag me on X (@gsferreira) or LinkedIn (@gferreira), and I will be there for you.

Let's do it!
