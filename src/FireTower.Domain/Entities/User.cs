using System;

namespace FireTower.Domain.Entities
{
    public class User : IEntity
    {
        public virtual string EncryptedPassword { get; set; }

        public virtual string Email { get; set; }

        public virtual int AgreementVersion { get; set; }

        public virtual bool Activated { get; set; }

        public virtual string Name { get; set; }

        #region IEntity Members

        public virtual Guid Id { get; set; }

        #endregion
    }
}