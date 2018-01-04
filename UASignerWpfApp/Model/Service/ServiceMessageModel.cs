using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Service;
namespace UASigner.WpfApp.Model
{
    public class ServiceMessageModel: NotifyModelBase
    {
        ServiceState state;
        string message;

        public ServiceState State
        {
            get { return state; }
            set { state = value; OnPropertyChanged(); }
        }

        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged(); }
        }
    }
}
