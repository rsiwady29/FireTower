using System.Net;
using AcklenAvenue.Testing.AAT;
using Machine.Specifications;
using FireTower.Presentation.Requests;
using RestSharp;

namespace FireTower.API.AAT
{
    public class when_logging_in_with_incorrect_credentials : given_an_api_server_context<CurrentlyDeveloping>
    {
        static IRestResponse _result;

        Because of =
            () => _result = Client.Execute("/login", Method.POST, new LoginRequest { Email = "incorrect@test.com", Password = "incorrect" });

        It should_return_a_token =
            () => _result.StatusCode.ShouldEqual(HttpStatusCode.Unauthorized);
    }
}