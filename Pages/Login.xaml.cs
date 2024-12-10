using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Tests_application.Connect;

namespace Tests_application.Pages
{
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string picklogin = login.Text;
            string pickpassword = password.Text;

            DataHelper dataHelper = new DataHelper(pickpassword, picklogin);

            if (DataHelper.CurrentUser == null)
            {
                MessageBox.Show("Неправильный логин или пароль");
            }
            else if (DataHelper.CurrentUser.ID_Type == 2)
            {
                Helper.frame.Navigate(new MainMenu_Teacher());
            }
            else if (DataHelper.CurrentUser.ID_Type == 3)
            {
                Helper.frame.Navigate(new MainMenu_Student());
            }
            else if (DataHelper.CurrentUser.ID_Type == 1)
            {
                Helper.frame.Navigate(new MainMenu_Admin());
            }
            else
            {
                MessageBox.Show("Неправтльный логин или пароль");
            }
            //Helper.frame.Navigate(new MainMenu_Teacher());
        }

        private void login_GotMouseCapture(object sender, MouseEventArgs e)
        {
            login.Foreground = Brushes.Black;
            if (login.Text == "Введите логин")
            {
                login.Text = "";
            }
            
        }

        private void password_GotMouseCapture(object sender, MouseEventArgs e)
        {
            password.Foreground = Brushes.Black;
            if (password.Text == "Введите пароль")
            {
                password.Text = "";
            }

        }

        private void login_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            
            if (login.Text == "")
            {
                login.Foreground = Brushes.Gray;
                login.Text = "Введите логин";
            }
        }

        private void password_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            
            if (password.Text == "")
            {
                password.Foreground = Brushes.Gray;
                password.Text = "Введите пароль";
            }

        }

        private void login_Initialized(object sender, EventArgs e)
        {
            
            login.Foreground = Brushes.Gray;
            //login.Text = "Введите логин";
            login.Text = "student1";

        }

        private void password_Initialized(object sender, EventArgs e)
        {
            password.Foreground = Brushes.Gray;
            //password.Text = "Введите пароль";
            password.Text = "333";
        }
    }
}
