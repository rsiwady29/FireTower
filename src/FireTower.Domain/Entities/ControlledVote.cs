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

        public virtual bool IsControlled { get; protected set; }

        public virtual User User { get; protected set; }
        public virtual Guid Id { get; protected set; }
    }
}