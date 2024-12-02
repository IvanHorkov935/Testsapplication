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
    /// Логика взаимодействия для TestRedactor.xaml
    /// </summary>
    class Exercise
    {
        public string Ques { get; set; }
        public string Ans1 { get; set; }
        public string Ans2 { get; set; }
        public string Ans3 { get; set; }
        public string Ans4 { get; set; }
        public int CorrNum { get; set; }
    }
    class Contains
    {
        public static int Counter = 0;
        public static ObservableCollection<Exercise> content = new ObservableCollection<Exercise>() {};
        public static string CorrAns = "";
    }

    public partial class TestRedactor : Page
    {
        public TestRedactor()
        {
            InitializeComponent();

            TestsListBox.ItemsSource = Contains.content;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Contains.content.Add(new Exercise());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int maxIdtest = 0;
            foreach (var partId in Helper.connect.Tests.ToList())
            {
                if (partId.ID > maxIdtest)
                {
                    maxIdtest = partId.ID;
                }
            }
            int maxIdresult = 0;
            foreach (var partId in Helper.connect.Tests.ToList())
            {
                if (partId.ID > maxIdresult)
                {
                    maxIdresult = partId.ID;
                }
            }
            List<Users> StudUsers = (List<Users>)Helper.connect.Users.Where(x => x.ID_Type == 3).ToList().AsEnumerable();
            for (int i = 0; i < Helper.connect.Users.Where(x => x.ID_Type == 3).Count(); i++)
            {
                Results result = new Results()
                {
                    ID = maxIdresult + 1 + i,
                    ID_Test = maxIdtest + 1,
                    ID_User = StudUsers[i].ID,
                    Per_Complete = 0,
                };
                Helper.connect.Results.Add(result);
            }
            Tests tests = new Tests()
            {
                ID = maxIdtest + 1,
                Name = TestName.Text,
            };
            Helper.connect.Tests.Add(tests);
            Helper.connect.SaveChanges();
            foreach (var item in Contains.content)
            {
                int maxId = 0;
                foreach (var partId in Helper.connect.Questions.ToList())
                {
                    if (partId.ID > maxId)
                    {
                        maxId = partId.ID;
                    }
                }
                int maxIdans = 0;
                foreach (var partId in Helper.connect.Answers.ToList())
                {
                    if (partId.ID > maxIdans)
                    {
                        maxIdans = partId.ID;
                    }
                }
                Questions que = new Questions()
                {
                    ID = maxId + 1,
                    ID_Test = maxIdtest + 1,
                    Contents = item.Ques,
                };
                Answers answers1 = new Answers()
                {
                    ID = maxIdans + 1,
                    ID_Question = maxId + 1,
                    Correctness = "нет",
                    Contents = item.Ans1
                };
                Answers answers2 = new Answers()
                {
                    ID = maxIdans + 2,
                    ID_Question = maxId + 1,
                    Correctness = "нет",
                    Contents = item.Ans2
                };
                Answers answers3 = new Answers()
                {
                    ID = maxIdans + 3,
                    ID_Question = maxId + 1,
                    Correctness = "нет",
                    Contents = item.Ans3
                };
                Answers answers4 = new Answers()
                {
                    ID = maxIdans + 4,
                    ID_Question = maxId + 1,
                    Correctness = "нет",
                    Contents = item.Ans4
                };
                switch (item.CorrNum)
                {
                    case 1: answers1.Correctness = "да"; break;
                    case 2: answers2.Correctness = "да"; break;
                    case 3: answers3.Correctness = "да"; break;
                    case 4: answers4.Correctness = "да"; break;
                }
                
                Helper.connect.Questions.Add(que);
                Helper.connect.Answers.Add(answers1);
                Helper.connect.Answers.Add(answers2);
                Helper.connect.Answers.Add(answers3);
                Helper.connect.Answers.Add(answers4);
                Helper.connect.SaveChanges();
            }
            Helper.frame.Navigate(new MainMenu_Teacher());
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj)
        where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                {
                    return (childItem)child;
                }
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ListBoxItem myListBoxItem =
               (ListBoxItem)(TestsListBox.ItemContainerGenerator.ContainerFromItem(TestsListBox.Items.CurrentItem));
            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
            RadioButton myRadio1 = (RadioButton)myDataTemplate.FindName("b1", myContentPresenter);
            RadioButton myRadio2 = (RadioButton)myDataTemplate.FindName("b2", myContentPresenter);
            RadioButton myRadio3 = (RadioButton)myDataTemplate.FindName("b3", myContentPresenter);
            RadioButton myRadio4 = (RadioButton)myDataTemplate.FindName("b4", myContentPresenter);

            myRadio1.GroupName = $"v{Contains.Counter}";
            myRadio2.GroupName = $"v{Contains.Counter}";
            myRadio3.GroupName = $"v{Contains.Counter}";
            myRadio4.GroupName = $"v{Contains.Counter}";

            Contains.Counter += 1;
            
            Exercise a = (Exercise)TestsListBox.SelectedItem;
 
            RadioButton pressed = (RadioButton)sender;
       
            if (pressed.Content.ToString() == "1-й ответ")
            {
                a.CorrNum = 1;
            }
            else if (pressed.Content.ToString() == "2-й ответ")
            {
                a.CorrNum = 2;
            }
            else if (pressed.Content.ToString() == "3-й ответ")
            {
                a.CorrNum = 3;
            }
            else if (pressed.Content.ToString() == "4-й ответ")
            {
                a.CorrNum = 4;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Helper.frame.Navigate(new MainMenu_Teacher());
        }
    }
}
