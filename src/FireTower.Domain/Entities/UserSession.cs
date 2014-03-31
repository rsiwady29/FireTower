using System;

namespace FireTower.Domain.Entities
{
    public class UserSession : IEntity
    {
        public virtual Guid Id { get; set; }
        public virtual User User { get; set; }
        public virtual DateTime Expires { get; set; }
    }
}