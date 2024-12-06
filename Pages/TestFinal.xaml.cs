using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

namespace Tests_application.Pages
{
    /// <summary>
    /// Логика взаимодействия для TestFinal.xaml
    /// </summary>
    public partial class TestFinal : Page
    {
        public string Value { get; set; }
        public TestFinal()
        {
            InitializeComponent();

            double com = 0.7; 
            Value = com.ToString("P0", CultureInfo.InvariantCulture);
            Progress1.Value = com * 100;
            Perc.Text = Value;
        }
    }
}
