using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using 节能计算.Model;
namespace 节能计算
{
    /// <summary>
    /// CustomParaDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class CustomParaDataGrid : UserControl
    {
      
        private string xpath;
        public string Xpath 
        {
            get
            {
                return xpath;
            }
            set
            {
                xpath = value;
               xdp.XPath = value;
            }
        }
        private XmlDocument doc;
        public XmlDocument Doc
        {
            get
            {
                return doc;
            }
            set
            {
                doc = value;
                xdp.Document = value;
            }
        }



        public XmlDocument MyDoc
        {
            get { return (XmlDocument)GetValue(MyDocProperty); }
            set {
                xdp.Document = value;    
                SetValue(MyDocProperty, value); 
            
            }
        }

        // Using a DependencyProperty as the backing store for MyDoc.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyDocProperty =
            DependencyProperty.Register("MyDoc", typeof(XmlDocument), typeof(CustomParaDataGrid), new PropertyMetadata(null,
                new PropertyChangedCallback(propertychanged)));




        static void propertychanged(DependencyObject denpendencyobject, DependencyPropertyChangedEventArgs dpce)
        {
            var datagrid = denpendencyobject as CustomParaDataGrid;
            datagrid.xdp = new XmlDataProvider();
            datagrid.xdp.IsAsynchronous = false;
            datagrid.xdp.XPath = datagrid.Xpath;
            datagrid.xdp.Document = dpce.NewValue as XmlDocument;
            datagrid.cvs = new CollectionViewSource();
            datagrid.cvs.Source = datagrid.xdp;
            PropertyGroupDescription group1 = new PropertyGroupDescription("@Group");
            PropertyGroupDescription group2 = new PropertyGroupDescription("@DeviceName");
            PropertyGroupDescription group3 = new PropertyGroupDescription("@Type");
            datagrid.cvs.GroupDescriptions.Add(group1);
            datagrid.cvs.GroupDescriptions.Add(group2);
            datagrid.cvs.GroupDescriptions.Add(group3);
            Binding binding = new Binding();
            binding.Source = datagrid.cvs;
            datagrid.MainGrid.SetBinding(ItemsControl.ItemsSourceProperty, binding);
        }

        XmlDataProvider xdp;
        CollectionViewSource cvs;      
        public CustomParaDataGrid()
        {
            InitializeComponent();

        }
        //private void InnerBtnDelete_Click(object sender, RoutedEventArgs e)
        //{




        //    //var fe = sender as Button;
        //    //var xmlelement = fe.Tag as XmlElement;
        //    //if (fe.Name == "InnerBtnDelete")
        //    //{
        //    //    xmlelement.ParentNode.RemoveChild(xmlelement);

        //    //}
        //}    
     
    }
}
