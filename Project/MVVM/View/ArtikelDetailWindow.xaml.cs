using System.Windows;

namespace Project.MVVM.View
{
    public partial class ArtikelDetailWindow : Window
    {
        public ArtikelDetailWindow()
        {
            InitializeComponent();
        }

        public void SetData(string name, string description, string unit, string quantity)
        {
            NameText.Text = $"Name: {name}";
            DescriptionText.Text = $"Description: {description}";
            UnitText.Text = $"Unit: {unit}";
            QuantityText.Text = $"Stock Quantity: {quantity}";
        }
    }
}