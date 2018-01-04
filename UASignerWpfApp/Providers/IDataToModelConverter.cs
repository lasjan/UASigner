using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.WpfApp.Providers
{
    public interface IDataToModelConverter<TData,TModel>
    {
        TModel ConvertToModel(TData data);
        TData ConvertFromModel(TModel model);
    }
}
