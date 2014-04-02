using BlingBag;
using FireTower.Domain.EventHandlers;
using FireTower.Domain.Events;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace FireTower.Domain.Specs
{
    public class after_creating_a_new_user
    {
        const string EmailAddress = "something@email.com";
        static IBlingHandler<NewUserCreated> _handler;
        static IVerificationEmailSender _sender;

        Establish context =
            () =>
                {
                    _sender = Mock.Of<IVerificationEmailSender>();
                    _handler = new NewUserVerificationEventHandler(_sender);
                };

        Because of =
            () => _handler.Handle(new NewUserCreated(EmailAddress, 1));

        It should_send_a_verification_email_to_the_user =
            () => Mock.Get(_sender).Verify(x => x.Send(EmailAddress));
    }
}