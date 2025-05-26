using System.Configuration;
using System.Data;
using System.Windows;
using PSS.Settings;

namespace PSS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Configuration appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        private UISettings UIAppearance;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (appConfig.Sections["UIAppearance"] is null)
            {
                appConfig.Sections.Add("UIAppearance", new UISettings());
            }

            UIAppearance = (UISettings)appConfig.GetSection("UIAppearance");


            ThemeManager.ApplyTheme(UIAppearance.DarkTheme);
        }
    }
}


// Example of using the connection string from App.config