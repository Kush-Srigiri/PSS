using Microsoft.Win32;
using Project.Settings;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic;
using System.IO;

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

        private static readonly object _fileLock = new object();  

        private async void Export(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Save Your File",
                Filter = "CSV Files (*.csv)|*.csv",
                FileName = "Export",
                DefaultExt = ".csv",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filename = saveFileDialog.SafeFileName;
                string filepath = saveFileDialog.FileName;

                var data = new List<string>();
                for (int i = 1; i <= 3000; i++)  
                {
                    data.Add($"Data{i},Data{i + 1},Data{i + 2}");
                }

                int chunkSize = data.Count / 6;
                var chunk1 = data.Take(chunkSize).ToList();
                var chunk2 = data.Skip(chunkSize).Take(chunkSize).ToList();
                var chunk3 = data.Skip(chunkSize * 2).Take(chunkSize).ToList();
                var chunk4 = data.Skip(chunkSize * 3).Take(chunkSize).ToList();
                var chunk5 = data.Skip(chunkSize * 4).Take(chunkSize).ToList();
                var chunk6 = data.Skip(chunkSize * 5).ToList();

                var task1 = Task.Run(() => ProcessAndWriteChunk(chunk1, filepath));
                var task2 = Task.Run(() => ProcessAndWriteChunk(chunk2, filepath));
                var task3 = Task.Run(() => ProcessAndWriteChunk(chunk3, filepath));
                var task4 = Task.Run(() => ProcessAndWriteChunk(chunk4, filepath));
                var task5 = Task.Run(() => ProcessAndWriteChunk(chunk5, filepath));
                var task6 = Task.Run(() => ProcessAndWriteChunk(chunk6, filepath));

                await Task.WhenAll(task1, task2, task3, task4, task5, task6);

                MessageBox.Show($"{filename} saved successfully!", "Export Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ProcessAndWriteChunk(List<string> chunk, string filepath)
        {
            try
            {
                lock (_fileLock) 
                {
                    using (StreamWriter strW = new StreamWriter(new FileStream(filepath, FileMode.Append)))
                    {
                        foreach (var line in chunk)
                        {
                            strW.WriteLine(line);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show($"Error: {ex.Message}", "Export Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                });
            }
        }

    }
}
