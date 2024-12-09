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
        public ObservableCollection<Users> AddedUsers = new ObservableCollection<Users>();
        public ObservableCollection<Users> UnknownUsers = new ObservableCollection<Users>();
        public int IDGroup = 0;
        public int NumTeach = 0;
        public AddMemders(Groups group)
        {
            InitializeComponent();

            IDGroup = group.ID;

            var listUsers = Helper.connect.Users.Where(x => x.ID_Group == IDGroup).ToList();
            foreach (Users u in listUsers)
            {
                AddedUsers.Add(u);
            }
            ListCurrent.ItemsSource = AddedUsers;

            combo.ItemsSource = AddedUsers.Where(x => x.ID_Type == 2).ToList();

            GroupName.DataContext = group.Name;

            var listunknownUsers = Helper.connect.Users.Where(x => x.ID_Group == null).ToList().AsQueryable();
            foreach(Users u in listunknownUsers)
            {
                UnknownUsers.Add(u);
            }
            ListUnallocated.ItemsSource = UnknownUsers;
        }

        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedValue == null) { return; }
            NumTeach = (int)comboBox.SelectedValue;
        }

        private void ListUnallocated_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Users user = ListUnallocated.SelectedItem as Users;
            if(user == null) {  return; }
            UnknownUsers.Remove(user);
            AddedUsers.Add(user);
            combo.ItemsSource = AddedUsers.Where(x => x.ID_Type == 2).ToList();
        }

        private void ListCurrent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Users user = ListCurrent.SelectedItem as Users;
            if (user == null) { return; }
            UnknownUsers.Add(user);
            AddedUsers.Remove(user);
            combo.ItemsSource = AddedUsers.Where(x => x.ID_Type == 2).ToList();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Helper.frame.Navigate(new MainMenu_Admin());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(AddedUsers.Where(x => x.ID_Type == 2).Count() > 1) { MessageBox.Show("В одной группе может быть только 1 учитель"); return; }
            if(combo.SelectedItem == null) { MessageBox.Show("Выберите руководителя"); return; }
            Users teacher = (Users)combo.SelectedItem;
            Helper.connect.Users.Where(x => x.ID == teacher.ID).FirstOrDefault().ID_Group = IDGroup;
            foreach (var user in AddedUsers)
            {
                Helper.connect.Users.Where(x => x.ID == user.ID).FirstOrDefault().ID_Group = IDGroup;
                
            }
            foreach (var user in UnknownUsers)
            {
                Helper.connect.Users.Where(x => x.ID == user.ID).FirstOrDefault().ID_Group = null;
            }
            //Helper.connect.SaveChanges();
            Helper.frame.Navigate(new MainMenu_Admin());
        }
    }
}
