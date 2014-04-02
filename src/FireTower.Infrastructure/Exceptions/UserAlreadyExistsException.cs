using System;

namespace FireTower.Infrastructure.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException()
            : base("A user with that email address already exists.")
        {
        }
    }
}