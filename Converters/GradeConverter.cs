using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Tests_application.Converters
{
    internal class GradeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int res = (int)value;
            string mode = (string)parameter;
            if(mode == "1")
            {
                if (res > 80) { return "5"; }
                else if (res > 60) { return "4"; }
                else if (res > 40) { return "3"; }
                else { return "2"; }
            }
            else if(mode == "2")
            {
                if (res > 80) { return Brushes.Green;}
                else if (res > 60) { return Brushes.Green; }
                else if (res > 40) { return Brushes.Orange;}
                else { return Brushes.Red; }
            }
            else { throw new NotImplementedException(); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
