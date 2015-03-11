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
using Visifire.Charts;
using 节能计算.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using System.Xml;
using System.Text.RegularExpressions;
using 节能计算.Calculator;
using System.Diagnostics;
using System.Xml.Linq;
using System.Threading;

//用于生成xls
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.UserModel.Contrib;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.HSSF.Util;




namespace 节能计算
{




    //////////////////////////excel表格处理类//////////////////////////////////
    static public class ExcelProcessor
    {

        //////////////////////模版赋值函数//////////////////////////////////////////
        /*
         * 参数：storagPath为用户选择的excel存储路径,excelName为保存的excel名称
         * 返回值：保存的excel完整名称
         */
        //static public string tempCopy(string storagePath, string excelName)
        static public string tempCopy(string storagePath)
        {
            string excelCompletePath = storagePath;
            try
            {
                //模版预先存储在根目录下，template.xls
                FileStream file = new FileStream(@"template.xls", FileMode.Open, FileAccess.Read);

                HSSFWorkbook wb = new HSSFWorkbook(file);
                HSSFSheet sheet1 = wb.GetSheet("Sheet1");
                sheet1.ForceFormulaRecalculation = true;
                
                //保存的excel的完整名称

                FileStream file1 = new FileStream(excelCompletePath, FileMode.Create);
                wb.Write(file1);
                file1.Close();
                file.Close();




            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
            return excelCompletePath;

        }

        ////////////////////////从xml文件中提取数据到容器中/////////////////////////
        /*
         * 参数：xmlPath为xml文件包含路径的完整名称
         * 返回值：从xml提取的要写入excel单元格的数据的list<double>类型容器
         */
        static public List<double> dataGet(string xmlPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            //获得Resultsxml.xml的根节点
            XmlNode xn = doc.SelectSingleNode("结果");
            //获得根节点的所有子节点存储在xn0,xn1,xn2中
            XmlNodeList xnL = xn.ChildNodes;

            //定义一个float数组，用于存储10个数字
            // double[] storager;
            // storager = new double[10];
            // int i = 0;

            List<double> resultData = new List<double>();
            double resultTemp;

            //开始读取数据
            XmlElement elemTemp;
            String strtemp;
            foreach (XmlNode xntemp in xnL)
            {
                XmlNodeList xnList = xntemp.ChildNodes;

                foreach (XmlNode xntemp1 in xnList)
                {
                    elemTemp = (XmlElement)xntemp1;
                    strtemp = elemTemp.GetAttribute("calculator").ToString();
                    resultTemp = Convert.ToDouble(strtemp);
                    resultData.Add(resultTemp);
                    // storager[i] = Convert.ToDouble(strtemp);
                    // i++;
                }
            }
            return resultData;
        }
        ////////////////////////////提取出单元格坐标//////////////////////
        /*
         * 示例：从"P13"中提取目标单元格的横坐标为13-1=12，纵坐标为P的ASCII码80-65=15
         * 参数：locationStr代表一个excel单元格坐标，有一个代表列的大写字母和一个代表行的数字组成
         * 返回值：
         */
        static public List<KeyValuePair<int, int>> locationGet(List<string> destinationGrids)
        {
            int column, row;
            List<KeyValuePair<int, int>> locationList = new List<KeyValuePair<int, int>>();
            foreach (string locationStr in destinationGrids)
            {
                column = locationStr[0] - 65;
                string tempStr = locationStr[1].ToString();
                tempStr = tempStr + locationStr[2].ToString();
                row = Convert.ToInt32(tempStr);
                //格式为<row,column>
                KeyValuePair<int, int> locationAxis = new KeyValuePair<int, int>(row, column);
                locationList.Add(locationAxis);
            }
            if (locationList.Count != destinationGrids.Count)
            {
                //弹出消息框
                MessageBox.Show("locationGet不成功");
            }
            return locationList;
        }

        ////////////////////////////将提取到的数据写入到新建的表格中////////////////////////////////////////////
        /*
         * 参数：excelPath为目标excel的完整名称，newdata为从xml文件中提取的数据，destinationGrids为所有单元格的地址如 P13,P14
         * 返回值：更新成功返回true
         */
        static public bool updateExcelData(string excelPath, List<double> newData, List<KeyValuePair<int, int>> locationList)
        {
            //list中数据个数
            int dataNum = newData.Count;
            //目标单元格个数
            int gridNum = locationList.Count;
            if (dataNum != gridNum)
            {
                MessageBox.Show("数据数目和单元格数目不一致");
                return false;
            }

            FileStream file = new FileStream(excelPath, FileMode.Open, FileAccess.Read);
            HSSFWorkbook wb = new HSSFWorkbook(file);
            HSSFSheet sheet1 = wb.GetSheet("sheet1");

            //sheet的最大行数
            int maxRowNumber = sheet1.PhysicalNumberOfRows;
            HSSFRow row;
            HSSFCell cell1 = sheet1.GetRow(12).GetCell(15);
            int rowIndex = 12;
            int colunmIndex = 15;
            int i = 0;
            foreach (KeyValuePair<int, int> locat in locationList)
            {
                try
                {

                    rowIndex = locat.Key;
                    colunmIndex = locat.Value;
                    //要使用SetCellValue()给单元格赋值前需要判断cell是否为空，如果是空的就要先创建

                    //如果行存在直接获取行
                    if (rowIndex <= maxRowNumber)
                        row = sheet1.GetRow(rowIndex);
                    //不存在就创建行
                    else
                        row = sheet1.CreateRow(rowIndex);
                    //获取列
                    cell1 = row.GetCell(colunmIndex);
                    //如果列不存在，创建列
                    if (cell1 == null)
                        cell1 = row.CreateCell(colunmIndex);
                    //给单元格填充数据
                    cell1.SetCellValue(newData[i]);
                    i++;

                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }
            i = 0;


            ////Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;
            //由于test1已经存在，查看定义可以发现FileMode.Create模式会覆盖原来的xls文件
            file = new FileStream(excelPath, FileMode.Create);
            wb.Write(file);
            file.Close();
            return true;
        }

        ///////////////////////////存储结果函数/////////////////////////////////////////////
        // static public string resultsUpdate(string newExcelPath, string newExcelName, string xmlPath)
        static public string resultsUpdate(string newExcelPath, string xmlPath)
        {

            //创建excel
            string completenewExcelPath = null;
            completenewExcelPath = tempCopy(newExcelPath);
            //提取数据
            List<double> xmlData = dataGet(xmlPath);
            //存储单元格坐标P13~P22
            List<string> destinationGrids = new List<string>();
            for (int i = 0; i < 10; i++)
                destinationGrids.Add("F" + (12 + i).ToString());
            //处理坐标
            List<KeyValuePair<int, int>> locationList = locationGet(destinationGrids);
            //更新excel
            bool finish = updateExcelData(completenewExcelPath, xmlData, locationList);
            return completenewExcelPath;
        }
    }


    /// <summary>
    /// OweredWindow.xaml 的交互逻辑
    /// </summary>
    public partial class OweredWindow : Window
    {
        public OweredWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(OweredWindow_Loaded);

        }

        void OweredWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (ProcessData(null))
            {
                busyindicator.IsBusy = false;
            }


        }

        private bool ProcessData(object state)
        {
            Chart columchart = new Chart();

            MainViewModel mainvm = SimpleIoc.Default.GetInstance<MainViewModel>();
            columchart.Legends.Add(new Legend() { Background = Brushes.Transparent, Title = "数值", HorizontalAlignment = System.Windows.HorizontalAlignment.Right, VerticalAlignment = System.Windows.VerticalAlignment.Center });
            DataSeries dataseries = new DataSeries();
            dataseries.LightingEnabled = true;
            dataseries.IncludeYValueInLegend = true;

            XmlDocument xmldocforsave = new XmlDocument();
            xmldocforsave.Load("..\\..\\OriginalXml\\Resultxml.xml");
            XmlElement rootelement = xmldocforsave.DocumentElement;

            var datapoints = mainvm.XMLResults.Select(
                (LocalDeviceImageXml) =>
                {
                    string cal = LocalDeviceImageXml.ElementAttributeCalculator;
                    MatchCollection mc = Regex.Matches(cal as string, "[A-Z][0-9]+");
                    XmlDocument xmldoc = mainvm.DatagridVM.Xdp.Document;
                    XmlElement xmlrootelement = xmldoc.DocumentElement;
                    var matchlist = mc.Cast<Match>();
                    foreach (Match m in matchlist)
                    {

                        XmlElement IDelement = (XmlElement)xmlrootelement.SelectSingleNode("//*[@ID=" + "\"" + m.Value + "\"" + "]");
                        //  Debug.WriteLine(IDelement.Name + " " + m.Value);
                        if (IDelement.HasAttribute("DefaultValue"))
                            cal = (cal as string).Replace(m.Value, IDelement.Attributes["DefaultValue"].Value);

                    }

                    IAlgorithm alo = new Algorithm();
                    double result = alo.Calculate(cal);

                    if (result < 0)
                        result = 0;
                    if (result > 5000)
                        result = 5000;
                    XmlElement selectedxmlelement = (XmlElement)rootelement.SelectSingleNode("/结果/结果类别/结果项[@Name=" + "\"" + LocalDeviceImageXml.ElementAttributeName + "\"" + "]");
                    selectedxmlelement.SetAttribute("calculator", result.ToString());
                    Debug.WriteLine(LocalDeviceImageXml.ElementAttributeName + "  " + result.ToString());
                    rootelement.OwnerDocument.Save("result.xml");
                    return new DataPoint() { LegendText = LocalDeviceImageXml.ElementAttributeName, AxisXLabel = LocalDeviceImageXml.ElementAttributeName, YValue = result };
                });

            dataseries.DataPoints = new DataPointCollection(datapoints);
            dataseries.LabelEnabled = true;
            //  dataseries.IncludePercentageInLegend = true;

            columchart.AxesY.Add(new Axis() { Title = "能耗(公斤标准煤)" });
            columchart.AxesX.Add(new Axis() { Title = "项目" });
            columchart.Titles.Add(new Visifire.Charts.Title() { Text = "能耗分析对比图表" });
            columchart.View3D = true;
            columchart.Height = 400;

            columchart.Width = 600;
            columchart.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            dataseries.ShowInLegend = true;
            columchart.Series.Add(dataseries);
            this.Dispatcher.Invoke((ThreadStart)(
                () =>
                {
                    Chartstackpanel.Children.Add(columchart);
                }));



            Chart piechart = new Chart();
            piechart.Legends.Add(new Legend() { Background = Brushes.Transparent, Title = "百分比", HorizontalAlignment = System.Windows.HorizontalAlignment.Right, VerticalAlignment = System.Windows.VerticalAlignment.Center });

            //  piechart.Legends.Add(new Legend() {Title="饼图" });
            DataSeries piedataseries = new DataSeries();
            piedataseries.IncludePercentageInLegend = true;
            //  piedataseries.LegendText = "饼图";
            piedataseries.ShowInLegend = true;
            piedataseries.RenderAs = RenderAs.Pie;
            // piedataseries.IncludePercentageInLegend = true;
            piechart.View3D = true;
            piechart.Height = 400;
            piechart.Width = 600;
            var piedatapoints =
                datapoints.Select(
                (datapoint) =>
                {

                    return new DataPoint() { LabelStyle = LabelStyles.OutSide, LegendText = datapoint.AxisXLabel, AxisXLabel = datapoint.AxisXLabel, YValue = datapoint.YValue, Exploded = true };
                });
            piedataseries.DataPoints = new DataPointCollection(piedatapoints);
            piechart.Series.Add(piedataseries);
            piechart.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

            this.Dispatcher.Invoke((ThreadStart)(
                () =>
                { Chartstackpanel.Children.Add(piechart); }
                ));

            return true;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //东西写到这里面

            //只需要创建一个对话框将文件路径保存在newExcelPath中，将文件名保存在newExcelName中，
            //使得newExcelPath+newExcelName是一个合法的包含路径的文件名
            //System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            //dialog.Description = "请选择文件路径";
            //if (dialog.ShowDialog() ==System.Windows.Forms.DialogResult.OK)
            //{
            //    string foldPath = dialog.SelectedPath;
            //    System.Windows.Forms.MessageBox.Show("已选择文件夹:" + foldPath, "选择文件夹提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            //}
            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            sfd.Filter = "Excel（*.xls）|*.xls";
            sfd.RestoreDirectory = true;
            //点了保存按钮进入 
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string localFilePath = sfd.FileName.ToString(); //获得文件路径 
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径
                string filePath = ExcelProcessor.resultsUpdate(localFilePath, "result.xml");
                MessageBox.Show("文件：" + filePath + "生成成功！");
                //获取文件路径，不带文件名 
                //FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));

                //给文件名前加上时间 
                //newFileName = DateTime.Now.ToString("yyyyMMdd") + fileNameExt;

                //在文件名里加字符 
                //saveFileDialog1.FileName.Insert(1,"dameng");

                //System.IO.FileStream fs = (System.IO.FileStream)sfd.OpenFile();//输出文件

                ////fs输出带文字或图片的文件，就看需求了 
            }
            //保存到用户桌面
            // string newExcelPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            // newExcelPath=newExcelPath+"\\";
            // string newExcelName = "TimCookGay";
            // string xmlPath = "result.xml";
            // //创建结果表格,此调用生成表格为H:\TimCookGay.xls
            //string filePath= ExcelProcessor.resultsUpdate(newExcelPath, newExcelName, xmlPath);
            // //文件名称
            //MessageBox.Show("文件："+filePath+"生成成功！");

        }
    }
}
