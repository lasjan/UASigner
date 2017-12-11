using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles;
namespace UASigner.WpfApp.Display
{
    public class DisplayableTSInfo:IDisplayable
    {
        TimeStampServerInfo tsinfo;
        public DisplayableTSInfo(TimeStampServerInfo tsinfo)
        {
            this.tsinfo = tsinfo;
        }
        public string Icon
        {
            get { return @"/Resources/Images/ts_info_ico.png"; }
        }

        public int Id
        {
            get { return (int)tsinfo.Id; }
        }

        public string DispName
        {
            get { return String.Format("{0} [{1}]", tsinfo.Url, tsinfo.Port); }
        }

        public string ToolTip
        {
            get { return string.Empty; }
        }
    }
}
