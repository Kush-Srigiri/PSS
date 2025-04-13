using Project.objects;
using Project.services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Project
{
    public partial class Login : Window
    {
        bool result = false;
        AppDataSave_service appData = new();
        DB db;
        UserDataLogin udl;

        public Login(DB db)
        {
            this.db = db;
            udl = new UserDataLogin(db.Get("SELECT * FROM User")[0, 1], db.Get("SELECT * FROM User")[0, 2], false);
            InitializeComponent();
            this.ShowInTaskbar = true;
            this.EmailTbx.Focus();

            ThemeManager.ThemeChanged += ApplyTheme;
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            this.Resources["ForegroundBrush"] = ThemeManager.GetForegroundBrush();
            this.Resources["AccentBrush"] = ThemeManager.GetAccentBrush();
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            IconX.Visibility = Visibility.Visible;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            IconX.Visibility = Visibility.Hidden;
        }

        private void Close_Click(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public bool Authentificate()
        {
            UserDataLogin obj = appData.LoadUserLoginData();

            if (obj != null && obj.Email == udl.Email && obj.PasswordHash == udl.PasswordHash)
            {
                return true;
            }

            this.ShowDialog();
            return result;
        }

        private void Border_KeyDown(object sender, MouseEventArgs e)
        {
            setLoading(true);
            LoginUser();
        }

        private void setLoading(bool isLoading)
        {
            LoadingImg.Visibility = isLoading ? Visibility.Visible : Visibility.Collapsed;
            LoginBtnName.Visibility = isLoading ? Visibility.Collapsed : Visibility.Visible;
        }

        private void LoginBtnName_KeyDown(object sender, MouseEventArgs e)
        {
            Border_KeyDown(sender, e);
        }

        private async void LoginUser()
        {
            EmailErrorLbl.Visibility = Visibility.Hidden;
            PasswordErrorLbl.Visibility = Visibility.Hidden;
            setLoading(true);

            string email = EmailTbx.Text.Trim().ToLower();
            string password = PasswordTbx.Password;

            await Task.Delay(100);

            if (email == udl.Email && udl.ValidatePassword(password))
            {
                await Task.Delay(1000);

                setLoading(false);
                result = true;

                if (RememberMeCheckBox.IsChecked == true)
                {
                    appData.SaveUserLoginData(new { Email = udl.Email, Password = udl.PasswordHash });
                }

                this.Close();
                return;
            }

            await Task.Delay(500);

            EmailTbx.Text = string.Empty;
            EmailErrorLbl.Visibility = Visibility.Visible;
            PasswordTbx.Password = string.Empty;
            PasswordErrorLbl.Visibility = Visibility.Visible;

            setLoading(false);
        }

        private void PasswordTbx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Border_KeyDown(sender, null);
            }
        }
    }
}
