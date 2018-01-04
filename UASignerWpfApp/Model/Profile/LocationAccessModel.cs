using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.WpfApp.Model
{
    public class LocationAccessModel:NotifyModelBase
    {
        string displayName;
        public string DisplayName
        {
            get { return displayName; }
            set { this.displayName = value; OnPropertyChanged(); }
        }
    }
}
