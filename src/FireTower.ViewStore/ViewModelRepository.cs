using FireTower.Domain.Services;
using MongoDB.Driver;

namespace FireTower.ViewStore
{
    public class ViewModelRepository : IViewModelRepository
    {
        readonly MongoDatabase _mongoDatabase;

        public ViewModelRepository(MongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public T Create<T>(T viewModel) where T : IViewModel
        {
            MongoCollection<T> mongoCollection = _mongoDatabase.GetCollection<T>(typeof (T).Name);
            mongoCollection.Insert(viewModel);
            return viewModel;
        }
    }
}