# Exercise: Test Suite Assessment

Let's put your new skills to work by evaluating and improving an existing test suite.

## Before: Poorly Implemented Tests

```csharp
public class Tests
{
    [Fact]
    public void Test1()
    {
        var calc = new Calculator();
        Assert.Equal(4, calc.Add(2, 2));
        Assert.Equal(0, calc.Add(2, -2));
        Assert.Equal(-4, calc.Add(-2, -2));
    }

    [Fact]
    public void Test2()
    {
        var calc = new Calculator();
        Assert.Throws<Exception>(() => calc.Divide(10, 0));
    }

    [Theory]
    [InlineData(5, 3, 2)]
    public void Test3(int a, int b, int expected)
    {
        var calc = new Calculator();
        var result = calc.Subtract(a, b);
        Assert.Equal(expected, result);
    }
}
```

## Assessment Checklist: Rate Your Test Suite

Use this checklist to evaluate any test suite against the fundamentals we've learned:

### ✅ Test Type Selection (Facts vs Theories)
- [ ] Facts used for specific business scenarios
- [ ] Theories used for same logic with different inputs  
- [ ] No over-parameterized simple scenarios
- [ ] No repetitive Facts that should be Theories

### ✅ Test Structure and Organization
- [ ] All tests follow AAA pattern consistently
- [ ] Test classes have focused responsibility
- [ ] Project structure mirrors production code
- [ ] Appropriate use of helper methods and shared setup

### ✅ Test Naming and Documentation  
- [ ] Test names clearly communicate intent and expected outcome
- [ ] Consistent naming pattern used throughout
- [ ] Business language used where appropriate
- [ ] Minimal need for additional comments (self-documenting)

### ✅ Overall Quality
- [ ] Tests are easy to read and understand
- [ ] Test failures provide actionable information
- [ ] New team members can easily contribute
- [ ] Test suite scales well with codebase growth


## Need help?
Go back and rewatch the previous lectures. It usually helps out. 
If you still need help after that, don't hesitate to reach out (https://guiferreira.me/about)!

## Looking for an accountability partner?
Tag me on X (@gsferreira) or LinkedIn (@gferreira), and I will be there for you.

Let's do it!