using System;

namespace FireTower.Infrastructure.Exceptions
{
    public class UserInputPropertyValidationException : Exception
    {
        public UserInputPropertyValidationException(string message):base(message)
        {            
        }
    }
}