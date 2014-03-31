using System;

namespace FireTower.Domain.Events
{
    public class UserActivated
    {
        public readonly Guid UserId;

        public UserActivated(Guid userId)
        {
            UserId = userId;
        }
    }
}