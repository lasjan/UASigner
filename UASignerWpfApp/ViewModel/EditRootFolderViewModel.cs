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
using UASigner.WpfApp.Utils;
namespace UASigner.WpfApp.ViewModel
{
    public class EditRootFolderViewModel:ViewModelBase
    {
        IModelGenericProviderProperty<RootFolderModel> rootFolderModelProvider;

        public ICommand FolderOpen { get { return new CommandHandler(ExecuteFolderOpen, () => { return true; }); } }
        public ICommand ShowCerts { get { return new CommandHandler(ExecuteShowCerts, () => { return true; }); } }
        public ICommand Save { get { return new CommandHandler(ExecuteSave, () => { return true; }); } }
        public ICommand Cancel { get { return new CommandHandler(ExecuteCancel, () => { return true; }); } }
        
        RootFolderModel rootFolder;
        public RootFolderModel RootFolder
        {
            get { return this.rootFolder; }
            set { this.rootFolder = value; OnPropertyChanged();

            }
        }

        ObservableCollection<CertificateModel> fetchedCerts;
        public ObservableCollection<CertificateModel> FetchedCerts
        {
            get { return this.fetchedCerts; }
            set { this.fetchedCerts = value; OnPropertyChanged(); }
        }

        bool showCertList = false;
        public bool ShowCertList
        {
            get { return this.showCertList; }
            set { this.showCertList = value; OnPropertyChanged(); }
        }



        public EditRootFolderViewModel(IModelGenericProviderProperty<RootFolderModel> rootFolderModelProvider)
            : base()
        {
            this.rootFolderModelProvider = rootFolderModelProvider;
            RootFolder = new RootFolderModel { RootPath = this.rootFolderModelProvider.Get().RootPath };

            FetchedCerts = CreateCertCollection();

        }

        ObservableCollection<CertificateModel> CreateCertCollection()
        {
            var col = new ObservableCollection<CertificateModel>();
            Helper.Fetcher.GetCertficatesFromPath(RootFolder.RootPath).ForEach(x =>
            {
                col.Add(x);
            });

            return col;
        }
        void ExecuteFolderOpen(object parameter)
        {
            System.Windows.Forms.FolderBrowserDialog browseFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = browseFolderDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                RootFolder.RootPath = browseFolderDialog.SelectedPath;
                FetchedCerts = CreateCertCollection();
                OnPropertyChanged("FetchedCerts");
            }


            return;
        }
        void ExecuteShowCerts(object parameter)
        {
            this.ShowCertList = !ShowCertList;
        }

        

        void ExecuteSave(object parameter)
        {
            this.ValidateAll();
            this.RootFolder.ValidationReady = true;
            this.RootFolder.ValidateAll();

            if (this.RootFolder.IsValid && this.IsValid)
            {
                this.rootFolderModelProvider.Set(RootFolder);
                if (parameter is System.Windows.Window)
                {
                    ((System.Windows.Window)parameter).Close();
                }
            }

            OnErrorChanged("RootFolder");
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
