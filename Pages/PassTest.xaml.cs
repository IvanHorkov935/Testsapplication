using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;
using Tests_application.Connect;

namespace Tests_application.Pages
{
    public partial class PassTest : Page, INotifyPropertyChanged
    {
        public DispatcherTimer timer;
        public TimeSpan TimeToEnd;
        public string StringCountdown
        {
            get
            {
                var frmt = "mm\\:ss";
                return $"Оставшееся время: {TimeToEnd.ToString(frmt)}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public string CorrAns { get; set; }
        public List<Questions> questions;
        public List<Answers> answers;
        public Questions CurrQues;
        public string SelectedValue;
        public int counter = 0;
        public int NumCorrAns = 0;

        public Users CurrUser;
        public Tests Test;
        public PassTest(Tests Test, Users CurrUser, TimeSpan Tend)
        {
            InitializeComponent();

            TimeToEnd = Tend;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += delegate
            {
                TimeToEnd -= TimeSpan.FromSeconds(1);
                OnPropertyChanged("StringCountdown");
                if (TimeToEnd == TimeSpan.Zero)
                {
                    timer.Stop();
                    MessageBox.Show("Время вышло");
                    Button_Click_1(null, null);
                }
            };
            timer.Start();

            this.CurrUser = CurrUser;
            this.Test = Test;

            Time.DataContext = this;
            Title.DataContext = Test;

            questions = Helper.connect.Questions.Where(x => x.ID_Test == Test.ID).ToList();
            Update_Question();
        }
        public void Update_Question()
        {
            CurrQues = questions[counter];
            answers = Helper.connect.Answers.Where(x => x.ID_Question == CurrQues.ID).ToList();
            CorrAns = answers.Where(x => x.Correctness == true).First().Contents;
            if (CurrQues == questions.Last()) { Next.Visibility = Visibility.Collapsed; Finish.Visibility = Visibility.Visible; }

            Question.DataContext = CurrQues;
            chek1.DataContext = answers[0];
            chek2.DataContext = answers[1];
            chek3.DataContext = answers[2];
            chek4.DataContext = answers[3];
            chek1.IsChecked = false;
            chek2.IsChecked = false;
            chek3.IsChecked = false;
            chek4.IsChecked = false;

            counter++;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedValue == CorrAns)
            {
                NumCorrAns++;
                
            }
            Update_Question();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SelectedValue == CorrAns)
            {
                NumCorrAns++;   
            }
            Helper.connect.Results.Add(new Results { ID_Test = Test.ID, ID_User = CurrUser.ID, Per_Complete = NumCorrAns / questions.Count()});
            
            //Helper.connect.SaveChanges();
            double temp = NumCorrAns / questions.Count();
            Helper.frame.Navigate(new TestFinal(temp));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Helper.frame.Navigate(new MainMenu_Student());
        }

        private void chek_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton item)
            {
                SelectedValue = item.Content.ToString();
            }
        }
    }
}
