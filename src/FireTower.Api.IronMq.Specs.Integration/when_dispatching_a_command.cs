using System;
using AcklenAvenue.Testing.Nancy;
using FireTower.Domain;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace FireTower.IronMq.Specs.Integration
{
    public class when_dispatching_a_command
    {
        static ICommandDispatcher _dispatcher;
        static IIronMqPusher _client;
        static object _command;
        static IUserSession _userSession;
        static readonly Guid _userSessionToken = Guid.NewGuid();

        Establish context =
            () =>
                {
                    _client = Mock.Of<IIronMqPusher>();
                    _dispatcher = new IronMqCommandDispatcher(_client);

                    _command = new TestCommand
                                   {
                                       Message = "cool!"
                                   };

                    _userSession = Mock.Of<IUserSession>();
                    Mock.Get(_userSession).Setup(x => x.Id).Returns(_userSessionToken);
                };

        Because of =
            () => _dispatcher.Dispatch(_userSession, _command);

        It should_push_command_to_the_queue_as_a_message =
            () => Mock.Get(_client).Verify(x => x.Push(_userSessionToken, _command));
    }
}