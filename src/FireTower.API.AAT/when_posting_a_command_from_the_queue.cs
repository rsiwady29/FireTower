using System;
using AcklenAvenue.Testing.AAT;
using FireTower.Domain;
using FireTower.Domain.Commands;
using Machine.Specifications;
using RestSharp;

namespace FireTower.API.AAT
{
    public class when_posting_a_command_from_the_queue : given_an_api_server_context<CurrentlyDeveloping>
    {
        static IRestResponse _result;
        static dynamic _commandFromQueue;

        Establish context = () =>
                                {
                                    Guid token = Login().Token;
                                    var command = new NewUserCommand
                                                      {
                                                          AgreementVersion = 1,
                                                          Email =
                                                              string.Format("sommardahl+{0}@gmail.com",
                                                                            new Random().Next(99999)),
                                                          EncryptedPassword =
                                                              new EncryptedPassword("4565432345654321")
                                                      };

                                    _commandFromQueue = command.ToDynamic();
                                    _commandFromQueue.Token = token;
                                    _commandFromQueue.Type = command.GetType().AssemblyQualifiedName;
                                };

        Because of =
            () => _result = Client.Post("/work", _commandFromQueue);

        It should_process_the_command =
            () => _result.ShouldBeOk();
    }
}