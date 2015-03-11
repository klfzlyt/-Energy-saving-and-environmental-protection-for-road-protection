using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Collections.ObjectModel;
namespace 节能计算
{
    /// <summary>
    /// CustomDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class CustomDataGrid : UserControl
    {
       
        public CustomDataGrid()
        {
            InitializeComponent();
            XmlDataProvider xdp = new XmlDataProvider();
            xdp.IsAsynchronous = false;
          //  xdp.Document = App.DataGriddoc;
            xdp.XPath = "Sections/Section[1]/Item";
            var cvs=new CollectionViewSource();
            cvs.Source =xdp;
            PropertyGroupDescription group1 = new PropertyGroupDescription("@Group");
            PropertyGroupDescription group2 = new PropertyGroupDescription("@DeviceName");
            PropertyGroupDescription group3 = new PropertyGroupDescription("@Type");
            cvs.GroupDescriptions.Add(group1);
            cvs.GroupDescriptions.Add(group2);
            cvs.GroupDescriptions.Add(group3);
            Binding binding=new Binding();
            binding.Source=cvs;
            MainGrid.SetBinding(ItemsControl.ItemsSourceProperty, binding);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var addgridxml = App.AddDevicesdoc.SelectSingleNode("Devices/Section[1]/Device[1]") as XmlElement;//Devices
            //var devicexml = App.Devicesdoc.SelectSingleNode("Sections/Section[1]/Devices[1]") as XmlElement;//EntityDevices
            //var ownerxml = App.DataGriddoc.SelectSingleNode("Sections/Section[1]") as XmlElement;//FixedItemsTemplate
            //var window = new AddDeviceItem(devicexml, ownerxml, addgridxml);
            //window.ShowDialog();
        }
    }
}
