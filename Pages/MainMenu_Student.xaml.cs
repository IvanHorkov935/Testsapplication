using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
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
using Tests_application.Pages.HelperClasses;

namespace Tests_application.Pages
{
    public partial class MainMenu_Student : Page
    {
        ObservableCollection<ForListBox> TestsResults = new ObservableCollection<ForListBox>();
        SingletoneStudent Student;
        public MainMenu_Student()
        {
            Student = SingletoneStudent.getInstance("", "");
            InitializeComponent();

            UserName.Text = $"Имя: {Student.CurrentUser.Full_Name}";
            UserGroup.Text = $"Группа: {Student.CurrentUserGroup.Name}";

            foreach (Tests test in Student.CurrentUserTests)
            {
                var results = Helper.connect.Results.Where(x => x.ID_User == Student.CurrentUser.ID && x.ID_Test == test.ID);
                if (results.Count() == 0)
                { 
                    TestsResults.Add(new ForListBox { NameTest = test.Name, PerComplete = 0 }); 
                }
                else
                {
                    double? percomp = results.Max(x => x.Per_Complete);
                    TestsResults.Add(new ForListBox { NameTest = test.Name, PerComplete = percomp, PerCompForProgress = (int)(percomp * 100) });
                }
            }

            TestsListBox.ItemsSource = TestsResults;
            TestsListBox.SelectionChanged += LBox_SelectionChanged;
        }

        private void Window_OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void LBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var test = TestsListBox.SelectedItem as ForListBox;
            Tests Test = Helper.connect.Tests.First(x => x.Name == test.NameTest);
            Helper.frame.Navigate(new PassTest(Test, Student.CurrentUser,  TimeSpan.FromMinutes(5)));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Helper.frame.Navigate(new Login());
        }
    }
}
