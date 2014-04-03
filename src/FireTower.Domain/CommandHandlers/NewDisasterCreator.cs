using System;
using FireTower.Domain.Commands;
using FireTower.Domain.Entities;
using FireTower.Domain.Events;

namespace FireTower.Domain.CommandHandlers
{
    public class NewDisasterCreator : ICommandHandler
    {
        readonly IWriteableRepository _writeableRepository;

        public NewDisasterCreator(IWriteableRepository writeableRepository)
        {
            _writeableRepository = writeableRepository;
        }

        #region ICommandHandler Members

        public Type CommandType { get { return typeof (CreateNewDisaster); } }

        public void Handle(IUserSession userSessionIssuingCommand, object command)
        {

            var c = (CreateNewDisaster) command;
            var u = (UserSession)userSessionIssuingCommand;

            var itemToCreate = new Disaster(c.CreatedDate, c.LocationDescription, c.Latitude, c.Longitude);

            var newDisasterImage = itemToCreate.AddImage(c.FirstImageUrl);
            var newSeverityVote = itemToCreate.AddSeverityVote(u.User, c.FirsSeverity);
            var newDisaster = _writeableRepository.Create(itemToCreate);

            NotifyObservers(new NewDisasterCreated(newDisaster.Id, newDisaster.CreatedDate, c.LocationDescription, c.Latitude, c.Longitude, c.FirstImageUrl,c.FirsSeverity));

        }

        public event DomainEvent NotifyObservers;

        #endregion
    }
}