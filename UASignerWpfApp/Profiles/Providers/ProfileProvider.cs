using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles.Configuration;
namespace UASigner.Profiles.Providers
{
    public class ProfileProvider : IGenericProviderAction<IProfile>
    {
        Configuration.Configuration config;
        List<IProfile> profiles;
        object locker = new object();
        public ProfileProvider(Configuration.Configuration config)
        {
            this.config = config;
            this.profiles = config.GetProfiles();
        }

        public void Observe(ActionType actionType, IObservable observable)
        {
            var pkInfoProvider = observable as IGenericProviderAction<PKInfo>;
            var tsInfoProvider = observable as IGenericProviderAction<TimeStampServerInfo>;
            lock (locker)
            {
                if (pkInfoProvider != null)
                {
                    var affectedKeys = pkInfoProvider.Get();
                    switch (actionType)
                    {
                        case ActionType.Delete:
                            foreach (var profile in this.profiles)
                            {
                                if (profile.SignProfile != null && profile.SignProfile.KeysInfo != null)
                                {
                                    var kinfos = profile.SignProfile.KeysInfo;
                                    List<PKInfo> toRemove = new List<PKInfo>();
                                    foreach (var kinfo in kinfos)
                                    {
                                        if(!affectedKeys.Any(x=>x.Id == kinfo.Id))
                                        {
                                            toRemove.Add(kinfo);
                                        }
                                    }
                                    toRemove.ForEach(x => kinfos.Remove(x));

                                }
                            }
                            break;
                        case ActionType.Edit:
                            foreach (var profile in this.profiles)
                            {
                                if (profile.SignProfile != null && profile.SignProfile.KeysInfo != null)
                                {
                                    var kinfos = profile.SignProfile.KeysInfo;
                                    List<PKInfo> toRemove = new List<PKInfo>();
                                    foreach (var affectedKeyInfo in affectedKeys)
                                    {
                                        var toReplace = profile.SignProfile.KeysInfo.FirstOrDefault(x => x.Id == affectedKeyInfo.Id);
                                        if (toReplace != null)
                                        {
                                            var index = profile.SignProfile.KeysInfo.IndexOf(toReplace);

                                            profile.SignProfile.KeysInfo[index] = affectedKeyInfo;
                                        }
                                    }
                                    toRemove.ForEach(x => kinfos.Remove(x));

                                }
                            }
                            break;
                    }
                }

                if (tsInfoProvider != null)
                {
                    var affectedTsInfos = tsInfoProvider.Get();
                    switch (actionType)
                    {
                        case ActionType.Delete:
                            foreach (var profile in this.profiles)
                            {
                                if (profile.SignProfile != null && profile.SignProfile.Tsinfo != null)
                                {
                                    if (!affectedTsInfos.Any(x => x.Id == profile.SignProfile.Tsinfo.Id))
                                    {
                                        profile.SignProfile.Tsinfo = null;
                                    }

                                }
                            }
                            break;
                        case ActionType.Edit:
                            foreach (var profile in this.profiles)
                            {
                                if (profile.SignProfile != null && profile.SignProfile.Tsinfo != null)
                                {
                                    foreach (var affectedTsInfo in affectedTsInfos)
                                    {
                                        if (profile.SignProfile.Tsinfo.Id == affectedTsInfo.Id)
                                        {
                                            profile.SignProfile.Tsinfo = affectedTsInfo;
                                        }
                                        var toReplace = profile.SignProfile.KeysInfo.FirstOrDefault(x => x.Id == affectedTsInfo.Id);
                                    }


                                }
                            }
                            break;
                    }
                }
                config.SetProfiles(profiles);
            }
        }

        public IEnumerable<IProfile> Get()
        {
            lock (locker)
            {
                return this.profiles;
            }
        }

        public void Add(IProfile item)
        {
            lock (locker)
            {
                if (item.Id == null)
                {
                    var maxId = this.profiles.Max(x => x.Id);
                    item.Id = maxId + 1;
                }
                profiles.Add(item);
                config.SetProfiles(profiles);
            }
        }

        public void Edit(IProfile item)
        {
            lock (locker)
            {
                var toReplace = this.profiles.FirstOrDefault(x => x.Id == item.Id);
                var index = this.profiles.IndexOf(toReplace);

                this.profiles[index] = item;

                config.SetProfiles(profiles);
            }
        }

        public void Delete(IProfile item)
        {
            lock (locker)
            {
                var toRemove = this.profiles.FirstOrDefault(x => x.Id == item.Id);
                this.profiles.Remove(toRemove);

                config.SetProfiles(profiles);
            }

        }

        public void AddObserver(IObserver observer)
        {
            
        }
    }
}
