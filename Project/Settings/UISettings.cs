using System;
using System.Configuration;

namespace Project.Settings
{
    public enum ThemeOption
    {
        Light,
        Normal,
        Dark
    }

    class UISettings : ConfigurationSection
    {
        [ConfigurationProperty("darkTheme", DefaultValue = false)]
        public bool DarkTheme
        {
            get => (bool)this["darkTheme"];
            set => this["darkTheme"] = value;
        }

        [ConfigurationProperty("iconTheme", DefaultValue = 1)]
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
