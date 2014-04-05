using System;
using RestSharp;

namespace AcklenAvenue.Testing.AAT
{
    public class AATServerUnreachableException : Exception
    {
        public AATServerUnreachableException(RestClient browser, RestRequest request)
            :base(string.Format("The server '{0}' is not reachable.", browser.BaseUrl))
        {
            
        }
    }
}