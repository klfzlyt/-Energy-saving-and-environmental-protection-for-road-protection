using GalaSoft.MvvmLight;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Collections.ObjectModel;
using 节能计算.Model;
using GalaSoft.MvvmLight.Command;
using System.Xml;
using System.Diagnostics;
using GalaSoft.MvvmLight.Messaging;
using System.Linq;
namespace 节能计算.ViewModel
{
 
    public class MainViewModel : ViewModelBase
    {

        public MainViewModel()
        {
           
              
            _datagridviewmodel = new DataGridViewModel(GlobalResource.RelativeEntityPath);
            _devicesVM = new DevicesViewModel(GlobalResource.RelativeDevicesPath);
            _resultsvm = new ResultsViewModel(GlobalResource.RelativeResultsPath);
            _xmlresults = new ObservableCollection<LocalDeviceImageXmlElement>();
            wrapbtns = new ObservableCollection<AbtnWrap>();
          
            XmlNodeList nodelist = _resultsvm.Xdp.Document.DocumentElement.SelectNodes("//结果项");
            var slectedXmlElements = nodelist.Cast<XmlElement>();
            foreach (XmlElement xmlelement in slectedXmlElements)
            {

                LocalDeviceImageXmlElement imagexml = new LocalDeviceImageXmlElement(xmlelement, "..\\..\\Pictures\\Image1.png");
                _xmlresults.Add(imagexml);
            
            }
            wrapbtns.Add(new AbtnWrap() { Name = "原材料能耗(绑定)", Image = new BitmapImage(new System.Uri("..\\..\\Pictures\\Image1.png", System.UriKind.Relative)) });
            wrapbtns.Add(new AbtnWrap() { Name = "混合料生产(绑定)", Image = new BitmapImage(new System.Uri("..\\..\\Pictures\\Image2.png", System.UriKind.Relative)) });
            wrapbtns.Add(new AbtnWrap() { Name = "混合料摊铺碾压(绑定)", Image = new BitmapImage(new System.Uri("..\\..\\Pictures\\Image3.png", System.UriKind.Relative)) });
            wrapbtns.Add(new AbtnWrap() { Name = "材料运输(绑定)", Image = new BitmapImage(new System.Uri("..\\..\\Pictures\\Image4.png", System.UriKind.Relative)) });
          

        }

        #region Command

        private RelayCommand<XmlElement > _testcommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        /// 

        public RelayCommand<XmlElement> TestCommand
        {
            get
            {
                return _testcommand
                    ?? (_testcommand = new RelayCommand<XmlElement>(
                                          xmlelement =>
                                          {
                                          //    Debug.WriteLine(xmlelement.GetAttribute("Name"));

                                              Messenger.Default.Send<NotificationMessage<XmlElement>>(new NotificationMessage<XmlElement>(xmlelement,"CheckOrUnChecked"));



                                          }));
            }
        }


        #endregion



        #region Property
        /// <summary>
        /// The <see cref="DatagridViewModel" /> property's name.
        /// </summary>
        public const string XMLResultsViewModelPropertyName = "XMLResults";

        private ObservableCollection<LocalDeviceImageXmlElement> _xmlresults = null;

        /// <summary>
        /// Sets and gets the DatagridViewModel property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<LocalDeviceImageXmlElement> XMLResults
        {
            get
            {
                return _xmlresults;
            }

            set
            {
                if (_xmlresults == value)
                {
                    return;
                }

                RaisePropertyChanging(XMLResultsViewModelPropertyName);
                _xmlresults = value;
                RaisePropertyChanged(XMLResultsViewModelPropertyName);
            }
        }




        /// <summary>
        /// The <see cref="DatagridViewModel" /> property's name.
        /// </summary>
        public const string ResultsViewModelPropertyName = "ResultsVM";

        private ResultsViewModel _resultsvm = null;

        /// <summary>
        /// Sets and gets the DatagridViewModel property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ResultsViewModel ResultsVM
        {
            get
            {
                return _resultsvm;
            }

            set
            {
                if (_resultsvm == value)
                {
                    return;
                }

                RaisePropertyChanging(ResultsViewModelPropertyName);
                _resultsvm = value;
                RaisePropertyChanged(ResultsViewModelPropertyName);
            }
        }






        /// <summary>
        /// The <see cref="DatagridViewModel" /> property's name.
        /// </summary>
        public const string DevicesViewModelPropertyName = "DevicesVM";

        private DevicesViewModel _devicesVM = null;

        /// <summary>
        /// Sets and gets the DatagridViewModel property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DevicesViewModel DevicesVM
        {
            get
            {
                return _devicesVM;
            }

            set
            {
                if (_devicesVM == value)
                {
                    return;
                }

                RaisePropertyChanging(DevicesViewModelPropertyName);
                _devicesVM = value;
                RaisePropertyChanged(DevicesViewModelPropertyName);
            }
        }

        public const string WrapbtnsViewModelPropertyName = "Wrapbtns";

        private  ObservableCollection<AbtnWrap> wrapbtns= null;

        /// <summary>
        /// Sets and gets the DatagridViewModel property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<AbtnWrap> Wrapbtns
        {
            get
            {
                return wrapbtns;
            }

            set
            {
                if (wrapbtns == value)
                {
                    return;
                }

                RaisePropertyChanging(WrapbtnsViewModelPropertyName);
                wrapbtns = value;
                RaisePropertyChanged(WrapbtnsViewModelPropertyName);
            }
        }
       


        /// <summary>
        /// The <see cref="DatagridViewModel" /> property's name.
        /// </summary>
        public const string DatagridViewModelPropertyName = "DatagridVM";

        private DataGridViewModel  _datagridviewmodel = null;

        /// <summary>
        /// Sets and gets the DatagridViewModel property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DataGridViewModel DatagridVM
        {
            get
            {
                return _datagridviewmodel;
            }

            set
            {
                if (_datagridviewmodel == value)
                {
                    return;
                }

                RaisePropertyChanging(DatagridViewModelPropertyName);
                _datagridviewmodel = value;
                RaisePropertyChanged(DatagridViewModelPropertyName);
            }
        }

        #endregion
    }
}
