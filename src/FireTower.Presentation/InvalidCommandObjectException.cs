using System;

namespace FireTower.Presentation
{
    public class InvalidCommandObjectException : Exception
    {
        public InvalidCommandObjectException(Exception innerException) : base("Invalid json string.", innerException)
        {
        }
    }
}