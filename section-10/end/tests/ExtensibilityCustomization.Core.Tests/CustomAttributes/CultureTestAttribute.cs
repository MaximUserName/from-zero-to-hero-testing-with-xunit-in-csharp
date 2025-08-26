using System.Globalization;
using System.Reflection;
using Xunit.v3;

namespace ExtensibilityCustomization.Core.Tests.CustomAttributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
public class UseCultureAttribute : BeforeAfterTestAttribute
{
    private readonly string _cultureName;
    private CultureInfo _originalCulture;
    private CultureInfo _originalUiCulture;

    public UseCultureAttribute(string cultureName)
    {
        _cultureName = cultureName;
    }

    public override void Before(MethodInfo methodUnderTest, IXunitTest test)
    {
        _originalCulture = Thread.CurrentThread.CurrentCulture;
        _originalUiCulture = Thread.CurrentThread.CurrentUICulture;

        Thread.CurrentThread.CurrentCulture = new CultureInfo(_cultureName);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(_cultureName);
    }

    public override void After(MethodInfo methodUnderTest, IXunitTest test)
    {
        Thread.CurrentThread.CurrentCulture = _originalCulture;
        Thread.CurrentThread.CurrentUICulture = _originalUiCulture;
    }
}