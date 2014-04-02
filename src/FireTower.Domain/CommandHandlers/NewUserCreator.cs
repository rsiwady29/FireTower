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
                                                Email = c.Email,
                                                EncryptedPassword = c.EncryptedPassword.Password,
                                                AgreementVersion = c.AgreementVersion
                                            });
            NotifyObservers(new NewUserCreated(c.Email, c.AgreementVersion));
        }

        public event DomainEvent NotifyObservers;

        #endregion
    }
}