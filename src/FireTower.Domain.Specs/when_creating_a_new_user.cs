using AcklenAvenue.Testing.Moq.ExpectedObjects;
using Machine.Specifications;
using Moq;
using FireTower.Domain.CommandHandlers;
using FireTower.Domain.Commands;
using FireTower.Domain.Entities;
using FireTower.Domain.Events;
using It = Machine.Specifications.It;

namespace FireTower.Domain.Specs
{
    public class when_creating_a_new_user
    {
        static ICommandHandler _handler;
        static NewUserCommand _newUserCommand;
        static IWriteableRepository _writeableRepository;
        static object _event;
        static NewUserCreated _expectedEvent;

        Establish context =
            () =>
                {
                    _writeableRepository = Mock.Of<IWriteableRepository>();
                    _handler = new NewUserCreator(_writeableRepository);

                    _newUserCommand = new NewUserCommand
                                          {
                                              Email = "email",
                                              AgreementVersion = 1,
                                              EncryptedPassword = new EncryptedPassword("password")
                                          };

                    _handler.NotifyObservers += x => _event = x;

                    _expectedEvent = new NewUserCreated(_newUserCommand.Email, _newUserCommand.AgreementVersion);
                };

        Because of =
            () => _handler.Handle(_newUserCommand);

        It should_create_the_new_user_in_the_repo =
            () => Mock.Get(_writeableRepository)
                      .Verify(x =>
                              x.Create(WithExpected.Object(new User
                                                               {
                                                                   EncryptedPassword =
                                                                       _newUserCommand.EncryptedPassword.Password,
                                                                   Email = _newUserCommand.Email,
                                                                   AgreementVersion = _newUserCommand.AgreementVersion
                                                               })));

        It should_handle_new_user_commands = () => _handler.CommandType.ShouldEqual(typeof (NewUserCommand));

        It should_notify_observers = () => _event.ShouldBeLike(_expectedEvent);
    }
}