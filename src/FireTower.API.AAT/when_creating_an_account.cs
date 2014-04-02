using System;
using System.Net;
using AcklenAvenue.Testing.AAT;
using FireTower.Presentation.Requests;
using FireTower.Presentation.Responses;
using Machine.Specifications;
using RestSharp;

namespace FireTower.API.AAT
{
    public class when_creating_an_account : given_an_api_server_context<CurrentlyDeveloping>
    {
        static readonly Random Rnd = new Random();
        static IRestResponse _result;
        static string _email;
        static string _password;

        Establish context =
            () =>
                {
                    _email = Rnd.Next(99999) + "@test.com";
                    _password = "some password";
                };

        Because of =
            () =>
            _result = Client.Post("/user", new NewUserRequest
                                               {
                                                   Email = _email,
                                                   Password = _password,
                                                   AgreementVersion = 1
                                               });

        It should_exist =
            () =>
            Client.Get<UserExistenceResponse>("/user/exists", new {email = _email}).Data.
                Exists.ShouldBeTrue();

        It should_success =
            () => _result.StatusCode.ShouldEqual(HttpStatusCode.OK);
    }
}