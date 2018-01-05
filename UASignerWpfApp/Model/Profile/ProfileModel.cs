using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using UASigner.WpfApp.Model.Validation;
using UASigner.WpfApp.Validation;
using UASigner.Profiles;

namespace UASigner.WpfApp.Model
{
    public class ProfileModel : NotifyModelBase, IModelItem
    {
        public int? Id
        {
            get;
            set;
        }
        string displayName;
        public string DisplayName
        {
            get { return displayName; }
            set { this.displayName = value; OnPropertyChanged(); }
        }


        LocationAccessModel inLocationAccess { get; set; }
        public LocationAccessModel InLocationAccess
        {
            get { return inLocationAccess; }
            set 
            { 
                this.inLocationAccess = value;
                DisplayName = InLocationAccess.DisplayName;
                OnPropertyChanged(); 
            }
        }

        LocationAccessModel backupLocationAccess { get; set; }
        public LocationAccessModel BackupLocationAccess
        {
            get { return backupLocationAccess; }
            set
            {
                this.backupLocationAccess = value;
                OnPropertyChanged();
            }
        }
        ObservableCollection<LocationAccessModel> outLocationsCollection { get; set; }
        public ObservableCollection<LocationAccessModel> OutLocationsCollection
        {
            get { return outLocationsCollection; }
            set
            {
                this.outLocationsCollection = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<PkInfoModel> usedKeysCollection { get; set; }
        public ObservableCollection<PkInfoModel> UsedKeysCollection
        {
            get { return usedKeysCollection; }
            set
            {
                this.usedKeysCollection = value;
                OnPropertyChanged();
            }
        }
        private TimestampServerModel usedTimestampServer;
        public TimestampServerModel UsedTimestampServer
        {
            get
            {
                return usedTimestampServer;
            }
            set
            {
                usedTimestampServer = value;
                OnPropertyChanged();
            }
        }

        Profiles.SignatureLevel signLevel;
        public Profiles.SignatureLevel SignLevel
        {
            get
            {
                return signLevel;
            }
            set
            {
                signLevel = value;
                OnPropertyChanged();
            }
        }

        bool addCertificate;
        public bool AddCertificate
        {
            get
            {
                return addCertificate;
            }
            set
            {
                addCertificate = value;
                OnPropertyChanged();
            }
        }

        bool addContentTimeStamp;
        public bool AddContentTimeStamp
        {
            get
            {
                return addContentTimeStamp;
            }
            set
            {
                addContentTimeStamp = value;
                OnPropertyChanged();
            }
        }
        bool addValidationData;
        public bool AddValidationData
        {
            get
            {
                return addValidationData;
            }
            set
            {
                addValidationData = value;
                OnPropertyChanged();
            }
        }

        public ProfileModel()
            : base()
        {
            List<IPropertyValidator<NotifyModelBase>> conditionalValidators = new List<WpfApp.Validation.IPropertyValidator<NotifyModelBase>>();
            TSServerNotDefinedValidator<NotifyModelBase> tsNotEmptyValidator = new Validation.TSServerNotDefinedValidator<NotifyModelBase>();
            conditionalValidators.Add(tsNotEmptyValidator);

            PropertyConditionalValidator<NotifyModelBase> tsVsSignLevelValidator = new WpfApp.Validation.PropertyConditionalValidator<NotifyModelBase>
            ("SignLevel", new List<object> { Profiles.SignatureLevel.CADES_101733_C, Profiles.SignatureLevel.CADES_101733_X_LONG, Profiles.SignatureLevel.CADES_BASELINE_T },
            conditionalValidators);

            PropertyConditionalValidator<NotifyModelBase> tsVsUseContentTsValidator = new WpfApp.Validation.PropertyConditionalValidator<NotifyModelBase>
           ("AddContentTimeStamp", new List<object> { true},
           conditionalValidators);

            this.validators.Add("UsedTimestampServer", new List<IPropertyValidator<NotifyModelBase>> { tsVsSignLevelValidator, tsVsUseContentTsValidator });

            NotEmptyListValidator<NotifyModelBase, PkInfoModel> notEmptyKeysValidator = new Validation.NotEmptyListValidator<NotifyModelBase, PkInfoModel>();
            this.validators.Add("UsedKeysCollection", new List<IPropertyValidator<NotifyModelBase>> { notEmptyKeysValidator });

            NotEmptyListValidator<NotifyModelBase, LocationAccessModel> notEmptyOutputLocValidator = new Validation.NotEmptyListValidator<NotifyModelBase, LocationAccessModel>();
            this.validators.Add("OutLocationsCollection", new List<IPropertyValidator<NotifyModelBase>> { notEmptyOutputLocValidator });

        }
        protected override List<string> ToValidateProperties()
        {
            List<string> l = new List<string> { "InLocationAccess", "UsedTimestampServer", "UsedKeysCollection","OutLocationsCollection"};
            l.AddRange(base.ToValidateProperties());
            return l;
        }

        
    }
}
