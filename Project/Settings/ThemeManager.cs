using System.Windows;
using Microsoft.Win32;

namespace Project.Settings;

public class ThemeManager
{
    private static ResourceDictionary _currentTheme;

    public static void ApplyTheme(ColorThemeOption themeOption)
    {
        if (_currentTheme != null)
        {
            Application.Current.Resources.MergedDictionaries.Remove(_currentTheme);
        }

        string themeFile = themeOption switch
        {
            ColorThemeOption.System => IsSystemInDarkMode() ? "assets/Colors/Darkmode.xaml" : "assets/Colors/Lightmode.xaml",
            ColorThemeOption.Dark => "assets/Colors/Darkmode.xaml",
            _ => "assets/Colors/Lightmode.xaml"
        };

        _currentTheme = new ResourceDictionary
        {
            Source = new Uri(themeFile, UriKind.Relative)
        };

        Application.Current.Resources.MergedDictionaries.Add(_currentTheme);
    }

    private static bool IsSystemInDarkMode()
    {
        try
        {
            using var key =
                Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            object value = key?.GetValue("AppsUseLightTheme");
            return value is int lightTheme && lightTheme == 0;
        }
        catch
        {
            return false;
        }
    }
}