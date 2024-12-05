using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Tests_application.Converters
{
    internal class PercentConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (value is double per)
                {
                    return per.ToString("P0", CultureInfo.InvariantCulture);
                }
                else
                {
                    HashSet<Results> res = (HashSet<Results>)value;
                    double com = 0;
                    foreach (Results a in res)
                    {
                        com = (double)a.Per_Complete;
                    }
                    return com.ToString("P0", CultureInfo.InvariantCulture);
                }

            }
            else
            {
                return "nan";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
