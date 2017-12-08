using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles;
namespace UASigner.WpfApp
{
    public class AddEditPKInfoEventArgs : EventArgs
    {
        public PKInfo PkInfoToAdd { get; set; }
    }

    public delegate void AddEditPkInfoDelegate(object sender, AddEditPKInfoEventArgs args);
}
