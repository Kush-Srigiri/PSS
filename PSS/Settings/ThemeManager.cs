using System.Windows;
using Microsoft.Win32;

namespace PSS.Settings;

public class ThemeManager
{
    private static ResourceDictionary _currentTheme;

    public static void ApplyTheme(ColorThemeOption themeOption)
    {

        string themeFile = themeOption switch
        {
            ColorThemeOption.System => IsSystemInDarkMode() ? "pack://application:,,,/assets/Colors/Darkmode.xaml" : "pack://application:,,,/assets/Colors/Lightmode.xaml",
            ColorThemeOption.Dark => "pack://application:,,,/assets/Colors/Darkmode.xaml",
            _ => "pack://application:,,,/assets/Colors/Lightmode.xaml"
        };

        var newTheme = new ResourceDictionary
        {
            Source = new Uri(themeFile, UriKind.Absolute)
        };

        Application.Current.Dispatcher.Invoke(() =>
        {
            if (_currentTheme != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(_currentTheme);
            }

            Application.Current.Resources.MergedDictionaries.Add(newTheme);
            _currentTheme = newTheme;
        });
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