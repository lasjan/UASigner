using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles;
namespace UASigner.WpfApp.Display
{
    public class DisplayablePkInfo:IDisplayable
    {
        PKInfo pkInfo;
        public DisplayablePkInfo(PKInfo pkInfo)
        {
            this.pkInfo = pkInfo;
        }

        public string Icon
        {
            get { return @"/Resources/Images/key_ico.png"; }
        }

        public int Id
        {
            get { return (int)pkInfo.Id; }
        }

        public string DispName
        {
            get { return pkInfo.Alias; }
        }

        public string ToolTip
        {
            get { return String.Empty; }
        }
    }
}
