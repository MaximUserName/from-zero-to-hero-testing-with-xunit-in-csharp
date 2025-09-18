using System.Globalization;
using System.Reflection;
using Xunit.v3;

namespace ExtensibilityCustomization.Core.Tests.CustomAttributes;

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
        // store previous culture
        _originalCulture = Thread.CurrentThread.CurrentCulture;
        _originalUiCulture = Thread.CurrentThread.CurrentUICulture;

        // set current culture
        Thread.CurrentThread.CurrentCulture = new CultureInfo(_cultureName);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(_cultureName);

        base.Before(methodUnderTest, test);
    }

    public override void After(MethodInfo methodUnderTest, IXunitTest test)
    {
        // restore previous culture
        Thread.CurrentThread.CurrentCulture = _originalCulture;
        Thread.CurrentThread.CurrentUICulture = _originalUiCulture;
    }
}
