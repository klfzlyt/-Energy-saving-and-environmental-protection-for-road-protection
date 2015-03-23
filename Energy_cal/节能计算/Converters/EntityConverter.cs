using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using 公路养护工程能耗计算软件ECMS.Model;
using 公路养护工程能耗计算软件ECMS.ViewModel;
namespace 公路养护工程能耗计算软件ECMS.Converters
{
  public    class EntityConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ExtendXmlElementViewmodel extendlistele = value as ExtendXmlElementViewmodel;
            if (extendlistele != null)
            {
                var devicedbelement=extendlistele.DeviceDBXmlElement;
                var element = extendlistele.Xmlelement;
                if (element.HasAttribute("IsBinding"))
                {
                    var bindingstr = element.Attributes["IsBinding"].Value;
                    var attributes = devicedbelement.SelectNodes("Device/@" + bindingstr);

                 return attributes;
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
