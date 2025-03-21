using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Project.objects;
using Project.services;

namespace Project.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für ArtikelView.xaml
    /// </summary>
    public partial class ArtikelView : UserControl
    {
        public ArtikelView()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(DescriptionTextBox.Text) ||
                string.IsNullOrWhiteSpace(UnitComboBox.Text) ||
                string.IsNullOrWhiteSpace(StockQuantityTextBox.Text))
            {
                MessageBox.Show("All fields must be filled out.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(StockQuantityTextBox.Text, out int stockQuantity))
            {
                MessageBox.Show("Stock quantity must be a valid number.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var arl = new Artikel(NameTextBox.Text, DescriptionTextBox.Text, UnitComboBox.Text, stockQuantity);
            
            DB.Instance.Execute("INSERT INTO Artikel(name, description, unit, StockQuantity) VALUES (?,?,?,?)", arl.name, arl.description, arl.unit, arl.StockQuantity.ToString());
        }
    }
}
