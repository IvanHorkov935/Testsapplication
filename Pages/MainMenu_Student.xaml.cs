using System;
using System.Collections.Generic;
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
        public static Tests NumTest = null;
        public static int counter = 1;
        public static int NumCorrAns = 0;
        public static CheckBox pressed = null;
        public static int NumQues = 0; //Helper.connect.Questions.Where(x => x.ID_Test == 1).Count();
        public static List<Questions> Questions = null;
        public static List<Answers> Answers = null;
        public static int StudentID = 0;
    }
    public partial class MainMenu_Student : Page
    {
        public MainMenu_Student(int id)
        {
            InitializeComponent();

            Params.StudentID = id;

            List<Users> user = (List<Users>)Helper.connect.Users.Where(x => x.ID == id).ToList().AsEnumerable();
            UserName.Text = user[0].Full_Name;
            int id_group = user[0].ID_Group;
            List<Groups> group = (List<Groups>)Helper.connect.Groups.Where(x => x.ID == id_group).ToList().AsEnumerable();
            UserGroup.Text = $"Группа: {group[0].Name}";


            List<Results> res = new List<Results>();
            List<int> con = new List<int>();
            List<Results> results = (List<Results>)Helper.connect.Results.Where(x => x.ID_User == id).ToList().AsEnumerable();
            foreach (Results r in results)
            {
                if (con.Contains((int)r.ID_Test))
                {
                    continue;
                }
                if(Helper.connect.Results.Where(x => x.ID_User == id && x.ID_Test == r.ID_Test).Count() > 1)
                {
                    con.Add((int)r.ID_Test);
                    Results maxres = new Results { Per_Complete = 0};
                    foreach(Results t in Helper.connect.Results.Where(x => x.ID_User == id && x.ID_Test == r.ID_Test).ToList())
                    {
                        if (t.Per_Complete > maxres.Per_Complete)
                        {
                            maxres = t;
                        }
                    }
                    res.Add(maxres);
                }
                else
                {
                    res.Add(r);
                }
            }

            //TestsListBox.ItemsSource = Helper.connect.Results.Where(x => x.ID_User == id).ToList();
            TestsListBox.ItemsSource = res.ToList();
            TestsListBox.SelectionChanged += LBox_SelectionChanged;
        }

        private void LBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Params.NumTest = (Tests)TestsListBox.SelectedItem;
            Results a = TestsListBox.SelectedItem as Results;
            Params.NumTest = Helper.connect.Tests.Find(a.ID_Test);

            Params.NumQues = Helper.connect.Questions.Where(x => x.ID_Test == Params.NumTest.ID).Count();
            Helper.frame.Navigate(new PassTest(Params.counter, Params.NumTest.ID));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Helper.frame.Navigate(new Login());
        }
    }
}
