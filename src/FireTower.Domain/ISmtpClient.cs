namespace FireTower.Domain
{
    public interface ISmtpClient
    {
        void Send(string emailAddress, string subject, string body);
    }
}