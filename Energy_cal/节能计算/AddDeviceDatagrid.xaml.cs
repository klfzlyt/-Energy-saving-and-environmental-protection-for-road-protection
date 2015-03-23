using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using 公路养护工程能耗计算软件ECMS.Model;
using 公路养护工程能耗计算软件ECMS.Converters;
using 公路养护工程能耗计算软件ECMS.Service;
using GalaSoft.MvvmLight.Command;
namespace 公路养护工程能耗计算软件ECMS
{
    /// <summary>
    /// AddDeviceDatagrid.xaml 的交互逻辑
    /// </summary>
    public partial class AddDeviceDatagrid : Window
    {
      
        ObservableCollection<ListElement> listelements = new ObservableCollection<ListElement>();      
        CollectionViewSource ListSource;
        XmlDataProvider xdp;
        XmlDataProvider Entityxdp;
        public XmlDataProvider Entityxdp1
        {
            get { return Entityxdp; }
            set { Entityxdp = value; }
        }
        private XmlDataProvider templatexdp;
        public AddDeviceDatagrid(XmlDataProvider XDP):this()
        {
            templatexdp = XDP;
          
        }
        private RelayCommand<object> _addexternxmlitem;

        /// <summary>
        /// Gets the AddExternXmlItem.
        /// </summary>
        public RelayCommand<object> AddExternXmlItem
        {
            get
            {
                return _addexternxmlitem
                    ?? (_addexternxmlitem = new RelayCommand<object>(
                                          button =>
                                          {

                                          }));
            }
        }
        public AddDeviceDatagrid()
        {
            InitializeComponent();

            xdp = maingrid.TryFindResource("xdp") as XmlDataProvider;
            #region 处理所有按钮
            this.AddHandler(Button.ClickEvent,
            new RoutedEventHandler(
                delegate(object s, RoutedEventArgs e)
                {
                    var btn = e.OriginalSource as Button;
                    if (btn != null)
                    {
                        //ICollectionView view = CollectionViewSource.GetDefaultView(listelements);
                        //try
                        //{
                          
                        //    Debug.WriteLine((view.CurrentItem as ListElement).Name);
                        //}
                        //catch (Exception)
                        //{
                            
                            
                        //}
                       
                    }
                })
                ); 
            #endregion
            Entityxdp = new XmlDataProvider();
            Entityxdp.IsAsynchronous = false;
            Entityxdp.Source = new Uri(GlobalResource.RelativeEntitydevices,UriKind.Relative);
          
           
            this.DataContext = this;
            Elementconverterr converter1 = new Elementconverterr();
            ElementConverter converter=new ElementConverter();
           PropertyGroupDescription toadd=     new PropertyGroupDescription("Xmlelement",converter);
            PropertyGroupDescription toadd1 = new PropertyGroupDescription("Xmlelement", converter1);
            ListSource = new CollectionViewSource();
            ListSource.Source = listelements;
            ListSource.GroupDescriptions.Add(toadd);
            ListSource.GroupDescriptions.Add(toadd1);
           // this.DataContext = listelements;
            Binding binder = new Binding();
            binder.Source = ListSource;
            listbox.SetBinding(ItemsControl.ItemsSourceProperty, binder);


         

        }

       
      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            var btn = e.OriginalSource as Button;
            if (btn != null)
            {
                var attribute=btn.Tag  as XmlAttribute;
                if (attribute != null)
                {
                  
                    var element = attribute.OwnerElement;

                    //element.HasChildNodes
                    if (element.HasAttribute("HasElement"))
                    {
                        foreach (XmlNode xmlelement in element.ChildNodes)
                        {
                            var xmleleme = xmlelement as XmlElement;
                            ListElement listele1 = new ListElement() { Xmlelement = xmleleme,  EntityDevices = Entityxdp };
                            listelements.Add(listele1);
                        }
                        return;
                    }

                    ListElement listele = new ListElement() { Xmlelement = element,  EntityDevices = Entityxdp};
                    listelements.Add(listele);


               //  var XmlElement=   xdp.Document.DocumentElement;
               //var xmlelement=  XmlElement.SelectSingleNode("Section/Device/DeviceItem[@Name="+"\""+attribute.Value+"\""+"]") as XmlElement;
               //xmlelement.SetAttribute("Display","false");

               //new XmlService().UpataDataNottoSaveTolocal(xdp);
                }
            }
         
          
        }

     

        private void listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Debug.WriteLine(listbox.SelectedItem);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var btn = e.OriginalSource as Button;
            if (btn != null)
            {
                var element = btn.Tag as ListElement;
                if (element != null)
                {

                    listelements.Remove(element);


                }
            
            
            }



        }

        private void DevicesDetailslistbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           var selectedxmlelement= DevicesDetailslistbox.SelectedItem as XmlElement;
          //  listelements
            //把所有含有IsBinding的项目挑出来
           foreach (ListElement listele in listelements)
           {
               var xmlele = listele.Xmlelement;
               if (xmlele.HasAttribute("IsBinding"))
               {
                   var str = xmlele.Attributes["IsBinding"].Value;
                   if(selectedxmlelement.HasAttribute(str))
                   listele.Text=   selectedxmlelement.Attributes[str].Value;
               }
           
           }


        }


        //这个是保存按钮（Groupitem内）的事件
        //按理说删除按钮的Click应该会冒泡到GroupItem上 而Groupitem内的按钮应该也会冒泡到GroupItem上
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            Mouse.OverrideCursor = Cursors.Wait;
            var infomation = (sender as Button).Tag as string;
            var entitydoc = Entityxdp.Document;
            var templatedoc = xdp.Document;
            var rootentity = entitydoc.DocumentElement;
            var roottemplate = templatedoc.DocumentElement;
            var specialdevicelist = roottemplate.SelectSingleNode("//Device[@Name=" + "\"" + infomation + "\"" + "]");//获得具体设备的细节
            var entitytocloneele = rootentity.GetElementsByTagName("Device")[0];//获得第一个只是用来复制
            var entitytoaddele = entitytocloneele.CloneNode(true);//复制一个节点
            var hasbindingelements = specialdevicelist.SelectNodes("./DeviceItem[@IsBinding]");


            XmlDocument doc = templatexdp.Document;
          //  doc.Load(@"../../OriginalXml/FixedItemsTemplate.xml");
          //  var rootelement=doc.DocumentElement;

            string namestring="";
            string typestring="";
            foreach (ListElement listele in listelements)
            {
                var xmlelement = listele.Xmlelement;              
                var str = listele.Text;
                var xmltext = templatedoc.CreateTextNode(str);
                xmlelement.AppendChild(xmltext);
              
                
                
                   // var Idstring = xmlelement.Attributes["ID"].Value;
                    var Namestring = xmlelement.Attributes["Name"].Value;
                 //   var Tooltipstring = xmlelement.Attributes["Tooltip"].Value;
                //    var Unitstring = xmlelement.Attributes["Unit"].Value;
                //    var Iscalculator = xmlelement.Attributes["IsCalculator"].Value;
                    if(Namestring =="设备名称")
                    {
                        namestring =str;
                        
                    }
                    if (Namestring == "型号")
                    {
                        typestring=str;
                    }
                



        //        BuildElementForGrid(doc,Namestring,Idstring,)
                if (xmlelement.HasAttribute("IsBinding"))
                {

                    if (string.IsNullOrWhiteSpace(str))
                    {
                        MessageBox.Show(xmlelement.Attributes["Name"].Value+"项   请输入内容");
                        continue;
                    }
                   var str11 = xmlelement.Attributes["IsBinding"].Value;
                   entitytoaddele.Attributes[str11].Value = str;
                }
               
                
                
            }
            
            foreach (ListElement elementt in listelements)
            {
                var xmlele = elementt.Xmlelement;
                var ele = BuildElementForGrid(doc, xmlele.Attributes["Name"].Value, infomation, namestring, typestring, elementt.Text, xmlele.Attributes["Tooltip"].Value, xmlele.Attributes["Unit"].Value, xmlele.Attributes["IsCalculator"].Value);
                if (ele.Attributes["Name"].Value == "设备名称" || ele.Attributes["Name"].Value == "型号")
                    continue;

                doc.DocumentElement.AppendChild(ele);
            
            }
            //下面这个判断条件不完善
            //TODO
            //更新实体Device
            if (entitytoaddele.Attributes["Name"].Value != entitytocloneele.Attributes["Name"].Value)
            { 
             entitytocloneele.ParentNode.InsertAfter(entitytoaddele, entitytocloneele);
             EnumVisual(listbox);
             MessageBox.Show("保存成功！");
            }
            //rootentity.AppendChild(entitytoaddele);
         
           



            Mouse.OverrideCursor = null;
       // VisualTreeHelper.getch
            //找到Combox 然后替换他们的ItemsSource

            //拿到用户的输入
            //Entityxdp
           // xdp
          //listelements
        }
        //先序递归遍历可视树
         private  void EnumVisual(Visual myVisual)
        {
            FrameworkElement fe = myVisual as FrameworkElement;
            if (fe != null)
            {
                Debug.WriteLine("FrameworkElment+ " + fe.GetType().Name);
                if (fe.GetType() == typeof(ComboBox))
                {
                    var combox = fe as ComboBox;
                    var listelement = combox.Tag as ListElement;
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








         //  <Item Name="实际工作能耗" Background="appbar_calculator" Isprimarykey="True" DeviceName="设备名称：三型摊铺AS300-TS"  Type="AS300" DefaultValue="" IsCaculator="True" Tooltip="写明燃料类型和单位" Group="厂拌热再生设备" />
         //  <Item Name="设备名称：" DefaultValue="" Tooltip="" NoneTooltip="无" Group="厂拌热再生设备" IsDisplay="false" DeviceName="设备名称：三型摊铺AS300-TS"  Type="AS300"/>
         private XmlElement BuildElementForGrid(XmlDocument doc, string Name, string Group, string DeviceName, string Type, string DefaultValue = "", string Tooltip = "", string Unit = "", string IsCaculator="false")
         {
             var element = doc.CreateElement("Item");
             var attri1 = doc.CreateAttribute("Name");
             attri1.Value = Name;
             var attri2 = doc.CreateAttribute("Group");
             attri2.Value = Group;
             var attri3 = doc.CreateAttribute("DeviceName");
             attri3.Value = DeviceName;
             var attri4 = doc.CreateAttribute("Type");
             attri4.Value = Type;
             var attri5 = doc.CreateAttribute("DefaultValue");
             attri5.Value = DefaultValue;
             var attri6 = doc.CreateAttribute("IsDisplay");
             attri6.Value = "true";
             var attri7 = doc.CreateAttribute("Tooltip");
             attri7.Value = Tooltip;
             var attri8 = doc.CreateAttribute("Unit");
             attri8.Value = Unit;
             var attri9 = doc.CreateAttribute("IsCaculator");
             attri9.Value = IsCaculator;
             element.Attributes.Append(attri1);
             element.Attributes.Append(attri2);
             element.Attributes.Append(attri3);
             element.Attributes.Append(attri4);
             if (Name == "型号")
             {
                 attri5.Value = string.Empty;//DefaultValue
                 attri6.Value = "false";//IsDisplay
             }
             if (Name == "设备名称")
             {
                 attri5.Value = string.Empty;//DefaultValue
                 attri6.Value = "false";//IsDisplay
             }
             element.Attributes.Append(attri5);
             element.Attributes.Append(attri6);
             element.Attributes.Append(attri7);
             element.Attributes.Append(attri8);
             element.Attributes.Append(attri9);
             return element;

         }

    }
}
