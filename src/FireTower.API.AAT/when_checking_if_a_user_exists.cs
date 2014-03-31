using System.Net;
using AcklenAvenue.Testing.AAT;
using Machine.Specifications;
using FireTower.Presentation.Responses;
using RestSharp;

namespace FireTower.API.AAT
{
    public class when_checking_if_a_user_exists : given_an_api_server_context<CurrentlyDeveloping>
    {
        static IRestResponse<UserExistenceResponse> _result;

        Because of =
            () =>
            _result =
            Client.Get<UserExistenceResponse>("/user/exists", new { email = "test@test.com" });

        It should_have_an_ok_response =
            () => _result.StatusCode.ShouldEqual(HttpStatusCode.OK);

        It should_say_that_the_user_exists = () => _result.Data.ShouldBeLike(new UserExistenceResponse
                                                                                 {
                                                                                     Exists = true,
                                                                                     Activated = true
                                                                                 });
    }
}