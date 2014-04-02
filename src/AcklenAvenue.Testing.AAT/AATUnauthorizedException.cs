using System;

namespace AcklenAvenue.Testing.AAT
{
    public class AATUnauthorizedException : Exception
    {
        public AATUnauthorizedException()
            : base("The request resulted in 'Unauthorized.")
        {

        }
    }
}