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
        public ObservableCollection<Users> ListTeachers = new ObservableCollection<Users>();
        public ObservableCollection<Users> ListStudents = new ObservableCollection<Users>();
        public ObservableCollection<Groups> ListGroups = new ObservableCollection<Groups>();
        public Groups CurrentGroup = null;

        public MainMenu_Admin()
        {
            InitializeComponent();
            foreach(var item in Helper.connect.Groups)
            {
                ListGroups.Add(item);
            }
            TeachersListBox.ItemsSource = ListTeachers;
            StudentsListBox.ItemsSource = ListStudents;
            GroupListBox.ItemsSource = ListGroups;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Helper.frame.Navigate(new Login());
        }

        private void GroupListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListTeachers != null || ListStudents != null) { ListTeachers.Clear(); ListStudents.Clear(); }
            CurrentGroup = GroupListBox.SelectedItem as Groups;
            if (CurrentGroup == null) { AddMembers.IsEnabled = true; return; }
            else { AddMembers.IsEnabled = true; }

            Title.Text = $"Состав группы [{CurrentGroup.Name}]:";
            foreach (Users_Groups usergroup in Helper.connect.Users_Groups.Where(x => x.ID_Group == CurrentGroup.ID))
            {
                if (usergroup.Users.ID_Type == 2) { ListTeachers.Add(usergroup.Users); }
                if (usergroup.Users.ID_Type == 3) { ListStudents.Add(usergroup.Users); }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Helper.frame.Navigate(new AddGroup());
        }

        private void AddMembers_Click(object sender, RoutedEventArgs e)
        {
            Groups clickedGroup = (Groups)GroupListBox.SelectedItem;
            Helper.frame.Navigate(new AddMemders(clickedGroup));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Helper.frame.Navigate(new AddUser());
        }
    }
}
