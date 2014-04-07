using BlingBag;
using FireTower.Domain.Events;

namespace FireTower.ViewStore
{
    public class DisasterViewModelCreator : IBlingHandler<NewDisasterCreated>
    {
        readonly IViewModelRepository _repository;

        public DisasterViewModelCreator(IViewModelRepository repository)
        {
            _repository = repository;
        }

        #region IBlingHandler<NewDisasterCreated> Members

        public void Handle(NewDisasterCreated @event)
        {
            _repository.Create(new DisasterViewModel(@event.DisasterId, @event.CreatedDate, @event.LocationDescription,
                                                     @event.Latitude, @event.Longitude, @event.FirstSeverityVote));
        }

        #endregion
    }
}