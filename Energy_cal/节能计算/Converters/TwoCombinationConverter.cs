using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using 节能计算.Model;
namespace 节能计算.Converters
{
    public class TwoCombinationConverter : IMultiValueConverter
    {
        private string typekey;
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var valueone = values[0];
            var valuetwo = values[1];
            //要进行遍历操作
            EnumVisual(valuetwo as Visual);

            return new DevicePirmaryKey() { DeviceName = valueone as string,DeviceType=typekey as string };
        }

        private  void   EnumVisual(Visual myVisual)
        {


            if (myVisual is TextBlock)
            {

                var textblockinfor = myVisual as TextBlock;
                if (textblockinfor.Name == "TypeInformation")
                {
                    if (string.IsNullOrEmpty(typekey))
                    typekey = textblockinfor.Text;
                }

            }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(myVisual); i++)
            {
                // Retrieve child visual at specified index value.

                Visual childVisual = (Visual)VisualTreeHelper.GetChild(myVisual, i);

                // Do processing of the child visual object.
          

                // Enumerate children of the child visual object.

                EnumVisual(childVisual);
            }
          
        }



        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
