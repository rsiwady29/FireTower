using Machine.Specifications;

namespace FireTower.Mailgun.Specs
{
    public class when_sending_email
    {
        static MailgunSmtpClient _mailgunSender;

        Establish context =
            () =>
                {
                    _mailgunSender = new MailgunSmtpClient();
                };

        Because of =
            () => _mailgunSender.Send("byron+mailgun_test@acklenavenue.com", "Test LastName", "This is a test.");

        It should_send_it =
            () => { };
    }


}