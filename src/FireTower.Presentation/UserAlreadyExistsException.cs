using System;

namespace FireTower.Presentation
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException()
            : base("A user with that email address already exists.")
        {
        }
    }
}