using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.WpfApp.Model;
using UASigner.Profiles;
namespace UASigner.WpfApp.Providers
{
    public class LocationConverter : IDataToModelConverter<LocationAccess,LocationAccessModel>
    {
        public LocationAccessModel ConvertToModel(LocationAccess data)
        {
            LocationAccessModel locationModel = null;

            if (data is DirectoryAccess)
            {
                locationModel = new FileLocationAccessModel
                {
                    DisplayName = data.ToString(),
                    Path = ((DirectoryAccess)data).DirectoryPath,
                    FileMask = ((DirectoryAccess)data).FileMask
                };
            }
            if (data is FtpAccess)
            {
                locationModel = new FtpLocationAccessModel
                {
                    DisplayName = data.ToString(),
                    Address = ((FtpAccess)data).Address,
                    Port = ((FtpAccess)data).Port
                };
            }

            return locationModel;
        }

        public LocationAccess ConvertFromModel(LocationAccessModel model)
        {
            LocationAccess locationAccess = null;
            if (model is FileLocationAccessModel)
            { 
                var fileModel = (FileLocationAccessModel)model;
                locationAccess = new DirectoryAccess(fileModel.Path, fileModel.FileMask);

            }
            if (model is FtpLocationAccessModel)
            {
                var ftpModel = (FtpLocationAccessModel)model;
                locationAccess = new FtpAccess(ftpModel.Address, (int)ftpModel.Port);

            }

            return locationAccess;
        }
    }
}
