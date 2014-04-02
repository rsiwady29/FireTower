using BlingBag;
using FireTower.Domain.Events;
using FireTower.Domain.Services;

namespace FireTower.ViewStore
{
    public class DisasterViewModelCreator : IBlingHandler<NewDisasterCreated>
    {
        readonly IViewModelRepository _repository;

        public DisasterViewModelCreator(IViewModelRepository repository)
        {
            _repository = repository;            
        }

        public void Handle(NewDisasterCreated @event)
        {
            _repository.Create(new DisasterViewModel(@event.DisasterId, @event.Latitude, @event.Longitude, @event.FirstImageUrl));
        }
    }
}