using System;

namespace FireTower.Domain.Entities
{
    public class Verification : IEntity
    {
        public virtual Guid Id { get; set; }

        public virtual string EmailAddress { get; set; }

        public virtual string VerificationCode { get; set; }
    }
}