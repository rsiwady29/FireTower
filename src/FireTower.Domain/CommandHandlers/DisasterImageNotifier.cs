using BlingBag;
using FireTower.Domain.Events;

namespace FireTower.Domain.CommandHandlers
{
    public class DisasterImageNotifier : IBlingHandler<NewImageAddedToDisaster>
    {
        readonly INotificationPublisher _notificationPublisher;
        readonly IReadOnlyRepository _readOnlyRepository;

        public DisasterImageNotifier(INotificationPublisher notificationPublisher, IReadOnlyRepository readOnlyRepository)
        {
            _notificationPublisher = notificationPublisher;
            _readOnlyRepository = readOnlyRepository;
        }

        public void Handle(NewImageAddedToDisaster @event)
        {
            _notificationPublisher.Publish(@event.UserId,new AddedImageUrl{ ImageUrl = @event.ImageUrl});
        }
    }
}