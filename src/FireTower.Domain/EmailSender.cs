namespace FireTower.Domain
{
    public class EmailSender : IEmailSender
    {
        readonly IEmailBodyRenderer _emailBodyRenderer;
        readonly IEmailSubjectProvider _emailSubjectProvider;
        readonly ISmtpClient _smtpClient;

        public EmailSender(IEmailBodyRenderer emailBodyRenderer, IEmailSubjectProvider emailSubjectProvider, ISmtpClient smtpClient)
        {
            _emailBodyRenderer = emailBodyRenderer;
            _emailSubjectProvider = emailSubjectProvider;
            _smtpClient = smtpClient;
        }

        public void Send<T>(T model, string emailAddress)
        {
            _smtpClient.Send(
                emailAddress,
                _emailSubjectProvider.GetSubjectFor(model),
                _emailBodyRenderer.Render(model));
        }
    }
}