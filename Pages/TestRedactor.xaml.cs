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
    class ContainsRed
    {
        public static int Counter = 0;
        public static ObservableCollection<Exercise> content = new ObservableCollection<Exercise>() {};
        public static string CorrAns = "";
    }

    public partial class TestRedactor : Page
    {
        public int idGroup { get; set; }
        public TestRedactor(int IDGroup)
        {
            InitializeComponent();

            idGroup = IDGroup;
            TestsListBox.ItemsSource = ContainsRed.content;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContainsRed.content.Add(new Exercise());
            if (ContainsRed.content.Count() == 1) { TestsListBox.Items.MoveCurrentToPosition(0); }
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
                ID_Group = idGroup,
                ID_Tests = Helper.connect.Tests.Max(x => x.ID),
            };
            Helper.connect.Groups_Tests.Add(groups);
            Helper.connect.SaveChanges();
            foreach (var item in ContainsRed.content)
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
                    Correctness = "нет",
                    Contents = item.Ans1
                };
                Answers answers2 = new Answers()
                {
                    ID_Question = MaxID_Ques,
                    Correctness = "нет",
                    Contents = item.Ans2
                };
                Answers answers3 = new Answers()
                {
                    ID_Question = MaxID_Ques,
                    Correctness = "нет",
                    Contents = item.Ans3
                };
                Answers answers4 = new Answers()
                {
                    ID_Question = MaxID_Ques,
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
                Helper.connect.Answers.Add(answers1);
                Helper.connect.Answers.Add(answers2);
                Helper.connect.Answers.Add(answers3);
                Helper.connect.Answers.Add(answers4);
                Helper.connect.SaveChanges();
            }
            Helper.frame.Navigate(new MainMenu_Teacher(idGroup));
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

            myRadio1.GroupName = $"v{ContainsRed.Counter}";
            myRadio2.GroupName = $"v{ContainsRed.Counter}";
            myRadio3.GroupName = $"v{ContainsRed.Counter}";
            myRadio4.GroupName = $"v{ContainsRed.Counter}";

            StackPanel stack = (StackPanel)pressed.Parent;
            Object obj = stack.DataContext;
            int index = TestsListBox.Items.IndexOf(obj);
            TestsListBox.Items.MoveCurrentToPosition(index);

            ContainsRed.Counter += 1;
            
            Exercise a = (Exercise)TestsListBox.SelectedItem;
            int ind = ContainsRed.content.IndexOf(a);

            if ((bool)myRadio1.IsChecked) { ContainsRed.content[ind].CorrNum = 1; }
            if ((bool)myRadio2.IsChecked) { ContainsRed.content[ind].CorrNum = 2; }
            if ((bool)myRadio3.IsChecked) { ContainsRed.content[ind].CorrNum = 3; }
            if ((bool)myRadio4.IsChecked) { ContainsRed.content[ind].CorrNum = 4; }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Helper.frame.Navigate(new MainMenu_Teacher(idGroup));
        }
    }
}
