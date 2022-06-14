using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace FakturyPro.Konwertery
{
    [ValueConversion(typeof(double),typeof(double))]
    public class ListWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double param = 0;
            if ( ! double.TryParse((string) parameter, out param))
            {
                throw new ArgumentException();            
            }
            return (double)value - param;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double param = 0;
            if (!double.TryParse((string)parameter, out param))
            {
                throw new ArgumentException();
            }
            return (double)value + param;
        }
    }
}
