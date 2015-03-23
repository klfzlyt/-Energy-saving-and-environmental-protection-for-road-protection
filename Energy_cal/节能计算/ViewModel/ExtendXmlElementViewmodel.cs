using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Data;
using System.Xml;
using System.Data;



namespace 公路养护工程能耗计算软件ECMS.ViewModel
{
   public  class ExtendXmlElementViewmodel:ViewModelBase
    {
       public ExtendXmlElementViewmodel(XmlElement DeviceDBXmlElement,XmlElement xmlelement)
       {
           _devicedbxmlelement = DeviceDBXmlElement;
           _xmlelement = xmlelement;
           
       }

        #region 公有属性


       #region 属性


       /// <summary>
       /// 公有属性
       /// </summary>
       public int Person { get; set; }

       private XmlComment _xmlcom;
       public XmlComment Xmlcom
       {
           set
           {
               _xmlcom = value;
           }

           get
           {
               return _xmlcom;
           }


       }
	         




       #endregion


       #region XmlInnertext
       /// <summary>
       /// The <see cref="XmlInnertext" /> property's name.
       /// </summary>
       public const string XmlInnertextPropertyName = "XmlInnertext";

       private string _xmlinnertext = string.Empty;

       /// <summary>
       /// Sets and gets the XmlInnertext property.
       /// Changes to that property's value raise the PropertyChanged event. 
       /// </summary>
       public string XmlInnertext
       {
           get
           {
               return _xmlinnertext;
           }

           set
           {
               if (_xmlinnertext == value)
               {
                   return;
               }

               RaisePropertyChanging(XmlInnertextPropertyName);
               _xmlinnertext = value;
               _xmlelement.InnerText = value;

               RaisePropertyChanged(XmlInnertextPropertyName);
           }
       } 
       #endregion




       #region Xmlelement
       /// <summary>
       /// The <see cref="Xmlelement" /> property's name.
       /// </summary>
       public const string XmlelementPropertyName = "Xmlelement";

       private XmlElement _xmlelement = null;

       /// <summary>
       /// Sets and gets the Xmlelement property.
       /// Changes to that property's value raise the PropertyChanged event. 
       /// </summary>
       public XmlElement Xmlelement
       {                 
           get
           {
               return _xmlelement;
           }

           set
           {
               if (_xmlelement == value)
               {
                   return;
               }

               RaisePropertyChanging(XmlelementPropertyName);
               _xmlelement = value;
               RaisePropertyChanged(XmlelementPropertyName);
           }
       } 
       #endregion



       #region DeviceDBXmlElement

       /// <summary>
       /// The <see cref="DeviceEntities" /> property's name.
       /// </summary>
       public const string DeviceEntitiesPropertyName = "DeviceDBXmlElement";

       private XmlElement _devicedbxmlelement = null;

       /// <summary>
       /// Sets and gets the DeviceEntities property.
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

               RaisePropertyChanging(DeviceEntitiesPropertyName);
               _devicedbxmlelement = value;
               RaisePropertyChanged(DeviceEntitiesPropertyName);
           }
       }




        #endregion



        #endregion



    }
}
