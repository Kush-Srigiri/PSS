using System;
using System.Windows.Media;

namespace Project
{
    public enum Theme
    {
        Normal,
        Dark
    }

    public static class ThemeManager
    {
        public static event Action ThemeChanged;

        private static Theme _currentTheme = Theme.Normal;
        public static Theme CurrentTheme
        {
            get => _currentTheme;
            set
            {
                if (_currentTheme != value)
                {
                    _currentTheme = value;
                    ThemeChanged?.Invoke();
                }
            }
        }

        public static SolidColorBrush GetBackgroundBrush()
        {
            return CurrentTheme == Theme.Dark
                ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1E1E1E"))
                : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FEFBF3"));
        }

        public static SolidColorBrush GetForegroundBrush()
        {
            return CurrentTheme == Theme.Dark
                ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F2F2F2"))
                : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#526D96"));
        }

        public static SolidColorBrush GetAccentBrush()
        {
            return CurrentTheme == Theme.Dark
                ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6C63FF"))
                : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#526D96"));
        }

        public static SolidColorBrush GetStandardBorderBrush()
        {
            return CurrentTheme == Theme.Dark
                ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6C63FF"))
                : new SolidColorBrush(Colors.Black);
        }

        public static SolidColorBrush GetButtonBrush()
        {
            return CurrentTheme == Theme.Dark
                ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6C63FF"))
                : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#526D96"));
        }

        public static SolidColorBrush GetButtonTextBrush()
        {
            return CurrentTheme == Theme.Dark
                ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1E1E1E"))
                : new SolidColorBrush(Colors.White);
        }

        public static SolidColorBrush ForegroundBrush => GetForegroundBrush();

        public static void ApplyTheme()
        {
            ThemeChanged?.Invoke();
        }
    }
}
