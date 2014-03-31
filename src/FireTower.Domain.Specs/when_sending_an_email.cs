using Machine.Specifications;
using Moq;
using FireTower.Domain.Entities;
using It = Machine.Specifications.It;

namespace FireTower.Domain.Specs
{
    public class when_sending_an_email
    {
        const string EmailAddress = "something@email.com";
        const string EmailBody = "email body";
        const string Subject = "Account Verification";
        static IEmailSender _emailSender;
        static IEmailBodyRenderer _emailBodyRenderer;
        static Verification _model;
        static ISmtpClient _smtpClient;
        static IEmailSubjectProvider _emailSubjectProvider;

        Establish context =
            () =>
                {
                    _emailBodyRenderer = Mock.Of<IEmailBodyRenderer>();
                    _smtpClient = Mock.Of<ISmtpClient>();
                    _emailSubjectProvider = Mock.Of<IEmailSubjectProvider>();
                    _emailSender = new EmailSender(_emailBodyRenderer, _emailSubjectProvider, _smtpClient);

                    _model = new Verification();

                    Mock.Get(_emailBodyRenderer).Setup(x => x.Render(_model)).Returns(EmailBody);

                    Mock.Get(_emailSubjectProvider).Setup(x => x.GetSubjectFor(_model)).Returns(Subject);
                };

        Because of =
            () => _emailSender.Send(_model, EmailAddress);

        It should_send_the_expected_email_body =
            () => Mock.Get(_smtpClient).Verify(x => x.Send(EmailAddress, Subject, EmailBody));
    }
}