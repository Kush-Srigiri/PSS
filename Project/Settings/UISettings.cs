using System;
using System.Configuration;
using Microsoft.Windows.Themes;

namespace Project.Settings
{
    public enum ThemeOption
    {
        Light,
        Normal,
        Dark
    }

    public enum ColorThemeOption
    {
        Light,
        Dark,
        System
    }

    class UISettings : ConfigurationSection
    {
        [ConfigurationProperty("darkTheme", DefaultValue = (int)ColorThemeOption.System)]
        public int DarkThemeOption
        {
            get => (int)this["darkTheme"];
            set => this["darkTheme"] = value;
        }
        public ColorThemeOption DarkTheme
        {
            get => (ColorThemeOption)DarkThemeOption;
            set => DarkThemeOption = (int)value;
        }

        [ConfigurationProperty("iconTheme", DefaultValue = (int)ThemeOption.Normal)]
        public int SelectedOption
        {
            get => (int)this["iconTheme"];
            set => this["iconTheme"] = value;
        }

        public ThemeOption SelectedTheme
        {
            get => (ThemeOption)SelectedOption;
            set => SelectedOption = (int)value;
        }
    }
}
