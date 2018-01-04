using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.WpfApp.Providers
{
    public interface IModelGenericProviderProperty<TModel>:IObservable<TModel>
    {
        TModel Get();
        void Set(TModel model);
    }
}
