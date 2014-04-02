using System;

namespace FireTower.Domain
{
    public interface IUserSession
    {
        Guid Id { get; }
    }
}