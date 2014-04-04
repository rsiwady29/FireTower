using System;
using AcklenAvenue.Testing.AAT;
using Machine.Specifications;
using FireTower.Presentation.Requests;
using FireTower.Presentation.Responses;
using RestSharp;

namespace FireTower.API.AAT
{
    public class when_logging_in_with_correct_credentials : given_an_api_server_context<CurrentlyDeveloping>
    {
        static IRestResponse<SuccessfulLoginResponse<Guid>> _result;

        Because of =
            () =>
            _result =
            Client.Execute<SuccessfulLoginResponse<Guid>>("/login", Method.POST,
                                                   new LoginRequest { FacebookId = 1817134138 });

        It should_return_a_token =
            () => (_result.Data ?? new SuccessfulLoginResponse<Guid>()).Token.ShouldNotEqual(Guid.Empty);
    }
}