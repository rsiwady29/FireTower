using System;
using FireTower.Domain.Commands;
using FireTower.Domain.Entities;
using FireTower.Domain.Events;

namespace FireTower.Domain.CommandHandlers
{
    public class NewDisasterCreator : ICommandHandler
    {
        readonly IWriteableRepository _writeableRepository;
        readonly ITimeProvider _timeProvider;

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

            itemToCreate.AddSeverityVote(u.User, c.FirstSeverity);
            Disaster newDisaster = _writeableRepository.Create(itemToCreate);

            NotifyObservers(new NewDisasterCreated(newDisaster.Id, newDisaster.CreatedDate, c.LocationDescription,
                                                   c.Latitude, c.Longitude, c.FirstSeverity));
        }

        public event DomainEvent NotifyObservers;

        #endregion
    }
}