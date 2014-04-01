using System;

namespace FireTower.Infrastructure.Exceptions
{
    public class InvalidCommandObjectException : Exception
    {
        public InvalidCommandObjectException(Exception innerException) : base("Invalid json string.", innerException)
        {
        }
    }
}