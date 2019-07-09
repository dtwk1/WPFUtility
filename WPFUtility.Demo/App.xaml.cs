using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPFUtility.Demo
{
    using System.IO;

    using WPFUtility.Demo.Properties;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, INotifyPropertyChanged
    {
        public string Path
        {
            get
            {
                string path = (string)Settings.Default["Path"];
                if(System.IO.File.Exists(path))
                    return System.IO.Path.GetFullPath(path);
                else
                {
                    return path;
                }
            } 
            set
            {
                Settings.Default["Path"] = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Path)));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected override void OnStartup(StartupEventArgs e)
        {
            if (!DoesSettingExist("Path"))
            {
                MessageBox.Show("Close application and create 'Path' Setting in Properties.Settings to save the location of saved files.");
            }
            else if (Settings.Default["Path"] == string.Empty)
            {
                var di = new DirectoryInfo("Files");
                di.Create();
                Settings.Default["Path"] = di.FullName;
                Settings.Default.Save();

            }
            base.OnStartup(e);

            this.PropertyChanged += App_PropertyChanged;
        }

        private void App_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }

        private bool DoesSettingExist(string settingName)
        {
            return Settings.Default.Properties.Cast<SettingsProperty>().Any(prop => prop.Name == settingName);
        }
    }
}
