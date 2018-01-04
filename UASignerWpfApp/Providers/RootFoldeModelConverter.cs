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
    public class RootFoldeModelConverter : IDataToModelConverter<string, RootFolderModel>
    {
        public RootFolderModel ConvertToModel(string data)
        {
            return new RootFolderModel { RootPath = data };
        }

        public string ConvertFromModel(RootFolderModel model)
        {
            return model.RootPath;
        }
    }
}
