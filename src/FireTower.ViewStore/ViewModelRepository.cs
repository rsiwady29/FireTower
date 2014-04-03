using System;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace FireTower.ViewStore
{
    public class ViewModelRepository : IViewModelRepository
    {
        readonly MongoDatabase _mongoDatabase;

        public ViewModelRepository(MongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        #region IViewModelRepository Members

        public T Create<T>(T viewModel) where T : IViewModel
        {
            MongoCollection<T> mongoCollection = _mongoDatabase.GetCollection<T>(typeof (T).Name);
            mongoCollection.Insert(viewModel);
            return viewModel;
        }

        public T FindOne<T>(Expression<Func<T, bool>> query) where T : IViewModel
        {
            MongoCollection<T> mongoCollection = _mongoDatabase.GetCollection<T>(typeof (T).Name);
            return mongoCollection.AsQueryable().First(query);
        }

        public void Update<T>(T obj) where T : IViewModel
        {
            MongoCollection<T> mongoCollection = _mongoDatabase.GetCollection<T>(typeof (T).Name);
            mongoCollection.Save(obj);
        }

        #endregion
    }
}