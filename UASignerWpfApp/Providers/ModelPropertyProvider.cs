using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles.Providers;
namespace UASigner.WpfApp.Providers
{
    public abstract class ModelPropertyProvider<TData, TModel>:IModelGenericProviderProperty<TModel>
    {
        protected List<IObserver<TModel>> observers = new List<IObserver<TModel>>();

        IGenericPropertyProvider<TData> baseProvider;
        protected IDataToModelConverter<TData, TModel> converter;
        TModel model;
        public ModelPropertyProvider(IGenericPropertyProvider<TData> baseProvider)
        {
            this.baseProvider = baseProvider;
        }
        public TModel Get()
        {
            TData baseObject = baseProvider.Get();
            model = converter.ConvertToModel(baseObject);

            return model;
        }

        public void Set(TModel model)
        {
            this.model = model;
            TData baseObject = converter.ConvertFromModel(model);

            baseProvider.Set(baseObject);

            foreach (var observer in observers)
            {
                observer.OnNext(model);       
            }
        }



        public IDisposable Subscribe(IObserver<TModel> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<TModel>> _observers;
            private IObserver<TModel> _observer;

            public Unsubscriber(List<IObserver<TModel>> observers, IObserver<TModel> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }
}
