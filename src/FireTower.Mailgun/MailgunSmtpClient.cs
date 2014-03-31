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
            throw new NotImplementedException("Please add mailgun account info.");
            _client = new MailgunClient("app17153.mailgun.org", "key-29d7lk4lmkbm45dm37s1mrsczzkcs0d1");
        }

        #region ISmtpClient Members

        public void Send(string emailAddress, string subject, string body)
        {
            var message = new MailMessage("FireTower <no-reply@FireTower.com>", emailAddress, subject, body);
            _client.SendMail(message);
        }

        #endregion
    }
}