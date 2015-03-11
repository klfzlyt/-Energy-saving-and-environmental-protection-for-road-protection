using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace 节能计算.Converters
{
    class BtnVisualConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var str=value as string;
            var str1 = parameter as string;
            if (str == null)
                return null; ;
            if (str == "false")
            {
            
                if (str1 != null)
                { 
                if(str1=="isenabled")
                {
                    return false;
                }
                if (str1 == "opacity")
                {
                    return (double)0;
                }
                return null;
                }
            }
            if (str == "true")
            {
                if (str1 == "isenabled")
                {
                    return true;
                }
             
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
