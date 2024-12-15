using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Tests_application.Connect;

namespace Tests_application.Converters
{
    internal class NameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int testnum = (int)value;
            List<Tests> test = Helper.connect.Tests.Where(x => x.ID == testnum).ToList();
            foreach(var a in test)
            {
                return a.Name;
            }
            return "non";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
