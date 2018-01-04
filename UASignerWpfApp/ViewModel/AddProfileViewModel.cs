using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.WpfApp.Model;
using UASigner.WpfApp.Providers;
using UASigner.WpfApp.Validation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Win32;

using UASigner.Profiles;
namespace UASigner.WpfApp.ViewModel
{
    class AddProfileViewModel : ViewModelBase
    {
        WorkMode workMode = WorkMode.Add;

        IModelGenericProviderAction<PkInfoModel> modelPkInfoProvider;
        IModelGenericProviderAction<ProfileModel> modelProfileProvider;
        IModelGenericProviderAction<TimestampServerModel> timestampServerProvider;

        Dictionary<LocationType, LocationAccessModel> avilableInLocationInstances;
        Dictionary<LocationType, LocationAccessModel> avilableOutLocationInstances; 

        public ICommand Save { get { return new CommandHandler(ExecuteSave, () => { return true; }); } }
        public ICommand Cancel { get { return new CommandHandler(ExecuteCancel, () => { return true; }); } }
        public ICommand AddOutputOpen { get { return new CommandHandler(ExecuteAddOutputOpen, () => { return true; }); } }
        public ICommand LocationOutAddCancel { get { return new CommandHandler(ExecuteLocationOutAddCancel, () => { return true; }); } }
        public ICommand LocationOutAdd { get { return new CommandHandler(ExecuteLocationOutAdd, () => { return true; }); } }
        public ICommand FolderOpen { get { return new CommandHandler(ExecuteFolderOpen, () => { return true; }); } }
        public ICommand OutputLocationDelete { get { return new CommandHandler(ExecuteOutputLocationDelete, () => { return true; }); } }
        
        private ObservableCollection<LocationTypeModel> avilableLocationTypes;
        public ObservableCollection<LocationTypeModel> AvilableLocationTypes
        {
            get { return avilableLocationTypes; }
            set { avilableLocationTypes = value; OnPropertyChanged(); }
        }


        private LocationTypeModel selectedInLocationType;
        public LocationTypeModel SelectedInLocationType
        {
            get { return selectedInLocationType; }
            set
            {
                selectedInLocationType = value;
                Profile.InLocationAccess = this.avilableInLocationInstances[value.Location];
                OnPropertyChanged();
                OnPropertyChanged("Profile");
            }
        }

        private LocationTypeModel selectedOutLocationType;
        public LocationTypeModel SelectedOutLocationType
        {
            get { return selectedOutLocationType; }
            set
            {
                selectedOutLocationType = value;
                OutLocationAccess = this.avilableOutLocationInstances[value.Location];
                OnPropertyChanged();

            }
        }

        public bool HasEmptyOutput
        {
            get 
            {
                return Profile.OutLocationsCollection == null ? true : (Profile.OutLocationsCollection.Count == 0 ? true : false); 
            }

        }
        public bool HasOutputElements
        {
            get
            {
                return !HasEmptyOutput;
            }
        }

        bool showAddOutputPanel = false;
        public bool ShowAddOutputPanel
        {
            get
            {
                return showAddOutputPanel;
            }
            set
            {
                showAddOutputPanel = value;
                OnPropertyChanged();
                OnPropertyChanged("ShowOutputGrid");

            }
        }

        public bool ShowOutputGrid
        {
            get 
            { 
                return !ShowAddOutputPanel; 
            }
        }

        LocationAccessModel outLocationAccess;
        public LocationAccessModel OutLocationAccess
        {
            get
            {
                return outLocationAccess;
            }
            set
            {
                outLocationAccess = value;
                OnPropertyChanged();

            }
        }

        private ObservableCollection<CheckBoxWrapper> pkInfoKeysSelection;
        public ObservableCollection<CheckBoxWrapper> PkInfoKeysSelection
        {
            get 
            { 
                return pkInfoKeysSelection; 
            }
            set 
            { 
                pkInfoKeysSelection = value; 
                OnPropertyChanged(); 
            }
        }

        private ObservableCollection<CheckBoxWrapper> timestampServerCollection;
        public ObservableCollection<CheckBoxWrapper> TimestampServerCollection
        {
            get
            {
                return timestampServerCollection;
            }
            set
            {
                timestampServerCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CadesProfileModel> cadesProfilesCollection;
        public ObservableCollection<CadesProfileModel> CadesProfilesCollection
        {
            get
            {
                return cadesProfilesCollection;
            }
            set
            {
                cadesProfilesCollection = value;
                OnPropertyChanged();
            }
        }

        public CadesProfileModel selectedCadesProfile;
        public CadesProfileModel SelectedCadesProfile
        {
            get
            {
                return selectedCadesProfile;
            }
            set
            {
                selectedCadesProfile = value;
                Profile.SignLevel = selectedCadesProfile.SignLevel;
                OnPropertyChanged();
                OnPropertyChanged("Profile");
            }
        }

        ProfileModel profile;
        public ProfileModel Profile
        {
            get
            {
                return profile;
            }
            set
            {
                profile = value;
                OnPropertyChanged();

            }
        }

        public AddProfileViewModel(IModelGenericProviderAction<ProfileModel> modelProfileProvider
            ,IModelGenericProviderAction<PkInfoModel> modelPkInfoProvider
            ,IModelGenericProviderAction<TimestampServerModel> timestampServerProvider
            
            , ProfileModel toEdit)
        {
            this.modelPkInfoProvider = modelPkInfoProvider;
            this.modelProfileProvider = modelProfileProvider;
            this.timestampServerProvider = timestampServerProvider;

            Profile = new ProfileModel();
            
            this.PkInfoKeysSelection = new ObservableCollection<CheckBoxWrapper>();
            Profile.UsedKeysCollection = new ObservableCollection<PkInfoModel>();
            foreach (var pkInfo in modelPkInfoProvider.Get())
            {
                CheckBoxWrapper pkInfoCbWrap = new CheckBoxWrapper(pkInfo);
                pkInfoCbWrap.PropertyChanged += pkInfoCbWrap_PropertyChanged;
                this.PkInfoKeysSelection.Add(pkInfoCbWrap);
            }

            this.TimestampServerCollection = new ObservableCollection<CheckBoxWrapper>();

            foreach (var tsInfo in timestampServerProvider.Get())
            {
                CheckBoxWrapper tsInfoCbWrap = new CheckBoxWrapper(tsInfo);
                tsInfoCbWrap.PropertyChanged += tsInfoCbWrap_PropertyChanged;
                this.TimestampServerCollection.Add(tsInfoCbWrap);
            }


            CadesProfilesCollection = ResourceManager.Model.GetCadesProfiles();

            avilableLocationTypes = new ObservableCollection<LocationTypeModel>();

            avilableLocationTypes.Add(new LocationTypeModel { LocationTypeName = "Directory", Location = LocationType.DIRECTORY });
            avilableLocationTypes.Add(new LocationTypeModel { LocationTypeName = "Ftp", Location =  LocationType.FTP });

            avilableInLocationInstances = new Dictionary<LocationType, LocationAccessModel>();
            avilableInLocationInstances.Add(LocationType.DIRECTORY, new Model.FileLocationAccessModel { FileMask = "*.*" });
            avilableInLocationInstances.Add(LocationType.FTP, new Model.FtpLocationAccessModel { Port = 21 });

            avilableOutLocationInstances = new Dictionary<LocationType, LocationAccessModel>();
            avilableOutLocationInstances.Add(LocationType.DIRECTORY, new Model.FileLocationAccessModel { FileMask = "*.*" });
            avilableOutLocationInstances.Add(LocationType.FTP, new Model.FtpLocationAccessModel { Port = 21 });

            SelectedInLocationType = avilableLocationTypes.First();
            SelectedOutLocationType = avilableLocationTypes.First();

            SelectedCadesProfile = CadesProfilesCollection.First();

            
            if (toEdit != null)
            {
                Profile.Id = toEdit.Id;
                Profile.InLocationAccess = toEdit.InLocationAccess;
                Profile.OutLocationsCollection = toEdit.OutLocationsCollection;
                Profile.UsedKeysCollection = toEdit.UsedKeysCollection;
                Profile.UsedTimestampServer = toEdit.UsedTimestampServer;
                Profile.AddCertificate = toEdit.AddCertificate;
                Profile.AddContentTimeStamp = toEdit.AddContentTimeStamp;
                profile.AddValidationData = toEdit.AddValidationData;


                SelectedCadesProfile = CadesProfilesCollection.First(x => x.SignLevel == toEdit.SignLevel);

                this.workMode = WorkMode.Edit;

                if (Profile.InLocationAccess is FileLocationAccessModel)
                {
                    avilableInLocationInstances[LocationType.DIRECTORY] = Profile.InLocationAccess;
                }
                if (Profile.InLocationAccess is FtpLocationAccessModel)
                {
                    avilableInLocationInstances[LocationType.FTP] = Profile.InLocationAccess;
                    this.SelectedInLocationType = avilableLocationTypes.First(x => x.Location == LocationType.FTP);
                }

                foreach (var pkCbInfo in this.PkInfoKeysSelection)
                { 
                    PkInfoModel pkInfoModel = (PkInfoModel)pkCbInfo.WrappedModel;
                    if(Profile.UsedKeysCollection.Any(x=>x.Id == pkInfoModel.Id))
                    {
                        pkCbInfo.IsChecked = true;
                    }
                }

                if (Profile.UsedTimestampServer != null)
                {
                    foreach (var tsCbinfo in TimestampServerCollection)
                    {
                        TimestampServerModel tsModel = (TimestampServerModel)tsCbinfo.WrappedModel;
                        if (tsModel.Id == Profile.UsedTimestampServer.Id)
                        {
                            tsCbinfo.IsChecked = true;
                        }
                    }
                }
            }

            if (Profile.OutLocationsCollection == null)
            {
                Profile.OutLocationsCollection = new ObservableCollection<LocationAccessModel>();
                Profile.OutLocationsCollection.CollectionChanged += OutLocationsCollection_CollectionChanged;
            }
        }

        void tsInfoCbWrap_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(sender is CheckBoxWrapper)
            {
                if (((CheckBoxWrapper)sender).IsChecked)
                {
                    var tsModel = ((CheckBoxWrapper)sender).WrappedModel as TimestampServerModel;
                    Profile.UsedTimestampServer = tsModel;
                    foreach (var tsCb in TimestampServerCollection)
                    {
                        if (tsModel != null)
                        {
                            if ((tsCb.WrappedModel as TimestampServerModel).Id != tsModel.Id)
                            {
                                tsCb.IsChecked = false;
                            }
                        }

                    }
                }
                else
                {
                    var tsModel = ((CheckBoxWrapper)sender).WrappedModel as TimestampServerModel;
                    if (Profile.UsedTimestampServer != null && Profile.UsedTimestampServer.Id == tsModel.Id)
                    {
                        Profile.UsedTimestampServer = null;
                    }
                }
            }

        }

        void pkInfoCbWrap_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.PkInfoKeysSelection.Where(x => x.IsChecked).ToList().ForEach(
                y => 
                {
                    if (!Profile.UsedKeysCollection.Any(z => z.Id == (y.WrappedModel as PkInfoModel).Id))
                    {
                        Profile.UsedKeysCollection.Add(y.WrappedModel as PkInfoModel);
                    }
                }
                );

            if (sender is CheckBoxWrapper)
            {
                if (!((CheckBoxWrapper)sender).IsChecked)
                {
                    var pkModel = ((CheckBoxWrapper)sender).WrappedModel as PkInfoModel;
                    if (Profile.UsedKeysCollection.Any(x => x.Id == pkModel.Id))
                    {
                        var item = Profile.UsedKeysCollection.First(x => x.Id == pkModel.Id);
                        Profile.UsedKeysCollection.Remove(item);
                    }
                }
            }

            Profile.ValidateProperty("UsedKeysCollection");
        }


        void OutLocationsCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("HasEmptyOutput");
            OnPropertyChanged("HasOutputElements");
        }

        void ExecuteAddOutputOpen(object parameter)
        {
            ShowAddOutputPanel = true;
            return;
        }
        void ExecuteLocationOutAddCancel(object parameter)
        {
            ShowAddOutputPanel = false;
            return;
        }
        void ExecuteLocationOutAdd(object parameter)
        {
            OutLocationAccess.ValidationReady = true;
            OutLocationAccess.ValidateAll();
            if (OutLocationAccess.IsValid)
            {
                if (this.Profile.OutLocationsCollection == null)
                {
                    this.Profile.OutLocationsCollection = new ObservableCollection<LocationAccessModel>();
                }
                this.Profile.OutLocationsCollection.Add(OutLocationAccess);

                ShowAddOutputPanel = false;
                Profile.ValidateProperty("OutLocationsCollection");
            }
            
            return;
        }

        void ExecuteOutputLocationDelete(object parameter)
        {
            var modelToDelete = parameter as LocationAccessModel;

            if (modelToDelete != null)
            {
                if (this.Profile.OutLocationsCollection != null)
                {
                    this.Profile.OutLocationsCollection.Remove(modelToDelete);
                }
            }
        }
        void ExecuteFolderOpen(object parameter)
        {
            System.Windows.Forms.FolderBrowserDialog browseFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = browseFolderDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                switch ((string)parameter)
                { 
                    case "OutLocationAccess":
                        if (this.OutLocationAccess is FileLocationAccessModel)
                        {
                            ((FileLocationAccessModel)this.OutLocationAccess).Path = browseFolderDialog.SelectedPath;
                        }
                        break;
                    case "InLocationAccess":
                        if (this.Profile.InLocationAccess is FileLocationAccessModel)
                        {
                            ((FileLocationAccessModel)this.Profile.InLocationAccess).Path = browseFolderDialog.SelectedPath;
                        }
                        break;
                }
            }
            return;
        }

 
        void ExecuteSave(object parameter)
        {
            this.ValidateAll();
            this.Profile.ValidationReady = true;
            this.Profile.ValidateAll();

            if (this.Profile.IsValid && this.IsValid)
            {
                switch (workMode)
                {
                    case WorkMode.Add:
                        this.modelProfileProvider.Add(Profile);
                        break;
                    case WorkMode.Edit:
                        this.modelProfileProvider.Edit(Profile);
                        break;
                }

                if (parameter is System.Windows.Window)
                {
                    ((System.Windows.Window)parameter).Close();
                }
            }

            OnErrorChanged("Profile");
            return;
        }

        void ExecuteCancel(object parameter)
        {
            if (parameter is System.Windows.Window)
            {
                ((System.Windows.Window)parameter).Close();
            }
        }
    }
}
