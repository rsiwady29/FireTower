namespace FireTower.ViewStore
{
    public interface IViewModelRepository
    {
        T Create<T>(T viewModel) where T : IViewModel;
    }
}