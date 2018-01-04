using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles;
using UASigner.WpfApp.Display;
using UASigner.Profiles.Providers;
using UASigner.Profiles.Configuration;
using UASigner.WpfApp.SubWindows;
using UASigner.WpfApp.Providers;
using UASigner.WpfApp.Model;
using System.Windows.Input;
namespace UASigner.WpfApp.ViewModel
{
    public class AppViewViewModel : ViewModelBase, IObserver<RootFolderModel>
    {
        Configuration config;
        IModelGenericProviderAction<PkInfoModel> pkInfoModelProvider;
        IModelGenericProviderAction<ProfileModel> profileModelProvider;
        IModelGenericProviderAction<TimestampServerModel> timestampServerModelProvider;
        IGenericProviderAction<IProfile> profileProvider;
        IGenericProviderAction<PKInfo> pkInfoProvider;
        IGenericProviderAction<TimeStampServerInfo> timestampServerProvider;

        IGenericPropertyProvider<string> rootFolderProvider;
        IModelGenericProviderProperty<RootFolderModel> rootFolderModelProvider;

        public ICommand KeyItemEdit { get { return new CommandHandler(ExecuteKeyItemEdit, () => { return true; }); } }
        public ICommand KeyItemDelete { get { return new CommandHandler(ExecuteKeyItemDelete, () => { return true; }); } }
        public ICommand KeyItemAdd{ get { return new CommandHandler(ExecuteKeyItemAdd, () => { return true; }); } }

        public ICommand ProfileItemEdit { get { return new CommandHandler(ExecuteProfileItemEdit, () => { return true; }); } }
        public ICommand ProfileItemDelete { get { return new CommandHandler(ExecuteProfileItemDelete, () => { return true; }); } }
        public ICommand ProfileItemAdd { get { return new CommandHandler(ExecuteProfileItemAdd, () => { return true; }); } }

        public ICommand RootFolderEdit { get { return new CommandHandler(ExecuteRootFolderEdit, () => { return true; }); } }

        ServiceMessageModel serviceMessage;
        public ServiceMessageModel ServiceMessage
        {
            get { return serviceMessage; }
            set { this.serviceMessage= value; OnPropertyChanged(); }
        }

        RootFolderModel rootFolder;
        public RootFolderModel RootFolder
        {
            get { return this.rootFolder; }
            set {this.rootFolder = value;OnPropertyChanged();}
        }
        
        ObservableCollection<PkInfoModel> pkInfoList;
        public ObservableCollection<PkInfoModel> PkInfoList
        {
            get { return this.pkInfoList; }
            set { this.pkInfoList = value; OnPropertyChanged(); }
        }

        ObservableCollection<ProfileModel> profileList;
        public ObservableCollection<ProfileModel> ProfileList
        {
            get { return this.profileList; }
            set { this.profileList = value; OnPropertyChanged(); }
        }

        ObservableCollection<TimestampServerModel> timestampServerList;
        public ObservableCollection<TimestampServerModel> TimestampServerList
        {
            get { return this.timestampServerList; }
            set { this.timestampServerList = value; OnPropertyChanged(); }
        }

        public AppViewViewModel(Configuration config)
        {
            this.config = config;

            profileProvider = new ProfileProvider(config);
            pkInfoProvider = new PkInfoProvider(config);
            pkInfoProvider.AddObserver(profileProvider);

            timestampServerProvider = new TsInfoProvider(config);
            timestampServerProvider.AddObserver(profileProvider);

            pkInfoModelProvider = new ModelPkInfoProvider(pkInfoProvider);
            profileModelProvider = new ModelProfileProvider(profileProvider);
            timestampServerModelProvider = new ModelTimestampServerProvider(timestampServerProvider);

            rootFolderProvider = new RootFolderProvider(config);
            rootFolderModelProvider = new ModelRootFolderProvider(rootFolderProvider);

            rootFolderModelProvider.Subscribe(this);

            PkInfoList = pkInfoModelProvider.Get();
            ProfileList = profileModelProvider.Get();
            TimestampServerList = timestampServerModelProvider.Get();

            RootFolder = rootFolderModelProvider.Get();

        }

        void ExecuteKeyItemEdit(object parameter)
        {
            var modelToEdit = parameter as PkInfoModel;
            if (modelToEdit != null)
            {
                var o = new UASigner.WpfApp.View.AddPkInfoView();
                var vm = new UASigner.WpfApp.ViewModel.AddPkInfoViewModel(pkInfoModelProvider, modelToEdit);
                o.DataContext = vm;
                o.Show();
            }
        }
        void ExecuteKeyItemDelete(object parameter)
        { 
            var modelToEdit = parameter as PkInfoModel;
            if (modelToEdit != null)
            {
                pkInfoModelProvider.Delete(modelToEdit);
            }
        }

        void ExecuteKeyItemAdd(object parameter)
        {
            var o = new UASigner.WpfApp.View.AddPkInfoView();
            var vm = new UASigner.WpfApp.ViewModel.AddPkInfoViewModel(pkInfoModelProvider, null);
            o.DataContext = vm;
            o.Show();
        }

        void ExecuteProfileItemEdit(object parameter)
        {
            var modelToEdit = parameter as ProfileModel;
            if (modelToEdit != null)
            {
                var o = new UASigner.WpfApp.View.AddProfileView();
                var vm = new UASigner.WpfApp.ViewModel.AddProfileViewModel(profileModelProvider, pkInfoModelProvider,timestampServerModelProvider, modelToEdit);
                o.DataContext = vm;
                o.Show();
            }
        }
        void ExecuteProfileItemDelete(object parameter)
        {
            var modelToEdit = parameter as ProfileModel;
            if (modelToEdit != null)
            {
                profileModelProvider.Delete(modelToEdit);
            }
        }

        void ExecuteProfileItemAdd(object parameter)
        {
            var o = new UASigner.WpfApp.View.AddProfileView();
            var vm = new UASigner.WpfApp.ViewModel.AddProfileViewModel(profileModelProvider, pkInfoModelProvider, timestampServerModelProvider, null);
            o.DataContext = vm;
            o.Show();
        }
        void ExecuteRootFolderEdit(object RootFolderEdit)
        {
            var o = new UASigner.WpfApp.View.EditRootFolder();
            var vm = new UASigner.WpfApp.ViewModel.EditRootFolderViewModel(rootFolderModelProvider);
            o.DataContext = vm;
            o.Show();
        }



        public void OnCompleted()
        {
            //throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            //throw new NotImplementedException();
        }

        public void OnNext(RootFolderModel value)
        {
            this.RootFolder = value;
        }
    }
}
