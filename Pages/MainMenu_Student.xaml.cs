using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
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
    /// Логика взаимодействия для MainMenu_Student.xaml
    /// </summary>
    class Params
    {
        public static Users user;
        public static int groupID;
        public static CheckBox pressed;
        public static int counter = 0;
        public static double NumCorrAns = 0;
    }
    public class ForListBox
    {
        public string NameTest { get; set; }
        public double PerComplete { get; set; }
    }
    public partial class MainMenu_Student : Page
    {
        public MainMenu_Student(Users CurrUser)
        {
            InitializeComponent();
            Groups StudentGroup = Helper.connect.Groups.FirstOrDefault(g => g.ID == CurrUser.ID_Group);

            Params.user = CurrUser;
            Params.groupID = StudentGroup.ID;

            UserName.Text = $"Имя: {CurrUser.Full_Name}";
            UserGroup.Text = $"Группа: {StudentGroup.Name}";

            List<int> GroupTests = new List<int>();
            ObservableCollection<Tests> StudentTests = new ObservableCollection<Tests>();
            foreach(var i in Helper.connect.Groups_Tests.Where(x => x.ID_Group == StudentGroup.ID))
            {
                GroupTests.Add(i.ID_Tests);
            }
            foreach(int i in GroupTests)
            {
                StudentTests.Add(Helper.connect.Tests.Where(x => x.ID == i).FirstOrDefault());
            }

            ObservableCollection<ForListBox> TestsResults = new ObservableCollection<ForListBox>();
            foreach(var i in StudentTests)
            {
                if(Helper.connect.Results.Where(x => x.ID_User == CurrUser.ID && x.ID_Test == i.ID).Count() == 0) { TestsResults.Add(new ForListBox { NameTest = i.Name, PerComplete = 0 }); continue; }
                var b = Helper.connect.Results.Where(x => x.ID_Test == i.ID && x.ID_User == CurrUser.ID);
                double j = 0;
                if (b != null) { j = (double)b.Max(x => x.Per_Complete); }
                TestsResults.Add(new ForListBox { NameTest = i.Name, PerComplete =  j});
            }

            TestsListBox.ItemsSource = TestsResults;
            TestsListBox.SelectionChanged += LBox_SelectionChanged;
        }

        private void LBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = TestsListBox.SelectedItem as ForListBox;
            int idTest = Helper.connect.Tests.First(x => x.Name == a.NameTest).ID;
            Helper.frame.Navigate(new PassTest(idTest));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Helper.frame.Navigate(new Login());
        }
    }
}
