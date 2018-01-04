using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UASigner.WpfApp.Model.Validation;
using UASigner.WpfApp.Validation;

namespace UASigner.WpfApp.Model
{
    public class CadesProfileModel:NotifyModelBase
    {
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

        int? id;
        public int? Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        string displayName;
        public string DisplayName
        {
            get
            {
                return displayName;
            }
            set
            {
                displayName = value;
                OnPropertyChanged();
            }
        }

        string toolTip;
        public string ToolTip
        {
            get
            {
                return toolTip;
            }
            set
            {
                toolTip = value;
                OnPropertyChanged();
            }
        }


        public override bool Equals(object obj)
        {
            CadesProfileModel cadesProfile = obj as CadesProfileModel;
            if (cadesProfile == null) return false;

            return cadesProfile.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }
}
