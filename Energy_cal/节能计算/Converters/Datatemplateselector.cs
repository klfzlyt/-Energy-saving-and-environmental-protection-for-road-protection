using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Xml;
namespace 节能计算.Converters
{
    class Datatemplateselector : DataTemplateSelector 
    {
       public  DataTemplate Normal { get; set; }
       public  DataTemplate Has { get; set; }



        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {

            var xmlelement = item as XmlElement;
            if (xmlelement != null)
            {
                if (xmlelement.HasAttribute("Tooltip"))
                {
                    if (!string.IsNullOrEmpty(xmlelement.Attributes["Tooltip"].Value))
                    {
                        return Has;
                    }
                }
            
            }

            return Normal;
        }


    }
}
