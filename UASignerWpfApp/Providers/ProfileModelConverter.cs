using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.WpfApp.Model;
using UASigner.Profiles;
namespace UASigner.WpfApp.Providers
{
    public class ProfileModelConverter : IDataToModelConverter<IProfile, ProfileModel>
    {
        PkInfoModelConverter pkc = new PkInfoModelConverter();
        LocationConverter lc = new LocationConverter();
        TimestampServerModelConverter tsc = new TimestampServerModelConverter();

        public ProfileModel ConvertToModel(IProfile data)
        {
            var inLocation = data.InLocationAccess;
            LocationAccessModel inLocationModel = null;

            inLocationModel = lc.ConvertToModel(inLocation);

            ProfileModel profileModel = new ProfileModel
            {
                Id = data.Id,
                InLocationAccess = inLocationModel

            };

            if(data.OutLocationAccess != null)
            {
                profileModel.OutLocationsCollection = new System.Collections.ObjectModel.ObservableCollection<LocationAccessModel>();
                foreach (var outputAccess in data.OutLocationAccess)
                {
                    LocationAccessModel outLocationModel = null;
                    outLocationModel = lc.ConvertToModel(outputAccess);
                    profileModel.OutLocationsCollection.Add(outLocationModel);
                }
            }

            if (data.SignProfile != null)
            {
                if (data.SignProfile.KeysInfo != null)
                {
                    profileModel.UsedKeysCollection = new System.Collections.ObjectModel.ObservableCollection<PkInfoModel>();
                    foreach (var keyInfo in data.SignProfile.KeysInfo)
                    {
                        PkInfoModel pkinfoModel = pkc.ConvertToModel(keyInfo);
                        profileModel.UsedKeysCollection.Add(pkinfoModel);
                    }
                }
                if (data.SignProfile.Tsinfo != null)
                {
                    TimestampServerModel tsModel = tsc.ConvertToModel(data.SignProfile.Tsinfo);
                    profileModel.UsedTimestampServer = tsModel;
                }

                profileModel.SignLevel = data.SignProfile.Level;

                profileModel.AddCertificate = data.SignProfile.AddCertificate;
                profileModel.AddContentTimeStamp = data.SignProfile.AddContentTimeStamp;
                profileModel.AddValidationData = data.SignProfile.AddValidationData;
               
            }
            return profileModel;
        }

        public IProfile ConvertFromModel(ProfileModel model)
        {
            GenericProfile profile = new GenericProfile { Id = model.Id, SignProfile = new SignContextProfile() };
            profile.InLocationAccess = lc.ConvertFromModel(model.InLocationAccess);

            profile.OutLocationAccess = new List<LocationAccess>();

            foreach (var outAccess in model.OutLocationsCollection)
            {
                ((List<LocationAccess>)profile.OutLocationAccess).Add(lc.ConvertFromModel(outAccess));
            }

            profile.SignProfile.KeysInfo = new List<PKInfo>();
            foreach (var keyInfo in model.UsedKeysCollection)
            { 
                profile.SignProfile.KeysInfo.Add(pkc.ConvertFromModel(keyInfo));
            }

            if (model.UsedTimestampServer != null)
            {
                profile.SignProfile.Tsinfo = tsc.ConvertFromModel(model.UsedTimestampServer);
            }

            profile.SignProfile.Level = model.SignLevel;

            profile.SignProfile.AddCertificate = model.AddCertificate;
            profile.SignProfile.AddContentTimeStamp = model.AddContentTimeStamp;
            profile.SignProfile.AddValidationData = model.AddValidationData;

            return profile;
        }
    }
}
