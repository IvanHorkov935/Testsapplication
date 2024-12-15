using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class AddMemders : Page
    {
        public ObservableCollection<Users> CurrentTeachers = new ObservableCollection<Users>();
        public ObservableCollection<Users> CurrentStudents = new ObservableCollection<Users>();
        public ObservableCollection<Users> UnknownUsers = new ObservableCollection<Users>();
        public int IDGroup = 0;
        public AddMemders(Groups group)
        {
            InitializeComponent();

            IDGroup = group.ID;

            var listUsers = Helper.connect.Users_Groups.Where(x => x.ID_Group == group.ID).ToList();
            foreach (Users_Groups user in listUsers)
            {
                if(user.Users.ID_Type == 2) { CurrentTeachers.Add(user.Users); }
                if (user.Users.ID_Type == 3) { CurrentStudents.Add(user.Users); }
            }
            ListCurrentTeachers.ItemsSource = CurrentTeachers;
            ListCurrentStudents.ItemsSource = CurrentStudents;
            GroupName.DataContext = group.Name;

            var listunknownUsers = Helper.connect.Users.Where(x => x.Users_Groups.Count() == 0).ToList();
            foreach(Users user in listunknownUsers)
            {
                if (user.ID_Type == 1) { continue; }
                UnknownUsers.Add(user);
            }
            ListUnallocated.ItemsSource = UnknownUsers;
        }
        private void ListUnallocated_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Users user = ListUnallocated.SelectedItem as Users;
            if(user == null) {  return; }
            UnknownUsers.Remove(user);
            if(user.ID_Type == 2) { CurrentTeachers.Add(user);}
            if (user.ID_Type == 3) { CurrentStudents.Add(user); }
        }

        private void ListCurrentTeachers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Users user = ListCurrentTeachers.SelectedItem as Users;
            if (user == null) { return; }
            UnknownUsers.Add(user);
            CurrentTeachers.Remove(user);
        }
        private void ListCurrentStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Users user = ListCurrentStudents.SelectedItem as Users;
            if (user == null) { return; }
            UnknownUsers.Add(user);
            CurrentStudents.Remove(user);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Helper.frame.Navigate(new MainMenu_Admin());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (Users user in CurrentTeachers)
            {
                Helper.connect.Users_Groups.Add(new Users_Groups() { ID_Group = IDGroup, ID_User = user.ID });
            }
            foreach (Users user in CurrentStudents)
            {
                Helper.connect.Users_Groups.Add(new Users_Groups() { ID_Group = IDGroup, ID_User = user.ID });

            }
            foreach (Users user in UnknownUsers)
            {
                if(user.Users_Groups.FirstOrDefault(x => x.ID_Group == IDGroup) != null)
                {
                    Helper.connect.Users_Groups.Remove(user.Users_Groups.First(x => x.ID_Group == IDGroup));
                }
            }
            //Helper.connect.SaveChanges();
            Helper.frame.Navigate(new MainMenu_Admin());
        }
    }
}
