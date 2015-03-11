﻿using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
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
using 节能计算.Model;
using System.Xml;
using System.Diagnostics;
using MahApps.Metro.Controls;
namespace 节能计算
{
    /// <summary>
    /// NewProject.xaml 的交互逻辑
    /// </summary>
    public partial class NewProject : MetroWindow
    {
        private string pjtxmllocation=null;
        public NewProject()
        {
            InitializeComponent();
            this.Closing += NewProject_Closing;
            this.AddHandler(Keyboard.KeyDownEvent,new  System.Windows.Input.KeyEventHandler(hanlderkeydown));                                                                  
        }
        private void hanlderkeydown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Debug.WriteLine("您按下了ESC");
                this.Close();
            }

        }


        void NewProject_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
         
        }



        /// <summary>
        /// 这部分代码可能要重构
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            string location  = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string original = GlobalResource.Templatepath;
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择项目文件路径";
            if (dialog.ShowDialog() ==System.Windows.Forms.DialogResult.OK)
            {
                string target = dialog.SelectedPath;
                this.prolocation.Text = target;
               // System.Windows.Forms.MessageBox.Show("已选择目标文件夹:" + target, "选择文件夹提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (string.IsNullOrEmpty(this.proname.Text))
                {
                    System.Windows.Forms.MessageBox.Show("请输入项目名称", "选择文件夹提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    System.Windows.Input.Mouse.OverrideCursor = null;
                    return;
                }
                var targetstr = target + "\\" + this.proname.Text;
                if (!Directory.Exists(targetstr))
                {
                    // Create the directory it does not exist.
                    Directory.CreateDirectory(targetstr);
                    CopyDirectoryAndFiles(targetstr, new DirectoryInfo(location + original));
                }
                else
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("项目名已存在，请重新创建！");
                    Mouse.OverrideCursor = null;
                    return;
                }
                pjtxmllocation = targetstr + "\\" + "Pjt.xml";
                XmlDocument doc = new XmlDocument();
                //targetstr = this.prolocation.Text;
                doc.Load(targetstr + "\\" + "Pjt.xml");
                var FinalitemsTemplateelement = doc.SelectSingleNode("Files/File[1]") as XmlElement;
                var DevicesDBelement = doc.SelectSingleNode("Files/File[2]") as XmlElement;
                var DevicesCategoryelement = doc.SelectSingleNode("Files/File[3]") as XmlElement;
                FinalitemsTemplateelement.SetAttribute("Location", targetstr + "\\" + "FinalitemsTemplate.xml");
                DevicesDBelement.SetAttribute("Location", targetstr + "\\" + "DevicesDB.xml");
                DevicesCategoryelement.SetAttribute("Location", targetstr + "\\" + "DevicesCategory.xml");
                doc.Save(targetstr + "\\" + "Pjt.xml");
               
            }
            System.Windows.Input.Mouse.OverrideCursor = null;
         
        }

        private void CopyDirectoryAndFiles(string des, DirectoryInfo srcDir)
        {
            if (!des.EndsWith("\\"))
            {
                des += "\\";
            }
          //  string desPath = des + srcDir.Name + "\\";
            //if (!Directory.Exists(desPath))
            //{
            //    Directory.CreateDirectory(desPath);
            //}

            foreach (FileInfo file in srcDir.GetFiles())
            {
                file.CopyTo(des + file.Name, true);
            }
            foreach (DirectoryInfo dirinfo in srcDir.GetDirectories())
            {
                CopyDirectoryAndFiles(des, dirinfo);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           // string location = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
         //   string original = GlobalResource.Templatepath;
            var proname = this.proname.Text;
         
            if (pjtxmllocation == null)
              System.Windows.Forms.MessageBox.Show("请选择文件夹！", this.proname.Text, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);               
            else
            {
                if (string.IsNullOrEmpty(this.工程名称textbox.Text) || string.IsNullOrEmpty(this.设计单位textbox.Text) || string.IsNullOrEmpty(this.施工单位textbox.Text) || string.IsNullOrEmpty(this.养护技术textbox.Text))
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("请填写完整！");
                    return;
                }
                else
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("项目创建成功!");
                    this.Close();
                    Messenger.Default.Send<Project>(new Project(pjtxmllocation, this.工程名称textbox.Text, this.设计单位textbox.Text, this.施工单位textbox.Text, this.养护技术textbox.Text) { ProjectName = proname });
                }
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
