# Data-Driven Testing Exercise

## Goal
Practice the three main data-driven testing patterns by creating tests for a simple `Calculator` class.

## Your Task
Create a `Calculator` class with these methods and write comprehensive tests:

### 1. Basic Math (Use InlineData)
```csharp
public int Add(int a, int b) => a + b;
public bool IsEven(int number) => number % 2 == 0;
```

**Test with InlineData:**
```csharp
[Theory]
[InlineData(2, 3, 5)]
[InlineData(0, 5, 5)]
[InlineData(-1, 1, 0)]
public void Add_ShouldReturnCorrectSum(int a, int b, int expected)
{
    // TODO: Implement test
}
```

### 2. Division with Error Handling (Use MemberData)
```csharp
public decimal Divide(decimal a, decimal b)
{
    if (b == 0) throw new DivideByZeroException();
    return a / b;
}
```

**Test with MemberData:**
```csharp
[Theory]
[MemberData(nameof(GetDivisionTestCases))]
public void Divide_ShouldCalculateCorrectly(decimal a, decimal b, decimal expected)
{
    // TODO: Implement test
}

public static IEnumerable<object[]> GetDivisionTestCases()
{
    // TODO: Add test cases like:
    // yield return new object[] { 10m, 2m, 5m };
    // yield return new object[] { 7m, 3m, 2.33m }; // rounded
}
```

### 3. Percentage Calculation (Use TheoryData)
```csharp
public decimal CalculatePercentage(decimal value, decimal percentage)
{
    return (value * percentage) / 100;
}
```

**Test with TheoryData:**
```csharp
public static TheoryData<decimal, decimal, decimal> PercentageData =>
    new()
    {
        { 100m, 10m, 10m },    // 10% of 100 = 10
        { 50m, 20m, 10m },     // 20% of 50 = 10
        { 200m, 0m, 0m }       // 0% of 200 = 0
    };
```

## Step-by-Step Instructions

1. **Create the Calculator class** in the Core project
2. **Start with InlineData tests** - they're the simplest
3. **Move to MemberData** - practice with `yield return`
4. **Finish with TheoryData** - enjoy the type safety!
5. **Run your tests** with `dotnet test`

## Success Criteria
- ✅ All tests pass
- ✅ You've used all three data patterns
- ✅ You understand when to use each pattern

## Bonus Challenge
Add a test for division by zero using `Assert.Throws<DivideByZeroException>()`

## Need help?
Go back and rewatch the previous lectures. It usually helps out. 
If you still need help after that, don't hesitate to reach out (https://guiferreira.me/about)!

## Looking for an accountability partner?
Tag me on X (@gsferreira) or LinkedIn (@gferreira), and I will be there for you.

Let's do it!