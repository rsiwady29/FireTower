using System;
using System.Net;
using AcklenAvenue.Testing.AAT;
using Machine.Specifications;
using FireTower.Domain;
using FireTower.Domain.Commands;
using RestSharp;

namespace FireTower.API.AAT
{
    public class when_posting_a_command_from_the_queue : given_an_api_server_context<CurrentlyDeveloping>
    {
        static IRestResponse _result;

        Because of =
            () => _result = Client.Post("/work", new NewUserCommand
                                                     {
                                                         AgreementVersion = 1,
                                                         Email =
                                                             string.Format("sommardahl+{0}@gmail.com",
                                                                           new Random().Next(99999)),
                                                         EncryptedPassword = new EncryptedPassword("4565432345654321")
                                                     });

        It should_process_the_command =
            () => _result.StatusCode.ShouldEqual(HttpStatusCode.OK);
    }
}