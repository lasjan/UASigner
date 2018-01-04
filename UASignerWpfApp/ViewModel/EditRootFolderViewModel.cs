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
namespace UASigner.WpfApp.ViewModel
{
    public class EditRootFolderViewModel:ViewModelBase
    {
        IModelGenericProviderProperty<RootFolderModel> rootFolderModelProvider;


        public ICommand Save { get { return new CommandHandler(ExecuteSave, () => { return true; }); } }
        public ICommand Cancel { get { return new CommandHandler(ExecuteCancel, () => { return true; }); } }

        RootFolderModel rootFolder;
        public RootFolderModel RootFolder
        {
            get { return this.rootFolder; }
            set { this.rootFolder = value; OnPropertyChanged(); }
        }

        public EditRootFolderViewModel(IModelGenericProviderProperty<RootFolderModel> rootFolderModelProvider)
            : base()
        {
            this.rootFolderModelProvider = rootFolderModelProvider;
            RootFolder = new RootFolderModel { RootPath = this.rootFolderModelProvider.Get().RootPath };
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
