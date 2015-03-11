using System.Windows;
using GalaSoft.MvvmLight.Threading;
using System.Xml;
namespace 节能计算
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
     //   public static XmlDocument DataGriddoc=new XmlDocument();
      //  public static XmlDocument Devicesdoc=new XmlDocument();
     //   public static XmlDocument AddDevicesdoc=new XmlDocument();


        static App()
        {
            //这三个待斟酌 因为这只是一个project的数据不是所有project的数据
            DispatcherHelper.Initialize();
       //     DataGriddoc.Load(@"../../OriginalXml/FinalitemsTemplate.xml");
        // /   Devicesdoc.Load(@"../../OriginalXml/DevicesDB.xml");
         //   AddDevicesdoc.Load(@"../../OriginalXml/DevicesCategory.xml");

        }
    }
}
