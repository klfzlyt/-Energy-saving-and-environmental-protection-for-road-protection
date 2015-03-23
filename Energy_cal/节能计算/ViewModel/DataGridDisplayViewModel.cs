using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Data;
using System.Xml;

namespace 公路养护工程能耗计算软件ECMS.ViewModel
{
  public   class DataGridDisplayViewModel : ViewModelBase
    {       
      public DataGridDisplayViewModel(XmlElement sectionxmlelement)
       {
           _sectionxmlelement = sectionxmlelement;

       }

        #region 公共属性

      /// <summary>
      /// The <see cref="SectionXmlElement" /> property's name.
      /// </summary>
      public const string SectionXmlElementPropertyName = "SectionXmlElement";

      private XmlElement _sectionxmlelement = null;

      /// <summary>
      /// Sets and gets the SectionXmlElement property.
      /// Changes to that property's value raise the PropertyChanged event. 
      /// </summary>
      public XmlElement SectionXmlElement
      {
          get
          {
              return _sectionxmlelement;
          }

          set
          {
              if (_sectionxmlelement == value)
              {
                  return;
              }

              RaisePropertyChanging(SectionXmlElementPropertyName);
              _sectionxmlelement = value;
              RaisePropertyChanged(SectionXmlElementPropertyName);
          }
      }
            


        #endregion

    }
}
