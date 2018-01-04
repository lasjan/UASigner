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
    public class TimestampServerModel:NotifyModelBase,IModelItem,ICheckBoxWrappable
    {
        int? id;
        public int? Id 
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        string url;
        public string Url
        {
            get { return url; }
            set { url = value; OnPropertyChanged(); }
        }


        int? port;
        public int? Port
        {
            get { return port; }
            set { port = value; OnPropertyChanged(); }
        }


        public string ExposedDisplayName
        {
            get { return Url; }
        }
    }
}
