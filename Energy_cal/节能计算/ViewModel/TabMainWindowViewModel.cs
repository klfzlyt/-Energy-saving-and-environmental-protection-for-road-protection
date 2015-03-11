using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Data;
using 节能计算.Model;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using 节能计算;
using System.Diagnostics;
using System.Xml;
using Xceed.Wpf.Toolkit;
using MahApps.Metro.Controls;
using System.IO;
using System.Windows.Input;
namespace 节能计算.ViewModel
{
  public   class TabMainWindowViewModel : ViewModelBase
    {
      //所有私有属性以“_”开头
      public Queue<Project> Projects = new Queue<Project>();
      public TabMainWindowViewModel()
      {
          Messenger.Default.Register<Project>(this,
              (project) =>
              {
                 ProjectList.Add(project);
                 Projects.Enqueue(project);
                 //Messenger.Default.Send<GenericMessage<Project>>(new GenericMessage<Project>(project));
              }
                  );
        
      
      }
      #region 命令
      private RelayCommand _openprojectcommand;

      /// <summary>
      /// Gets the OpenProjectCommand.
      /// </summary>
      public RelayCommand OpenProjectCommand
      {
          get
          {
              return _openprojectcommand
                  ?? (_openprojectcommand = new RelayCommand(
                                        () =>
                                        {
                                            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
                                            dialog.Description = "请选择项目文件路径";
                                            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                            {
                                                var projectpath = dialog.SelectedPath;
                                                var projectname = projectpath.Substring(projectpath.LastIndexOf("\\") + 1);
                                                //最好加个判断有没有Pjt.xml文件
                                                var projectxmlpath = projectpath + "\\" + "Pjt.xml";
                                                if (File.Exists(projectxmlpath))
                                                    Messenger.Default.Send<Project>(new Project(projectxmlpath) { ProjectName = projectname });
                                                else
                                                    Xceed.Wpf.Toolkit.MessageBox.Show("您所选文件夹不存在工程，请重试！");
                                            }
                                        }));
          }
      }





      private RelayCommand _newcommmand;

      /// <summary>
      /// Gets the NewCommand.
      /// </summary>
      public RelayCommand NewCommand
      {
          get
          {
              return _newcommmand
                  ?? (_newcommmand = new RelayCommand(
                                        () =>
                                        {
                                            new NewProject().ShowDialog();
                                        }));
          }
      }

      private RelayCommand<object> _closetabcommand;

      /// <summary>
      /// Gets the CloseTabCommand.
      /// </summary>
      public RelayCommand<object> CloseTabCommand
      {
          get
          {
              return _closetabcommand
                  ?? (_closetabcommand = new RelayCommand<object>(
                                        pro =>
                                        {
                                            var e = pro as MahApps.Metro.Controls.BaseMetroTabControl.TabItemClosingEventArgs;
                                           var metrotabitem= e.ClosingTabItem as MetroTabItem;
                                           var project=metrotabitem.DataContext as Project;
                                           if (System.Windows.MessageBoxResult.OK == MessageBox.Show("是否要关闭并保存项目？", project.ProjectName, System.Windows.MessageBoxButton.OKCancel, System.Windows.MessageBoxImage.Question))
                                           {
                                               project.SaveTheProjectXmlFile();
                                               ProjectList.Remove(project);
                                               e.Cancel = false;
                                               return;
                                           }
                                           else
                                           {
                                               e.Cancel = true;
                                           }
                                            
                                        }));
          }
      }


      private RelayCommand _saveallprojectxmlfile;
      /// <summary>
      /// Gets the SaveProjectXmlFile.
      /// </summary>
      public RelayCommand SaveAllProjectXmlFile
      {
          get
          {
              return _saveallprojectxmlfile
                  ?? (_saveallprojectxmlfile = new RelayCommand(
                                        () =>
                                        {
                                            if (ProjectList.Count == 0)
                                            {
                                                Xceed.Wpf.Toolkit.MessageBox.Show("还没有工程，请创建工程！","创建工程",System.Windows.MessageBoxButton.OK,System.Windows.MessageBoxImage.Asterisk);
                                                return;
                                            }
                                           foreach(Project project in ProjectList)
                                           {
                                               project.SaveTheProjectXmlFile();                                           
                                           }
                                           Xceed.Wpf.Toolkit.MessageBox.Show("保存成功！","项目保存",System.Windows.MessageBoxButton.OK,System.Windows.MessageBoxImage.Information);

                                        }));
          }
      }
      #endregion

      #region 公共属性
      /// <summary>
      /// The <see cref="ProjectList" /> property's name.
      /// </summary>
      public const string ProjectListPropertyName = "ProjectList";

      private ObservableCollection<Project> _projectlist = new ObservableCollection<Project>();

      /// <summary>
      /// Sets and gets the ProjectList property.
      /// Changes to that property's value raise the PropertyChanged event. 
      /// </summary>
      public ObservableCollection<Project> ProjectList
      {
          get
          {
              return _projectlist;
          }

          set
          {
              if (_projectlist == value)
              {
                  return;
              }

              RaisePropertyChanging(ProjectListPropertyName);
              _projectlist = value;
              RaisePropertyChanged(ProjectListPropertyName);
          }
      }
      #endregion




    }
}
