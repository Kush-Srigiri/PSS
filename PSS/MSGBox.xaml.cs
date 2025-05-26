using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace PSS;

public partial class MSGBox : Window
{
    public MSGBox(string msg)
    {
        InitializeComponent();
        this.Msg.Text = msg;
    }

    public MSGBox(string msg ,string PrimaryButtonName, string SecondaryButtonName) : this(msg)
    {
        this.PrimBtn.Content = PrimaryButtonName;
        this.SecoBtn.Content = SecondaryButtonName;
        this.PrimBtn.Click += new RoutedEventHandler(PrimBtn_Click);
        this.SecoBtn.Click += new RoutedEventHandler(SecoBtn_Click);
    }

    public virtual void PrimBtn_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
        this.Close();
    }

    public virtual void SecoBtn_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
        this.Close();
    }
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void Border_MouseEnter(object sender, MouseEventArgs e)
    {
        this.IconX.Visibility = Visibility.Visible;
    }

    private void Border_MouseLeave(object sender, MouseEventArgs e)
    {
        this.IconX.Visibility = Visibility.Hidden;
    }

    private void Close_Click(object sender, MouseButtonEventArgs e)
    {
        this.Close();
    }

    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            this.DragMove();
        }
    }
}