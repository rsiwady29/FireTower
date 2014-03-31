using AcklenAvenue.Testing.Nancy;
using Machine.Specifications;
using Moq;
using FireTower.Domain;
using It = Machine.Specifications.It;

namespace FireTower.IronMq.Specs.Integration
{
    public class when_dispatching_a_command
    {
        static ICommandDispatcher _dispatcher;
        static IIronMqPusher _client;
        static object _command;

        Establish context =
            () =>
                {
                    _client = Mock.Of<IIronMqPusher>();
                    _dispatcher = new IronMqCommandDispatcher(_client);

                    _command = new TestCommand
                                   {
                                       Message = "cool!"
                                   };
                };

        Because of =
            () => _dispatcher.Dispatch(_command);

        It should_push_command_to_the_queue_as_a_message =
            () => Mock.Get(_client).Verify(x => x.Push(_command));
    }
}