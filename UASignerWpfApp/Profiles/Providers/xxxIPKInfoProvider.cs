using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.Profiles.Providers
{
    public interface IPKInfoProvider:IObservable
    {
        IEnumerable<PKInfo> GetPkInfos();
        void AddPKInfo(PKInfo pkInfo);
        void EditPKInfo(PKInfo pkInfo);
        void DeletePKInfo(PKInfo pkInfo);
    }
}
