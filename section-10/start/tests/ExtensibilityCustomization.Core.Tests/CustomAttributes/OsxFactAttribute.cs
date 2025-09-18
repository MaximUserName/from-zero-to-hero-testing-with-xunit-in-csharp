using System.Runtime.InteropServices;

namespace ExtensibilityCustomization.Core.Tests.CustomAttributes;

[AttributeUsage(AttributeTargets.Method)]
public class OsxFactAttribute : FactAttribute
{
    public OsxFactAttribute()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            this.Skip = "Test only runs on MacOS";
    }
}

[AttributeUsage(AttributeTargets.Method)]
public class WindowsFactAttribute : FactAttribute
{
    public WindowsFactAttribute()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            this.Skip = "Test only runs on Windows";
    }
}
