using System;
using AcklenAvenue.Testing.Moq;
using AcklenAvenue.Testing.Moq.ExpectedObjects;
using FireTower.Domain.Commands;
using FireTower.Domain.Entities;
using FireTower.Domain.EventHandlers;
using FireTower.Domain.Events;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace FireTower.Domain.Specs
{
    public class when_activating_an_account
    {
        const string TestEmail = "testEmail";
        static ICommandHandler _handler;
        static ActivateUser _command;
        static IReadOnlyRepository _readOnlyRepository;
        static User _user;
        static IWriteableRepository _writeableRepository;
        static Guid _verificationId;
        static object _eventRaised;
        static UserActivated _expectedEvent;

        Establish context =
            () =>
                {
                    _writeableRepository = Mock.Of<IWriteableRepository>();
                    _readOnlyRepository = Mock.Of<IReadOnlyRepository>();

                    _handler = new ActivateUserHandler(_readOnlyRepository, _writeableRepository);
                    _command = new ActivateUser(TestEmail);

                    _user = new User
                                {
                                    Id = Guid.NewGuid(),
                                    Email = TestEmail,
                                    Activated = false,
                                };
                    Mock.Get(_readOnlyRepository).Setup(
                        x =>
                        x.First(ThatHas.AnExpressionFor<User>().ThatMatches(_user).ThatDoesNotMatch(new User()).Build()))
                        .Returns(_user);

                    _verificationId = Guid.NewGuid();
                    var verification = new Verification
                                           {
                                               Id = _verificationId,
                                               EmailAddress = TestEmail,
                                           };
                    Mock.Get(_readOnlyRepository).Setup(
                        x =>
                        x.First(
                            ThatHas.AnExpressionFor<Verification>().ThatMatches(verification).ThatDoesNotMatch(
                                new Verification()).Build())).Returns(verification);

                    _expectedEvent = new UserActivated(_user.Id);
                    _handler.NotifyObservers += x => _eventRaised = x;
                };

        Because of =
            () => _handler.Handle(new VisitorSession(), _command);

        It should_delete_the_verification =
            () => Mock.Get(_writeableRepository).Verify(x => x.Delete<Verification>(_verificationId));

        It should_handle_the_expected_command_type = () => _handler.CommandType.ShouldEqual(_command.GetType());

        It should_raise_the_expected_event = () => _eventRaised.ShouldBeLike(_expectedEvent);

        It should_update_the_expected_account =
            () =>
            Mock.Get(_writeableRepository).Verify(
                x => x.Update(WithExpected.Object(new User
                                                      {
                                                          Id = _user.Id,
                                                          Email = TestEmail,
                                                          Activated = true
                                                      })));
    }
}