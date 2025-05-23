using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Shapes;
using PSS.objects;
using PSS.services;

namespace PSS
{
    public partial class Setup : Window
    {
        private DB db;

        public Setup(DB db)
        {
            this.db = db;
            InitializeComponent();
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

        public bool setUp()
        {
            this.ShowDialog();
            return this.DialogResult == true;
        }

        private void PasswordTbx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Border_MouseDown(sender, null);
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PasswrodsError.Visibility = Visibility.Hidden;

            if (PasswordTbx.Password != ConfirmPasword.Password)
            {
                PasswrodsError.Visibility = Visibility.Visible;
                return;
            }

            CreateAccount();
        }

        private async void CreateAccount()
        {
            await Task.Delay(500);

            db.Execute(@"CREATE TABLE IF NOT EXISTS User (
                id INTEGER PRIMARY KEY CHECK (id = 1), 
                email TEXT NOT NULL,
                password TEXT NOT NULL
            );");

            db.Execute("INSERT INTO User(id, email, password) VALUES ('1', ?, ?)", EmailTbx.Text,
                UserDataLogin.HashPassword(PasswordTbx.Password));

            AppDataSave_service appData = new();
            appData.SaveUserLoginData(new UserDataLogin(EmailTbx.Text, PasswordTbx.Password, true));

            this.DialogResult = true;
            this.Close();
        }
    }
}