using System;
using System.Net.Mail;
using FireTower.Domain;
using Typesafe.Mailgun;

namespace FireTower.Mailgun
{
    public class MailgunSmtpClient : ISmtpClient
    {
        readonly MailgunClient _client;

        public MailgunSmtpClient()
        {
            _client = new MailgunClient("app93dde6e95ec447a9b43da69fae655152.mailgun.org", "key-9-66abzuc7opdzxcsc0d9mdj6jdnig51");
        }

        #region ISmtpClient Members

        public void Send(string emailAddress, string subject, string body)
        {
            var message = new MailMessage("FireTower <no-reply@firetower.com>", emailAddress, subject, body);
            message.IsBodyHtml = true;
            _client.SendMail(message);
        }

        #endregion
    }
}