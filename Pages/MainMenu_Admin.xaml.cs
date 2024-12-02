using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

    public class Contain
    {
        public static ObservableCollection<Users> users = new ObservableCollection<Users>();
        public static ObservableCollection<Groups> groups = new ObservableCollection<Groups>();
        public static Groups CurrentGroup;
    }
    public partial class MainMenu_Admin : Page
    {
        public MainMenu_Admin()
        {
            InitializeComponent();
            foreach(var item in Helper.connect.Groups)
            {
                Contain.groups.Add(item);
            }
            InfListBox.ItemsSource = Contain.users;
            GroupListBox.ItemsSource = Contain.groups;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Helper.frame.Navigate(new Login());
        }

        private void GroupListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contain.users.Clear();
            Groups group = GroupListBox.SelectedItem as Groups;
            AddMembers.IsEnabled = true;
            Contain.CurrentGroup = group;
            if (group.ID == 1 || group.ID == 7)
            {
                Users user = new Users()
                {
                    Full_Name = "-"
                };
                t1.DataContext = user;
                var list = Helper.connect.Users.Where(x => x.ID_Group == group.ID).ToList().AsEnumerable();
                foreach (var item in list)
                {
                    Contain.users.Add(item);
                }
            }
            else
            {
                t1.DataContext = Helper.connect.Users.Where(x => x.ID_Group == group.ID && x.ID_Type == 2).FirstOrDefault();
                var list = Helper.connect.Users.Where(x => x.ID_Group == group.ID && x.ID_Type != 2).ToList().AsEnumerable();
                foreach (var item in list)
                {
                    Contain.users.Add(item);
                }
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddGroup h = new AddGroup();
            if(h.ShowDialog() == true)
            {
                Groups group = new Groups
                {
                    Name = h.NewGroupName
                };
                Contain.groups.Add(group);
                Helper.connect.Groups.Add(group);
                Helper.connect.SaveChanges();
            }
            else
            {
                //MessageBox.Show("no");
            }
        }

        private void AddMembers_Click(object sender, RoutedEventArgs e)
        {
            Helper.frame.Navigate(new AddMemders());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddUser h = new AddUser();
            if (h.ShowDialog() == true)
            {

            }
            else
            {
                Helper.connect.Users.Add(h.NewUser);
            }
        }
    }
}
