using System;

namespace FireTower.Domain.Entities
{
    public class SeverityVote : IEntity
    {
        protected SeverityVote()
        {
        }

        public SeverityVote(User user, int severity)
        {
            User = user;
            Severity = severity;
        }

        public virtual User User { get; protected set; }
        public virtual int Severity { get; protected set; }

        #region IEntity Members

        public virtual Guid Id { get; protected set; }

        #endregion
    }
}