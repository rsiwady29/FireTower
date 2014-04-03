using System;
using System.Linq.Expressions;

namespace FireTower.ViewStore
{
    public interface IViewModelRepository
    {
        T Create<T>(T viewModel) where T : IViewModel;
        T FindOne<T>(Expression<Func<T, bool>> query) where T : IViewModel;
        void Update<T>(T obj) where T : IViewModel;
    }
}