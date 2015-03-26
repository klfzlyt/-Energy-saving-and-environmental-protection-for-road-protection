using System.Xml;
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
using 公路养护工程能耗计算软件ECMS.Model;
using Visifire.Charts;
using MahApps.Metro.Controls;

namespace 公路养护工程能耗计算软件ECMS
{
    /// <summary>
    /// RenderResults.xaml 的交互逻辑
    /// </summary>
    public partial class RenderResults : MetroWindow
    {
        private Project _project;
        public RenderResults()
        {
            InitializeComponent();
        }
        public RenderResults(XmlDocument resultdoc)
            : this()
        { 
        
        
        }
        public RenderResults(XmlDocument resultdoc,Project project,double ProjectDUn,double Comsuption)
            : this(resultdoc)
        {
            this.DirectConsumption.Content = Comsuption.ToString("0.00") + "\r\n公斤标准煤";
            this.ProjectDun.Content = ProjectDUn.ToString("0.00") + "吨";
            _project = project;
            var datas = ParseAllResultDocToDataPoint(resultdoc);
            BuilderColumGraphChart(datas);
            var datas1 = ParseAllResultDocToDataPoint(resultdoc);
            BuilderPieGraphChart(datas1);

        }
        #region 私有方法
        private List<DataPoint> ParseAllResultDocToDataPoint(XmlDocument resultdoc)
        {
            var allresults = resultdoc.SelectNodes("//结果项");//calculator
            List<DataPoint> datapoints = new List<DataPoint>();
            foreach (XmlNode node in allresults)
            {
                var element = node as XmlElement;
                var tOadddatapoint = new DataPoint() { LegendText = element.Attributes["Name"].Value, 
                    AxisXLabel = element.Attributes["Name"].Value,
                    YValue =double.Parse(element.Attributes["calculator"].Value)};
                datapoints.Add(tOadddatapoint);                
            }
            return datapoints;
        }


        private void BuilderColumGraphChart(List<DataPoint> datapoints)
        {
            Chart columchart = new Chart();
            columchart.ToolBarEnabled = true;
            columchart.Legends.Add(new Legend() { Background = Brushes.Transparent, Title = "数值", HorizontalAlignment = System.Windows.HorizontalAlignment.Right, VerticalAlignment = System.Windows.VerticalAlignment.Center });
            DataSeries dataseries = new DataSeries();
            dataseries.LightingEnabled = true;
            dataseries.IncludeYValueInLegend = true;
            dataseries.DataPoints = new DataPointCollection(datapoints);
            dataseries.LabelEnabled = true;
            columchart.AxesY.Add(new Axis() { Title = "能耗(公斤标准煤)" });
            columchart.AxesX.Add(new Axis() { Title = "项目" });
            columchart.Titles.Add(new Visifire.Charts.Title() { Text = "能耗分析对比图表" });
            columchart.View3D = true;
            columchart.Height = 400;
            columchart.Width = 600;
            columchart.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            dataseries.ShowInLegend = true;
            columchart.Series.Add(dataseries);        
            Chartstackpanel.Children.Add(columchart);
        }

        private void BuilderPieGraphChart(List<DataPoint> piedatapoints)
        {
            Chart piechart = new Chart();
            piechart.Legends.Add(new Legend() { Background = Brushes.Transparent, Title = "百分比", HorizontalAlignment = System.Windows.HorizontalAlignment.Right, VerticalAlignment = System.Windows.VerticalAlignment.Center });
            DataSeries piedataseries = new DataSeries();
            piedataseries.IncludePercentageInLegend = true;
            piedataseries.ShowInLegend = true;
            //piechart.IndicatorEnabled = true;          
            //piechart.SmartLabelEnabled = true;
            piechart.ToolBarEnabled = true;
            //piechart.ToolTipEnabled = true;
            piedataseries.RenderAs = RenderAs.Pie;
            piechart.View3D = true;
            piechart.Height = 400;
            piechart.Width = 600;
            piedataseries.DataPoints = new DataPointCollection(piedatapoints);
            piechart.Series.Add(piedataseries);
            piechart.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            Chartstackpanel.Children.Add(piechart);
        }
        #endregion

        private void SaveResult_Click(object sender, RoutedEventArgs e)
        {

            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            sfd.Filter = "Excel（*.xls）|*.xls";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string localFilePath = sfd.FileName.ToString(); //获得文件路径 
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径
                string xmlpath = _project.ProjectXMLLocation.Substring(0, _project.ProjectXMLLocation.LastIndexOf("\\") + 1) + "Result.xml";
                string filePath = ExcelProcessor.resultsUpdate(localFilePath, xmlpath);
                Xceed.Wpf.Toolkit.MessageBox.Show("文件：" + filePath + "生成成功！");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Xceed.Wpf.Toolkit.MessageBox.Show("结果已保存成功！已保存到： "+_project.ProjectXMLLocation.Substring(0,_project.ProjectXMLLocation.LastIndexOf("\\")));
            


        }



    }
}
