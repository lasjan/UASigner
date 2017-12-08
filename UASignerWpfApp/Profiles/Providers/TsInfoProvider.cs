using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles.Configuration;
namespace UASigner.Profiles.Providers
{
    public class TsInfoProvider:IGenericProviderAction<TimeStampServerInfo>
    {
        Configuration.Configuration config;
        List<TimeStampServerInfo> tsInfos;
        object locker = new object();

        List<IObserver> observers = new List<IObserver>();

        public TsInfoProvider(Configuration.Configuration config)
        {
            this.config = config;
            this.tsInfos = config.GetTimeStampServerInfos();
        }
        public IEnumerable<TimeStampServerInfo> Get()
        {
            lock (locker)
            {
                return this.tsInfos;
            }
        }

        public void Add(TimeStampServerInfo item)
        {
            lock (locker)
            {
                if (item.Id == null)
                {
                    var maxId = this.tsInfos.Max(x => x.Id);
                    item.Id = maxId + 1;
                }
                tsInfos.Add(item);
                config.SetTimeStampServerInfos(tsInfos);
            }
        }

        public void Edit(TimeStampServerInfo item)
        {
            lock (locker)
            {
                var toReplace = this.tsInfos.First(x => x.Id == item.Id);
                var index = this.tsInfos.IndexOf(toReplace);

                this.tsInfos[index] = item;

                this.observers.ForEach(x => x.Observe(ActionType.Edit, this));

                config.SetTimeStampServerInfos(tsInfos);
            }
        }

        public void Delete(TimeStampServerInfo item)
        {
            lock (locker)
            {
                var toRemove = this.tsInfos.First(x => x.Id == item.Id);
                this.tsInfos.Remove(toRemove);
                this.observers.ForEach(x => x.Observe(ActionType.Delete, this));

                config.SetTimeStampServerInfos(tsInfos);
            }
        }

        public void Observe(ActionType actionType, IObservable observable)
        {
            
        }

        public void AddObserver(IObserver observer)
        {
            this.observers.Add(observer);
        }
    }
}
