using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using 节能计算.Converters;
using 节能计算.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System.Diagnostics;
using MahApps.Metro.Controls;
namespace 节能计算
{
    /// <summary>
    /// AddDeviceItem.xaml 的交互逻辑
    /// </summary>
    public partial class AddDeviceItem : MetroWindow
    {
        private AddDeviceViewModel _vm;
        private XmlElement _devicedbxml;
        public AddDeviceItem()
        {
            InitializeComponent();
            //var element = App.AddDevicesdoc.SelectSingleNode("Devices/Section[1]/Device[2]") as XmlElement;
            //var element1 = App.Devicesdoc.SelectSingleNode("Sections/Section[1]/Devices[2]") as XmlElement;
            //var element3 = App.DataGriddoc.SelectSingleNode("Sections/Section[1]") as XmlElement;
            //_vm = new AddDeviceViewModel(element1, element3, element);
            //Elementconverterr converter1 = new Elementconverterr();
            //ElementConverter converter = new ElementConverter();
            //PropertyGroupDescription toadd = new PropertyGroupDescription("Xmlelement", converter);
            //PropertyGroupDescription toadd1 = new PropertyGroupDescription("Xmlelement", converter1);
            //var _listSource = new CollectionViewSource();
            //_listSource.Source = _vm.ExtendXmlElementList;
            //_listSource.GroupDescriptions.Add(toadd);
            //_listSource.GroupDescriptions.Add(toadd1);
            //// this.DataContext = listelements;
            //Binding binding = new Binding();
            //binding.Source = _listSource;
            //listbox.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            //this.DataContext = _vm;
        }
        public AddDeviceItem(XmlElement devicedbxml,XmlElement finalxml,XmlElement devicecategory,string windowtitle,string dbxpath=""):this()
        {
            this.Title = windowtitle;
            _devicedbxml = devicedbxml;
            _vm = new AddDeviceViewModel(devicedbxml, finalxml, devicecategory);
            Elementconverterr converter1 = new Elementconverterr();
            ElementConverter converter = new ElementConverter();
            PropertyGroupDescription toadd = new PropertyGroupDescription("Xmlelement", converter);
            PropertyGroupDescription toadd1 = new PropertyGroupDescription("Xmlelement", converter1);
            var  _listSource = new CollectionViewSource();
            _listSource.Source = _vm.ExtendXmlElementList;
            _listSource.GroupDescriptions.Add(toadd);
            _listSource.GroupDescriptions.Add(toadd1);
            // this.DataContext = listelements;
            Binding binding = new Binding();
            binding.Source = _listSource;
            listbox.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            this.DataContext = _vm;
            this.Loaded += AddDeviceItem_Loaded;


            XmlDataProvider _xdpdb=new XmlDataProvider();
           _xdpdb.Document=_devicedbxml.OwnerDocument;
           _xdpdb.XPath = dbxpath;
            Binding bindingdb=new Binding();
            bindingdb.Source = _xdpdb;
            DevicesDetailslistbox.SetBinding(ItemsControl.ItemsSourceProperty, bindingdb);
            Messenger.Default.Register<NotificationMessage>(this,
                (notificationmessage)=>
                    {
                        Mouse.OverrideCursor = Cursors.Wait;
                        if (notificationmessage.Notification == "UpdateCombox")
                        {
                            EnumVisual(listbox);
                        }
                        Mouse.OverrideCursor = null;
                    }
                );
        }


        #region 私有方法
        private void EnumVisual(Visual myVisual)
        {
            FrameworkElement fe = myVisual as FrameworkElement;
            if (fe != null)
            {
                Debug.WriteLine("FrameworkElment+ " + fe.GetType().Name);
                if (fe.GetType() == typeof(ComboBox))
                {
                    var combox = fe as ComboBox;
                    var listelement = combox.Tag as ExtendXmlElementViewmodel;
                    try
                    {
                        combox.ItemsSource = (XmlNodeList)new EntityConverter().Convert(listelement, null, null, null);
                    }
                    catch
                    { }
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

        void AddDeviceItem_Loaded(object sender, RoutedEventArgs e)
        {
            //Step1:从VM中拿到element
            var element = _vm.DeviceCategoryXmlElement;
            //Step2:获得预先加载项（预先加载项需要在属性里面标记）
            var preloadedelments = element.SelectNodes("DeviceItem[@IsPreLoaded=\"true\"]");
            foreach (XmlNode xmlnode in preloadedelments)
            {
                var xmlelement = xmlnode as XmlElement;
                _vm.ExtendXmlElementList.Add(new ExtendXmlElementViewmodel(_devicedbxml, xmlelement));
                //载入预先加载项
            }
        }

        private void _btnadd_Click(object sender, RoutedEventArgs e)
        {
            var btn = e.OriginalSource as Button;
            if (btn != null)
            {
                var attribute = btn.Tag as XmlAttribute;
                if (attribute != null)
                {

                    var element = attribute.OwnerElement;

                    //element.HasChildNodes
                    if (element.HasAttribute("HasElement"))
                    {
                        foreach (XmlNode xmlelement in element.ChildNodes)
                        {
                            var xmleleme = xmlelement as XmlElement;
                            ExtendXmlElementViewmodel listele1 = new ExtendXmlElementViewmodel(_devicedbxml, xmleleme);
                            _vm.ExtendXmlElementList.Add(listele1);
                        }
                        return;
                    }

                    ExtendXmlElementViewmodel listele = new ExtendXmlElementViewmodel(_devicedbxml, element);
                    _vm.ExtendXmlElementList.Add(listele);
                }
            }
        }

        private void _deletebtn_Click(object sender, RoutedEventArgs e)
        {
            var btn = e.OriginalSource as Button;
            if (btn != null)
            {
                var element = btn.Tag as ExtendXmlElementViewmodel;
                if (element != null)
                {
                    _vm.ExtendXmlElementList.Remove(element);
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var menuitem = sender as MenuItem;
            if (menuitem != null)
            {
                var xmlelement = menuitem.Tag as XmlElement;
                _devicedbxml.RemoveChild(xmlelement);
            }
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = Xceed.Wpf.Toolkit.MessageBox.Show(this, "是否将设备添加到主窗口？", "关闭窗口", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Yes);

            if (result == MessageBoxResult.Yes)
            {

                // _vm.SaveDeviceCommand.Execute("");
                _vm.AddItemCommand.Execute("save");
                return;
            }
            if (result == MessageBoxResult.No)
            {

                e.Cancel = false;
            }
            if (result == MessageBoxResult.Cancel)
            {

                e.Cancel = true;
            }
        }
        #endregion

      


    }
}
