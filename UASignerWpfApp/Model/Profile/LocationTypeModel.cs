using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.WpfApp.Model
{
    class LocationTypeModel : NotifyModelBase
    {
        string locationTypeName { get; set; }
        LocationType location { get; set; }

        public string LocationTypeName
        {
            get { return locationTypeName; }
            set { this.locationTypeName = value; OnPropertyChanged(); }
        }
        public LocationType Location
        {
            get { return location; }
            set { this.location = value; OnPropertyChanged(); }
        }
    }
}
