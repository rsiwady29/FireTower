using System;
using System.Net;
using AcklenAvenue.Testing.AAT;
using Machine.Specifications;
using FireTower.Presentation.Requests;
using FireTower.Presentation.Responses;
using RestSharp;

namespace FireTower.API.AAT
{
    public class when_getting_me : given_an_api_server_context<CurrentlyDeveloping>
    {
        static IRestResponse<MeResponse> _result;
        static Guid _token;

        Establish context =
            () =>
                {
                    IRestResponse<SuccessfulLoginResponse<Guid>> login =
                        Client.Execute<SuccessfulLoginResponse<Guid>>("/login", Method.POST,
                                                                      new FacebookLoginRequest
                                                                          {
                                                                              FacebookId = 1817134138
                                                                          });
                    _token = login.Data.Token;
                };

        Because of =
            () =>
            _result =
            Client.Get<MeResponse>("/me", new { token = _token });

        It should_have_an_ok_response =
            () => _result.StatusCode.ShouldEqual(HttpStatusCode.OK);

        It should_say_that_the_user_exists = () => _result.Data.ShouldBeLike(new MeResponse
                                                                                 {
                                                                                     FacebookId = 1817134138
                                                                                 });
    }
}