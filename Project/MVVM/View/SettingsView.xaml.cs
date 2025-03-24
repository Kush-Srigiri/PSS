using Microsoft.Win32;
using Project.Settings;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic;
using System.IO;
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

            string path;

            if (openFileDialog.ShowDialog() == true)
            {
                path = openFileDialog.FileName;
            }
        }
        
        private async void Export(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Save Your File",
                Filter = "CSV Files (*.csv)|*.csv|JSON Files (*.json)|*.json",
                FileName = "Export",
                DefaultExt = ".csv",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                if (Path.GetExtension(filePath).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    string filename = saveFileDialog.SafeFileName;
                    string filepath = saveFileDialog.FileName;

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
                else
                {
                    string[,] entries = DB.Instance.Get("SELECT * FROM Artikel");

                    int rows = entries.GetLength(0);
                    int cols = entries.GetLength(1);

                    var dataList = new System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>();

                    for (int i = 0; i < rows; i++)
                    {
                        var rowDict = new System.Collections.Generic.Dictionary<string, string>();
                        rowDict["id"] = entries[i, 0];
                        rowDict["name"] = entries[i, 1];
                        rowDict["description"] = entries[i, 2];
                        rowDict["unit"] = entries[i, 3];
                        rowDict["StockQuantity"] = entries[i, 4];
                        rowDict["timestamp"] = entries[i, 5];

                        dataList.Add(rowDict);
                    }

                    string json = System.Text.Json.JsonSerializer.Serialize(dataList, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });

                    File.WriteAllText(filePath, json);
                }
            }
        }
    }
}