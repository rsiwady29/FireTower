using System.Net;
using AcklenAvenue.Testing.AAT;
using RestSharp;

namespace FireTower.API.AAT
{
    public static class ResultShouldExtensions
    {
        public static void ShouldBeOk(this IRestResponse response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new AATRestResponseException(response, response.Request);
            }
        }
    }
}