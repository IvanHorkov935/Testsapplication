using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для TestFinal.xaml
    /// </summary>
    public partial class TestFinal : Page, INotifyPropertyChanged
    {
        public int Result { get; set; }
        public int ResultForProgress { get; set; }
        private double _resfortext;
        public double ResForText
        {
            get
            {
                return _resfortext;
            }
            set
            {
                _resfortext = value;
                OnPropertyChanged("ResForText");
            }
        }
        public TestFinal(double Res)
        {
            InitializeComponent();

            Result = (int)(Res * 100);
            GradeNumber.DataContext = this;
            ResultForProgress = (int)(Res * 100);
            ResBlock.DataContext = this;
        }

        private void Window_OnLoaded(object sender, RoutedEventArgs e)
        {
            Action action = () => { Progress1.Value++; ResForText += 0.01; };
            var task = new Task(() =>
            {
                for (int i = 0; i < ResultForProgress; i++)
                {
                    Progress1.Dispatcher.Invoke(action);
                    Thread.Sleep(2 + (i));
                }
            });
            task.Start();

            //if (Result > 80) { Grade.Background = Brushes.Green; Grade.Text = "5"; }
            //else if (Result > 60) { Grade.Background = Brushes.Green; Grade.Text = "4"; }
            //else if (Result > 40) { Grade.Background = Brushes.Orange; Grade.Text = "3"; }
            //else { Grade.Background = Brushes.Red; Grade.Text = "2"; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Helper.frame.Navigate(new MainMenu_Student());
        }
    }
}
