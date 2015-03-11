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
using System.Windows.Shapes;
using 节能计算.ViewModel;
using System.Diagnostics;
using 节能计算.Service;
using System.Xml;
using System.Xml.Linq;
using 节能计算.Model;
namespace 节能计算
{
    /// <summary>
    /// ForTest.xaml 的交互逻辑
    /// </summary>
    public partial class ForTest : Window
    {
     
     
        ResultsViewModel vm;
        CollectionViewSource cvs;
     
        public ForTest()
        {
            
           
            InitializeComponent();
            vm = new ResultsViewModel(@"../../OriginalXml/Finalitems.xml");
           //  vm.Xdp.XPath = "/Sections/Section[@Name=\"混合料生产\"]/Item";
            vm.Xdp.XPath = "Sections/Section[1]/Item";
            //这些只是用来测试的 上面的Xpath要变
            this.DataContext = vm;
      //    MainGrid.row
         // DataGrid

            cvs = maingrid.TryFindResource("cvs") as CollectionViewSource;


            //MainGrid.LoadingRow += new EventHandler<DataGridRowEventArgs>(MainGrid_LoadingRow);
            //CommandBinding cb = new CommandBinding();
            //cb.Command = ApplicationCommands.Open;
            //cb.Executed += new ExecutedRoutedEventHandler(cb_Executed);
            //this.CommandBindings.Add(cb);
            //CommandBinding cb1 = new CommandBinding();
            //cb1.Command = ApplicationCommands.Print;
            //cb1.Executed += new ExecutedRoutedEventHandler(cb1_Executed);
            //this.CommandBindings.Add(cb1);

        }

        //void cb1_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    var btn = e.OriginalSource as Button;
        //    var tag = btn.Tag as string;
        //    new XmlService().DeleteElements(vm.Xdp, "/Sections/Section[@Name=\"混合料生产\"]/Item[@Group=\""+tag+"\"]", GlobalResource.RelativeFixedItemsPath);
        //}

   

        void cb_Executed(object sender, ExecutedRoutedEventArgs e)
        {
         //   throw new NotImplementedException();


            //new XmlService().AddXElement(vm.Xdp, new System.Xml.Linq.XElement("结果类别",
            // new System.Xml.Linq.XAttribute("Name", "test")
            //), "结果", GlobalResource.RelativeResultsPath);
         //   MainGrid.ItemContainerGenerator.ContainerFromIndex(3);
          
         
            //new XmlService().AddXElement(vm.Xdp,new System.Xml.Linq.XElement("结果项",
            //    new System.Xml.Linq.XAttribute("Name","test"),
            //    new System.Xml.Linq.XAttribute("calculator","test")),"结果类别",GlobalResource.RelativeResultsPath,"Name","test");
            //var doc = new XmlDocument();
            //doc.Load(GlobalResource.RelativeDevicesPath);
            //var deviceitems = doc.DocumentElement.SelectNodes("//Device[@Name=\"厂拌热再生设备\"]/descendant::DeviceItem[not(@HasElement)]");//厂拌冷再生设备
            //var deviceitemsnew = deviceitems.Cast<XmlElement>();
            //var items = deviceitemsnew.Select(
            //    (xmlelementnew) =>
            //    {
                    
            //            return new XElement("Item",
            //                new XAttribute("Name", xmlelementnew.Attributes["Name"].Value),
            //                new XAttribute("DefaultValue", xmlelementnew.InnerText),
            //                new XAttribute("Tooltip", xmlelementnew.Attributes["Tooltip"].Value),
            //                new XAttribute("Group", "厂拌热再生设备")
            //                );
            //    });
            //foreach (XElement xelement in items)
            //{
            //    new XmlService().AddXElement(vm.Xdp, xelement, "Section", GlobalResource.RelativeFixedItemsPath, "Name", "混合料生产");
            //}

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var addwindow = new AddDeviceDatagrid(vm.Xdp);
            //addwindow.Owner = this;
            //addwindow.ShowDialog();
            //var element = App.AddDevicesdoc.SelectSingleNode("Devices/Section[1]/Device[2]") as XmlElement;
            //var element1 = App.Devicesdoc.SelectSingleNode("Sections/Section[1]/Devices[1]") as XmlElement;
            //var addwidn = new AddDeviceItem(element1, null, element);
            //addwidn.Show();

        }

        private void InnerBtn_Click(object sender, RoutedEventArgs e)
        {
            var fe=sender as Button;
            var xmlelement = fe.Tag as XmlElement;
            if (fe.Name == "InnerBtnDelete")
            {
                xmlelement.ParentNode.RemoveChild(xmlelement);
            
            }
        }



        /// <summary>
        /// grouptest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Devicetest_Click(object sender, RoutedEventArgs e)
        {
            //Debug.WriteLine((cvs.View.CurrentItem as XmlElement).Attributes["Name"].Value);
            //var view=CollectionViewSource.GetDefaultView(MainGrid.ItemsSource);
            //Debug.WriteLine((view.CurrentItem as XmlElement).Attributes["Name"].Value);
            //var groups=view.Groups;
            
         //   Debug.WriteLine(cvs.View.CurrentItem);

            if (e.OriginalSource is Button)
            {
                var btn = e.OriginalSource as Button;
                if (btn.Name == "Devicetest")
                {
                    var tag = btn.Tag as DevicePirmaryKey;
                    Debug.WriteLine("设备名称：" + tag.DeviceName);
                    Debug.WriteLine("设备型号：" + tag.DeviceType);
                }
            }

        }

      
    }
}
