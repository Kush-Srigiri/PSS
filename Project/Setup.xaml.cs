using Project.objects;
using Project.services;
using System.Diagnostics;
using System.DirectoryServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Project;

public partial class Setup : Window
{
  private DB db;
  public Setup(DB db)
  {
    this.db = db;
    InitializeComponent();
  }

  private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
  {
    this.PasswrodsError.Visibility = Visibility.Hidden;



    if (this.PasswordTbx.Password != this.ConfirmPasword.Password)
    {
      this.PasswrodsError.Visibility = Visibility.Visible;
      return;
    }

    this.SetupScreen.Visibility = Visibility.Collapsed;
    this.LoadingScreen.Visibility = Visibility.Visible;

    CreateAccount();
  }

  public bool setUp()
  {
    this.ShowDialog();

    if (this.DialogResult == true) { return true; }
    return false;
  }

  private void PasswordTbx_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.Key == Key.Enter)
    {
      CreateAccount();
    }
  }

  private void Border_MouseEnter(object sender, MouseEventArgs e)
  {
    this.IconX.Visibility = Visibility.Visible;
  }

  private void Border_MouseLeave(object sender, MouseEventArgs e)
  {
    this.IconX.Visibility = Visibility.Hidden;
  }

  private async void CreateAccount()
  {

    StartAnimation(circle1, dot1, 0);
    StartAnimation(circle2, dot2, 0.3);
    StartAnimation(circle3, dot3, 0.6);
    StartAnimation(circle4, dot4, 0.9);

    SetupScreen.Visibility = Visibility.Collapsed;
    LoadingScreen.Visibility = Visibility.Visible;

    await Task.Delay(2000);

    db.Execute(@"
            CREATE TABLE IF NOT EXISTS User (
                id INTEGER PRIMARY KEY CHECK (id = 1), 
                email TEXT NOT NULL,
                password TEXT NOT NULL
            );
        ");

    db.Execute("INSERT INTO User(id, email, password) VALUES ('1', ?, ?)", this.EmailTbx.Text, UserDataLogin.HashPassword(this.PasswordTbx.Password));

    AppDataSave_service appData = new();

    appData.SaveUserLoginData(new UserDataLogin(this.EmailTbx.Text, this.PasswordTbx.Password, true));

    this.DialogResult = true;
    this.Close();
  }

  private void Close_Click(object sender, MouseButtonEventArgs e)
  {
    Application.Current.Shutdown();
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

