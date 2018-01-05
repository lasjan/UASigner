using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.Profiles
{
    public class DirectoryProfile:IProfile
    {
        public DirectoryProfile(int? id, DirectoryAccess inLocationAccess, List<DirectoryAccess> outLocationAccess, SignContextProfile signProfile)
        {
            this.Id = id;
            this.InLocationAccess = inLocationAccess;
            this.OutLocationAccess = outLocationAccess;
            this.SignProfile = signProfile;
        }
        public int? Id
        {
            get;
            set;
        }

        public LocationAccess InLocationAccess
        {
            get;
            set;
        }

        public IEnumerable<LocationAccess> OutLocationAccess
        {
            get;
            set;
        }
 
        public SignContextProfile SignProfile
        {
            get;
            set;
        }
        public LocationAccess BackupLocationAccess
        {
            get;
            set;
        }
        public EnvelopeType EnvelopeType
        {
            get;
            set;
        }
        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
