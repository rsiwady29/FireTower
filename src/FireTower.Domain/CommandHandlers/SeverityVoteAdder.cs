using System;
using FireTower.Domain.Commands;
using FireTower.Domain.Entities;
using FireTower.Domain.Events;

namespace FireTower.Domain.CommandHandlers
{
    public class SeverityVoteAdder : ICommandHandler
    {
        readonly IReadOnlyRepository _readOnlyRepository;

        public SeverityVoteAdder(IReadOnlyRepository readOnlyRepository)
        {
            _readOnlyRepository = readOnlyRepository;
        }

        #region ICommandHandler members

        public Type CommandType
        {
            get { return typeof (VoteOnSeverity); }
        }

        public void Handle(IUserSession userSessionIssuingCommand, object command)
        {
            var c = (VoteOnSeverity) command;
            var u = (UserSession) userSessionIssuingCommand;

            var disasterToUpdate = _readOnlyRepository.GetById<Disaster>(c.DisasterId);

            disasterToUpdate.AddSeverityVote(u.User, c.Severity);

            NotifyObservers(new SeverityVoteAdded(u.User.Id, c.DisasterId, c.Severity));
        }

        public event DomainEvent NotifyObservers;

        #endregion
    }
}