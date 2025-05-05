using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Project.objects;

namespace Project.MVVM.View
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void Article_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock tb && tb.DataContext is Artikel artikel)
            {
                var detailWindow = new ArtikelDetailWindow();
                detailWindow.SetData(artikel.name, artikel.description, artikel.unit, artikel.StockQuantity.ToString());
                detailWindow.ShowDialog();
            }
        }
    }
}