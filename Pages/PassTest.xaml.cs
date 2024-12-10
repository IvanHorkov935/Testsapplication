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
        private DateTime _startCountdown; 
        private TimeSpan _startTimeSpan {  get; set; } 
        private TimeSpan _timeToEnd; 
        private TimeSpan _interval = TimeSpan.FromMilliseconds(15); 
        private DateTime _pauseTime;
        private DispatcherTimer _timer;

        public TimeSpan TimeToEnd
        {
            get
            {
                return _timeToEnd;
            }

            set
            {
                _timeToEnd = value;
                if (value.TotalMilliseconds <= 0)
                {
                    StopTimer();
                    //действия при окончании таймера
                }

                OnPropertyChanged("StringCountdown");
            }
        }

        public string StringCountdown
        {
            get
            {
                var frmt = TimeToEnd.Minutes < 1 ? "ss\\.ff" : "mm\\:ss";
                return $"Оставшееся время: {_timeToEnd.ToString(frmt)}";
            }
        }

        public bool TimerIsEnabled
        {
            get { return _timer.IsEnabled; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void StopTimer()
        {
            if (TimerIsEnabled)
                _timer.Stop();
            TimeToEnd = _startTimeSpan;
        }

        private void StartTimer(DateTime sDate)
        {
            _startCountdown = sDate;
            _timer.Start();
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
            this.CurrUser = CurrUser;
            this.Test = Test;

            Time.DataContext = this;
            Title.DataContext = Test;

            questions = Helper.connect.Questions.Where(x => x.ID_Test == Test.ID).ToList();

            _startTimeSpan = Tend;
            _timer = new DispatcherTimer();
            _timer.Interval = _interval;
            _timer.Tick += delegate
            {
                var now = DateTime.Now;
                var elapsed = now.Subtract(_startCountdown);
                TimeToEnd = _startTimeSpan.Subtract(elapsed);
            };
            StartTimer(DateTime.Now);
            Update_Question();
        }
        public void Update_Question()
        {
            CurrQues = questions[counter];
            answers = Helper.connect.Answers.Where(x => x.ID_Question == CurrQues.ID).ToList();
            CorrAns = answers.Where(x => x.Correctness == 1).First().Contents;
            if (CurrQues == questions.Last()) { b1.Visibility = Visibility.Collapsed; b2.Visibility = Visibility.Visible; }

            Question.DataContext = CurrQues;
            ans1.DataContext = answers[0];
            ans2.DataContext = answers[1];
            ans3.DataContext = answers[2];
            ans4.DataContext = answers[3];
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
            double tem = (double)NumCorrAns / (double)questions.Count();
            Helper.frame.Navigate(new TestFinal(tem));
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
