using ExtensibilityCustomization.Core.CustomAttributes;
using System.Globalization;

namespace ExtensibilityCustomization.Core.Tests.CustomAttributes;

public class CustomAttributeTests
{
    [Fact]
    public void MacOsSpecific_OnlyRunsOnMacOS()
    {
        // This test only runs on macOS
        var service = new UserService();
        var user = service.GetUserById(1);
        
        // On macOS, we can test OS-specific functionality
        Assert.NotNull(user);
        Console.WriteLine("This test is running on macOS!");
    }

    [Fact]
    public void FormatCurrency_WithGermanCulture_ReturnsEuroFormat()
    {
        decimal amount = 1234.56m;
        string formatted = amount.ToString("C");
        
        // Should format as German currency
        Assert.Contains("€", formatted);
        
        // Verify culture is actually set
        Assert.Equal("de-DE", CultureInfo.CurrentCulture.Name);
    }

    [Fact]
    public void FormatDate_WithPortugueseCulture_ReturnsCorrectFormat()
    {
        var date = new DateTime(2023, 12, 25);
        string formatted = date.ToString("D");
        
        // Should be in Portuguese
        Assert.Contains("dezembro", formatted.ToLower());
        
        // Verify culture is actually set
        Assert.Equal("pt-PT", CultureInfo.CurrentCulture.Name);
    }
}
