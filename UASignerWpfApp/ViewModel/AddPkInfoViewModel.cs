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
    class AddPkInfoViewModel : ViewModelBase
    {
        IModelGenericProviderAction<PkInfoModel> modelPkInfoProvider;
        Dictionary<Profiles.PK_LOCATION, PkInfoModel> avilablePkInfosInstances; 
        private ObservableCollection<PkTypeModel> avilablePkTypes;
        WorkMode workMode = WorkMode.Add;
        

        public ICommand Save { get { return new CommandHandler(ExecuteSave, () => { return true; }); } }
        public ICommand Cancel { get { return new CommandHandler(ExecuteCancel, () => { return true; }); } }
        public ICommand OpenFile { get { return new CommandHandler(ExecuteOpen, () => { return true; }); } }
        string test;

        public string Test
        {
            get { return test; }
            set { test = value; OnPropertyChanged(); }
        }

        bool isSaving = false;
        public bool IsSaving
        {
            get { return isSaving; }
            set { isSaving = value; OnPropertyChanged(); }
        }

        private PkTypeModel selectedPkType;
        public PkTypeModel SelectedPkType 
        {
            get { return selectedPkType; }
            set 
            { 
                selectedPkType = value;
                ChosenPkInfo = this.avilablePkInfosInstances[value.Location];
                OnPropertyChanged();
                OnPropertyChanged("FileLocation");
                OnPropertyChanged("VendorDllPath");
            }
        }

        private PkInfoModel chosenPkInfo;
        public PkInfoModel ChosenPkInfo
        {
            get 
            {
                this.chosenPkInfo = this.avilablePkInfosInstances[SelectedPkType.Location];
                return chosenPkInfo;
            }
            set 
            {
                this.chosenPkInfo = value;
                
                OnPropertyChanged();
            }
        }

        public string VendorDllPath
        {
            get
            {
                if (ChosenPkInfo is PkUsbStickPkInfoModel)
                {
                    return ((PkUsbStickPkInfoModel)ChosenPkInfo).VendorDllPath;
                }
                return String.Empty;
            }
            set
            {
                if (ChosenPkInfo is PkUsbStickPkInfoModel)
                {
                    ((PkUsbStickPkInfoModel)ChosenPkInfo).VendorDllPath = value;
                }

                OnPropertyChanged();
            }
        }
        
        public ObservableCollection<PkTypeModel> AvilablePkTypes
        {
            get { return avilablePkTypes; }
            set { avilablePkTypes = value; }
        }

        public AddPkInfoViewModel(IModelGenericProviderAction<PkInfoModel> modelPkInfoProvider,PkInfoModel toEdit)
        {
            AvilablePkTypes = new ObservableCollection<PkTypeModel>();

            AvilablePkTypes.Add(new PkTypeModel { KeyTypeName = "File", Location = Profiles.PK_LOCATION.FILE });
            AvilablePkTypes.Add(new PkTypeModel { KeyTypeName = "Usb", Location = Profiles.PK_LOCATION.USBSTICK });
            //AvilablePkTypes.Add(new PkTypeModel { KeyTypeName = "Hsm", Location = Profiles.PK_LOCATION.HSM });

            selectedPkType = AvilablePkTypes.First(x => x.Location == Profiles.PK_LOCATION.FILE);

            avilablePkInfosInstances = new Dictionary<Profiles.PK_LOCATION, PkInfoModel>();

            avilablePkInfosInstances.Add(Profiles.PK_LOCATION.FILE, new PkFileInfoModel { Alias = "fileAlias" });
            avilablePkInfosInstances.Add(Profiles.PK_LOCATION.USBSTICK, new PkUsbStickPkInfoModel { Alias = "usbStickAlias" });
           // avilablePkInfosInstances.Add(Profiles.PK_LOCATION.HSM, new PkInfoModel { Alias = "hsmStickAlias" });

           this.modelPkInfoProvider = modelPkInfoProvider;
           
           if (toEdit != null)
           {
               this.workMode = WorkMode.Edit;
               this.ChosenPkInfo = toEdit;

               if (toEdit is PkFileInfoModel)
               {
                   avilablePkInfosInstances[Profiles.PK_LOCATION.FILE] = toEdit;
               }
               if (toEdit is PkUsbStickPkInfoModel)
               {
                   avilablePkInfosInstances[Profiles.PK_LOCATION.USBSTICK] = toEdit;
               }
           }

           if (workMode == WorkMode.Add)
           {
               this.validators.Add("ChosenPkInfo", new List<IPropertyValidator<ViewModelBase>> { new Validation.AliasTakenValidator<ViewModelBase>(this.modelPkInfoProvider) });
           }
        }

        void ExecuteSave(object parameter)
        {
            this.IsSaving = true;
            this.ValidateAll();
            this.ChosenPkInfo.ValidationReady = true;
            this.ChosenPkInfo.ValidateAll();
            if (this.ChosenPkInfo.IsValid && this.IsValid)
            {
                switch(workMode)
                { 
                    case WorkMode.Add:
                        this.modelPkInfoProvider.Add(ChosenPkInfo);
                        break;
                    case WorkMode.Edit:
                        this.modelPkInfoProvider.Edit(ChosenPkInfo);
                        break;
                }

                if (parameter is System.Windows.Window)
                {
                    ((System.Windows.Window)parameter).Close();
                }
            }
            this.IsSaving = false;
            OnErrorChanged("ChosenPkInfo");
            return;
        }

        void ExecuteOpen(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true && (ChosenPkInfo is PkFileInfoModel) )
            {
                (ChosenPkInfo as PkFileInfoModel).Location = openFileDialog.FileName;
            }
        }
        void ExecuteCancel(object parameter)
        {
            if (parameter is System.Windows.Window)
            {
                ((System.Windows.Window)parameter).Close();
            }
        }
        protected override List<string> ToValidateProperties()
        {
            List<string> l = new List<string> { "ChosenPkInfo" };
            l.AddRange(base.ToValidateProperties());
            return l;
        }

    }
}
