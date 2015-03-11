using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace 节能计算.Model
{
    /// <summary>
    /// 必须跟Devices耦合
    /// </summary>
 public    class LocalDeviceImageXmlElement
 {
     #region Property
     /// <summary>
        /// 图片的本地相对路径
        /// </summary>
        public string ImageSource { get; set; }
        public XmlElement Xmlelement { get; set; }

     /// <summary>
     /// 获得元素Attribute名 
     /// </summary>
        public string ElementAttributeName
        {
            get
            {
                return
                    Xmlelement.GetAttribute("Name");
            
            }
            set 
            {
                Xmlelement.SetAttribute("Name", value);
            
            }
        
        
        }

     /// <summary>
     /// 获得算子 
     /// </summary>
        public string ElementAttributeCalculator
        {
            get
            {

                return Xmlelement.GetAttribute("calculator");
               
            }
            set
            {
                Xmlelement.SetAttribute("calculator", value);
            
            }
        
        }


     #endregion
     

        /// <summary>
     /// 
     /// </summary>
     /// <param name="xmlelement"></param>
     /// <param name="imagesource">图片的相对路径</param>
        public LocalDeviceImageXmlElement(XmlElement xmlelement, string imagesource)
        {
            Xmlelement = xmlelement;
            ImageSource = imagesource;
        }



        public string GetAttribute(string name)
        {
            return Xmlelement.GetAttribute(name);
       
        
        }
        



    }
}
