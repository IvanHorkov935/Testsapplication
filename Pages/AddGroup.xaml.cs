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
using Tests_application.Connect;

namespace Tests_application.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddGroup.xaml
    /// </summary>
    public partial class AddGroup : Page
    {
        public AddGroup()
        {
            InitializeComponent();
        }
        public string NewGroupName
        {
            get { return gname.Text; }
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if(NewGroupName == null) { MessageBox.Show("Введите название группы"); return; }
            Helper.connect.Groups.Add(new Groups() { Name = NewGroupName});
            //Helper.connect.SaveChanges();
            Helper.frame.Navigate(new MainMenu_Admin());
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Helper.frame.Navigate(new MainMenu_Admin());
        }
    }
}
