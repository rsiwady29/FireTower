namespace FireTower.IronMq
{
    public interface IIronMqSubscriber
    {
        void Subscribe(string queueName, string postUrl);
    }
}