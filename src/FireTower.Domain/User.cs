using System;

namespace Invio.Domain
{
    public class User : IEntity
    {
        public User()
        {
            CurrentAmount = 0;
        }
        public virtual Guid Id { get; protected set; }

        public virtual string Password { get; set; }

        public virtual string Email { get; set; }

        public virtual string Name { get; set; }

        public virtual string BankName { get; set; }

        public virtual string BankAccount { get; set; }

        public virtual int CurrentAmount { get; set; }
    }
}