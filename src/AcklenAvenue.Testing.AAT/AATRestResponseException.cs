using System;
using System.Linq;
using System.Net;
using RestSharp;

namespace AcklenAvenue.Testing.AAT
{
    public class AATRestResponseException : Exception
    {
        public IRestResponse Response { get; set; }
        public IRestRequest Request { get; set; }

        public AATRestResponseException(IRestResponse response, IRestRequest request)
            : base(GetMessage(response, request))
        {
            Response = response;
            Request = request;
        }

        public static string GetMessage(IRestResponse response, IRestRequest restRequest)
        {
            if ((int)response.StatusCode == 0)
                return "The server is not accepting requests. It could be down or the url is not correct.";

            switch (response.StatusCode)
            {
                case HttpStatusCode.Forbidden:
                    Parameter emailArg = restRequest.Parameters.FirstOrDefault(x => x.Name == "email");
                    object username = emailArg == null ? "(unknown)" : emailArg.Value;

                    return string.Format("The user '{0}' is not activated, resulting in a 'Forbidden' response.",
                                         username);
                case HttpStatusCode.NotFound:
                    return string.Format("The route '{0}' was not accessible, resulting in a 'NotFound' response.",
                                         restRequest.Resource);

                case HttpStatusCode.InternalServerError:
                    return response.Content;
                default:
                    return string.Format("The route '{0}' resulted in '{1}'!", restRequest.Resource, response.StatusCode);
            }
        }
    }
}