using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Project;

public partial class MSGBox : Window
{
    private string _primarybuttonName = string.Empty;
    public string PrimarybuttonName
    {
        get { return _primarybuttonName; }
        set
        {
            if (_primarybuttonName != value)
            {
                _primarybuttonName = value;
                OnPropertyChanged(nameof(PrimarybuttonName));
            }
        }
    }

    private string _secundaryButtonName = string.Empty;
    public string SecondarybuttonName
    {
        get { return _secundaryButtonName; }
        set
        {
            if (_secundaryButtonName != value)
            {
                _secundaryButtonName = value;
                OnPropertyChanged(nameof(SecondarybuttonName));
            }
        }
    }


    public MSGBox()
    {
        DataContext = this;
        InitializeComponent();
    }

    public MSGBox(string PrimaryButtonName, string SecondaryButtonName) : this()
    {
        this.PrimarybuttonName = PrimaryButtonName;
        this.SecondarybuttonName = SecondaryButtonName;
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