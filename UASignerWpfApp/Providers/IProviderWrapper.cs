using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles.Providers;
namespace UASigner.WpfApp.Providers
{
    public interface IProviderWrapper<TData, TModel> : IModelGenericProviderAction<TModel> where TData : IItem
    {
    }
}
