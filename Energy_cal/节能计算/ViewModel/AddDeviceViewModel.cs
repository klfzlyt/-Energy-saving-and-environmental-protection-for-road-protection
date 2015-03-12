using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Xml;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;
using System.Windows.Input;
using System.Xml.Linq;
using GalaSoft.MvvmLight.Messaging;
namespace 节能计算.ViewModel
{
    /// <summary>
    /// 负责DataGrid的交互
    /// </summary>
   public  class AddDeviceViewModel:ViewModelBase
    {

       /*这个VM掌管几件事情：
        * 1:掌管着自建的带XMLelement的VM
        * 
        * 
        * 
       */
       /// <summary>
       /// 构造VM
       /// </summary>
       /// <param name="xmlpath">XML的相对路径</param>
       /// 



        //DeviceCategoryXmlElement------------------->Device.xml //从这个element里面能得到具体设备信息
        //DeviceDBXmlElement------------------------>EntityDevices.xml
        //owernersectionxmlelement---------------------->Fixedxxxxxxxxx.xml
       public AddDeviceViewModel(XmlElement devicedbxml,XmlElement finalxml,XmlElement devicecategory)
       {
           _devicedbxmlelement = devicedbxml;
           _finalxmlelement = finalxml;
           _devicecategoryxmlelement = devicecategory;                               
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
                //   var listelement = combox.Tag as ListElement;
                   try
                   {
                      // combox.ItemsSource = (XmlNodeList)new EntityConverter().Convert(listelement, null, null, null);
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
       private XmlElement BuildElementForGrid(XmlElement element1, string Name, string Group, string DeviceName, string Type, string DefaultValue = "", string Tooltip = "", string Unit = "", string IsCaculator = "false")
       {
           var doc = element1.OwnerDocument;
           
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
       //确认“名称”作为唯一主键
       private bool HasDeviceNameInFinalDoc(string name)
       {
         var nodelist=  _finalxmlelement.SelectNodes("Item/@DeviceName");
         foreach (XmlNode attributenode in nodelist)
         {
             var xmlattribute = attributenode as XmlAttribute;
             if (xmlattribute.Value == name)
             {
                 return true;             
             }         
         }
         return false;
       }
       //private bool checkstring(XmlElement xmlelement, string name, string xpathforattribute)
       //{
       //    var nodelist = xmlelement.SelectNodes(xpathforattribute);
       //    foreach (XmlNode attributenode in nodelist)
       //    {
       //        var xmlattribute = attributenode as XmlAttribute;
       //        if (xmlattribute.Value == name)
       //        {
       //            return true;
       //        }
       //    }
       //    return false;            
       //}

       private XmlElement checkstring(XmlElement xmlelement, string name, string xpathforattribute)
       {
           var nodelist = xmlelement.SelectNodes(xpathforattribute);
           foreach (XmlNode attributenode in nodelist)
           {
               var xmlattribute = attributenode as XmlAttribute;
               if (xmlattribute.Value == name)
               {
                   return xmlattribute.OwnerElement ;
               }
           }
           return null;
       }


       private bool HasDeviceNameDbInDBdoc(string name)
       {
           var nodelist = _devicedbxmlelement.SelectNodes("Device/@Name");
           foreach (XmlNode attributenode in nodelist)
           {
               var xmlattribute = attributenode as XmlAttribute;
               if (xmlattribute.Value == name)
               {
                   return true;
               }
           }
           return false;       
       }

       private XmlNodeList CheckDeviceExsit(XmlElement rootxmlelement,string name,string type)
       {
           return rootxmlelement.SelectNodes("Device[@Name=" + "\"" + name + "\"" + " and " + "@Type=" + "\"" + type + "\"" + "]");
       
       
       }

       private XmlNodeList CheckItemExsit(XmlElement rootxmlelement, string name, string type)
       {
           return rootxmlelement.SelectNodes("Item[@DeviceName=" + "\"" + name + "\"" + " and " + "@Type=" + "\"" + type + "\"" + "]");


       }


       #endregion


       #region 思考
       /// <summary>
       /// 添加设备还是有一些缺陷
       /// 还是学会记录在evernote 或者是 XMind中
       /// 不断学习
       /// </summary>
       #endregion



       #region 命令


       #region SaveDeviceCommand
       private RelayCommand _savedevicecommand;

       /// <summary>
       /// Gets the SaveDeviceCommand.
       /// </summary>
       public RelayCommand SaveDeviceCommand
       {
           get
           {
               return _savedevicecommand
                   ?? (_savedevicecommand = new RelayCommand(
                                         () =>
                                         {
                                             var forclonedeviceelement = _devicedbxmlelement.SelectSingleNode("Device[1]");
                                             XmlNode newcloneddeviceelement;
                                             if (forclonedeviceelement == null)
                                             {
                                                 var element = _devicedbxmlelement.OwnerDocument.CreateElement("Device");
                                                 var attributename = _devicedbxmlelement.OwnerDocument.CreateAttribute("Name");
                                                 attributename.Value = string.Empty;
                                                 var attributetype = _devicedbxmlelement.OwnerDocument.CreateAttribute("Type");
                                                 attributetype.Value = string.Empty;
                                                 element.Attributes.Append(attributename);
                                                 element.Attributes.Append(attributetype);
                                                 newcloneddeviceelement = element;
                                             }
                                             else
                                             {
                                                 newcloneddeviceelement = forclonedeviceelement.CloneNode(true);
                                             }
                                             //克隆设备
                                             foreach (ExtendXmlElementViewmodel externalxmlelement in ExtendXmlElementList)
                                             {
                                                 var xmlelement = externalxmlelement.Xmlelement;
                                                 var str = externalxmlelement.XmlInnertext;
                                                 if (xmlelement.HasAttribute("IsBinding"))
                                                 {
                                                     if (string.IsNullOrWhiteSpace(str))
                                                     {
                                                         MessageBox.Show("添加设备："+xmlelement.Attributes["Name"].Value + "项   请输入内容");
                                                         continue;
                                                     }
                                                     var str11 = xmlelement.Attributes["IsBinding"].Value;
                                                     if(!string.IsNullOrEmpty(str11))
                                                     newcloneddeviceelement.Attributes[str11].Value = str;                           
                                                 }
                                             }
                                             var devicedbname = newcloneddeviceelement.Attributes["Name"].Value;
                                             var devicedbtype = newcloneddeviceelement.Attributes["Type"].Value;
                                             if(CheckDeviceExsit(_devicedbxmlelement,devicedbname,devicedbtype).Count!=0)
                                             {
                                                 Xceed.Wpf.Toolkit.MessageBox.Show("已有此设备，添加无效！");
                                                 return;
                                             }
                                             _devicedbxmlelement.AppendChild(newcloneddeviceelement);
                                      //   forclonedeviceelement.ParentNode.InsertAfter(newcloneddeviceelement, forclonedeviceelement);
                                             Messenger.Default.Send<NotificationMessage>(new NotificationMessage("UpdateCombox"));
                                             Xceed.Wpf.Toolkit.MessageBox.Show("添加设备成功！");



                                         //    AddItemCommand.Execute("save");
                                         }));
           }
       }
       #endregion
       #region EditItemCommand
       private RelayCommand _edititemcommand;

       /// <summary>
       /// Gets the EditItemCommand.
       /// </summary>
       public RelayCommand EditItemCommand
       {
           get
           {
               return _edititemcommand
                   ?? (_edititemcommand = new RelayCommand(
                                         () =>
                                         {
                                             //编辑现有条目
                                             string namestring = string.Empty;
                                             string typestring = string.Empty;                                       
                                             foreach (ExtendXmlElementViewmodel externalxmlelement in ExtendXmlElementList)
                                             {
                                                 var xmlelement = externalxmlelement.Xmlelement;
                                                 var str = externalxmlelement.XmlInnertext;                                             
                                                 var Namestring = xmlelement.Attributes["Name"].Value;
                                                 if (Namestring == "设备名称")
                                                 {
                                                     namestring = str;//筛选去设备的名称
                                                 }
                                                 if (Namestring == "型号")
                                                 {
                                                     typestring = str;//筛选出设备的型号
                                                 }
                                             }
                                             //拿到名称和型号，要改finaldocxml里面的
                                             bool iseditsucceed=false;
                                           foreach (ExtendXmlElementViewmodel externalxmlelement in ExtendXmlElementList)
                                           {
                                               var xmlelement = externalxmlelement.Xmlelement;
                                               var str = externalxmlelement.XmlInnertext;
                                               var itemname=xmlelement.Attributes["Name"].Value;
                                               if (itemname == "设备名称" || itemname == "型号")
                                                   continue;
                                               var eachmatcheditem=  _finalxmlelement.SelectSingleNode("Item[@DeviceName=" + "\"" + namestring + "\"" +" and "+ "@Type=" + "\"" + typestring + "\"" +" and "+"@Name="+"\""+itemname +"\""+"]") as XmlElement;
                                               if (eachmatcheditem == null)
                                               {
                                                   MessageBox.Show("发现新添: " + itemname + "项 是否添加该项？", "默认先添加", MessageBoxButton.OKCancel);
                                                   var ele = BuildElementForGrid(_finalxmlelement, xmlelement.Attributes["Name"].Value, _devicecategoryxmlelement.Attributes["Name"].Value, namestring, typestring, externalxmlelement.XmlInnertext, xmlelement.Attributes["Tooltip"].Value, xmlelement.Attributes["Unit"].Value, xmlelement.Attributes["IsCalculator"].Value);
                                                   _finalxmlelement.AppendChild(ele);
                                               }
                                               else
                                                   if (eachmatcheditem.Attributes["DefaultValue"].Value!=str)
                                                   { 
                                                   eachmatcheditem.Attributes["DefaultValue"].Value = str;
                                                   iseditsucceed = true;
                                                   }
                                           }
                                           if (iseditsucceed)
                                               Xceed.Wpf.Toolkit.MessageBox.Show("修改成功！！");
                                           else
                                               Xceed.Wpf.Toolkit.MessageBox.Show("没做任何修改！");

                                         }));
           }
       }
       #endregion


       #region AddItemCommand
       private RelayCommand<object> _additemcommand;

       /// <summary>
       /// Gets the AddItemCommand.只复制添加作用
       /// </summary>
       public RelayCommand<object> AddItemCommand
       {
           get
           {
               return _additemcommand
                   ?? (_additemcommand = new RelayCommand<object>(
                                         information =>
                                         {
                                          //   Mouse.OverrideCursor = Cursors.Wait;
                                             var infomation = information as string;//
                                             if (infomation != "save")
                                                 return;
                                             string namestring = string.Empty;
                                             string typestring = string.Empty;
                                             foreach (ExtendXmlElementViewmodel externalxmlelement in ExtendXmlElementList)
                                             {
                                                 var xmlelement = externalxmlelement.Xmlelement;
                                                 var str = externalxmlelement.XmlInnertext;
                                                 var Namestring = xmlelement.Attributes["Name"].Value;
                                                 if (Namestring == "设备名称")
                                                 {
                                                     namestring = str;//筛选去设备的名称
                                                 }
                                                 if (Namestring == "型号")
                                                 {
                                                     typestring = str;//筛选出设备的型号
                                                 }
                                             }
                                             //1.以上步骤只是把名字跟型号拿出来
                                             //2.然后把有Isbinding属性的元素赋给克隆值
                                             if (CheckItemExsit(FinalXmlElement,namestring,typestring).Count!=0)
                                             {
                                                 Xceed.Wpf.Toolkit.MessageBox.Show("主窗口存在同样设备型号，不能添加该条目！");
                                              //   Mouse.OverrideCursor = null;
                                                 return;
                                             }
                                             //遇到主键重复，不做任何修改，直接返回

                                             else//要添加了
                                             {
                                                 foreach (ExtendXmlElementViewmodel elementt in ExtendXmlElementList)
                                                 {
                                                     var xmlele = elementt.Xmlelement;
                                                     var ele = BuildElementForGrid(_finalxmlelement, xmlele.Attributes["Name"].Value, _devicecategoryxmlelement.Attributes["Name"].Value, namestring, typestring, elementt.XmlInnertext, xmlele.Attributes["Tooltip"].Value, xmlele.Attributes["Unit"].Value, xmlele.Attributes["IsCalculator"].Value);
                                                     if (ele.Attributes["Name"].Value == "设备名称" || ele.Attributes["Name"].Value == "型号")
                                                         //跳过名称和型号两项
                                                         continue;
                                                     _finalxmlelement.AppendChild(ele);
                                                 }
                                                 Xceed.Wpf.Toolkit.MessageBox.Show("添加新条目到主窗口成功！");
                                             }
                                        //     Mouse.OverrideCursor = null;
                                         }));
           }
       }


       #endregion

       #region AddExternXmlItemCommand
       private RelayCommand<object> _addexternxmlitemcommand;

       /// <summary>
       /// Gets the AddExternXmlItemCommand.
       /// </summary>
       public RelayCommand<object> AddExternXmlItemCommand
       {
           get
           {
               return _addexternxmlitemcommand
                   ?? (_addexternxmlitemcommand = new RelayCommand<object>(
                                         button =>
                                         {
                                             var btn = button as Button;                                          
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
                                                             ExtendXmlElementViewmodel listele1 = new ExtendXmlElementViewmodel(DeviceDBXmlElement, xmleleme);
                                                             ExtendXmlElementList.Add(listele1);
                                                         }
                                                         return;
                                                     }

                                                     ExtendXmlElementViewmodel listele = new ExtendXmlElementViewmodel(DeviceDBXmlElement,element);
                                                     ExtendXmlElementList.Add(listele);
                                                 }
                                             }

                                         }));
           }
       }
       #endregion


       #region DeleteExternalXmlCommand
       private RelayCommand<object > _deleteexternalxmlcommand;

       /// <summary>
       /// Gets the DeleteExternalXmlCommand.
       /// </summary>
       public RelayCommand<object > DeleteExternalXmlCommand
       {
           get
           {
               return _deleteexternalxmlcommand
                   ?? (_deleteexternalxmlcommand = new RelayCommand<object>(
                                         button =>
                                         {
                                             var btn = button as Button;
                                             if (btn != null)
                                             {
                                                 var element = btn.Tag as ExtendXmlElementViewmodel;
                                                 if (element != null)
                                                 {
                                                     ExtendXmlElementList.Remove(element);
                                                 }
                                             }
                                         }));
           }
       }
       #endregion


       #region ExpanderContentSelectedCommand

       private RelayCommand<object> _expandercontentselectedcommand;

       /// <summary>
       /// Gets the ExpanderContentSelectedCommand.
       /// </summary>
       public RelayCommand<object> ExpanderContentSelectedCommand
       {
           get
           {
               return _expandercontentselectedcommand
                   ?? (_expandercontentselectedcommand = new RelayCommand<object>(
                                         selecteditem =>
                                         {
                                             var selectedxmlelement = selecteditem as XmlElement;
                                             //  listelements
                                             //把所有含有IsBinding的项目挑出来
                                             if (selectedxmlelement == null)
                                                 return;
                                             foreach (ExtendXmlElementViewmodel listele in ExtendXmlElementList)
                                             {
                                                 var xmlele = listele.Xmlelement;
                                                 if (xmlele.HasAttribute("IsBinding"))
                                                 {
                                                     var str = xmlele.Attributes["IsBinding"].Value;
                                                     
                                                         if (selectedxmlelement.HasAttribute(str))
                                                             listele.XmlInnertext = selectedxmlelement.Attributes[str].Value;                                                                                                          
                                                 }
                                             }
                                         }));
           }
       }



       #endregion


       private RelayCommand _deteledevicedbelementcomand;

       /// <summary>
       /// Gets the DeleteDeviceDBelementCommand.
       /// </summary>
       public RelayCommand DeleteDeviceDBelementCommand
       {
           get
           {
               return _deteledevicedbelementcomand
                   ?? (_deteledevicedbelementcomand = new RelayCommand(
                                         () =>
                                         {

                                             string ss = "fds";
                                         }));
           }
       }


       #endregion



       #region 私有字段
     
    



       #endregion

       #region 公有属性
       
       #region DeviceCategoryXmlElement
       /// <summary>
       /// The <see cref="DeviceCategoryXmlElement" /> property's name.
       /// </summary>
       public const string DeviceCategoryXmlElementPropertyName = "DeviceCategoryXmlElement";

       private XmlElement  _devicecategoryxmlelement = null;

       /// <summary>
       /// Sets and gets the DeviceCategoryXmlElement property.
       /// Changes to that property's value raise the PropertyChanged event. 
       /// </summary>
       public XmlElement  DeviceCategoryXmlElement
       {
           get
           {
               return _devicecategoryxmlelement;
           }

           set
           {
               if (_devicecategoryxmlelement == value)
               {
                   return;
               }

               RaisePropertyChanging(DeviceCategoryXmlElementPropertyName);
               _devicecategoryxmlelement = value;
               RaisePropertyChanged(DeviceCategoryXmlElementPropertyName);
           }
       }


       #endregion


       #region FinalXmlElement
       /// <summary>
       /// The <see cref="FinalXmlElement" /> property's name.
       /// </summary>
       public const string FinalXmlElementPropertyName = "FinalXmlElement";
                                                                             
       private XmlElement _finalxmlelement = null;

       /// <summary>
       /// Sets and gets the FinalXmlElement property.
       /// Changes to that property's value raise the PropertyChanged event. 
       /// </summary>
       public XmlElement FinalXmlElement
       {
           get
           {
               return _finalxmlelement;
           }

           set
           {
               if (_finalxmlelement == value)
               {
                   return;
               }

               RaisePropertyChanging(FinalXmlElementPropertyName);
               _finalxmlelement = value;
               RaisePropertyChanged(FinalXmlElementPropertyName);
           }
       }
       #endregion

       #region ExtendXmlElementList

       /// <summary>
       /// The <see cref="ExtendXmlElementList" /> property's name.
       /// </summary>
       public const string ExtendXmlElementListPropertyName = "ExtendXmlElementList";

       private ObservableCollection<ExtendXmlElementViewmodel> _extendxmlelementlist = new ObservableCollection<ExtendXmlElementViewmodel>();

       /// <summary>
       /// Sets and gets the ExtendXmlElementList property.
       /// Changes to that property's value raise the PropertyChanged event. 
       /// </summary>
       public ObservableCollection<ExtendXmlElementViewmodel> ExtendXmlElementList
       {
           get
           {
               return _extendxmlelementlist;
           }

           set
           {
               if (_extendxmlelementlist == value)
               {
                   return;
               }

               RaisePropertyChanging(ExtendXmlElementListPropertyName);
               _extendxmlelementlist = value;
               RaisePropertyChanged(ExtendXmlElementListPropertyName);
           }

       }
       #endregion

       #region DeviceDBXmlElement

       /// <summary>
        /// The <see cref="DeviceDBXmlElement" /> property's name.
        /// </summary>
        public const string DeviceDBXmlElementPropertyName = "DeviceDBXmlElement";

        private XmlElement _devicedbxmlelement = null;

        /// <summary>
        /// Sets and gets the DeviceDBXmlElement property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public XmlElement DeviceDBXmlElement
        {
            get
            {
                return _devicedbxmlelement;
            }

            set
            {
                if (_devicedbxmlelement == value)
                {
                    return;
                }

                RaisePropertyChanging(DeviceDBXmlElementPropertyName);
                _devicedbxmlelement = value;
                RaisePropertyChanged(DeviceDBXmlElementPropertyName);
            }
        }


       #endregion


        #endregion

    }
}
