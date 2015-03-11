using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace 节能计算.Converters
{
  public   class WindowsStateToBoolenConverter : IValueConverter
    {


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            System.Windows.WindowState windowstate= (System.Windows.WindowState)value;
            if (windowstate == System.Windows.WindowState.Maximized)
            {
                return System.Windows.Controls.Orientation.Horizontal;
            }
            else
            {
            return System.Windows.Controls.Orientation.Vertical;
            }
            //value
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
