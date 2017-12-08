using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles;
namespace UASigner.WpfApp.Display
{
    public class DisplayableProfile:IDisplayable
    {
        IProfile profile;
        public DisplayableProfile(IProfile profile)
        {
            this.profile = profile;
        }

        public string Icon
        {
            get
            {
                if (profile.InLocationAccess is DirectoryAccess)
                {
                    return @"/Resources/Images/folder_ico.png";
                }
                return String.Empty;
            }
        }

        public int Id
        {
            get { return (int)this.profile.Id; }
        }

        public string DispName
        {
            get 
            {
                if (profile.InLocationAccess is DirectoryAccess)
                { 
                    var dp = profile.InLocationAccess as DirectoryAccess;
                    return String.Format("{0},{1}", dp.DirectoryPath, dp.FileMask);
                }

                return "unknown";
            }
        }

        public string ToolTip
        {
            get
            {
                if (profile.InLocationAccess is DirectoryAccess)
                {
                    return "Folder input";
                }
                return String.Empty;
            }
        }
    }
}
