using System;

namespace FireTower.Domain.Entities
{
    public class ControlledVote : IEntity
    {
        protected ControlledVote()
        {
        }

        public ControlledVote(User user, bool isControlled)
        {
            User = user;
            IsControlled = isControlled;
        }

        public bool IsControlled { get; protected set; }

        public User User { get; protected set; }
        public Guid Id { get; protected set; }
    }
}