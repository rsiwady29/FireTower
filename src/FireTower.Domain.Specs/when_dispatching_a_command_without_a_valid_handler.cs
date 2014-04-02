using System;
using System.Collections.Generic;
using FireTower.Domain.Entities;
using Machine.Specifications;
using FireTower.Domain.CommandDispatchers;
using FireTower.Domain.Commands;
using FireTower.Domain.Exceptions;

namespace FireTower.Domain.Specs
{
    public class when_dispatching_a_command_without_a_valid_handler
    {
        static SynchronousCommandDispatcher _commandDispatcher;
        static NewUserCommand _command;
        static List<ICommandHandler> _handlers;
        static Exception _exception;

        Establish context =
            () =>
                {
                    _handlers = new List<ICommandHandler>
                                    {
                                    };

                    _commandDispatcher = new SynchronousCommandDispatcher(null, _handlers);

                    _command = new NewUserCommand();
                };

        Because of =
            () => _exception = Catch.Exception(() => _commandDispatcher.Dispatch(new UserSession(), _command));

        It should_dispatch_to_the_correct_processor =
            () => _exception.ShouldBeOfExactType<NoAvailableHandlerException>();
    }
}