namespace FireTower.Domain
{
    public interface IVerificationEmailSender
    {
        void Send(string emailAddress);
    }
}