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

        private void PauseTimer()
        {
            _timer.Stop();
            _pauseTime = DateTime.Now;
        }

        private void ReleaseTimer()
        {
            var now = DateTime.Now;
            var elapsed = now.Subtract(_pauseTime);
            _startCountdown = _startCountdown.Add(elapsed);
            _timer.Start();
        }


        public string CorrAns { get; set; }
        public double CountQues { get; set; }
        public int IDTest { get; set; }
        public string TestName {  get; set; }
        public PassTest(int idTest, TimeSpan Tend)
        {
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


            InitializeComponent();

            Time.DataContext = this;
            TestName = Helper.connect.Tests.FirstOrDefault(x => x.ID == idTest).Name;
            Title.DataContext = this;

            List<Questions> questions = (List<Questions>)Helper.connect.Questions.Where(x => x.ID_Test == idTest).ToList().AsEnumerable();
            Questions CurrQues = questions[Params.counter];
            if( CurrQues == questions.Last()) { b1.Visibility = Visibility.Collapsed; b2.Visibility = Visibility.Visible; }
            List<Answers> answers = (List<Answers>)Helper.connect.Answers.Where(x => x.ID_Question == CurrQues.ID).ToList().AsEnumerable();

            CorrAns = answers.Where(x => x.Correctness == "да" || x.Correctness == "да   ").First().Contents;
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
            Helper.frame.Navigate(new PassTest(IDTest, TimeToEnd));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if ((string)Params.pressed.Content == CorrAns)
            {
                Params.NumCorrAns += 1;
            }
            Helper.connect.Results.Add(new Results { ID_Test = IDTest, ID_User = Params.user.ID, Per_Complete = Params.NumCorrAns / CountQues});
            //Helper.connect.SaveChanges();
            double tem = Params.NumCorrAns / CountQues;
            Params.counter = 0;
            Params.NumCorrAns = 0;
            Helper.frame.Navigate(new TestFinal(tem));
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CorrAns = null;
            CountQues = 0;
            IDTest = 0;
            TestName = null;
            Params.counter = 0;
            Params.NumCorrAns = 0;
            Helper.frame.Navigate(new MainMenu_Student(Params.user));
        }
    }
}
