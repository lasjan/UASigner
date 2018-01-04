using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.WpfApp.Providers
{
    public interface IModelGenericProviderAction<TModel>
    {
        ObservableCollection<TModel> Get();
        void Add(TModel item);
        void Edit(TModel item);
        void Delete(TModel item);
    }
}
