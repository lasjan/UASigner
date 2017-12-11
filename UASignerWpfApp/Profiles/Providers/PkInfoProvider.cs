using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles.Configuration;

namespace UASigner.Profiles.Providers
{
    public class PkInfoProvider : IGenericProviderAction<PKInfo>
    {
        Configuration.Configuration config;
        List<PKInfo> pkInfos;
        object locker = new object();

        List<IObserver> observers = new List<IObserver>();

        public PkInfoProvider(Configuration.Configuration config)
        {
            this.config = config;
            pkInfos = config.GetPkInfos();

        }

        public void AddObserver(IObserver observer)
        {
            this.observers.Add(observer);
        }

        public IEnumerable<PKInfo> Get()
        {
            lock (locker)
            {
                return pkInfos;
            }
        }

        public void Add(PKInfo item)
        {
            lock (locker)
            {
                if (this.pkInfos != null)
                {
                    if (this.pkInfos.Any(x => x.Alias.Equals(item.Alias, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        throw new Exceptions.ConfigurationException(101, "This alias already exists", item.Alias);
                    }
                }
                if (item.Id == null)
                {
                    var maxId = this.pkInfos.Max(x => x.Id);
                    item.Id = maxId + 1;
                }
                pkInfos.Add(item);
                config.SetPkInfos(pkInfos);
            }
        }

        public void Edit(PKInfo item)
        {
            lock (locker)
            {
                var toReplace = this.pkInfos.FirstOrDefault(x => x.Id == item.Id);
                var index = this.pkInfos.IndexOf(toReplace);

                this.pkInfos[index] = item;

                this.observers.ForEach(x => x.Observe(ActionType.Edit, this));

                config.SetPkInfos(pkInfos);
            }
        }

        public void Delete(PKInfo item)
        {
            lock (locker)
            {
                var toRemove = this.pkInfos.FirstOrDefault(x => x.Id == item.Id);
                this.pkInfos.Remove(toRemove);
                this.observers.ForEach(x => x.Observe(ActionType.Delete, this));

                config.SetPkInfos(pkInfos);
            }
        }

        public void Observe(ActionType actionType, IObservable observable)
        {

        }
    }
}
