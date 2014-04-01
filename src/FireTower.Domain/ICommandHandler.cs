using System;

namespace FireTower.Domain
{
    public interface ICommandHandler
    {
        Type CommandType { get; }
        void Handle(IUserSession userSessionIssuingCommand, object command);
        event DomainEvent NotifyObservers;
    }
}