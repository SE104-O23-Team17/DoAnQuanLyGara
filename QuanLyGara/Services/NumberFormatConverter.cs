using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace QuanLyGara.Services
{
    public class NumberFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CultureInfo ci = new CultureInfo("vi-VN");
            double number = (double)value;
            return number.ToString("N0", ci);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valueAsString = value as string;
            if (string.IsNullOrEmpty(valueAsString))
            {
                return 0;
            }
            valueAsString = valueAsString.Replace(".", "");
            return double.Parse(valueAsString, NumberStyles.Any);
        }

    }
}
