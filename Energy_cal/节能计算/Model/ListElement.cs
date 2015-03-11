using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml;
namespace 节能计算.Model
{
   public  class ListElement:INotifyPropertyChanged
    {
       private string text;
       public string Text
       {
           get
           {
               return text;

           }
           set
           {
               if (text == value)
                   return;
               else
                   text = value;
                   if (PropertyChanged != null)
                   {
                       PropertyChanged(this, new PropertyChangedEventArgs("Text"));
                   }
           }

       }
       private XmlDataProvider entitydevices;


       public XmlDataProvider EntityDevices
       {
           get
           {
               return entitydevices;

           }
           set
           {
               if (entitydevices == value)
                   return;
               else
                   entitydevices = value;
               if (PropertyChanged != null)
               {
                   PropertyChanged(this, new PropertyChangedEventArgs("EntityDevices"));
               }
           }
       
       }


        private ICommand textBoxButtonCmd;

        public ICommand TextBoxButtonCmd
        {
            get
            {
                return this.textBoxButtonCmd ?? (this.textBoxButtonCmd = new TextBoxButtonCommand());
            }
        }

        public class TextBoxButtonCommand : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                //if (parameter is TextBox)
                //{
                //    MessageBox.Show("TextBox Button was clicked!" + Environment.NewLine + "Text: " + ((TextBox)parameter).Text);
                //}
                //else if (parameter is PasswordBox)
                //{
                //    MessageBox.Show("PasswordBox Button was clicked!" + Environment.NewLine + "Text: " + ((PasswordBox)parameter).Password);
                //}
            }
        }

        public XmlElement Xmlelement { get; set; }
       public ListElement()
       { 
       
       }


       public event PropertyChangedEventHandler PropertyChanged;
    }
}
