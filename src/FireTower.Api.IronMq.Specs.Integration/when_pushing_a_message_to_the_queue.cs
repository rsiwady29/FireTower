using System;
using AcklenAvenue.Testing.Nancy;
using Machine.Specifications;

namespace FireTower.IronMq.Specs.Integration
{
    public class when_pushing_a_message_to_the_queue
    {
        static IIronMqPusher _client;

        Establish context =
            () => { _client = new RestSharpIronMqClientAdapter("test_queue"); };

        Because of =
            () => _client.Push(Guid.NewGuid(), new TestCommand
                                   {
                                       Message = "it worked",
                                       Success = true,
                                       Attempts = 1
                                   });

        It should_return_an_ok_response =
            () => { };
    }
}