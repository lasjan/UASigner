using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles.Providers;
using UASigner.WpfApp.Model;
namespace UASigner.WpfApp.Providers
{
    public abstract class ModelProvider<TData, TModel> : IProviderWrapper<TData, TModel> where TData:IItem where TModel:IModelItem
    {
        protected IDataToModelConverter<TData, TModel> converter;
        protected IGenericProviderAction<TData> baseProvider;
        ObservableCollection<TModel> currentCollection;
        
        public ModelProvider(IGenericProviderAction<TData> baseProvider)
        {
            this.baseProvider = baseProvider;
            this.currentCollection = new ObservableCollection<TModel>();
        }
        public ObservableCollection<TModel> Get()
        {
            this.currentCollection.Clear();
            foreach (var baseObject in baseProvider.Get())
            {
                this.currentCollection.Add(converter.ConvertToModel(baseObject));
            }

            return this.currentCollection;
        }

        public void Add(TModel item)
        {
            var baseObject = converter.ConvertFromModel(item);
            baseProvider.Add(baseObject);
            this.currentCollection = Get();
        }

        public void Edit(TModel item)
        {
            var baseObject = converter.ConvertFromModel(item);
            baseProvider.Edit(baseObject);
            //var toEdit = this.currentCollection.First(x=>x.Id == item.Id);
            //var i = this.currentCollection.IndexOf(toEdit);
            //this.currentCollection[i] = item;
            this.currentCollection = Get();
        }

        public void Delete(TModel item)
        {
            var baseObject = converter.ConvertFromModel(item);
            baseProvider.Delete(baseObject);
            //var toDelete = this.currentCollection.First(x => x.Id == item.Id);
            //this.currentCollection.Remove(toDelete);
            this.currentCollection = Get();
        }


    }
}
