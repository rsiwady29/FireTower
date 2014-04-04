using System;
using FireTower.Domain.Commands;
using FireTower.Domain.Entities;
using FireTower.Domain.Events;

namespace FireTower.Domain.CommandHandlers
{
    public class PutOutVoteAdder : ICommandHandler
    {
        private readonly IReadOnlyRepository _readOnlyRepository;

        public PutOutVoteAdder(IReadOnlyRepository readOnlyRepository)
        {
            _readOnlyRepository = readOnlyRepository;
        }

        public Type CommandType { get { return typeof (VoteOnPutOut); } }

        public void Handle(IUserSession userSessionIssuingCommand, object command)
        {
            var u = (UserSession) userSessionIssuingCommand;
            var c = (VoteOnPutOut) command;

            var disasterToUpdate = _readOnlyRepository.GetById<Disaster>(c.DisasterId);
            disasterToUpdate.addPutOutVote(u.User, c.DisasterId, c.IsPutOut);
            NotifyObservers.Invoke(new PutOutVoteAdded(u.User.Id, c.DisasterId, c.IsPutOut));
        }

        public event DomainEvent NotifyObservers;
    }
}