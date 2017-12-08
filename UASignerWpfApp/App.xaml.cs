using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UASigner.Profiles.Configuration;
using UASigner.Profiles;
namespace UASigner.WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public event EventHandler LanguageChangedEvent;

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            e.Handled = true;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ConfigurationManager.InitWithFile(@"e:\UA\Configuration.xml");
            ResourceManager.ResourceNs = "UASigner.WpfApp.Properties.Resources";
            ResourceManager.SetCulture("en-US");
            
        }

        public void OnLanguageChange()
        {
            var rm = ResourceManager.GetResourceManager();
            var resources = rm.GetResourceSet(System.Globalization.CultureInfo.DefaultThreadCurrentUICulture, true, true);
          
            foreach (var window in App.Current.Windows)
            {
                var frElem = window as System.Windows.FrameworkElement;
                if (frElem != null)
                {
                    foreach (var resource in resources)
                    {
                        var entry = resource as System.Collections.DictionaryEntry?;
                        if (entry != null)
                        {
                            frElem.Resources["langRes." + entry.Value.Key] = entry.Value.Value;
                        }
                    }
                }
            }
            if (LanguageChangedEvent != null)
            {
                LanguageChangedEvent(this, null);
            }
        }
    }
}
