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
        bool failed = true;

        public string Path
        {
            get
            {
                string path = failed ? "c:\\temp" : (string)Settings.Default["Title"];
                if (System.IO.File.Exists(path))
                    return System.IO.Path.GetFullPath(path);
                else
                {
                    return path;
                }
            }
            set
            {
                if (!failed)
                {
                    Settings.Default["Title"] = value;
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Path)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                if (!DoesSettingExist("Title"))
                {
                    MessageBox.Show("Close application and create 'Title' Setting in Properties.Settings to save the location of saved files.");
                }
                else if (Settings.Default["Title"] == string.Empty)
                {
                    var di = new DirectoryInfo("Files");
                    di.Create();
                    Settings.Default["Title"] = di.FullName;
                    Settings.Default.Save();
                    this.failed = false;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
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
