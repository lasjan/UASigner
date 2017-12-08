using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles;
namespace UASigner.Profiles.Providers
{
    public interface IProfileProvider:IObserver
    {
        IEnumerable<IProfile> GetProfiles();
        void AddProfile(IProfile profile);
        void EditProfile(IProfile profile);
        void DeleteProfile(IProfile profile);
    }
}
