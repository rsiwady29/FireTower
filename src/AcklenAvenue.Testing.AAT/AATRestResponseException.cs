using System;
using System.Net;

namespace AcklenAvenue.Testing.AAT
{
    public class AATRestResponseException : Exception
    {
        public AATRestResponseException(HttpStatusCode httpStatusCode) : base(GetMessage(httpStatusCode))
        {
            HttpStatusCode = httpStatusCode;
        }

        public HttpStatusCode HttpStatusCode { get; set; }

        public static string GetMessage(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.Forbidden:
                    return "The user is not activated, resulting in a 'Forbidden' response.";
                default:
                    return string.Format("The rest request resulted in '{0}'!", statusCode);
            }
        }
    }
}