using GalaSoft.MvvmLight;
using System.Windows.Data;
using System;

namespace 公路养护工程能耗计算软件ECMS.ViewModel
{
  
    public class DevicesViewModel : ViewModelBase
    {
       
         /// <summary>
       /// 构造VM
       /// </summary>
       /// <param name="xmlpath">XML的相对路径</param>
        public DevicesViewModel(string xmlpath)
       {
           _xdp = new XmlDataProvider();
           _xdp.Source = new Uri(xmlpath, UriKind.Relative);       
       }

        #region Property



        /// <summary>
        /// The <see cref="Xdp" /> property's name.
        /// </summary>
        public const string XdpPropertyName = "Xdp";

        private XmlDataProvider _xdp = null;

        /// <summary>
        /// Sets and gets the Xdp property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public XmlDataProvider Xdp
        {
            get
            {
                return _xdp;
            }

            set
            {
                if (_xdp == value)
                {
                    return;
                }

                RaisePropertyChanging(XdpPropertyName);
                _xdp = value;
                RaisePropertyChanged(XdpPropertyName);
            }
        }
        #endregion
    }
}