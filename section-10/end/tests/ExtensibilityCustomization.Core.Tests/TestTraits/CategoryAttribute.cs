using Xunit.v3;

namespace ExtensibilityCustomization.Core.Tests.TestTraits;

public enum Priority
{
    High,
    Medium,
    Low,
    Critical
}

public class PriorityAttribute: Attribute, ITraitAttribute
{
    private readonly Priority _priority;

    public PriorityAttribute(Priority priority)
    {
        _priority = priority;
    }
    
    public IReadOnlyCollection<KeyValuePair<string, string>> GetTraits()
    {
        return new List<KeyValuePair<string, string>>()
        {
            new("Priority", _priority.ToString()),
        };
    }
}

public class TaxesAttribute : Attribute, ITraitAttribute
{
    public IReadOnlyCollection<KeyValuePair<string, string>> GetTraits()
    {
        return new List<KeyValuePair<string, string>>()
        {
            new("Domain", "Taxes"),
        };
    }
}