using System;

namespace FireTower.Domain.Entities
{
    public class DisasterSeverity : IEntity
    {
        protected DisasterSeverity()
        {
        }

        public DisasterSeverity(int severity)
        {
            Severity = severity;
        }

        public virtual int Severity { get; set; }

        #region IEntity Members

        public virtual Guid Id { get; protected set; }

        #endregion
    }
}