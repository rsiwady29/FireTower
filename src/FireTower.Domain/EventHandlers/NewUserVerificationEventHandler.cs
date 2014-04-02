using BlingBag;
using FireTower.Domain.Events;

namespace FireTower.Domain.EventHandlers
{
    public class NewUserVerificationEventHandler : IBlingHandler<NewUserCreated>
    {
        readonly IVerificationEmailSender _sender;

        public NewUserVerificationEventHandler(IVerificationEmailSender sender)
        {
            _sender = sender;
        }

        public void Handle(NewUserCreated @event)
        {
            //_sender.Send(@event.FacebookId);
        }
    }
}