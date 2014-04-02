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

            var itemToCreate = new Disaster(c.Latitude, c.Longitude);

            var newDisasterImage = itemToCreate.AddImage(c.FirstImageUrl);
            var newDisaster = _writeableRepository.Create(itemToCreate);

            NotifyObservers(new NewDisasterCreated(newDisaster.Id, c.Latitude, c.Longitude, c.FirstImageUrl));

        }

        public event DomainEvent NotifyObservers;

        #endregion
    }
}