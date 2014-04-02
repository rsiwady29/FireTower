using System;
using FireTower.Domain.Commands;
using FireTower.Domain.Entities;
using FireTower.Domain.Events;

namespace FireTower.Domain.EventHandlers
{
    public class ActivateUserHandler : ICommandHandler
    {
        readonly IReadOnlyRepository _readOnlyRepository;
        readonly IWriteableRepository _writeableRepository;

        public ActivateUserHandler(IReadOnlyRepository readOnlyRepository, IWriteableRepository writeableRepository)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeableRepository = writeableRepository;
        }

        #region ICommandHandler Members

        public Type CommandType
        {
            get { return typeof (ActivateUser); }
        }

        public void Handle(IUserSession userSession, object command)
        {
            var c = (ActivateUser) command;

            var user = _readOnlyRepository.First<User>(x => x.Email == c.Email);
            user.Activated = true;
            _writeableRepository.Update(user);

            var verification = _readOnlyRepository.First<Verification>(x => x.EmailAddress == c.Email);
            _writeableRepository.Delete<Verification>(verification.Id);

            NotifyObservers(new UserActivated(user.Id));
        }

        public event DomainEvent NotifyObservers;

        #endregion
    }
}