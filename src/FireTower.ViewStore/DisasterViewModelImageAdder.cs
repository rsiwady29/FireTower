using BlingBag;
using FireTower.Domain.Events;

namespace FireTower.ViewStore
{
    public class DisasterViewModelImageAdder : IBlingHandler<NewImageAddedToDisaster>
    {
        readonly IViewModelRepository _viewModelRepository;

        public DisasterViewModelImageAdder(IViewModelRepository viewModelRepository)
        {
            _viewModelRepository = viewModelRepository;
        }

        public void Handle(NewImageAddedToDisaster @event)
        {
            var disasterViewModel = _viewModelRepository.FindOne<DisasterViewModel>(x => x.DisasterId == @event.DisasterId.ToString());
            disasterViewModel.AddImage(@event.ImageUrl);
            _viewModelRepository.Update(disasterViewModel);
        }
    }
}