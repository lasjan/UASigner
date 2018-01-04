using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.WpfApp.Model;
using UASigner.Profiles;
using UASigner.Profiles.Providers;
namespace UASigner.WpfApp.Providers
{
    public class ModelProfileProvider: ModelProvider<IProfile, ProfileModel>
    {
        public ModelProfileProvider(IGenericProviderAction<IProfile> baseProvider):base(baseProvider)
        {
            this.converter = new ProfileModelConverter();
            
        }
    
    }
}
