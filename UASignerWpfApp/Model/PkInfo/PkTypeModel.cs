using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles;
namespace UASigner.WpfApp.Model
{
    class PkTypeModel : NotifyModelBase
    {
        string keyTypeName { get; set; }
        PK_LOCATION location { get; set; }

        public string KeyTypeName 
        {
            get { return keyTypeName; }
            set { this.keyTypeName = value; OnPropertyChanged(); }
        }
        public PK_LOCATION Location
        {
            get { return location; }
            set { this.location = value; OnPropertyChanged(); }
        }
    }
}
