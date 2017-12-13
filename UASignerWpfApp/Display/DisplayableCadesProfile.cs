using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles;
namespace UASigner.WpfApp.Display
{
    public class DisplayableCadesProfile:IDisplayable
    {
        public SignatureLevel SignLevel
        {
            get;
            set;
        }
        public string Icon
        {
            get { return String.Empty; }
        }

        public int Id
        {
            get;
            set;
        }

        public string DispName
        {
            get;
            set;
        }

        public string ToolTip
        {
            get;
            set;
        }

    }
}
