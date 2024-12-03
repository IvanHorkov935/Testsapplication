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
    /// <summary>
    /// Логика взаимодействия для AddMemders.xaml
    /// </summary>
    public class Contain2
    {
        public static ObservableCollection<Users> AddedUsers = new ObservableCollection<Users>();
        public static ObservableCollection<Users> UnknownUsers = new ObservableCollection<Users>();
        public static int IDGroup;
    }
    public partial class AddMemders : Page
    {

        public int NumTeach = 0;
        public AddMemders(int idgroup)
        {
            InitializeComponent();

            Contain2.IDGroup = idgroup;

            //--------------------------------------------------------------------- && x.ID_Type == 3
            var a1 = Helper.connect.Users.Where(x => x.ID_Group == Contain2.IDGroup).ToList().AsQueryable();
            foreach (Users u in a1)
            {
                Contain2.AddedUsers.Add(u);
            }
            ListCurrent.ItemsSource = Contain2.AddedUsers;

            //---------------------------------------------------------------------

            combo.ItemsSource = Contain2.AddedUsers.Where(x => x.ID_Type == 2).ToList();

            GroupName.DataContext = Helper.connect.Groups.Where(x => x.ID == idgroup).FirstOrDefault().Name;

            //----------------------------------------------------------------------------
            var a2 = Helper.connect.Users.Where(x => x.ID_Group == 7).ToList().AsQueryable();
            foreach(Users u in a2)
            {
                Contain2.UnknownUsers.Add(u);
            }
            ListUnallocated.ItemsSource = Contain2.UnknownUsers;
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
            Contain2.UnknownUsers.Remove(user);
            Contain2.AddedUsers.Add(user);
            combo.ItemsSource = Contain2.AddedUsers.Where(x => x.ID_Type == 2).ToList();
        }

        private void ListCurrent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Users user = ListCurrent.SelectedItem as Users;
            if (user == null) { return; }
            Contain2.UnknownUsers.Add(user);
            Contain2.AddedUsers.Remove(user);
            combo.ItemsSource = Contain2.AddedUsers.Where(x => x.ID_Type == 2).ToList();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Helper.frame.Navigate(new MainMenu_Admin());
            Contain2.AddedUsers.Clear();
            Contain2.UnknownUsers.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(Contain2.AddedUsers.Where(x => x.ID_Type == 2).Count() > 1) { MessageBox.Show("В одной группе может быть только 1 учитель"); return; }
            if(combo.SelectedItem == null) { MessageBox.Show("Выберите руководителя"); return; }
            Users a = (Users)combo.SelectedItem;
            Helper.connect.Users.Where(x => x.ID == a.ID).FirstOrDefault().ID_Group = Contain2.IDGroup;
            foreach (var user in Contain2.AddedUsers)
            {
                Helper.connect.Users.Where(x => x.ID == user.ID).FirstOrDefault().ID_Group = Contain2.IDGroup;
                
            }
            foreach (var user in Contain2.UnknownUsers)
            {
                Helper.connect.Users.Where(x => x.ID == user.ID).FirstOrDefault().ID_Group = 7;
            }
            Helper.connect.SaveChanges();
            Helper.frame.Navigate(new MainMenu_Admin());
        }
    }
}
