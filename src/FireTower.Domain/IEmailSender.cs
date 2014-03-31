namespace FireTower.Domain
{
    public interface IEmailSender
    {
        void Send<T>(T model, string emailAddress);
    }
}