using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace FakturyPro.Konwertery
{

    [ValueConversion(typeof(double), typeof(Brush))]
    public class AmountToBackgroundConverter : IValueConverter
    {
        //public double AmountThresholdToHighlight { get; set; }
        public Brush HighlightBrush { get; set; }
        public Brush DefaultBrush { get; set; }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double amount = (double)value;
            if (amount < 0)
                return HighlightBrush;
            else
                return DefaultBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
