using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    // Использовать RadioButton вместо ChekBox
    // Продумать логику программы
    public class Gay
    {

    }
    public partial class PassTest : Page
    {
        public string CorrAns { get; set; }
        public double CountQues { get; set; }
        public int IDTest { get; set; }
        public PassTest(int idTest)
        {
            InitializeComponent();

            List<Questions> questions = (List<Questions>)Helper.connect.Questions.Where(x => x.ID_Test == idTest).ToList().AsEnumerable();
            Questions CurrQues = questions[Params.counter];
            if( CurrQues == questions.Last()) { b1.Visibility = Visibility.Collapsed; b2.Visibility = Visibility.Visible; }
            List<Answers> answers = (List<Answers>)Helper.connect.Answers.Where(x => x.ID_Question == CurrQues.ID).ToList().AsEnumerable();

            CorrAns = answers.Where(x => x.Correctness == "да   ").First().Contents;
            IDTest = idTest;
            CountQues = questions.Count();

            question.DataContext = CurrQues;
            ans1.DataContext = answers[0];
            ans2.DataContext = answers[1];
            ans3.DataContext = answers[2];
            ans4.DataContext = answers[3];

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((string)Params.pressed.Content == CorrAns)
            {
                Params.NumCorrAns += 1;
            }
            Params.counter++;
            Helper.frame.Navigate(new PassTest(IDTest));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if ((string)Params.pressed.Content == CorrAns)
            {
                Params.NumCorrAns += 1;
            }
            Helper.connect.Results.Add(new Results { ID_Test = IDTest, ID_User = Params.user.ID, Per_Complete = Params.NumCorrAns / CountQues});
            Helper.connect.SaveChanges();
            Params.counter = 0;
            Params.NumCorrAns = 0;
            Helper.frame.Navigate(new MainMenu_Student(Params.user));
        }

        private void chek1_Checked(object sender, RoutedEventArgs e)
        {
            Params.pressed = (CheckBox)sender;
            if ((bool)chek2.IsChecked || (bool)chek3.IsChecked || (bool)chek4.IsChecked)
            {
                chek2.IsChecked = false;
                chek3.IsChecked = false;
                chek4.IsChecked = false;
            }
        }

        private void chek2_Checked(object sender, RoutedEventArgs e)
        {
            Params.pressed = (CheckBox)sender;
            if ((bool)chek1.IsChecked || (bool)chek3.IsChecked || (bool)chek4.IsChecked)
            {
                chek1.IsChecked = false;
                chek3.IsChecked = false;
                chek4.IsChecked = false;
            }
        }

        private void chek3_Checked(object sender, RoutedEventArgs e)
        {
            Params.pressed = (CheckBox)sender;
            if ((bool)chek2.IsChecked || (bool)chek3.IsChecked || (bool)chek1.IsChecked)
            {
                chek2.IsChecked = false;
                chek1.IsChecked = false;
                chek4.IsChecked = false;
            }
        }

        private void chek4_Checked(object sender, RoutedEventArgs e)
        {
            Params.pressed = (CheckBox)sender;
            if ((bool)chek2.IsChecked || (bool)chek3.IsChecked || (bool)chek1.IsChecked)
            {
                chek2.IsChecked = false;
                chek3.IsChecked = false;
                chek1.IsChecked = false;
            }
        }

    }
}
