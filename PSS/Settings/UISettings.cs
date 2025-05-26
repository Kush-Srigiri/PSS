using System;
using System.ComponentModel;
using System.Configuration;

namespace PSS.Settings
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

    class UISettings : ConfigurationSection, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        [ConfigurationProperty("darkTheme", DefaultValue = (int)ColorThemeOption.System)]
        public int DarkThemeOption
        {
            get => (int)this["darkTheme"];
            set
            {
                if ((int)this["darkTheme"] != value)
                {
                    this["darkTheme"] = value;
                    OnPropertyChanged(nameof(DarkTheme));
                }
            }
        }

        public ColorThemeOption DarkTheme
        {
            get => (ColorThemeOption)DarkThemeOption;
            set
            {
                if (DarkThemeOption != (int)value)
                {
                    DarkThemeOption = (int)value;
                    OnPropertyChanged(nameof(DarkTheme));
                }
            }
        }

        [ConfigurationProperty("iconTheme", DefaultValue = (int)ThemeOption.Normal)]
        public int SelectedOption
        {
            get => (int)this["iconTheme"];
            set
            {
                if ((int)this["iconTheme"] != value)
                {
                    this["iconTheme"] = value;
                    OnPropertyChanged(nameof(SelectedTheme));
                }
            }
        }

        public ThemeOption SelectedTheme
        {
            get => (ThemeOption)SelectedOption;
            set
            {
                if (SelectedOption != (int)value)
                {
                    SelectedOption = (int)value;
                    OnPropertyChanged(nameof(SelectedTheme));
                }
            }
        }
    }
}
