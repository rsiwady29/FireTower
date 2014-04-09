using System;
using FireTower.Domain.Commands;
using FireTower.Domain.Entities;
using FireTower.Domain.Events;

namespace FireTower.Domain.CommandHandlers
{
    public class NewDisasterCreator : ICommandHandler
    {
        readonly ITimeProvider _timeProvider;
        readonly IWriteableRepository _writeableRepository;

        public NewDisasterCreator(IWriteableRepository writeableRepository, ITimeProvider timeProvider)
        {
            _writeableRepository = writeableRepository;
            _timeProvider = timeProvider;
        }

        #region ICommandHandler Members

        public Type CommandType
        {
            get { return typeof (CreateNewDisaster); }
        }

        public void Handle(IUserSession userSessionIssuingCommand, object command)
        {
            var c = (CreateNewDisaster) command;
            var u = (UserSession) userSessionIssuingCommand;

            var itemToCreate = new Disaster(_timeProvider.Now(), c.LocationDescription, c.Latitude, c.Longitude);

            Disaster newDisaster = _writeableRepository.Create(itemToCreate);
            NotifyObservers(new NewDisasterCreated(u.User.Id, newDisaster.Id, newDisaster.CreatedDate,
                                                   c.LocationDescription,
                                                   c.Latitude, c.Longitude));
        }

        public event DomainEvent NotifyObservers;

        #endregion
    }
}