using 公路养护工程能耗计算软件ECMS;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;
using 公路养护工程能耗计算软件ECMS.Service;
using System.Windows.Input;
namespace 公路养护工程能耗计算软件ECMS.Model
{
    public class Project : ViewModelBase
    {

        public bool IsLoaded { get; set; }

        public  int LiqingIndex { set; get; }
        public string ProjectName { get; set; }//项目名称 唯一标示项目
        public string ProjectXMLLocation { get; set; }//保留为绝对地址 

        private XmlDocument _finaldoc;


        public XmlDocument FinalDoc
        {
            set
            {
                _finaldoc = value;
            }
            get
            {
                if (_finaldoc == null)
                {
                    var xmlelemnt = _projectpjt.SelectSingleNode("Files/File[1]") as XmlElement;//Finaldoc是第一个
                    var location = xmlelemnt.Attributes["Location"].Value;
                    _finaldoc = new XmlDocument();
                    _finaldoc.Load(location);
                }
                return _finaldoc;
            }
        
        }
        #region 构造函数
        public Project(string ProjectXMLLocation)
        {
            this.ProjectXMLLocation = ProjectXMLLocation;
            var project = Projectpjt;//初始化Projectpjt
            IsLoaded = false;
           
            //初始化工程项目   
        }
        public Project(string ProjectXMLLocation, string 工程名称, string 设计单位, string 施工单位, string 养护技术)           
        {
            this.ProjectXMLLocation = ProjectXMLLocation;
            var project = Projectpjt;//初始化Projectpjt
            IsLoaded = false;
            this.工程名称 = 工程名称;
            this.设计单位 = 设计单位;
            this.施工单位 = 施工单位;
            this.养护技术 = 养护技术;
        
        }
        #endregion
        private XmlDocument _devicedbdoc;

        public XmlDocument DeviceDBDoc 
        
        {
            set
            {
                _devicedbdoc = value;
            }

            get
            {

                if (_devicedbdoc == null)
                {
                    var xmlelemnt = _projectpjt.SelectSingleNode("Files/File[2]") as XmlElement;//DB是第二个
                    var location = xmlelemnt.Attributes["Location"].Value;
                    _devicedbdoc = new XmlDocument();
                    _devicedbdoc.Load(location);
                }
                return _devicedbdoc;
            }
        
        }


        #region 工程名称
        /// <summary>
        /// The <see cref="工程名称" /> property's name.
        /// </summary>
        public const string 工程名称PropertyName = "工程名称";

        private string _工程名称 = string.Empty;

        /// <summary>
        /// Sets and gets the 工程名称 property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string 工程名称
        {
            get
            {
                return Projectpjt.SelectSingleNode("//ProjectItem[@Name=\"工程名称\"]").InnerText;
            }

            set
            {
                Projectpjt.SelectSingleNode("//ProjectItem[@Name=\"工程名称\"]").InnerText = value;
                if (_工程名称 == value)
                {
                    return;
                }

                RaisePropertyChanging(工程名称PropertyName);
                _工程名称 = value;
                RaisePropertyChanged(工程名称PropertyName);
            }
        }
        #endregion

        #region 养护技术
        /// <summary>
        /// The <see cref="养护技术" /> property's name.
        /// </summary>
        public const string 养护技术PropertyName = "养护技术";

        private string _养护技术 = string.Empty;

        /// <summary>
        /// Sets and gets the 养护技术 property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string 养护技术
        {
            get
            {
                return Projectpjt.SelectSingleNode("//ProjectItem[@Name=\"养护技术\"]").InnerText;
            }

            set
            {
                Projectpjt.SelectSingleNode("//ProjectItem[@Name=\"养护技术\"]").InnerText = value;
                if (_养护技术 == value)
                {
                    return;
                }

                RaisePropertyChanging(养护技术PropertyName);
                _养护技术 = value;
                RaisePropertyChanged(养护技术PropertyName);
            }
        }



        #endregion

        #region 设计单位
        /// <summary>
        /// The <see cref="设计单位" /> property's name.
        /// </summary>
        public const string 设计单位PropertyName = "设计单位";

        private string _设计单位 = string.Empty;

        /// <summary>
        /// Sets and gets the 设计单位 property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string 设计单位
        {
            get
            {
                return Projectpjt.SelectSingleNode("//ProjectItem[@Name=\"设计单位\"]").InnerText; 
            }

            set
            {
                Projectpjt.SelectSingleNode("//ProjectItem[@Name=\"设计单位\"]").InnerText = value;
                if (_设计单位 == value)
                {
                    return;
                }

                RaisePropertyChanging(设计单位PropertyName);
                _设计单位 = value;
                RaisePropertyChanged(设计单位PropertyName);
            }
        }


        #endregion

        #region 施工单位
        /// <summary>
        /// The <see cref="施工单位" /> property's name.
        /// </summary>
        public const string 施工单位PropertyName = "施工单位";

        private string _施工单位 = string.Empty;

        /// <summary>
        /// Sets and gets the 施工单位 property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string 施工单位
        {
            get
            {
                return   Projectpjt.SelectSingleNode("//ProjectItem[@Name=\"施工单位\"]").InnerText;
            }

            set
            {
                Projectpjt.SelectSingleNode("//ProjectItem[@Name=\"施工单位\"]").InnerText = value;
                if (_施工单位 == value)
                {
                    return;
                }

                RaisePropertyChanging(施工单位PropertyName);
                _施工单位 = value;
                RaisePropertyChanged(施工单位PropertyName);
            }
        }



        #endregion




        private XmlDocument _devicecategorydoc=null;

       
        public XmlDocument DevicesCategoryDoc 
        
        {
            set
            {
                _devicecategorydoc = value;
            }

            get
            {
                if(_devicecategorydoc==null)
                {
                    var xmlelemnt = _projectpjt.SelectSingleNode("Files/File[3]") as XmlElement;//Category是第三个
                    var location = xmlelemnt.Attributes["Location"].Value;
                    _devicecategorydoc = new XmlDocument();
                    _devicecategorydoc.Load(location);
                }
                return _devicecategorydoc;
            }
        
        }

        private XmlDocument _projectpjt=null;
        public XmlDocument Projectpjt 
        {
            set
            {
                _projectpjt = value;
            }
            get
            {
                if (_projectpjt == null)
                {
                    _projectpjt = new XmlDocument();
                    if (string.IsNullOrEmpty(ProjectXMLLocation))
                        throw new  NotImplementedException();
                    try
                    {
                        _projectpjt.Load(ProjectXMLLocation);
                    }
                    catch(Exception e)
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show(e.Message+" 请重新选择文件！");
                    
                    }
                }              
                return _projectpjt;
                    
            }
        }


        #region 私有方法
        //private void LoadProject()
        //{
        //    Projectpjt = new XmlDocument();
        //    Projectpjt.Load(ProjectName);
        //}

        //private void LoadFinalDoc()
        //{
        //  XmlElement xmlelement=Projectpjt.SelectSingleNode("") as XmlElement; 
        //}

      
        #endregion


        #region 公共方法

        public void SaveTheProjectXmlFile()
        {
            F11NodeProcess();
            Projectpjt.Save(Projectpjt.BaseURI.Substring(8));
            DevicesCategoryDoc.Save(DevicesCategoryDoc.BaseURI.Substring(8));
            FinalDoc.Save(FinalDoc.BaseURI.Substring(8));
            DeviceDBDoc.Save(DeviceDBDoc.BaseURI.Substring(8));

        }

        private void F11NodeProcess()
        {
            var F11Node = FinalDoc.SelectSingleNode("Sections/Section[1]/Item[@ID='F11']") as XmlElement;
            if (F11Node == null)
            {
                //说明F11Node不存在，设置F11Node
                XmlElement LiqingStylexmlelement = FinalDoc.SelectSingleNode("Sections/Section[1]/Item[@LiQingIndex=" + "\"" + LiqingIndex.ToString() + "\"" + "]") as XmlElement;
                LiqingStylexmlelement.SetAttribute("ID", "F11");
            }
        }

        #endregion

        #region 命令

        private RelayCommand _calculateitems;

        /// <summary>
        /// Gets the CalculateItem.
        /// </summary>
        public RelayCommand CalculateItem
        {
            get
            {
                return _calculateitems
                    ?? (_calculateitems = new RelayCommand(
                                          () =>
                                          {
                                              F11NodeProcess();
                                              var service = new XmlService();                                             
                                              var caculatordocment=new XmlDocument();
                                              caculatordocment.Load(@"./Resultxml.xml");//算式放在根目录下
                                              var resultdoc = service.FinalCalculatate(caculatordocment, FinalDoc, ProjectXMLLocation.Substring(0, ProjectXMLLocation.LastIndexOf("\\") + 1) + "Result.xml");//算出来的结果放在工程文件夹下，名称为Result.xml
                                              Mouse.OverrideCursor = Cursors.Wait;
                                              new RenderResults(resultdoc, this).Show();
                                              Mouse.OverrideCursor = null;
                                          }));
            }
        }

        private RelayCommand _savetheprojectcommand;

        /// <summary>
        /// Gets the SaveTheProjectCommadn.
        /// </summary>
        public RelayCommand SaveTheProjectCommand
        {
            get
            {
                return _savetheprojectcommand
                    ?? (_savetheprojectcommand = new RelayCommand(
                                          () =>
                                          {
                                              SaveTheProjectXmlFile();
                                              Xceed.Wpf.Toolkit.MessageBox.Show("工程" + ProjectName + "保存" + ProjectXMLLocation + "成功！", ProjectName);
                                          }));
            }
        }




        private RelayCommand<object> _openaddwindow;

        /// <summary>
        /// Gets the OpenAddItemsWindow.
        /// </summary>
        public RelayCommand<object> OpenAddItemsWindow
        {
            get
            {
                return _openaddwindow
                    ?? (_openaddwindow = new RelayCommand<object>(
                                          str =>
                                          { 
                                              //Temp
                                             
                                              var strin = str as string;
                                              var index=strin.IndexOf('.');
                                              var section = strin.Substring(0, index);
                                              var device = strin.Substring(index + 1);
                                             // Debug.WriteLine("section" + section);
                                           //   Debug.WriteLine("device" + device);
                                              //      var devicedb = DeviceDBDoc.SelectSingleNode("Sections/Section[@Name=" + "\"" + section + "\"" + "]" + "/Devices[@Name=" + "\"" + device + "\"" + "]") as XmlElement;
                                              var devicedb = DeviceDBDoc.SelectSingleNode("//Devices[@Name=" + "\"" + device + "\"" + "]") as XmlElement;
                                              //var devicecategory = DevicesCategoryDoc.SelectSingleNode("Sections/Section[@Name=" + "\"" + section + "\"" + "]" + "/Device[@Name=" + "\"" + device + "\"" + "]") as XmlElement;
                                              var devicecategory = DevicesCategoryDoc.SelectSingleNode("//Device[@Name=" + "\"" + device + "\"" + "]") as XmlElement;
                                              var devicefinal = FinalDoc.SelectSingleNode("Sections/Section[@Name=" + "\"" + section + "\"" + "]") as XmlElement;
                                              new AddDeviceItem(devicedb, devicefinal, devicecategory,device, "//Devices[@Name=" + "\"" + device + "\"" + "]" + "/Device").Show();


                                          }));
            }
        }

        #endregion

    }
}
