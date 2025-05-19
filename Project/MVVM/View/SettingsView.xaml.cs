using Microsoft.Win32;
using Project.Settings;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic;
using System.IO;
using CsvHelper;
using Microsoft.Windows.Themes;
using Project.objects;
using Project.services;

namespace Project.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für SettingsView.xaml
    /// </summary>
    public partial class SettingsView : System.Windows.Controls.UserControl
    {
        Configuration appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        private readonly UISettings UIAppearance;

        public SettingsView()
        {
            InitializeComponent();

            if (appConfig.Sections["UIAppearance"] is null)
            {
                appConfig.Sections.Add("UIAppearance", new UISettings());
            }

            UIAppearance = (UISettings)appConfig.GetSection("UIAppearance");

            this.DataContext = UIAppearance;

            switch (UIAppearance.SelectedTheme)
            {
                case ThemeOption.Light:
                    LightRadioBTNIcon.IsChecked = true;
                    break;
                case ThemeOption.Normal:
                    NormalRadioBTNIcon.IsChecked = true;
                    break;
                case ThemeOption.Dark:
                    DarkRadioBTNIcon.IsChecked = true;
                    break;
            }

            switch (UIAppearance.DarkTheme)
            {
                case ColorThemeOption.System:
                    SystemThemeBTN.IsChecked = true;
                    break; 
                case ColorThemeOption.Dark:
                    DarkThemeRadioBTN.IsChecked = true;
                    break;
                case ColorThemeOption.Light:
                    LightThemeRadioBTN.IsChecked = true;
                    break;
            }

            this.Unloaded += SettingsView_Closed;
        }

        private void SettingsView_Closed(object sender, EventArgs e)
        {
            UIAppearance.SectionInformation.ForceSave = true;
            appConfig.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("UIAppearance");
        }


        private void Import(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv";
            openFileDialog.Title = "Select a CSV File";

            string path = "";

            if (openFileDialog.ShowDialog() == true)
            {
                path = openFileDialog.FileName;

                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<dynamic>();
                    foreach (var record in records)
                    {
                        var recordDict = (IDictionary<string, object>)record;

                        string name = recordDict["name"]?.ToString();
                        string description = recordDict["description"]?.ToString();
                        string unit = recordDict["unit"]?.ToString();
                        int StockQuantity = Convert.ToInt32(recordDict["stockquantity"]);
                        string CreationDate = recordDict["creationdate"]?.ToString();

                        Artikel artikel = new Artikel(name, description, unit, StockQuantity, CreationDate);
                    }
                }
            }
        }

        private async void Export(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Save Your File",
                Filter = "CSV Files (*.csv)|*.csv", 
                FileName = "Export",
                DefaultExt = "csv", 
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };
            saveFileDialog.ShowDialog();

            string filepath = saveFileDialog.FileName;
            string filename = saveFileDialog.SafeFileName;

            if (DB.Instance.TableExists("Artikel"))
            {
                string[,] entries = DB.Instance.Get("SELECT * FROM Artikel");

                int rows = entries.GetLength(0);
                int cols = entries.GetLength(1);

                string val = "id,name,description,unit,StockQuantity,timestamp\n";

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (j == cols - 1)
                            val += entries[i, j];
                        else
                            val += entries[i, j] + ",";
                    }

                    val += "\n";
                }

                File.WriteAllText(filepath, val);
            }
        }
    }
}