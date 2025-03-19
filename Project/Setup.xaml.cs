using Project.objects;
using Project.services;
using System.Diagnostics;
using System.DirectoryServices;
using System.Windows;
using System.Windows.Input;

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
        SetupScreen.Visibility = Visibility.Collapsed;
        LoadingScreen.Visibility = Visibility.Visible;
        
        await Task.Delay(500);
        
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
}