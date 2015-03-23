using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Xml;
namespace 公路养护工程能耗计算软件ECMS.Converters
{
  public   class ElementConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            var element = value as XmlElement;
            var strpara = parameter as string;
            if (element != null)
            {


                if (strpara != null)
                {
                    try
                    {
                        return element.Attributes[strpara].Value;
                    }
                    catch (Exception)
                    {
                        return value;
                       
                    }
                   
                }
                else
                try
                {
                    return element.Attributes["Group"].Value;
                }
                catch (Exception)
                {
                    return value;

                }
            }
            return value;
                
                
                
            }
          
        
    

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
