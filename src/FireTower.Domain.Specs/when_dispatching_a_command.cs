using System.Collections.Generic;
using BlingBag;
using Machine.Specifications;
using Moq;
using FireTower.Domain.CommandDispatchers;
using FireTower.Domain.Commands;
using It = Machine.Specifications.It;

namespace FireTower.Domain.Specs
{
    public class when_dispatching_a_command
    {
        static SynchronousCommandDispatcher _commandDispatcher;
        static NewUserCommand _command;
        static List<ICommandHandler> _handlers;
        static ICommandHandler _commandHandler1;
        static IBlingInitializer<DomainEvent> _blingInitializer;
        static ICommandHandler _commandHandler2;

        Establish context =
            () =>
                {
                    _blingInitializer = Mock.Of<IBlingInitializer<DomainEvent>>();

                    _commandHandler1 = Mock.Of<ICommandHandler>();
                    Mock.Get(_commandHandler1).Setup(x => x.CommandType).Returns(typeof (NewUserCommand));

                    _commandHandler2 = Mock.Of<ICommandHandler>();
                    Mock.Get(_commandHandler2).Setup(x => x.CommandType).Returns(typeof (NewUserCommand));

                    _handlers = new List<ICommandHandler>
                                    {
                                        _commandHandler1,
                                        _commandHandler2
                                    };

                    _commandDispatcher = new SynchronousCommandDispatcher(_blingInitializer, _handlers);

                    _command = new NewUserCommand();
                };

        Because of =
            () => _commandDispatcher.Dispatch(_command);

        It should_dispatch_to_the_first_handler =
            () => Mock.Get(_commandHandler1).Verify(x => x.Handle(_command));

        It should_dispatch_to_the_second_handler =
            () => Mock.Get(_commandHandler2).Verify(x => x.Handle(_command));

        It should_initialize_the_first_handlers_domain_events =
            () => Mock.Get(_blingInitializer).Verify(x => x.Initialize(_commandHandler1));

        It should_initialize_the_second_handlers_domain_events =
            () => Mock.Get(_blingInitializer).Verify(x => x.Initialize(_commandHandler2));
    }
}