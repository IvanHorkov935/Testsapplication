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
    public class Exercise
    {
        public string Ques { get; set; }
        public string Ans1 { get; set; }
        public string Ans2 { get; set; }
        public string Ans3 { get; set; }
        public string Ans4 { get; set; }
        public int CorrNum { get; set; }
    }

    public partial class TestRedactor : Page
    {
        public int Counter = 0;
        public ObservableCollection<Exercise> NewQuestions = new ObservableCollection<Exercise>();
        public TestRedactor()
        {
            InitializeComponent();

            TestsListBox.ItemsSource = NewQuestions;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewQuestions.Add(new Exercise());
            if (NewQuestions.Count() == 1) { TestsListBox.Items.MoveCurrentToPosition(0); }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Tests tests = new Tests()
            {
                Name = TestName.Text,
            };
            Helper.connect.Tests.Add(tests);
            Helper.connect.SaveChanges();
            Groups_Tests groups = new Groups_Tests()
            {
                ID_Group = DataHelper.CurrentUserGroup.ID,
                ID_Tests = Helper.connect.Tests.Max(x => x.ID),
            };
            Helper.connect.Groups_Tests.Add(groups);
            Helper.connect.SaveChanges();
            foreach (var item in NewQuestions)
            {
                int MaxID_Test = Helper.connect.Tests.Max(x => x.ID);
                Questions que = new Questions()
                {
                    ID_Test = MaxID_Test,
                    Contents = item.Ques,
                };
                Helper.connect.Questions.Add(que);
                Helper.connect.SaveChanges();
                int MaxID_Ques = Helper.connect.Questions.Max(x => x.ID);
                Answers answers1 = new Answers()
                {
                    ID_Question = MaxID_Ques,
                    Correctness = 0,
                    Contents = item.Ans1
                };
                Answers answers2 = new Answers()
                {
                    ID_Question = MaxID_Ques,
                    Correctness = 0,
                    Contents = item.Ans2
                };
                Answers answers3 = new Answers()
                {
                    ID_Question = MaxID_Ques,
                    Correctness = 0,
                    Contents = item.Ans3
                };
                Answers answers4 = new Answers()
                {
                    ID_Question = MaxID_Ques,
                    Correctness = 0,
                    Contents = item.Ans4
                };
                switch (item.CorrNum)
                {
                    case 1: answers1.Correctness = 1; break;
                    case 2: answers2.Correctness = 1; break;
                    case 3: answers3.Correctness = 1; break;
                    case 4: answers4.Correctness = 1; break;
                }
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
            RadioButton pressed = (RadioButton)sender;
 
            ListBoxItem myListBoxItem =
               (ListBoxItem)(TestsListBox.ItemContainerGenerator.ContainerFromItem(TestsListBox.Items.CurrentItem));
            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
            RadioButton myRadio1 = (RadioButton)myDataTemplate.FindName("b1", myContentPresenter);
            RadioButton myRadio2 = (RadioButton)myDataTemplate.FindName("b2", myContentPresenter);
            RadioButton myRadio3 = (RadioButton)myDataTemplate.FindName("b3", myContentPresenter);
            RadioButton myRadio4 = (RadioButton)myDataTemplate.FindName("b4", myContentPresenter);

            myRadio1.GroupName = $"v{Counter}";
            myRadio2.GroupName = $"v{Counter}";
            myRadio3.GroupName = $"v{Counter}";
            myRadio4.GroupName = $"v{Counter}";

            StackPanel stack = (StackPanel)pressed.Parent;
            Object obj = stack.DataContext;
            int index = TestsListBox.Items.IndexOf(obj);
            TestsListBox.Items.MoveCurrentToPosition(index);

            Counter++;
            
            Exercise a = (Exercise)TestsListBox.SelectedItem;
            int ind = NewQuestions.IndexOf(a);

            if ((bool)myRadio1.IsChecked) { NewQuestions[ind].CorrNum = 1; }
            if ((bool)myRadio2.IsChecked) { NewQuestions[ind].CorrNum = 2; }
            if ((bool)myRadio3.IsChecked) { NewQuestions[ind].CorrNum = 3; }
            if ((bool)myRadio4.IsChecked) { NewQuestions[ind].CorrNum = 4; }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Helper.frame.Navigate(new MainMenu_Teacher());
        }
    }
}
