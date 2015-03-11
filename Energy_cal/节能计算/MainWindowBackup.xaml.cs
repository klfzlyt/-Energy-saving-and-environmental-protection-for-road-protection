using System.Windows;
using 节能计算.ViewModel;
using MahApps.Metro.Controls;
using 节能计算.Service;
using Xceed.Wpf.AvalonDock.Themes;
using System;
using System.Xml;
using System.Xml.Linq;
using GalaSoft.MvvmLight.Messaging;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Controls;
using System.Threading;
using 节能计算.Calculator;
using System.Text.RegularExpressions;
using System.Linq;
using 节能计算.Model;
using Visifire.Charts;
using Visifire.Commons;
namespace 节能计算
{
   
    public partial class MainWindowBackup : Window
    {

       // private MainViewModel _mainvm;
        // Theme them=new AeroTheme();

        private ObservableCollection<XmlElement> _subdevicesxmlelements;


        public MainWindowBackup()
        {
           // Xceed.Wpf.Toolkit.Panels.WrapPanel s;
           // s.Orientation
           // Xceed.Wpf.Toolkit.Panels.AnimationPanel
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
          
            // _mainvm = this.DataContext as MainViewModel;

            #region 相当于checklistbox的check和uncheck事件
            Messenger.Default.Register<NotificationMessage<XmlElement>>(this,
                (meesage) =>
                {
                    if (meesage.Notification != "CheckOrUnChecked")
                        return;
                    if (_subdevicesxmlelements != null)
                    {
                        _subdevicesxmlelements.Clear();
                        _subdevicesxmlelements = null;
                    }
                  // Debug.WriteLine(ChecklistBox1.SelectedItems.Count);
                    _subdevicesxmlelements = new ObservableCollection<XmlElement>();
                    foreach (object  parentxmlelement in ChecklistBox1.SelectedItems)
                    {

                        var children = (parentxmlelement as XmlElement).ChildNodes;
                        if (children.Count != 0)
                        {
                         
                          
                            foreach (XmlNode subelement in children)
                            {
                                _subdevicesxmlelements.Add((subelement as XmlElement));  
                               
                            
                            }
                       
                        }
                    
                    }
                    subdeviceschecklist.DataContext = _subdevicesxmlelements;
                
                });
            #endregion


            // var msgBox = new Xceed.Wpf.Toolkit.MessageBox();

     // msgBox.Text =" StyledMsgBoxMessage";
     // msgBox.Caption = "StyledMsgBoxTitle";
     // msgBox.CancelButtonContent = "fds";
     // var ss=this.TryFindResource("messageBoxStyle") as Style;
     ////  = (Style)this.Resources["messageBoxStyle"];
     // msgBox.Style = ss;
     // msgBox.ShowDialog();
          // Uri uri=them.GetResourceUri();
         //  Xceed.Wpf.AvalonDock.Themes.DictionaryTheme ff = new Xceed.Wpf.AvalonDock.Themes.DictionaryTheme(this.Resources);
           this.AddHandler(Button.ClickEvent,
               (RoutedEventHandler)
               ((sender,eve)=>
            {
                Debug.WriteLine(sender.GetType().Name);
                Debug.WriteLine("OriginalSource: " + eve.OriginalSource);
                Debug.WriteLine("Source: "+eve.Source);
                if (eve.OriginalSource.GetType() != typeof(Button))
                {
                    return;
                }
                var cal = (eve.OriginalSource as Button).Tag;
                cal = cal as string;
                if (cal == null)
                    return;
                Debug.WriteLine("Tag: "+cal.ToString());
                MatchCollection mc = Regex.Matches(cal as string, "[A-Z][0-9]+");
                XmlDocument xmldoc = (this.DataContext as MainViewModel).DatagridVM.Xdp.Document;
                XmlElement xmlrootelement = xmldoc.DocumentElement;
                var matchlist = mc.Cast<Match>();
                foreach (Match m in matchlist)
                {
                   
                    XmlElement IDelement = (XmlElement)xmlrootelement.SelectSingleNode("//*[@ID=" + "\"" + m.Value + "\"" + "]");
                    Debug.WriteLine(IDelement.Name + " " + m.Value);
                    if (IDelement.HasAttribute("DefaultValue"))
                        cal = (cal as string).Replace(m.Value, IDelement.Attributes["DefaultValue"].Value);
                   
                }
            
                Debug.WriteLine(cal);
                if (cal != null)
                {
                    IAlgorithm calcu = new Algorithm();
                    MessageBox.Show(calcu.Calculate(cal.ToString()).ToString(), "结果");
                }
            }));
           
        }


        private void Bold_Checked(object sender, RoutedEventArgs e)
        {
            textBox1.FontWeight = FontWeights.Bold;
        }

        private void Bold_Unchecked(object sender, RoutedEventArgs e)
        {
            textBox1.FontWeight = FontWeights.Normal;
        }

        private void Italic_Checked(object sender, RoutedEventArgs e)
        {
            textBox1.FontStyle = FontStyles.Italic;
        }

        private void Italic_Unchecked(object sender, RoutedEventArgs e)
        {
            textBox1.FontStyle = FontStyles.Normal;
        }

        private void IncreaseFont_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.FontSize < 18)
            {
                textBox1.FontSize += 2;
            }
        }

        private void DecreaseFont_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.FontSize > 10)
            {
                textBox1.FontSize -= 2;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
         
        }
        
      private void  UpdateDevices()
      {
         
           
      }

      private void Button_Click_1(object sender, RoutedEventArgs e)
      {
          Debug.WriteLine(" ActualWidth " + testgrid.ActualWidth);
          Debug.WriteLine(" ColumnWidth " + testgrid.ColumnWidth);
    
          Debug.WriteLine(" MaxWidth " + testgrid.MaxWidth);
          Debug.WriteLine(" MaxColumnWidth " + testgrid.MaxColumnWidth);
      //   var devicevm = _mainvm.DevicesVM;
          //XmlDocument xmldoc = devicevm.Xdp.Document;
        //  XElement xele=new XElement("拌合设备",new XAttribute("Name",deviceClasswatertextbox.Text));
        //  new XmlService().AddXElement(devicevm.Xdp, xele, "代码测试设备", GlobalResource.RelativeDevicesPath);
      }
      

      private void Button_Click_2(object sender, RoutedEventArgs e)
      {
         // var selecetedxmlelement = ChecklistBox1.SelectedItem as XmlElement ;
        //  var devicevm = _mainvm.DevicesVM;
          //XmlDocument xmldoc = devicevm.Xdp.Document;
        //  XElement xele = new XElement("拌合设备种类一", new XAttribute("Name", DeviceswaterTextbox.Text));
        //  new XmlService().AddXElement(devicevm.Xdp, xele, "拌合设备", GlobalResource.RelativeDevicesPath, "Name", selecetedxmlelement.GetAttribute("Name"));


      }

      private void RestoreToDefaultColor_Click(object sender, RoutedEventArgs e)
      {
          colorpicker.SelectedColor =(Color) ColorConverter.ConvertFromString("SkyBlue");
         
      }

      private void MenuItem_Click(object sender, RoutedEventArgs e)
      {
        
         // Chart columchart = new Chart();

          OweredWindow subwindow = new OweredWindow();
          subwindow.Owner = this;
          subwindow.Height = 500;
          subwindow.Width = 800;
          subwindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
          subwindow.Show();
         // columchart.Legends.Add(new Legend() { Background=Brushes.Transparent, Title = "百分比", HorizontalAlignment=System.Windows.HorizontalAlignment.Right ,VerticalAlignment=System.Windows.VerticalAlignment.Center});
         // DataSeries dataseries = new DataSeries();
         // dataseries.LightingEnabled = true;
         // dataseries.IncludePercentageInLegend = true;
       

         // var datapoints = (this.DataContext as MainViewModel).XMLResults.Select(
         //     (LocalDeviceImageXml) =>
         //     {
         //        string cal= LocalDeviceImageXml.ElementAttributeCalculator;
         //        MatchCollection mc = Regex.Matches(cal as string, "[A-Z][0-9]+");
         //        XmlDocument xmldoc = (this.DataContext as MainViewModel).DatagridVM.Xdp.Document;
         //        XmlElement xmlrootelement = xmldoc.DocumentElement;
         //        var matchlist = mc.Cast<Match>();
         //        foreach (Match m in matchlist)
         //        {

         //            XmlElement IDelement = (XmlElement)xmlrootelement.SelectSingleNode("//*[@ID=" + "\"" + m.Value + "\"" + "]");
         //          //  Debug.WriteLine(IDelement.Name + " " + m.Value);
         //            if (IDelement.HasAttribute("DefaultValue"))
         //                cal = (cal as string).Replace(m.Value, IDelement.Attributes["DefaultValue"].Value);

         //        }

         //        IAlgorithm alo = new Algorithm();
         //        double result = alo.Calculate(cal);
         //        if (result < 0)
         //            result = 0;
         //        if (result > 5000)
         //            result = 5000;
         //        Debug.WriteLine(LocalDeviceImageXml.ElementAttributeName + "  " + result.ToString());
         //        return new DataPoint() {LegendText=LocalDeviceImageXml.ElementAttributeName, AxisXLabel = LocalDeviceImageXml.ElementAttributeName, YValue = result };
              
         //     });
         //  dataseries.DataPoints=new DataPointCollection(datapoints);
         //  dataseries.LabelEnabled = true;
         ////  dataseries.IncludePercentageInLegend = true;
        
         //  columchart.AxesY.Add(new Axis() { Title = "能耗(公斤标准煤)" });
         //  columchart.AxesX.Add(new Axis() { Title = "项目" });
         //  columchart.Titles.Add(new Visifire.Charts.Title() { Text = "能耗分析对比图表" });
         //     columchart.View3D = true;
         //     columchart.Height = 400;
           
         //     columchart.Width = 600;
         //     columchart.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
         //     dataseries.ShowInLegend = true;
         //     columchart.Series.Add(dataseries);
         //     Chartstackpanel.Children.Add(columchart);



         //     Chart piechart = new Chart();
         //   //  piechart.Legends.Add(new Legend() {Title="饼图" });
         //     DataSeries piedataseries = new DataSeries();
         //   //  piedataseries.LegendText = "饼图";
         //     piedataseries.ShowInLegend = true;
         //     piedataseries.RenderAs = RenderAs.Pie;
         //    // piedataseries.IncludePercentageInLegend = true;
         //     piechart.View3D = true;
         //     piechart.Height = 400;
         //     piechart.Width = 400;
         //     var piedatapoints =
         //         datapoints.Select(
         //         (datapoint) =>
         //         {

         //             return new DataPoint() { LabelStyle=LabelStyles.OutSide, LegendText = datapoint.AxisXLabel, AxisXLabel = datapoint.AxisXLabel, YValue = datapoint.YValue, Exploded = true };
         //         });
         //     piedataseries.DataPoints = new DataPointCollection(piedatapoints);
         //     piechart.Series.Add(piedataseries);
         //     piechart.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
         //     Chartstackpanel.Children.Add(piechart);
         
          //Chartbar



      }

    

        //private void button1_Click(object sender, RoutedEventArgs e)
        //{
        //  var xmlservice=  new XmlService();
        //  xmlservice.UpdateData(((MainViewModel)(this.DataContext)).DatagridVM.Xdp, GlobalResource.RelativeEntityPath);
        //}


        /*
        * 
        *  
        * 
        * 
        * 
          public ICommand Updata
       {
           get {
               return
               updata??
                new MyCommand(
                   () =>
                   {
                       OperateXML.UpdateData(Xdp,"..\\..\\EntityXML.xml");
                       //J18=(F18-25)/135
                       //OperateXML.AddElement(Xdp, "原材料能耗", "丰富", 123, "吨", "..\\..\\EntityXML.xml");
                       //"F19/(F16/100+1)*F16/100*((F18-25)/135)*1.429"
                       //"F16/(100+F16)*F11";
                       #region 计算
                       string caculator = "F19/(F16/100+1)*F16/100*((F18-25)/135)*1.429";
                       MatchCollection mc = Regex.Matches(caculator, "[A-Z][0-9]+");
                       XmlDocument xmldoc = xdp.Document;
                       XmlElement xmlrootelement = xmldoc.DocumentElement;
                       var matchlist = mc.Cast<Match>();
                     
                       matchlist = matchlist.Distinct(new compare());
                     foreach (Match m in matchlist)
                       {

                          XmlElement IDelement=(XmlElement) xmlrootelement.SelectSingleNode("//*[@ID="+"\""+m.Value+"\""+"]");
                          Debug.WriteLine(IDelement.Name + " " + m.Value);
                          if (IDelement.HasAttribute("DefaultValue"))
                              caculator = caculator.Replace(m.Value, IDelement.Attributes["DefaultValue"].Value);
                      //    Debug.WriteLine(caculator);
                       }
                       Debug.WriteLine(caculator);
                   }
               #endregion
                   );
           }
        
       }
         * * 
         * 
         * 
         * 
        * 
        * 
        * 
       */

    }
}