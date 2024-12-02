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
    public partial class PassTest : Page
    {
        public PassTest(int con, int NumTest)
        {
            InitializeComponent();

            if(Params.NumQues == Params.counter)
            {
                b1.Visibility = Visibility.Collapsed;
                b2.Visibility = Visibility.Visible;
            }
            a.DataContext = Helper.connect.Tests.Where(x => x.ID == NumTest).ToList();

            Params.Questions = (List<Questions>)Helper.connect.Questions.Where(x => x.ID_Test == NumTest).ToList().AsEnumerable();

            int CurrentQues = Params.Questions[con-1].ID;

            Params.Answers = (List<Answers>)Helper.connect.Answers.Where(x => x.ID_Question == CurrentQues).ToList().AsEnumerable();

            //question.DataContext = Params.Questions.Where(x => x.ID_Test == NumTest).ToList();
            question.DataContext = Params.Questions[con - 1];
            ans1.DataContext = Params.Answers[0];
            ans2.DataContext = Params.Answers[1];
            ans3.DataContext = Params.Answers[2];
            ans4.DataContext = Params.Answers[3];

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string ans = Params.pressed.Content.ToString();
            List<Answers> CorrectBns = (List<Answers>)Helper.connect.Answers.Where(x => x.Contents == ans && x.Correctness == "да").ToList();
            if (CorrectBns.Count > 0)
            {
                Params.NumCorrAns += 1;
            }
            else
            {
                
            }
          
            Params.counter += 1;
            Helper.frame.Navigate(new PassTest(Params.counter, Params.NumTest.ID));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string ans = Params.pressed.Content.ToString();
            List<Answers> CorrectBns = (List<Answers>)Helper.connect.Answers.Where(x => x.Contents == ans && x.Correctness == "да").ToList();
            if (CorrectBns.Count > 0)
            {
                Params.NumCorrAns += 1;
            }
            else
            {
                
            }

            Results result = new Results()
            {
                ID_Test = Params.NumTest.ID,
                ID_User = Params.StudentID,
                Per_Complete = (float)Params.NumCorrAns / Params.NumQues
            };

            int maxId = 0;
            foreach (var partId in Helper.connect.Results.ToList())
            {
                if (partId.ID > maxId)
                {
                    maxId = partId.ID;
                }
            }
            result.ID = maxId + 1;
            Helper.connect.Results.Add(result);
            Helper.connect.SaveChanges();
            Params.counter = 1;
            Params.NumCorrAns = 0;
            Params.pressed = null;
            Params.NumQues = 0;
            Params.NumTest = null;
            Params.Questions = null;
            Params.Answers = null;
            Helper.frame.Navigate(new MainMenu_Student(Params.StudentID));
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
