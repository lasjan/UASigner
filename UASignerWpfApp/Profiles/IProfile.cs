using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles.Providers;
namespace UASigner.Profiles
{
    public interface IProfile:IItem
    {
        int? Id { get; set; }
        LocationAccess InLocationAccess { get; set; }
        LocationAccess BackupLocationAccess { get; set; }
        IEnumerable<LocationAccess> OutLocationAccess { get; set; }
        SignContextProfile SignProfile { get; set; }
        EnvelopeType EnvelopeType {get;set;}
        void Validate();
    }
}
