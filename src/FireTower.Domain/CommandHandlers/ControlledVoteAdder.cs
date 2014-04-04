using System;
using FireTower.Domain.Commands;
using FireTower.Domain.Entities;
using FireTower.Domain.Events;

namespace FireTower.Domain.CommandHandlers
{
    public class ControlledVoteAdder : ICommandHandler
    {
        private readonly IReadOnlyRepository _readOnlyRepository;

        public ControlledVoteAdder(IReadOnlyRepository readOnlyRepository)
        {
            _readOnlyRepository = readOnlyRepository;
        }

        public Type CommandType { get { return typeof (VoteOnControlled); } }

        public void Handle(IUserSession userSessionIssuingCommand, object command)
        {
            var c = (VoteOnControlled) command;
            var u = (UserSession) userSessionIssuingCommand;

            var disasterToUpdate = _readOnlyRepository.GetById<Disaster>(c.DisasterId);

            disasterToUpdate.addControlledVote(u.User, c.DisasterId, c.IsControlled);

            NotifyObservers(new ControlledVoteAdded(u.User.Id, c.DisasterId, c.IsControlled));
        }

        public event DomainEvent NotifyObservers;
    }
}