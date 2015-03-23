using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Xml;

namespace 公路养护工程能耗计算软件ECMS.Converters
{
    class Elementconverterr:IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var element = value as XmlElement;
          
            try
            {
                if ((element.ParentNode as XmlElement).HasAttribute("Constant"))
                {
                    return (element.ParentNode as XmlElement).Attributes["Constant"].Value;
                }
                else
                return (element.ParentNode as XmlElement).Attributes["Name"].Value;
            }
            catch (Exception)
            {
                return value;
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
