using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Xml.Linq;
using System.Xml;
using System.Diagnostics;
using System.Text.RegularExpressions;
using 公路养护工程能耗计算软件ECMS.Calculator;

namespace 公路养护工程能耗计算软件ECMS.Service
{
  public class XmlService
    {

      public void ProcessCategoryXmlFile(XmlDocument doc,string locationtosave)
      {
          var alldeviceitem = doc.SelectNodes("//DeviceItem");
          foreach (XmlNode node in alldeviceitem)
          {
              var xmlelement = node as XmlElement;

              if (!xmlelement.HasAttribute("IsCalculator"))
              {
                  var xmlattribute = doc.CreateAttribute("IsCalculator");
                  xmlattribute.Value = string.Empty;
                  xmlelement.Attributes.Append(xmlattribute);
              }
              if (!xmlelement.HasAttribute("Unit"))
              {
                  var xmlattribute = doc.CreateAttribute("Unit");
                  xmlattribute.Value = string.Empty;
                  xmlelement.Attributes.Append(xmlattribute);
              }
              if (!xmlelement.HasAttribute("Tooltip"))
              {
                  var xmlattribute = doc.CreateAttribute("Tooltip");
                  xmlattribute.Value = string.Empty;
                  xmlelement.Attributes.Append(xmlattribute);
              }
              if (!xmlelement.HasAttribute("Display"))
              {
                  var xmlattribute = doc.CreateAttribute("Display");
                  xmlattribute.Value = "true";
                  xmlelement.Attributes.Append(xmlattribute);
              }
              else
              {
                  xmlelement.Attributes["Display"].Value = "true";
              }
              if (!xmlelement.HasAttribute("Group"))
              {
                  var xmlattribute = doc.CreateAttribute("Group");
                  if (!(xmlelement.ParentNode as XmlElement).HasAttribute("HasElement"))
                      xmlattribute.Value = xmlelement.ParentNode.Attributes["Name"].Value;
                  else
                      xmlattribute.Value = xmlelement.ParentNode.ParentNode.Attributes["Name"].Value;                                    
                  xmlelement.Attributes.Append(xmlattribute);
              }



          }
          doc.Save(locationtosave);
      }

      public void ProcessTwoXmlFile(XmlDocument docentity, XmlDocument docfinal, string locationtosave)
      {
         var yuancailiao = docentity.SelectSingleNode("//原材料能耗") as XmlElement;
         var hunheliao = docentity.SelectSingleNode("//混合料生产") as XmlElement;
         var putanniaoya = docentity.SelectSingleNode("//混合料摊铺碾压") as XmlElement;
         var cailiaoyunsu = docentity.SelectSingleNode("//材料运输") as XmlElement;
         var allyuancai = yuancailiao.SelectNodes("*");
         var allhunheliao = hunheliao.SelectNodes("*");
         var allputannianya = putanniaoya.SelectNodes("*");
         var allcailiaoyunsu = cailiaoyunsu.SelectNodes("*");

         var selectedallyuancai = allyuancai.Cast<XmlNode>();
         var selectedallhunheliao = allhunheliao.Cast<XmlNode>();
         var selectedallputannianya = allputannianya.Cast<XmlNode>();
         var selectedallcailiaoyunsu = allcailiaoyunsu.Cast<XmlNode>();



         List<XmlElement> Itentallyuancailist = new List<XmlElement>();
         List<XmlElement> Itentallhunheliaolist = new List<XmlElement>();
         List<XmlElement> Itentallputannianyalist = new List<XmlElement>();
         List<XmlElement> Itentallcailiaoyunsulist = new List<XmlElement>();

         #region allyuancai
                 foreach (XmlElement yuancai in selectedallyuancai)
                 {
                     var xmlelement = yuancai as XmlElement;
                     var elementName = xmlelement.Name;
                     string elementDefaultvalue = string.Empty;
                     string elementId = string.Empty;
                     string elementUnit = string.Empty;
                     string elementBack = string.Empty;
                     if (xmlelement.HasAttribute("DefaultValue"))
                     {
                         elementDefaultvalue = xmlelement.Attributes["DefaultValue"].Value;
                     }
                     if (xmlelement.HasAttribute("DefaultValue"))
                     {
                         elementId = xmlelement.Attributes["ID"].Value;
                     }
                     if (xmlelement.HasAttribute("Unit"))
                     {
                         elementUnit = xmlelement.Attributes["Unit"].Value;
                     }
                     if (xmlelement.HasAttribute("Back"))
                     {
                         elementBack = xmlelement.Attributes["Back"].Value;
                     }
                     var AnItem = docfinal.CreateElement("Item");



                     var Nameattribute = docfinal.CreateAttribute("Name");
                     Nameattribute.Value = elementName;
                     AnItem.Attributes.Append(Nameattribute);

                     var Defaultvalueattribute = docfinal.CreateAttribute("DefaultValue");
                     Defaultvalueattribute.Value = elementDefaultvalue;
                     AnItem.Attributes.Append(Defaultvalueattribute);

                     var IDattribute = docfinal.CreateAttribute("ID");
                     IDattribute.Value = elementId;
                     AnItem.Attributes.Append(IDattribute);

                     var Tooltipattribute = docfinal.CreateAttribute("Tooltip");
                     Tooltipattribute.Value = elementBack;
                     AnItem.Attributes.Append(Tooltipattribute);

                     var Unitattribute = docfinal.CreateAttribute("Unit");
                     Unitattribute.Value = elementUnit;
                     AnItem.Attributes.Append(Unitattribute);

                     var Groupattribute = docfinal.CreateAttribute("Group");
                     Groupattribute.Value = "常量";
                     AnItem.Attributes.Append(Groupattribute);

                     var DeviceNameattribute = docfinal.CreateAttribute("DeviceName");
                     DeviceNameattribute.Value = "必填项";
                     AnItem.Attributes.Append(DeviceNameattribute);

                     var Tyepattribute = docfinal.CreateAttribute("Type");
                     Tyepattribute.Value = string.Empty;
                     AnItem.Attributes.Append(Tyepattribute);


                     Itentallyuancailist.Add(AnItem);
                     //Group="常量" DeviceName="必填项"  Type=""
                 }
           
        #endregion

         #region hunheliao
                 foreach (XmlElement hunhe in selectedallhunheliao)
                 {
                     var xmlelement = hunhe as XmlElement;
                     var elementName = xmlelement.Name;
                     string elementDefaultvalue = string.Empty;
                     string elementId = string.Empty;
                     string elementUnit = string.Empty;
                     string elementBack = string.Empty;
                     if (xmlelement.HasAttribute("DefaultValue"))
                     {
                         elementDefaultvalue = xmlelement.Attributes["DefaultValue"].Value;
                     }
                     if (xmlelement.HasAttribute("DefaultValue"))
                     {
                         elementId = xmlelement.Attributes["ID"].Value;
                     }
                     if (xmlelement.HasAttribute("Unit"))
                     {
                         elementUnit = xmlelement.Attributes["Unit"].Value;
                     }
                     if (xmlelement.HasAttribute("Back"))
                     {
                         elementBack = xmlelement.Attributes["Back"].Value;
                     }
                     var AnItem = docfinal.CreateElement("Item");

                     var Nameattribute = docfinal.CreateAttribute("Name");
                     Nameattribute.Value = elementName;
                     AnItem.Attributes.Append(Nameattribute);

                     var Defaultvalueattribute = docfinal.CreateAttribute("DefaultValue");
                     Defaultvalueattribute.Value = elementDefaultvalue;
                     AnItem.Attributes.Append(Defaultvalueattribute);

                     var IDattribute = docfinal.CreateAttribute("ID");
                     IDattribute.Value = elementId;
                     AnItem.Attributes.Append(IDattribute);

                     var Tooltipattribute = docfinal.CreateAttribute("Tooltip");
                     Tooltipattribute.Value = elementBack;
                     AnItem.Attributes.Append(Tooltipattribute);

                     var Unitattribute = docfinal.CreateAttribute("Unit");
                     Unitattribute.Value = elementUnit;
                     AnItem.Attributes.Append(Unitattribute);

                     var Groupattribute = docfinal.CreateAttribute("Group");
                     Groupattribute.Value = "常量";
                     AnItem.Attributes.Append(Groupattribute);

                     var DeviceNameattribute = docfinal.CreateAttribute("DeviceName");
                     DeviceNameattribute.Value = "必填项";
                     AnItem.Attributes.Append(DeviceNameattribute);

                     var Tyepattribute = docfinal.CreateAttribute("Type");
                     Tyepattribute.Value = string.Empty;
                     AnItem.Attributes.Append(Tyepattribute);


                     Itentallhunheliaolist.Add(AnItem);
                     //Group="常量" DeviceName="必填项"  Type=""
                 }
           

                 #endregion

         #region hunheliao
                 foreach (XmlElement tanpu in selectedallputannianya)
                 {
                     var xmlelement = tanpu as XmlElement;
                     var elementName = xmlelement.Name;
                     string elementDefaultvalue = string.Empty;
                     string elementId = string.Empty;
                     string elementUnit = string.Empty;
                     string elementBack = string.Empty;
                     if (xmlelement.HasAttribute("DefaultValue"))
                     {
                         elementDefaultvalue = xmlelement.Attributes["DefaultValue"].Value;
                     }
                     if (xmlelement.HasAttribute("DefaultValue"))
                     {
                         elementId = xmlelement.Attributes["ID"].Value;
                     }
                     if (xmlelement.HasAttribute("Unit"))
                     {
                         elementUnit = xmlelement.Attributes["Unit"].Value;
                     }
                     if (xmlelement.HasAttribute("Back"))
                     {
                         elementBack = xmlelement.Attributes["Back"].Value;
                     }
                     var AnItem = docfinal.CreateElement("Item");



                     var Nameattribute = docfinal.CreateAttribute("Name");
                     Nameattribute.Value = elementName;
                     AnItem.Attributes.Append(Nameattribute);

                     var Defaultvalueattribute = docfinal.CreateAttribute("DefaultValue");
                     Defaultvalueattribute.Value = elementDefaultvalue;
                     AnItem.Attributes.Append(Defaultvalueattribute);

                     var IDattribute = docfinal.CreateAttribute("ID");
                     IDattribute.Value = elementId;
                     AnItem.Attributes.Append(IDattribute);

                     var Tooltipattribute = docfinal.CreateAttribute("Tooltip");
                     Tooltipattribute.Value = elementBack;
                     AnItem.Attributes.Append(Tooltipattribute);

                     var Unitattribute = docfinal.CreateAttribute("Unit");
                     Unitattribute.Value = elementUnit;
                     AnItem.Attributes.Append(Unitattribute);

                     var Groupattribute = docfinal.CreateAttribute("Group");
                     Groupattribute.Value = "常量";
                     AnItem.Attributes.Append(Groupattribute);

                     var DeviceNameattribute = docfinal.CreateAttribute("DeviceName");
                     DeviceNameattribute.Value = "必填项";
                     AnItem.Attributes.Append(DeviceNameattribute);

                     var Tyepattribute = docfinal.CreateAttribute("Type");
                     Tyepattribute.Value = string.Empty;
                     AnItem.Attributes.Append(Tyepattribute);


                     Itentallputannianyalist.Add(AnItem);
                     //Group="常量" DeviceName="必填项"  Type=""
                 }


                 #endregion

         #region hunheliao
                 foreach (XmlElement yunsu in selectedallcailiaoyunsu)
                 {
                     var xmlelement = yunsu as XmlElement;
                     var elementName = xmlelement.Name;
                     string elementDefaultvalue = string.Empty;
                     string elementId = string.Empty;
                     string elementUnit = string.Empty;
                     string elementBack = string.Empty;
                     if (xmlelement.HasAttribute("DefaultValue"))
                     {
                         elementDefaultvalue = xmlelement.Attributes["DefaultValue"].Value;
                     }
                     if (xmlelement.HasAttribute("DefaultValue"))
                     {
                         elementId = xmlelement.Attributes["ID"].Value;
                     }
                     if (xmlelement.HasAttribute("Unit"))
                     {
                         elementUnit = xmlelement.Attributes["Unit"].Value;
                     }
                     if (xmlelement.HasAttribute("Back"))
                     {
                         elementBack = xmlelement.Attributes["Back"].Value;
                     }
                     var AnItem = docfinal.CreateElement("Item");



                     var Nameattribute = docfinal.CreateAttribute("Name");
                     Nameattribute.Value = elementName;
                     AnItem.Attributes.Append(Nameattribute);

                     var Defaultvalueattribute = docfinal.CreateAttribute("DefaultValue");
                     Defaultvalueattribute.Value = elementDefaultvalue;
                     AnItem.Attributes.Append(Defaultvalueattribute);

                     var IDattribute = docfinal.CreateAttribute("ID");
                     IDattribute.Value = elementId;
                     AnItem.Attributes.Append(IDattribute);

                     var Tooltipattribute = docfinal.CreateAttribute("Tooltip");
                     Tooltipattribute.Value = elementBack;
                     AnItem.Attributes.Append(Tooltipattribute);

                     var Unitattribute = docfinal.CreateAttribute("Unit");
                     Unitattribute.Value = elementUnit;
                     AnItem.Attributes.Append(Unitattribute);

                     var Groupattribute = docfinal.CreateAttribute("Group");
                     Groupattribute.Value = "常量";
                     AnItem.Attributes.Append(Groupattribute);

                     var DeviceNameattribute = docfinal.CreateAttribute("DeviceName");
                     DeviceNameattribute.Value = "必填项";
                     AnItem.Attributes.Append(DeviceNameattribute);

                     var Tyepattribute = docfinal.CreateAttribute("Type");
                     Tyepattribute.Value = string.Empty;
                     AnItem.Attributes.Append(Tyepattribute);


                     Itentallcailiaoyunsulist.Add(AnItem);
                     //Group="常量" DeviceName="必填项"  Type=""
                 }


                 #endregion  

        var yuancaifinalelement=docfinal.SelectSingleNode("//Section[@Name=\"原材料能耗\"]");
        var hunhefinalelement=docfinal.SelectSingleNode("//Section[@Name=\"混合料生产\"]");
        var tanpunianyafinalelement=docfinal.SelectSingleNode("//Section[@Name=\"混合料摊铺碾压\"]");
        var yunsufinalelment=docfinal.SelectSingleNode("//Section[@Name=\"材料运输\"]");

        foreach (XmlElement element in Itentallyuancailist)
        {
            yuancaifinalelement.AppendChild(element);
        }
        foreach (XmlElement element in Itentallhunheliaolist)
        {
            hunhefinalelement.AppendChild(element);
        }
        foreach (XmlElement element in Itentallputannianyalist)
        {
            tanpunianyafinalelement.AppendChild(element);
        }
        foreach (XmlElement element in Itentallcailiaoyunsulist)
        {
            yunsufinalelment.AppendChild(element);
        }
        docfinal.Save(locationtosave);



      }



      /// <summary>
      /// 根据算子算结果
      /// </summary>
      /// <param name="CalculateXmldoc">Resultxml.xml格式</param>
        /// <param name="FinalxmlDoc">Finalitems.xml格式</param>
      /// <returns></returns>
      public XmlDocument  FinalCalculatate(XmlDocument CalculateXmldoc, XmlDocument FinalxmlDoc,string pathtosave)
      { 
      //STEP1:选择一个算子
      //STEP2:从符合算子的算式中找项目
      //STEP3:计算改算子

          var resultelementlist = CalculateXmldoc.SelectNodes("//结果项");
          foreach (XmlNode xmlnode in resultelementlist)
          {
              XmlElement Xmlelement = xmlnode as XmlElement;//Xmlelement是每个结果项           
              if (Xmlelement.HasAttribute("calculator"))//只是保险用
              {
                  string cal = Xmlelement.Attributes["calculator"].Value;//带字母算子 这个算子得保留到最后计算
                  var matchlist = Regex.Matches(cal, "[A-Z][0-9]+").Cast<Match>();//正则把字母找出来
                  foreach (Match m in matchlist)
                  {

                      XmlElement IDelement = (XmlElement)FinalxmlDoc.SelectSingleNode("//*[@ID=" + "\"" + m.Value + "\"" + "]");//ID是主键，唯一的 以ID把Element找出来                   
                      if (IDelement.HasAttribute("DefaultValue"))
                          cal = cal.Replace(m.Value, IDelement.Attributes["DefaultValue"].Value);
                  }
                  IAlgorithm alo = new Algorithm();
                  double Oneresult = alo.Calculate(cal);
                  if (Oneresult < 0)
                      Oneresult = 0;
                  if (Oneresult > 5000)
                      Oneresult = 5000;
                  Xmlelement.SetAttribute("calculator", Oneresult.ToString());                
              }
          }
          CalculateXmldoc.Save(pathtosave);
          return CalculateXmldoc;
      }



      public void DeleteElements(XmlDataProvider xmltosave,string XPATHToDelete,string pathtosave)
      {
          var doc = xmltosave.Document;
          var rootele = doc.DocumentElement;
          var todeleteelements = rootele.SelectNodes(XPATHToDelete);
        foreach(XmlNode xmlnode in todeleteelements)
        {
          var xmlelement= xmlnode as XmlElement;
            if(xmlelement!=null)
            {
                xmlelement.ParentNode.RemoveChild(xmlnode);
            }
        }
        doc.Save(pathtosave);
        XmlDocument xmldoc = doc.CloneNode(true) as XmlDocument;
        xmltosave.Document = xmldoc;
               
              
      }
      /// <summary>
        /// 将变化过的XmlDataProvider保存到指定文件
      /// </summary>
        /// <param name="xmltosave">变化过的XmlDataProvider</param>
      /// <param name="pathtosave">要保存到的文件地址</param>
       public void UpdateData(XmlDataProvider xmltosave, string pathtosave)
      {
          using (XmlNodeReader xmlnodereader = new XmlNodeReader(xmltosave.Document))
          {
              XDocument xdoc = XDocument.Load(xmlnodereader);
              xdoc.Save(pathtosave);
          }

      }
       public void UpataDataNottoSaveTolocal(XmlDataProvider xdp)
       {

           XmlDocument xmldoc = xdp.Document.CloneNode(true) as XmlDocument;
           xdp.Document = xmldoc;
       }

      /// <summary>
       /// 将xelementtoadd加入指定父节点下的子集此方法 （可以添加监测） 只适用于父元素没有属性的情况
      /// </summary>
      /// <param name="xmltosave">提供XMLDocment的源头</param>
       /// <param name="xelementtoadd">要添加的XElement</param>
       /// <param name="parentname">父节点的名字</param>
      /// <param name="pathtosave">要保存到的文件相对路径</param>
       public  void AddXElement(XmlDataProvider xmltosave,XElement xelementtoadd,string parentname, string pathtosave)
       {
           using (XmlNodeReader xmlnodereader = new XmlNodeReader(xmltosave.Document))
           {
               XDocument xdoc = XDocument.Load(xmlnodereader);//XML TO X
               var allXelements = xdoc.Root.DescendantsAndSelf();
               var parentXelement = allXelements.First(
                     (xelement) =>
                     {
                         return xelement.Name.ToString() == parentname;
                     });
               parentXelement.Add(xelementtoadd);
               xdoc.Save(pathtosave);
               XmlDocument xmldoc = new XmlDocument();
               xmldoc.Load(xdoc.CreateReader());
               xmltosave.Document = xmldoc;
             //  xmltosave.Refresh();
           }
       }




       /// <summary>
       /// 将xelementtoadd加入指定父节点下的子集此方法要确保父元素有属性值
       /// </summary>
       /// <param name="xmltosave">提供XMLDocment的源头</param>
       /// <param name="xelementtoadd">要添加的XElement</param>
       /// <param name="parentname">父节点的名字</param>
       /// <param name="pathtosave">要保存到的文件相对路径</param>
       /// <param name="parentattributename">要查询的父元素的属性名不能为NULL，要保证XML里必须有对应的Key</param>
       /// <param name="parentattributevalue">要查询的父元素对应的属性值</param>
       public void AddXElement(XmlDataProvider xmltosave, XElement xelementtoadd, string parentname, string pathtosave,string parentattributename,string parentattributevalue)
       {
           using (XmlNodeReader xmlnodereader = new XmlNodeReader(xmltosave.Document))
           {
               XDocument xdoc = XDocument.Load(xmlnodereader);//XML TO X
               var allXelements = xdoc.Root.DescendantsAndSelf();
               XElement parentXelement=null;
               try
               {
                    parentXelement = allXelements.First(
                         (xelement) =>
                         {
                             return (xelement.Name.ToString() == parentname && (xelement.Attribute(parentattributename).Value == parentattributevalue));
                         }
                             );
               }
               catch
               {
                   return;
               }
               if (xelementtoadd == null)
                   return;

                   var elementss = parentXelement.Elements();
                   foreach (XElement xelement in elementss)
                   {
                       if (xelement.Attribute("Name").Value == xelementtoadd.Attribute("Name").Value)
                           return;
                        
                   }
               
               
                
                   parentXelement.Add(xelementtoadd);
                   xdoc.Save(pathtosave);
                   XmlDocument xmldoc = new XmlDocument();
                   xmldoc.Load(xdoc.CreateReader());
                   xmltosave.Document = xmldoc;
               
              
                   //parentXelement.Add(xelementtoadd);
                   //xdoc.Save(pathtosave);
                   //XmlDocument xmldoc = new XmlDocument();
                   //xmldoc.Load(xdoc.CreateReader());
                   //xmltosave.Document = xmldoc;
                   // xmltosave.Refresh();
               
           }
       }




    }
}
