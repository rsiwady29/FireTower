using System;

namespace FireTower.Domain.Entities
{
    public class PutOutVote : IEntity
    {
        public PutOutVote()
        {
        }

        public PutOutVote(User user, bool isPutOut)
        {
            User = user;
            IsPutOut = isPutOut;
        }

        public virtual bool IsPutOut { get; set; }

        public virtual User User { get; protected set; }
        public virtual Guid Id { get; protected set; }
    }
}