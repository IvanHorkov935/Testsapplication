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
    public partial class MainMenu_Admin : Page
    {
        public ObservableCollection<Users> ListUsers = new ObservableCollection<Users>();
        public ObservableCollection<Groups> ListGroups = new ObservableCollection<Groups>();
        public Groups CurrentGroup = null;

        public MainMenu_Admin()
        {
            InitializeComponent();
            foreach(var item in Helper.connect.Groups)
            {
                ListGroups.Add(item);
            }
            InfListBox.ItemsSource = ListUsers;
            GroupListBox.ItemsSource = ListGroups;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Helper.frame.Navigate(new Login());
        }

        private void GroupListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListUsers != null) { ListUsers.Clear(); }
            CurrentGroup = GroupListBox.SelectedItem as Groups;
            if (CurrentGroup == null) { AddMembers.IsEnabled = true; return; }
            else { AddMembers.IsEnabled = true; }

            t1.DataContext = Helper.connect.Users.Where(x => x.ID_Group == CurrentGroup.ID && x.ID_Type == 2).FirstOrDefault();
            foreach (Users user in Helper.connect.Users.Where(x => x.ID_Group == CurrentGroup.ID && x.ID_Type != 2)) { ListUsers.Add(user); }
            //MessageBox.Show(CurrentGroup.Name);
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
                ListGroups.Add(group);
                Helper.connect.Groups.Add(group);
                //Helper.connect.SaveChanges();
            }
            else
            {
                //MessageBox.Show("no");
            }
        }

        private void AddMembers_Click(object sender, RoutedEventArgs e)
        {
            Groups clickedGroup = (Groups)GroupListBox.SelectedItem;
            Helper.frame.Navigate(new AddMemders(clickedGroup));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddUser h = new AddUser();
            if (h.ShowDialog() == true)
            {
                ListUsers.Add(h.newUser);
                Helper.connect.Users.Add(h.newUser);
                //Helper.connect.SaveChanges();
            }
            else
            {
                //MessageBox.Show("non");
            }
        }
    }
}
