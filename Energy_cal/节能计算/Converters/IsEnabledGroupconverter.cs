using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace 节能计算.Converters
{
  public   class IsEnabledGroupconverter:IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string str =value as string;
            string para=parameter as string;
            if (str == "燃料一" || str == "燃料二" || str == "实际工程项目")
            {
                //不显示的
                if (para == "bool")

                    return false;
                if (para == "double")
                    return (double)0;
                if (para == "string")
                    return value;
                return value;

            }
          
                //不显示的
                if (para == "bool")

                    return false;
                if (para == "double")
                    return (double)0;
                if (para == "string")
                    return "参数";
                return value;

            
         
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
