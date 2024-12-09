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

namespace Tests_application.Pages
{
    public class ForListBox
    {
        public string NameTest { get; set; }
        public double PerComplete { get; set; }
        public int PerCompForProgress { get; set; }
    }
    public partial class MainMenu_Student : Page
    {
        ObservableCollection<Tests> StudentTests = new ObservableCollection<Tests>();
        ObservableCollection<ForListBox> TestsResults = new ObservableCollection<ForListBox>();
        public Users CurrUser;
        public MainMenu_Student(Users CurrUser, Groups CurrGroup)
        {
            InitializeComponent();
            this.CurrUser = CurrUser;

            UserName.Text = $"Имя: {CurrUser.Full_Name}";
            UserGroup.Text = $"Группа: {CurrGroup.Name}";
            
            foreach(var i in Helper.connect.Groups_Tests.Where(x => x.ID_Group == CurrGroup.ID))
            {
                StudentTests.Add(i.Tests);
            }

            foreach(var i in StudentTests)
            {
                if(Helper.connect.Results.Where(x => x.ID_User == CurrUser.ID && x.ID_Test == i.ID).Count() == 0) { TestsResults.Add(new ForListBox { NameTest = i.Name, PerComplete = 0 }); continue; }
                var ResultsThisTest = Helper.connect.Results.Where(x => x.ID_Test == i.ID && x.ID_User == CurrUser.ID);
                double percomp = (double)ResultsThisTest.Max(x => x.Per_Complete);
                TestsResults.Add(new ForListBox { NameTest = i.Name, PerComplete = percomp, PerCompForProgress = (int)(percomp * 100) });
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
            Helper.frame.Navigate(new PassTest(Test, CurrUser,  TimeSpan.FromMinutes(5)));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Helper.frame.Navigate(new Login());
        }
    }
}
