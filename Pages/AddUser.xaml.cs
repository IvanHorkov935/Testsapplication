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

namespace Tests_application.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();

            Users user = NewUser;
        }

        public Users newUser = new Users();
        public Users NewUser
        {
            get
            {
                return newUser;
            }
            set
            {
                newUser.ID_Type = value.ID_Type;
            }
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if(newUser.ID_Type != 0 && t1.Text != null && t2.Text != null && t3.Text != null)
            {
                newUser.Login = t1.Text;
                newUser.Password = t2.Text;
                newUser.ID_Group = 7;
                newUser.Full_Name = t3.Text;
                
                DialogResult = true;
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
                        newUser = new Users() { ID_Type = 1};
                        break;
                    case "Учитель":
                        newUser = new Users() { ID_Type = 2 };
                        break;
                    case "Ученик":
                        newUser = new Users() { ID_Type = 3 };
                        break;
                }
            }
        }
    }
}
