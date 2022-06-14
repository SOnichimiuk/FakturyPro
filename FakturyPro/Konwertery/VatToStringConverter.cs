using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using FakturyPro.Klasy;
using System.Windows.Controls;

namespace FakturyPro.Konwertery
{
    [ValueConversion(typeof(Towar.StawkaVat), typeof(ComboBoxItem))]
    public class VatToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((Towar.StawkaVat)value)
            {
                case Towar.StawkaVat.Vat23:
                    return "23";
                case Towar.StawkaVat.Vat8:
                    return "8";
                case Towar.StawkaVat.Vat5:
                    return "5";
            }
            return "23";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int val = 23;
            int.TryParse(((ComboBoxItem)value).Content.ToString(), out val);
            switch (val)
            {
                case 23:
                    return Towar.StawkaVat.Vat23;
                case 8:
                    return Towar.StawkaVat.Vat8;
                case 5:
                    return Towar.StawkaVat.Vat5;
            }
            return Towar.StawkaVat.Vat23;
        }
    }
}
