using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.Profiles.Providers
{
    public interface ITsInfoProvider:IObservable
    {
        IEnumerable<TimeStampServerInfo> GetTimeStampServerInfos();
        void AddTimeStampServerInfo(TimeStampServerInfo pkInfo);
        void EditTimeStampServerInfo(TimeStampServerInfo pkInfo);
        void DeleteTimeStampServerInfo(TimeStampServerInfo pkInfo);
    }
}
