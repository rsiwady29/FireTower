using System;
using FireTower.Domain.Commands;
using FireTower.Domain.Entities;
using FireTower.Domain.Events;

namespace FireTower.Domain.CommandHandlers
{
    public class NewUserCreator : ICommandHandler
    {
        readonly IWriteableRepository _writeableRepository;

        public NewUserCreator(IWriteableRepository writeableRepository)
        {
            _writeableRepository = writeableRepository;
        }

        #region ICommandHandler Members

        public Type CommandType
        {
            get { return typeof (NewUserCommand); }
        }

        public void Handle(IUserSession userSession, object command)
        {
            var c = (NewUserCommand) command;
            _writeableRepository.Create(new User
                                            {
                                               FirstName = c.FirstName,
                                               LastName = c.LastName,
                                               Name = c.Name,
                                               FacebookId = c.FacebookId,
                                               Locale = c.Locale,
                                               Username = c.Username,
                                               Verified = false,
                                               Location = c.Location
                                            });
            NotifyObservers(new NewUserCreated(c.FacebookId));
        }

        public event DomainEvent NotifyObservers;

        #endregion
    }
}