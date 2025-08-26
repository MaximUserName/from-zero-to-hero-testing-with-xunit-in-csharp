using System.Runtime.InteropServices;

namespace ExtensibilityCustomization.Core.Tests.CustomAttributes;

[AttributeUsage(AttributeTargets.Method)]
public class OsxFactAttribute : FactAttribute
{
    public OsxFactAttribute()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Skip = "Test only runs on macOS";
        }
    }
}
[AttributeUsage(AttributeTargets.Method)]
public class OsxTheoryAttribute : TheoryAttribute
{
    public OsxTheoryAttribute()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Skip = "Test only runs on macOS";
        }
    }
}

[AttributeUsage(AttributeTargets.Method)]
public class WindowsFactAttribute : FactAttribute
{
    public WindowsFactAttribute()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Skip = "Test only runs on Windows";
        }
    }
}
