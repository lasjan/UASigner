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
using Microsoft.Win32;
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
        WorkMode workMode;
        PkInfoProvider pkInfoProvider;
        TsInfoProvider tsInfoProvider;
        List<LocationAccess> tmpOutLocationList;
        protected void OnAddEdit(IProfile newProfile)
        {
            if (this.MyEvent != null)
            {
                var ea = new AddEditProfileEventArgs() { ProfileToAdd = newProfile, WorkMode = workMode };
                this.MyEvent(this, ea);
            }
        }

        public AddEditProfileWindow(IProfile editProfile)
        {
            InitializeComponent();
            workMode = WorkMode.Add;
            tmpOutLocationList = new List<LocationAccess>();
            if (editProfile != null)
            {
                profile = editProfile;
                if (profile.OutLocationAccess != null)
                {
                    profile.OutLocationAccess.ToList().ForEach(x => { tmpOutLocationList.Add(x); });
                }

                workMode = WorkMode.Edit;
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
            comboBoxEdit_InAccess.ItemsSource = ResourceManager.GetLocationTypes();
            comboBoxEdit_InAccess.SelectedIndex = 0;

            comboBoxEdit_OutAccess.ItemsSource = ResourceManager.GetLocationTypes();
            comboBoxEdit_OutAccess.SelectedIndex = 0;

            comboBoxEdit_CadesProfile.ItemsSource = ResourceManager.GetCadesProfiles();
            comboBoxEdit_CadesProfile.SelectedIndex = 0;

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
                cb.PropertyChanged += cb_TsPropertyChanged;
                selectTsList.Add(cb);
            }
            this.listView_SelectKeys.ItemsSource = selectKeyList;
            this.listView_SelectTsInfo.ItemsSource = selectTsList;

            Display();
        }

        void cb_TsPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (((UASigner.WpfApp.Display.CheckBoxWrapper)(sender)).Selected)
            {
                var selectedId = ((UASigner.Profiles.TimeStampServerInfo)(((UASigner.WpfApp.Display.CheckBoxWrapper)(sender)).WrappedObject)).Id;
                ObservableCollection<CheckBoxWrapper> selectTsList = new ObservableCollection<CheckBoxWrapper>();
                foreach (var ts in tsInfoProvider.Get())
                {

                    CheckBoxWrapper cb = new CheckBoxWrapper(ts) { Selected = ts.Id == selectedId, DispName = ts.Url };
                    cb.PropertyChanged += cb_TsPropertyChanged;
                    selectTsList.Add(cb);
                }
                this.listView_SelectTsInfo.ItemsSource = selectTsList;
                this.listView_SelectTsInfo.Items.Refresh();
            }
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LocationAccess inLocationAccess = null;
            var selectedInput = comboBoxEdit_InAccess.SelectedItem as DisplayableAccessType;
            switch (selectedInput.AccessType)
            { 
                case AccessType.Directory:
                    inLocationAccess = new DirectoryAccess(this.textBox_NewDirectory.Text, this.textBox_NewDirectoryFileMask.Text);
                    break;
            }
            profile.InLocationAccess = inLocationAccess;

            profile.OutLocationAccess = tmpOutLocationList;
            foreach (var tsInfo in this.listView_SelectTsInfo.Items)
            {
                var cbItem = tsInfo as CheckBoxWrapper;
                if (cbItem != null && cbItem.Selected)
                {
                    var wrappedTsInfo = cbItem.WrappedObject as UASigner.Profiles.TimeStampServerInfo;
                    if (wrappedTsInfo != null)
                    {
                        this.profile.SignProfile.Tsinfo = wrappedTsInfo;
                    }
                }
            }
            List<PKInfo> newList = new List<PKInfo>();

            foreach (var tsKeyInfo in this.listView_SelectKeys.Items)
            {
                var cbItem = tsKeyInfo as CheckBoxWrapper;
                
                if (cbItem != null && cbItem.Selected)
                {
                    var wrappedKeyInfo = cbItem.WrappedObject as UASigner.Profiles.PKInfo;
                    if (wrappedKeyInfo != null)
                    {
                        newList.Add(wrappedKeyInfo); 
                    }
                }

            }
            this.profile.SignProfile.KeysInfo = newList;

            this.profile.SignProfile.AddCertificate = (bool)this.checkBox_AddCertificate.IsChecked;
            this.profile.SignProfile.AddContentTimeStamp = (bool)this.checkBox_AddContentTs.IsChecked;
            this.profile.SignProfile.AddValidationData = (bool)this.checkBox_AddValidationData.IsChecked;

            var selectedCadesProfile = (this.comboBoxEdit_CadesProfile.SelectedItem as DisplayableCadesProfile);
            this.profile.SignProfile.Level = selectedCadesProfile.SignLevel;

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
        private void ComboBoxEdit_CadesProfile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                var rr = (e.AddedItems[0] as DisplayableCadesProfile);
                this.textBlock_signProfileDesc.Text = rr.ToolTip;
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
            textBlock_EmptyOutputList.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void btn_CancelOutProfileClick(object sender, RoutedEventArgs e)
        {
            panel_AddSingleOutput.Visibility = System.Windows.Visibility.Collapsed;
            btn_AddOutput.Visibility = System.Windows.Visibility.Visible;

            Display();
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
                tmpOutLocationList.Add(la);
            }

            Display();
        }
        private void btn_newDirectoryDialogClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog browseFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = browseFolderDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                textBox_NewDirectory.Text = browseFolderDialog.SelectedPath;
            }
            
        }
        private void btn_outNewDirectoryDialogClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog browseFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = browseFolderDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                textBox_OutNewDirectory.Text = browseFolderDialog.SelectedPath;
            }
            
        }
        private void listBox_OutProfilesList_Remove(object sender, RoutedEventArgs e)
        {
            var commandParam = ((System.Windows.Controls.MenuItem)(sender)).CommandParameter;
            int profileId = (int)commandParam;
            var items = (this.listBox_OutProfilesList.ItemsSource as List<UASigner.WpfApp.Display.DisplayableAccessType>);
            if (items != null)
            {
                var itemToRemove = items.First(x => x.Id == profileId);
                var found = tmpOutLocationList.FirstOrDefault(x => x == itemToRemove.LocationAccess);
                if (found != null)
                {
                    tmpOutLocationList.Remove(found);
                }
                
            }
           // listBox_OutProfilesList.Items as 
            Display();
        }

        private void ts_cb_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            //var items = ((System.Windows.Controls.ListBox)(this.listView_SelectTsInfo)).Items.SourceCollection;
            //foreach (var item in items)
            //{
            //    var cbItem = (item as CheckBoxWrapper);
            //    //if(cbItem != sender as Ch)
            //}
            //this.listView_SelectTsInfo.Items.Refresh();
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
        private void SwitchOutListPane(bool hasItems)
        {
            if (hasItems)
            {
                this.sp_OutProfilesList.Visibility = System.Windows.Visibility.Visible;
                this.textBlock_EmptyOutputList.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                this.sp_OutProfilesList.Visibility = System.Windows.Visibility.Collapsed;
                this.textBlock_EmptyOutputList.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void Display()
        {
            if (profile.InLocationAccess is DirectoryAccess)
            {
                this.comboBoxEdit_InAccess.SelectedIndex = 0;
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
            if (profile.SignProfile != null && profile.SignProfile.Tsinfo != null)
            {
                foreach (var tsInfo in this.listView_SelectTsInfo.ItemsSource)
                { 
                    CheckBoxWrapper wrapper = tsInfo as CheckBoxWrapper;
                    if (wrapper != null)
                    {
                        TimeStampServerInfo wrappedTsInfo = wrapper.WrappedObject as TimeStampServerInfo;
                        if (wrappedTsInfo != null)
                        {
                            if (profile.SignProfile.Tsinfo.Id == wrappedTsInfo.Id)
                            {
                                wrapper.Selected = true;
                            }
                        }
                    }
                }
            }
            if (profile.SignProfile != null)
            {
                foreach (var cadesProfile in this.comboBoxEdit_CadesProfile.ItemsSource)
                {
                    var cadesDispProfile = cadesProfile as DisplayableCadesProfile;
                    if(cadesDispProfile!=null)
                    {
                        if (cadesDispProfile.SignLevel == profile.SignProfile.Level)
                        {
                            this.comboBoxEdit_CadesProfile.SelectedItem = cadesProfile;
                            break;
                        }
                    }
                }
            }

            if (profile.SignProfile != null)
            {
                this.checkBox_AddCertificate.IsChecked = profile.SignProfile.AddCertificate;
                this.checkBox_AddContentTs.IsChecked = profile.SignProfile.AddContentTimeStamp;
                this.checkBox_AddValidationData.IsChecked = profile.SignProfile.AddValidationData;
            }
            List<UASigner.WpfApp.Display.DisplayableAccessType> displayableList = new List<DisplayableAccessType>();
            int outIndex = 0;
            foreach(var la in this.tmpOutLocationList)
            {
                UASigner.WpfApp.Display.DisplayableAccessType dat = new DisplayableAccessType(la) { Id = outIndex++ };
                displayableList.Add(dat);
            }
            this.listBox_OutProfilesList.ItemsSource = displayableList;

            this.listBox_OutProfilesList.Items.Refresh();

            SwitchOutListPane(this.listBox_OutProfilesList.Items != null && this.listBox_OutProfilesList.Items.Count != 0);
           
        }


        void AddEditProfileWindow_LanguageChangedEvent(object sender, EventArgs e)
        {
            var i = comboBoxEdit_InAccess.SelectedIndex;
            comboBoxEdit_InAccess.ItemsSource = ResourceManager.GetLocationTypes();
            comboBoxEdit_InAccess.SelectedIndex = i;
        }

        void ExceptionHandler(Exception ex)
        {
            string msg = ex.Message;
            if (ex is UASigner.Exceptions.IVisualException)
            {
                msg = ResourceManager.BuildExceptionMessage(ex as UASigner.Exceptions.IVisualException);
            }

            MessageBox.Show(msg, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}

 
