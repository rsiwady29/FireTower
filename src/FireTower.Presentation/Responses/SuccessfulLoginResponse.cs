using System;

namespace FireTower.Presentation.Responses
{
    public class SuccessfulLoginResponse<T>
    {
        public SuccessfulLoginResponse()
        {
            
        }

        public SuccessfulLoginResponse(T token, DateTime expires)
        {
            Token = token;
            Expires = expires;
        }

        public T Token { get; private set; }

        public DateTime Expires { get; private set; }
    }
}