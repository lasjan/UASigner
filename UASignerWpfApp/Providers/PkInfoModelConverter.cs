using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.WpfApp.Model;
using UASigner.Profiles;
namespace UASigner.WpfApp.Providers
{
    public class PkInfoModelConverter:IDataToModelConverter<PKInfo,PkInfoModel>
    {
        public PkInfoModel ConvertToModel(PKInfo data)
        {
            if (data is PKFileInfo)
            {
                var fileData = data as PKFileInfo;
                PkFileInfoModel fileModel = new PkFileInfoModel { Alias = fileData.Alias, Location = fileData.PkFilePath, Password = fileData.Password,Id = fileData.Id };

                return fileModel;
            }

            throw new Exception("Not supported type!");
            
        }

        public PKInfo ConvertFromModel(PkInfoModel model)
        {
            if (model is PkFileInfoModel)
            {
                var  modelData = model as PkFileInfoModel;
                PKFileInfo fileData = new PKFileInfo { Alias = modelData.Alias, PkFilePath = modelData.Location, Password = modelData.Password, Id = modelData.Id };

                return fileData;
            }

            throw new Exception("Not supported type!");
        }
    }
}
