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
using System.Windows.Media.Animation;
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


        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            StartAnimation(circle1, dot1, 0);
            StartAnimation(circle2, dot2, 0.3);
            StartAnimation(circle3, dot3, 0.6);
            StartAnimation(circle4, dot4, 0.9);
            
            Input.Visibility = Visibility.Collapsed;
            LoadingScreen.Visibility = Visibility.Visible;
            
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

            var arl = new Artikel(NameTextBox.Text, DescriptionTextBox.Text, UnitComboBox.Text, stockQuantity, true);
            
            UnitComboBox.Text = "";
            StockQuantityTextBox.Text = "";
            DescriptionTextBox.Text = "";
            NameTextBox.Text = "";
            
            await Task.Delay(500);
            
            LoadingScreen.Visibility = Visibility.Collapsed;
            Input.Visibility = Visibility.Visible;
        }
        
          private void StartAnimation(Ellipse circle, Ellipse dot, double delay)
          {
            DoubleAnimation scaleAnim = new DoubleAnimation
            {
              From = 1.0,
              To = 1.5,
              Duration = TimeSpan.FromSeconds(1),
              AutoReverse = true,
              RepeatBehavior = RepeatBehavior.Forever,
              BeginTime = TimeSpan.FromSeconds(delay)
            };
        
            DoubleAnimation opacityAnim = new DoubleAnimation
            {
              From = 1.0,
              To = 0.5,
              Duration = TimeSpan.FromSeconds(1),
              AutoReverse = true,
              RepeatBehavior = RepeatBehavior.Forever,
              BeginTime = TimeSpan.FromSeconds(delay)
            };
        
            DoubleAnimation dotScaleAnim = new DoubleAnimation
            {
              From = 1.0,
              To = 0.0,
              Duration = TimeSpan.FromSeconds(1),
              AutoReverse = true,
              RepeatBehavior = RepeatBehavior.Forever,
              BeginTime = TimeSpan.FromSeconds(delay)
            };
        
            ScaleTransform circleScale = new ScaleTransform();
            circle.RenderTransform = circleScale;
            circle.RenderTransformOrigin = new Point(0.5, 0.5);
            circleScale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnim);
            circleScale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnim);
            circle.BeginAnimation(UIElement.OpacityProperty, opacityAnim);
        
            ScaleTransform dotScale = new ScaleTransform();
            dot.RenderTransform = dotScale;
            dot.RenderTransformOrigin = new Point(0.5, 0.5);
            dotScale.BeginAnimation(ScaleTransform.ScaleXProperty, dotScaleAnim);
            dotScale.BeginAnimation(ScaleTransform.ScaleYProperty, dotScaleAnim);
          }
    }
}
