using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.WpfApp.Model;
using UASigner.Profiles;
namespace UASigner.WpfApp.Providers
{
    public class TimestampServerModelConverter : IDataToModelConverter<TimeStampServerInfo, TimestampServerModel>
    {
        public TimestampServerModel ConvertToModel(TimeStampServerInfo data)
        {
            TimestampServerModel tsServerModel = new TimestampServerModel
            {
                Id = data.Id,
                Url = data.Url,
                Port = data.Port

            };

            return tsServerModel;
        }

        public TimeStampServerInfo ConvertFromModel(TimestampServerModel model)
        {
            TimeStampServerInfo tsServerInfo = new TimeStampServerInfo
            {
                Id = model.Id,
                Port = (int)model.Port,
                Url = model.Url
            };
           
            return tsServerInfo;
        }
    }
}
