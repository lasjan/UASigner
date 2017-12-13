using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles;
namespace UASigner.WpfApp
{
    public class AddEditProfileEventArgs : EventArgs
    {
        public IProfile ProfileToAdd { get; set; }
        public WorkMode WorkMode { get; set; }
    }

    public delegate void AddEditProfileDelegate (object sender,AddEditProfileEventArgs args);


}
