using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using UASigner.Profiles.Configuration;
using UASigner.Profiles;
using UASigner.Profiles.Providers;
namespace UASigner.WpfApp.SubTabs
{
    public class Communicator:INotifyPropertyChanged
    {
        StringBuilder sb = new StringBuilder();
        public string DataLine
        {
            get { return sb.ToString(); }
            set { this.sb.Append(String.Format("{0}{1}", value, Environment.NewLine)); 
                NotifyPropertyChanged("DataLine"); }
        }
        public void AppendLine(string text)
        {
            this.sb.Append(String.Format("{0}{1}", text, Environment.NewLine));
            NotifyPropertyChanged("DataLine");
        }
        private void NotifyPropertyChanged(String data)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(data));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
    /// <summary>
    /// Interaction logic for ProcessTab.xaml
    /// </summary>
    public partial class ProcessTab : UserControl
    {
        Communicator cm = new Communicator();
        Task t;
        CancellationTokenSource tokenSource;
        CancellationToken ct;
        Configuration cfg = ConfigurationManager.GetConfiguration();
        IGenericProviderAction<IProfile> profileProvider;
        public ProcessTab()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            cfg.ConfigurationChanged += cfg_ConfigurationChanged;
            profileProvider = new ProfileProvider(cfg);
            this.DataContext = cm;
        }

        void cfg_ConfigurationChanged(object sender, EventArgs e)
        {
            try
            {
                tokenSource.Cancel();
                t.Wait();
            }  
            catch { }
            cfg = ConfigurationManager.GetConfiguration();
            profileProvider = new ProfileProvider(cfg);
            DoWork();
        }
        private void bt_AddText_Click(object sender, RoutedEventArgs e)
        {
            cm.DataLine = "dasdasdasd";
    
        }
        private void DoWork()
        {
            tokenSource = new CancellationTokenSource();
            ct = tokenSource.Token;

            t = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(2000);
                   
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        foreach (var profile in profileProvider.Get())
                        {
                            var inLoc = profile.InLocationAccess;
                            cm.AppendLine("Processing: " + inLoc.ToString());
                            var documents = inLoc.GetDocuments();
                            foreach (var document in documents)
                            {
                                foreach (var key in profile.SignProfile.KeysInfo)
                                {
                                    cm.AppendLine("["+DateTime.Now.ToString()+"]Signing: " + document.DocumentName + " with " + key.Alias);
                                }

                                inLoc.RemoveDocument(document, profile.BackupLocationAccess);
                            }

                        }
                        textbox_Console.SelectionStart = textbox_Console.Text.Length;
                        textbox_Console.ScrollToEnd();
                    }), null);
                    if (ct.IsCancellationRequested)
                    {
                        break;
                    }
                }

            }, ct);
        }
        private void bt_Start_Click(object sender, RoutedEventArgs e)
        {
            DoWork();
        }

        private void bt_Stop_Click(object sender, RoutedEventArgs e)
        {
            tokenSource.Cancel();
        }
    }
}
