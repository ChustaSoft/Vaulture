using System.Runtime.InteropServices;
using Wpf.Ui.Appearance;
using ThemeMode = ChustaSoft.Vaulture.Domain.Settings.ThemeMode;

namespace ChustaSoft.Vaulture.UI.Services;


public interface IThemeManager
{
    void Apply(ThemeMode theme);
}


public partial class ThemeManager : IThemeManager
{

    [LibraryImport("UXTheme.dll", EntryPoint = "#138", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]

    public static partial bool IsSystemUsingDarkMode();


    public void Apply(ThemeMode theme)
    {
        switch (theme)
        {
            case ThemeMode.Light:
                ApplicationThemeManager.Apply(ApplicationTheme.Light);
                break;

            case ThemeMode.Dark:
                ApplicationThemeManager.Apply(ApplicationTheme.Dark);
                break;

            case ThemeMode.System:
            default:
                var isUsingBlackTheme = IsSystemUsingDarkMode();
                Apply(isUsingBlackTheme ? ThemeMode.Dark : ThemeMode.Light);
                break;
        }
    }
}
