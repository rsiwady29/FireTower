using System;

namespace FireTower.Domain.Entities
{
    public class UserSession : IEntity, IUserSession
    {
        public virtual Guid Id { get; set; }
        public virtual User User { get; set; }
        public virtual DateTime Expires { get; set; }

        public static UserSession New(User user, DateTime? expires = null)
        {
            return new UserSession
            {
                Id = Guid.NewGuid(),
                User = user,
                Expires = expires ?? DateTime.Now.AddDays(10)
            };
        }
    }
}