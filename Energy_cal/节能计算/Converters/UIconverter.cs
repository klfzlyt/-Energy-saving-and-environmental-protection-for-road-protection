using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Data;
using System.Windows;
using System.Windows.Controls;

namespace 节能计算.Converters
{
    class UIconverter:IValueConverter
    {


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter as string != null)
            {
               
                var str = parameter as string;
                if ((bool)value == true)
                {
                    return (Canvas)Application.Current.FindResource("appbar_book_open");

                }
                else
                {
                    return (Canvas)Application.Current.FindResource("appbar_book_perspective");
                }
            }
            string location = (string)value;
           return (Canvas)Application.Current.FindResource(location);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
