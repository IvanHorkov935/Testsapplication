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
using System.Windows.Shapes;
using System.Xml.Linq;
using Tests_application.Connect;

namespace Tests_application.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Page
    {
        public int NewUserType = 0;
        public AddUser()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if(NewUserType != 0 && login.Text != null && password.Text != null && fullname.Text != null)
            {
                Helper.connect.Users.Add(new Users() { ID_Type = NewUserType, Login = login.Text, Password = password.Text, Full_Name = fullname.Text});
                Helper.connect.SaveChanges();
                Helper.frame.Navigate(new MainMenu_Admin());
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedValue = selectedItem.Content.ToString();

                switch (selectedValue)
                {
                    case "Админ":
                        NewUserType = 1;
                        break;
                    case "Учитель":
                        NewUserType = 2;
                        break;
                    case "Ученик":
                        NewUserType = 3;
                        break;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Helper.frame.Navigate(new MainMenu_Admin());
        }
    }
}
