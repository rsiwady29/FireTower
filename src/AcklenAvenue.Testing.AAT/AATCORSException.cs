using System;

namespace AcklenAvenue.Testing.AAT
{
    public class AATCORSException : Exception
    {
        public AATCORSException()
            : base("The request does not allow CORS.")
        {

        }
    }
}