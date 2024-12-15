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
    public partial class TeacherMatchGroup : Page
    {
        public TeacherMatchGroup()
        {
            InitializeComponent();

            List<Groups> groups = SingletoneTeacher.getInstance("", "").CurrentUserGroups;
            GroupsListBox.ItemsSource = groups;
        }

        private void GroupsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Groups group = GroupsListBox.SelectedItem as Groups;
            SingletoneTeacher.getInstance("", "").SelectedGroup = group;
            Helper.frame.Navigate(new MainMenu_Teacher());
        }
    }
}
