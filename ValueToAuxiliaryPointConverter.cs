using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace Tests_application
{
    class ValueToAuxiliaryPointConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Contains(DependencyProperty.UnsetValue))
                return DependencyProperty.UnsetValue;
            var v = (double)values[0];
            var min = (double)values[1];
            var max = (double)values[2];
            var r = (double)values[3];
            var ratio = (v - min) / (max - min);
            var angle = Math.PI * ratio;
            var x = 1 - r * Math.Cos(angle);
            var y = 1 - r * Math.Sin(angle);
            return new Point(x, y);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
