using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using 公路养护工程能耗计算软件ECMS.Model;
using 公路养护工程能耗计算软件ECMS.ViewModel;
using 公路养护工程能耗计算软件ECMS.Service;
using System.Xml;
//公路养护技术国家工程研究中心
//中公高科养护科技股份有限公司
namespace 公路养护工程能耗计算软件ECMS
{
    /// <summary>
    /// Tabwindow.xaml 的交互逻辑
    /// </summary>
    public partial class Tabwindow :MetroWindow
    {

        Storyboard additem;
        List<string> xpathlist = new List<string>();
        public Tabwindow()
        {
            InitializeComponent();
            this.DataContext = new TabMainWindowViewModel();
            this.Closing += Tabwindow_Closing;
           additem= this.TryFindResource("additemstoryboard") as Storyboard;
           this.AddHandler(Button.ClickEvent, new RoutedEventHandler(handlerbuttonclick));
           #region 键盘按钮命令

          // ICommand _saveallcommand = (this.DataContext as TabMainWindowViewModel).SaveAllProjectXmlFile;


           CommandBindings.Add(new CommandBinding(OpenFile, OpenFileHandler));
           CommandBindings.Add(new CommandBinding(NewProject, NewProjectHandler));
           CommandBindings.Add(new CommandBinding(FileELapse, FileELapseHandler));
           CommandBindings.Add(new CommandBinding(SaveProject, SaveProjectHandler));
           CommandBindings.Add(new CommandBinding(CheckChart,CheckChartHandler));
           CommandBindings.Add(new CommandBinding(OptionOpen, OptionOpenHandler));
           CommandBindings.Add(new CommandBinding(HelperOpen, HelperOpenHandler));
           #endregion

            #region 替换功能代码

            //  var finaldoc = new XmlDocument();
         //  var entitydoc = new XmlDocument();
         //  finaldoc.Load(@"../../OriginalXml/FinalitemsTemplate.xml");
        //   entitydoc.Load(@"../../OriginalXml/EntityXML.xml");
           //new  XmlService().ProcessTwoXmlFile()
         //  new XmlService().ProcessTwoXmlFile(entitydoc, finaldoc, "test.xml");
         //  var categorydoc = new XmlDocument();
        //   categorydoc.Load(@"../../OriginalXml/DevicesCategory.xml");
       //    new XmlService().ProcessCategoryXmlFile(categorydoc, "3test.xml");

            #endregion




        }
        #region 按键命令

        #region 打开文件命令
        public static RoutedUICommand OpenFile = new RoutedUICommand("OpenFile",
             "OpenFile", typeof(TabMainWindowViewModel), new InputGestureCollection(
                 new InputGesture[] { new KeyGesture(Key.O, ModifierKeys.Alt, "Open File") }
                 ));

        private void OpenFileHandler(object sender, ExecutedRoutedEventArgs e)
        {
            File.Focus();
            File.IsSubmenuOpen = true;
            TabMainWindowViewModel _tabmain=this.DataContext as TabMainWindowViewModel;
            _tabmain.OpenProjectCommand.Execute("");
        }
        #endregion

        #region 新建项目命令

        public static RoutedUICommand NewProject = new RoutedUICommand("NewProject",
          "NewProject", typeof(TabMainWindowViewModel), new InputGestureCollection(
              new InputGesture[] { new KeyGesture(Key.N, ModifierKeys.Alt, "New Project") }
              ));
        private void NewProjectHandler(object sender, ExecutedRoutedEventArgs e)
        {
            File.Focus();
            File.IsSubmenuOpen = true;
            TabMainWindowViewModel _tabmain = this.DataContext as TabMainWindowViewModel;
            _tabmain.NewCommand.Execute("");
        }

        #region 全部保存命令
        private void SaveAllfile(object sender, ExecutedRoutedEventArgs e)
        {
            File.Focus();
            File.IsSubmenuOpen = true;
            TabMainWindowViewModel _tabmain = this.DataContext as TabMainWindowViewModel;
            _tabmain.SaveAllProjectXmlFile.Execute("");
        }



        #endregion 


        #endregion


        #region 弹出文件命令

        public static RoutedUICommand FileELapse = new RoutedUICommand("FileELapse",
           "FileELapse", typeof(TabMainWindowViewModel), new InputGestureCollection(
               new InputGesture[] { new KeyGesture(Key.F, ModifierKeys.Alt, "File ELapse") }
               ));

        private void FileELapseHandler(object sender, ExecutedRoutedEventArgs e)
        {
            File.Focus();
            File.IsSubmenuOpen = true;
        }

        #endregion


        #region 保存工程

        public static RoutedUICommand SaveProject = new RoutedUICommand("SaveProject",
         "SaveProject", typeof(TabMainWindowViewModel), new InputGestureCollection(
             new InputGesture[] { new KeyGesture(Key.S, ModifierKeys.Control, "Save Project") }
             ));

        private void SaveProjectHandler(object sender, ExecutedRoutedEventArgs e)
        {
            SaveProjecttt_Click(null, null);
            //TabMainWindowViewModel _vm = this.DataContext as TabMainWindowViewModel;
            //_vm.SaveAllProjectXmlFile.Execute("");
        }



        #endregion


        #region 查看饼图
        //万一没有项目会出现什么

        public static RoutedUICommand CheckChart = new RoutedUICommand("CheckChart",
         "CheckChart", typeof(TabMainWindowViewModel), new InputGestureCollection(
             new InputGesture[] { new KeyGesture(Key.P, ModifierKeys.Alt, "Check Chart") }
             ));

        private void CheckChartHandler(object sender, ExecutedRoutedEventArgs e)
        {
            if (MainTabControl.SelectedItem == null)
            { 
                Xceed.Wpf.Toolkit.MessageBox.Show("还未有工程！", "注意", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var _projectvm = MainTabControl.SelectedItem as Project;
            _projectvm.CalculateItem.Execute("");
        }



        #endregion

        #region 选项打开
        public static RoutedUICommand OptionOpen = new RoutedUICommand("OptionOpen",
          "OptionOpen", typeof(TabMainWindowViewModel), new InputGestureCollection(
              new InputGesture[] { new KeyGesture(Key.E, ModifierKeys.Alt, "Option Open") }
              ));

        private void OptionOpenHandler(object sender, ExecutedRoutedEventArgs e)
        {
            OptionOpenMenuItem.Focus();
            OptionOpenMenuItem.IsSubmenuOpen = true;
           // TabMainWindowViewModel _tabmain = this.DataContext as TabMainWindowViewModel;
           // _tabmain.OpenProjectCommand.Execute("");
        }





        #endregion

        #region 帮助打开

        public static RoutedUICommand HelperOpen = new RoutedUICommand("HelperOpen",
   "HelperOpen", typeof(TabMainWindowViewModel), new InputGestureCollection(
       new InputGesture[] { new KeyGesture(Key.H, ModifierKeys.Alt, "Helper Open") }
       ));
        private void HelperOpenHandler(object sender, ExecutedRoutedEventArgs e)
        {
            HelpermenuItem.Focus();
            //打开帮
            HelpermenuItem.IsSubmenuOpen = true;
            // TabMainWindowViewModel _tabmain = this.DataContext as TabMainWindowViewModel;
            // _tabmain.OpenProjectCommand.Execute("");
        }
        #endregion

        #endregion
        //我也会渐渐变成你不喜欢的样子
        private void handlerbuttonclick(object sender, RoutedEventArgs e)
        {
            var btn = e.OriginalSource as Button;
            if (btn == null)
                return;
            if (btn.Name != "InnerBtnDelete")
                return;
            var boardcontent = btn.Tag as Border;
            Animate.Background = new VisualBrush(boardcontent);
            if (additem != null)
            {
                additem.Begin();
            }
        }
        void Tabwindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TabMainWindowViewModel _vm = this.DataContext as TabMainWindowViewModel;
            var result = Xceed.Wpf.Toolkit.MessageBox.Show("是否要退出并保存数据？", "退出程序", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
            if (result == MessageBoxResult.OK)
            {
                _vm.SaveAllProjectXmlFile.Execute("");


                Application.Current.Shutdown();
                return;
            }
            else
            {

                e.Cancel = true;
                return;
            }



           
          
        }
        private void EnumVisual(Visual myVisual)
        {
            if (myVisual == null) return;
            FrameworkElement fe = myVisual as FrameworkElement;
            if (fe != null)
            {
                //Debug.WriteLine("FrameworkElment+ " + fe.GetType().Name);
                if (fe.GetType() == typeof(GroupBox))
                {
                   
                    var groupbox = fe as GroupBox;
                    var sectionstr = groupbox.Tag as string;                 
                    if (string.IsNullOrEmpty(sectionstr))
                        return;

                    try
                    {
                        var project = groupbox.DataContext as Project;
                        var Xpath = "Sections/Section[@Name=" + "\"" + sectionstr + "\"" + "]" + "/Item";
                        //   Debug.WriteLine("Name："+project.ProjectName+"  Xpath: "+Xpath);
                        if (!xpathlist.Contains(project.ProjectName + Xpath))
                        {
                            xpathlist.Add(project.ProjectName + Xpath);
                          //  var customdatagrid = new CustomParaDataGrid(project.FinalDoc, Xpath);
                            // Debug.WriteLine("groupbox子项创建了");
                            //groupbox.Content = customdatagrid;
                        }
                        groupbox.ApplyTemplate();
                        groupbox.UpdateLayout();

                    }
                    catch
                    { }
                }
            }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(myVisual); i++)
            {
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(myVisual, i);
                EnumVisual(childVisual);
            }
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj)where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }


        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine(Environment.NewLine);
            Debug.WriteLine("****************************");
            Debug.WriteLine("MainTabControl_SelectionChanged");
            Debug.WriteLine("Sender:  " + (sender as FrameworkElement).Name);
            Debug.WriteLine("MainTabControl_SelectionChanged  OriginalSource: " + (e.OriginalSource as FrameworkElement).Name + " Type: " + e.OriginalSource.GetType().ToString());
            Debug.WriteLine("MainTabControl_SelectionChanged  Source: " + (e.Source as FrameworkElement).Name + " Type: " + e.Source.GetType().ToString());
            Debug.WriteLine("****************************");
            Debug.WriteLine(Environment.NewLine);
            var originalframe = e.OriginalSource as FrameworkElement;
            if (originalframe.Name == "innertabcontrol")
            {
                try
                {
                    e.Handled = true;
                    MetroAnimatedSingleRowTabControl inner = e.OriginalSource as MetroAnimatedSingleRowTabControl;
                    var tabitem = inner.ItemContainerGenerator.ContainerFromIndex(inner.SelectedIndex) as MetroTabItem;
                    var visual = tabitem.Content as Visual;
                  //  EnumVisual(visual);
                }
                catch
                { }
                return;
            
            }
            if (originalframe.Name == "MainTabControl")
            {
                try
                {
                    e.Handled = true;
                    MetroAnimatedSingleRowTabControl inner = e.OriginalSource as MetroAnimatedSingleRowTabControl;
                    var tabitem = inner.ItemContainerGenerator.ContainerFromIndex(inner.SelectedIndex) as MetroTabItem;
                    var innertabcontrol = tabitem.Content as MetroAnimatedSingleRowTabControl;
                  
                   //     var tabiteminner = innertabcontrol.ItemContainerGenerator.ContainerFromItem(innertabcontrol.SelectedContent) as MetroTabItem;
                 //       var visual = tabiteminner.Content as Visual;
                       // EnumVisual(MainTabControl);
                    
                }
                catch
                { }

            }

        }

        private void MetroAnimatedSingleRowTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void MainTabControl_Loaded(object sender, RoutedEventArgs e)
        {
   
        }

        private void MainTabControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
        }

      

       

        private void MainTabControl_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            Debug.WriteLine("SourceUpdated");
        }

        private void MainTabControl_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            Debug.WriteLine("TargetUpdated");
        }

        private void innertabcontrol_Loaded(object sender, RoutedEventArgs e)
        {
    
        }

        private void tabitemmetro_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void tabitemmetro_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            Debug.WriteLine(Environment.NewLine);
            Debug.WriteLine("****************************");
            Debug.WriteLine("tabitemmetro_RequestBringIntoView");
            Debug.WriteLine("Sender:  " + (sender as FrameworkElement).Name);
            Debug.WriteLine("tabitemmetro_RequestBringIntoView  OriginalSource: " + (e.OriginalSource as FrameworkElement).Name + " Type: " + e.OriginalSource.GetType().ToString());
            Debug.WriteLine("tabitemmetro_RequestBringIntoView  Source: " + (e.Source as FrameworkElement).Name +" Type: "+e.Source.GetType().ToString());
            Debug.WriteLine("****************************");
            Debug.WriteLine(Environment.NewLine);
        }

        private void innertabcontrol_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            Debug.WriteLine(Environment.NewLine);
            Debug.WriteLine("****************************");
            Debug.WriteLine("innertabcontrol_RequestBringIntoView");
            Debug.WriteLine("Sender:  " + (sender as FrameworkElement).Name);
            Debug.WriteLine("innertabcontrol_RequestBringIntoView  OriginalSource: " + (e.OriginalSource as FrameworkElement).Name + "  Type: " + e.OriginalSource.GetType().ToString());
            Debug.WriteLine("innertabcontrol_RequestBringIntoView  Source: " + (e.Source as FrameworkElement).Name + " Type: " + e.Source.GetType().ToString());
            Debug.WriteLine("****************************");
            Debug.WriteLine(Environment.NewLine);

           // Debug.WriteLine()


            //if (sender.GetType() == typeof(MetroAnimatedSingleRowTabControl))
            //{
            //    var tabcontrol = sender as MetroAnimatedSingleRowTabControl;
            //    if (tabcontrol.Name == "innertabcontrol")
            //    {
            //        var innertablcontrol = sender as MetroAnimatedSingleRowTabControl;
            //        var metrotabitem = innertablcontrol.ItemContainerGenerator.ContainerFromIndex(innertablcontrol.SelectedIndex) as MetroTabItem;
            //        // Debug.WriteLine(metrotabitem.Content.ToString());
            //        var visual = metrotabitem.Content as Visual;
            //        EnumVisual(visual);
            //    }
            //}
        
        }

        private void tabitemmetro_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
        //    Debug.WriteLine("tabitemmetro_IsVisibleChanged");
        }

        private void 打开侧窗_Click(object sender, RoutedEventArgs e)
        {
            this.ToggleFlyout(0);
        }
        private void ToggleFlyout(int index)
        {
            var flyout = this.Flyouts.Items[index] as Flyout;
            if (flyout == null)
            {
                return;
            }

            flyout.IsOpen = !flyout.IsOpen;
        }

        private void ExitMenu_Click(object sender, RoutedEventArgs e)
        {
            var result=Xceed.Wpf.Toolkit.MessageBox.Show("是否要退出公路养护工程能耗计算软件ECMS？", "退出程序", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
            if (result == MessageBoxResult.OK)
            {
                this.Close();
                return;
            }
            else
            {

                e.Handled = true;
                return;
            }
            
        }

        private void AboutWindow_Click(object sender, RoutedEventArgs e)
        {
            new MyAboutBox().Show();
            
        
        }

        private void 添加_Click(object sender, RoutedEventArgs e)
        {
            var passstr = string.Empty;
            var sectionstr=string.Empty;
            var frameworelement=sender as FrameworkElement;
            var name=frameworelement.Name;
            if (name == "添加")
            {
                var tag = (sender as Button).Tag;
                var xmlattribute = tag as XmlAttribute ;
                var ownerxmlelement = xmlattribute.OwnerElement;
                if (ownerxmlelement.HasAttribute("HasFolder"))
                {
                    if (ownerxmlelement.Attributes["HasFolder"].Value == "true")
                    {
                       sectionstr=ownerxmlelement.ParentNode.ParentNode.Attributes["Name"].Value;
                    
                    }
                
                }
                else
                {
                    sectionstr = ownerxmlelement.ParentNode.Attributes["Name"].Value;


                }
                passstr = sectionstr +"."+ xmlattribute.Value;
            }     
    
        }

        private void Caculator_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();
            Info.FileName = "calc.exe ";//"calc.exe"为计算器，"notepad.exe"为记事本
            System.Diagnostics.Process Proc = System.Diagnostics.Process.Start(Info);
        }

        private void checkchart_Click(object sender, RoutedEventArgs e)
        {
            if (MainTabControl.SelectedItem == null)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("还未有工程！", "注意", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var _projectvm = MainTabControl.SelectedItem as Project;
            _projectvm.CalculateItem.Execute("");
        }

        private void caculatorxiaohao_Click(object sender, RoutedEventArgs e)
        {

            var _vm = this.DataContext as TabMainWindowViewModel;
            if (_vm.ProjectList.Count != 0)
                Xceed.Wpf.Toolkit.MessageBox.Show("能耗计算成功！", "能耗计算", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                Xceed.Wpf.Toolkit.MessageBox.Show("没有项目，请创建项目！","项目创建",MessageBoxButton.OK,MessageBoxImage.Information);
        }

        private void SaveProjecttt_Click(object sender, RoutedEventArgs e)
        {

            if (MainTabControl.SelectedItem == null)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("还未有工程！", "注意", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var _projectvm = MainTabControl.SelectedItem as Project;
            _projectvm.SaveTheProjectCommand.Execute("");
            
           
        }

  

    

    

    }
}
