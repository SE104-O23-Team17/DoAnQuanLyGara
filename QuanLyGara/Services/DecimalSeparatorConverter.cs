using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace QuanLyGara.Services
{
    public class DecimalSeparatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            NumberFormatInfo nfi = new NumberFormatInfo { NumberDecimalSeparator = ",", NumberGroupSeparator = "" };
            double number = (double)value;
            return number.ToString("0.##", nfi);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valueAsString = value as string;
            NumberFormatInfo nfi = new NumberFormatInfo { NumberDecimalSeparator = "," };
            return double.Parse(valueAsString, NumberStyles.Any, nfi);
        }
    }
}
