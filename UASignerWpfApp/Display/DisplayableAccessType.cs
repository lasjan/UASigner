using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles;
namespace UASigner.WpfApp.Display
{
    public class DisplayableAccessType:IDisplayable
    {
        LocationAccess la;
        public DisplayableAccessType()
        { 

        }

        public DisplayableAccessType(LocationAccess la)
        {
            this.la = la;

            if (la is DirectoryAccess)
            {
                Icon = @"/Resources/Images/folder_ico.png";
            }

            if (la is DirectoryAccess)
            {
                var dp = la as DirectoryAccess;
                DispName = String.Format("{0} [{1}]", dp.DirectoryPath, dp.FileMask).ToUpper();
            }
           
        }
        public AccessType AccessType { get; set; }
        public string Icon
        {
            get;
            set;
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
