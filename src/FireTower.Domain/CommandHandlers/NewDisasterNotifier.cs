using System;
using BlingBag;
using FireTower.Domain.Entities;
using FireTower.Domain.Events;

namespace FireTower.Domain.CommandHandlers
{
    public class NewDisasterViewModelNotifier : IBlingHandler<DisasterViewModelCreated>
    {
        readonly INotificationPublisher _notificationPublisher;
        readonly IReadOnlyRepository _readOnlyRepository;

        public NewDisasterViewModelNotifier(INotificationPublisher notificationPublisher, IReadOnlyRepository readOnlyRepository)
        {
            _notificationPublisher = notificationPublisher;
            _readOnlyRepository = readOnlyRepository;
        }

        public void Handle(DisasterViewModelCreated @event)
        {
            _notificationPublisher.Publish(@event.UserId, new DisasterViewModel(){oid = @event.BsonValue});
        }
    }

    public class DisasterViewModel
    {
        public string oid { get; set; }
    }

    public class NewDisasterNotifier : IBlingHandler<NewDisasterCreated>
    {
        private readonly INotificationPublisher _notificationPublisher;
        private readonly IReadOnlyRepository _readOnlyRepository;
        private readonly IDisasterMapping _disasterMapping;
        private readonly Guid _id;

        public NewDisasterNotifier(INotificationPublisher notificationPublisher, IReadOnlyRepository readOnlyRepository, IDisasterMapping disasterMapping)
        {
            _notificationPublisher = notificationPublisher;
            _readOnlyRepository = readOnlyRepository;
            _disasterMapping = disasterMapping;
        }

        public void Handle(NewDisasterCreated @event)
        {
            var disaster = _readOnlyRepository.GetById<Disaster>(@event.DisasterId);
            var mappedDisaster = _disasterMapping.Map(disaster);
            _notificationPublisher.Publish(@event.UserId,  mappedDisaster);
        }
    }
}