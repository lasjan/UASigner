using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles;
using UASigner.Profiles.Configuration;
namespace UASigner.Profiles.Providers
{
    public class DirectoryProfileProvider:IProfileProvider
    {
        List<DirectoryProfile> profiles;
        Configuration.Configuration config;

        public DirectoryProfileProvider(Configuration.Configuration config)
        {
            this.config = config;

            profiles = new List<DirectoryProfile>();
            DirectoryProfile dp1 = new DirectoryProfile
            (
            1, new DirectoryAccess(@"E:\UA1\in", "xml"), new List<DirectoryAccess> { new DirectoryAccess(@"E:\UA1\out", "xml") }, null
            );
            DirectoryProfile dp2 = new DirectoryProfile
            (
            2, new DirectoryAccess(@"E:\UA2\in", "xml"), new List<DirectoryAccess> { new DirectoryAccess(@"E:\UA2\out", "xml") }, null
            );

            profiles.Add(dp1);
            profiles.Add(dp2);
        }
        public IEnumerable<IProfile> GetProfiles()
        {
            return profiles;
        }

        public void AddProfile(IProfile profile)
        {
            DirectoryProfile dp = profile as DirectoryProfile;
            if(dp==null)
            {
                throw new Exception("Only DirectoryProfile can be added");
            }
            if (dp.Id == null)
            {
                var maxId = this.profiles.Max(x => x.Id);
                dp.Id = maxId + 1;
            }
            profiles.Add(dp);
        }

        public void EditProfile(IProfile profile)
        {
            throw new NotImplementedException();
        }

        public void DeleteProfile(IProfile profile)
        {
            throw new NotImplementedException();
        }

        public void Observe(ActionType actionType, IObservable observable)
        {
            throw new NotImplementedException();
        }
    }
}
