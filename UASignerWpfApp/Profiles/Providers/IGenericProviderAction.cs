using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.Profiles.Providers
{
    public interface IGenericProviderAction<T>:IObserver,IObservable where T:IItem
    {
        IEnumerable<T> Get();
        void Add(T item);
        void Edit(T item);
        void Delete(T item);
    }
}
