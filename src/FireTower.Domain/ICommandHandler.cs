using System;

namespace FireTower.Domain
{
    public interface ICommandHandler
    {
        Type CommandType { get; }
        void Handle(object command);
        event DomainEvent NotifyObservers;
    }
}