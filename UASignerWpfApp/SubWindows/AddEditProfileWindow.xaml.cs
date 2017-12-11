using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UASigner.Profiles;
using UASigner.WpfApp.Display;
using UASigner.Profiles.Providers;
using UASigner.Profiles.Configuration;
using System.Collections.ObjectModel;
 
namespace UASigner.WpfApp.SubWindows
{
    /// <summary>
    /// Interaction logic for AddProfileWindow.xaml
    /// </summary>
    public partial class AddEditProfileWindow : Window
    {
        public event AddEditProfileDelegate MyEvent;
        IProfile profile;
        Configuration cfg = ConfigurationManager.GetConfiguration();
        AccessType chosenAccessType = AccessType.Directory;
        PkInfoProvider pkInfoProvider;
        TsInfoProvider tsInfoProvider;
        List<LocationAccess> tmpLocationList;
        protected void OnAddEdit(IProfile newProfile)
        {
            if (this.MyEvent != null)
            {
                var ea = new AddEditProfileEventArgs() { ProfileToAdd = newProfile };
                this.MyEvent(this, ea);
            }
        }

        public AddEditProfileWindow(IProfile editProfile)
        {
            InitializeComponent();
            if (editProfile != null)
            {
                profile = editProfile;
            }
            else
            {
                var userProf = System.Environment.GetEnvironmentVariable("USERPROFILE");

                SignContextProfile csp = new SignContextProfile { CertificatePath = cfg.GetDefaultCertPath(), KeysInfo = new List<PKInfo>(), Level = SignatureLevel.CADES_BASELINE_B, Tsinfo = new TimeStampServerInfo() };
                profile = new GenericProfile { OutLocationAccess = new List<LocationAccess>(), InLocationAccess = new DirectoryAccess(userProf, "*.*"), SignProfile = csp };
            }
            Init();
        }
        private void Init()
        {
            pkInfoProvider = new PkInfoProvider(cfg);
            tsInfoProvider = new TsInfoProvider(cfg);
            ComboBoxEdit_InAccess.ItemsSource = ResourceManager.GetLocationTypes();
            ComboBoxEdit_InAccess.SelectedIndex = 0;

            comboBoxEdit_OutAccess.ItemsSource = ResourceManager.GetLocationTypes();
            comboBoxEdit_OutAccess.SelectedIndex = 0;
            ((App)Application.Current).LanguageChangedEvent += AddEditProfileWindow_LanguageChangedEvent;
            List<CheckBoxWrapper> selectKeyList = new List<CheckBoxWrapper>();
            ObservableCollection<CheckBoxWrapper> selectTsList = new ObservableCollection<CheckBoxWrapper>();
            foreach (var pk in pkInfoProvider.Get())
            {
                CheckBoxWrapper cb = new CheckBoxWrapper(pk) { Selected = false, DispName = pk.Alias };
                selectKeyList.Add(cb);
            }
            foreach (var ts in tsInfoProvider.Get())
            {
                CheckBoxWrapper cb = new CheckBoxWrapper(ts) { Selected = false, DispName = ts.Url };
                cb.PropertyChanged += cb_PropertyChanged;
                selectTsList.Add(cb);
            }
            this.listView_SelectKeys.ItemsSource = selectKeyList;
            this.listView_SelectTsInfo.ItemsSource = selectTsList;

            Display();
        }

        void cb_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
        }

        void AddEditProfileWindow_LanguageChangedEvent(object sender, EventArgs e)
        {
            var i = ComboBoxEdit_InAccess.SelectedIndex;
            ComboBoxEdit_InAccess.ItemsSource = ResourceManager.GetLocationTypes();
            ComboBoxEdit_InAccess.SelectedIndex = i;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OnAddEdit(profile);
            this.Close();
        }

        private void ComboBoxEdit_InAccess_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                var rr = (e.AddedItems[0] as DisplayableAccessType);
                SwitchAddInPanel(rr.AccessType);
               // ResourceManager.SetCulture(rr.Culture);

            }
        }

        private void comboBoxEdit_OutAccess_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                var rr = (e.AddedItems[0] as DisplayableAccessType);
                SwitchAddOutPanel(rr.AccessType);
               // ResourceManager.SetCulture(rr.Culture);

            }
        }
        private void btn_AddOutputClick(object sender, RoutedEventArgs e)
        {
            panel_AddSingleOutput.Visibility = System.Windows.Visibility.Visible;
            btn_AddOutput.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void btn_CancelOutProfileClick(object sender, RoutedEventArgs e)
        {
            panel_AddSingleOutput.Visibility = System.Windows.Visibility.Collapsed;
            btn_AddOutput.Visibility = System.Windows.Visibility.Visible;
        }
        private void btn_SavelOutProfileClick(object sender, RoutedEventArgs e)
        {
            panel_AddSingleOutput.Visibility = System.Windows.Visibility.Collapsed;
            btn_AddOutput.Visibility = System.Windows.Visibility.Visible;

            var selectedType = comboBoxEdit_OutAccess.SelectedItem as DisplayableAccessType;
            LocationAccess la = null;
            switch (selectedType.AccessType)
            { 
                case AccessType.Directory:
                    la = new DirectoryAccess(textBox_OutNewDirectory.Text, textBox_OutNewDirectoryFileMask.Text);
                    break;
            }

            if (la != null)
            {
                var current = profile.OutLocationAccess;
                var newList = current.ToList();
                newList.Add(la);
                profile.OutLocationAccess = newList;
            }

            Display();
        }
        private void ts_cb_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            var items = ((System.Windows.Controls.ListBox)(this.listView_SelectTsInfo)).Items.SourceCollection;
            foreach (var item in items)
            {
                var cbItem = (item as CheckBoxWrapper);
                //if(cbItem != sender as Ch)
            }
            this.listView_SelectTsInfo.Items.Refresh();
        }
        private void SwitchAddOutPanel(AccessType accessType)
        {
            this.wp_OutDirectoryInfo.Visibility = System.Windows.Visibility.Collapsed;
            this.wp_OutFtpPanel.Visibility = System.Windows.Visibility.Collapsed;

            switch (accessType)
            {
                case AccessType.Directory:
                    this.wp_OutDirectoryInfo.Visibility = System.Windows.Visibility.Visible;
                    break;
                case AccessType.Ftp:
                    this.wp_OutFtpPanel.Visibility = System.Windows.Visibility.Visible;
                    break;

            }

        }
        private void SwitchAddInPanel(AccessType accessType)
        {
            this.wp_DirectoryInfo.Visibility = System.Windows.Visibility.Collapsed;
            this.wp_FtpPanel.Visibility = System.Windows.Visibility.Collapsed;

            switch (accessType)
            { 
                case AccessType.Directory:
                    this.wp_DirectoryInfo.Visibility = System.Windows.Visibility.Visible;
                    break;
                case AccessType.Ftp:
                    this.wp_FtpPanel.Visibility = System.Windows.Visibility.Visible;
                    break;

            }
            
        }

        private void Display()
        {
            if (profile.InLocationAccess is DirectoryAccess)
            {
                this.ComboBoxEdit_InAccess.SelectedIndex = 0;
                this.textBox_NewDirectory.Text = (profile.InLocationAccess as DirectoryAccess).DirectoryPath;
                this.textBox_NewDirectoryFileMask.Text = (profile.InLocationAccess as DirectoryAccess).FileMask;

            }
            if (profile.SignProfile != null && profile.SignProfile.KeysInfo != null)
            {
                foreach (var keyInfoCb in this.listView_SelectKeys.Items)
                {
                    CheckBoxWrapper wrapper = keyInfoCb as CheckBoxWrapper;
                    if (wrapper != null)
                    {
                        PKInfo wrappedPkInfo = wrapper.WrappedObject as PKInfo;
                        if (wrappedPkInfo != null)
                        {
                            if (profile.SignProfile.KeysInfo.Any(x => x.Id == wrappedPkInfo.Id))
                            {
                                wrapper.Selected = true;
                            }
                        }
                    }
                }
            }
            List<UASigner.WpfApp.Display.DisplayableAccessType> displayableList = new List<DisplayableAccessType>();
            foreach(var la in this.profile.OutLocationAccess)
            {
                UASigner.WpfApp.Display.DisplayableAccessType dat = new DisplayableAccessType(la);
                displayableList.Add(dat);
            }
            this.listBox_OutProfilesList.ItemsSource = displayableList;

            this.listBox_OutProfilesList.Items.Refresh();
        }
    }
}
