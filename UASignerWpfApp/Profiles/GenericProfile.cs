using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.Profiles
{
    public class GenericProfile : IProfile
    {
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
    }
}
