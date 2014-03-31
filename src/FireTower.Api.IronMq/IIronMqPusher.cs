namespace FireTower.IronMq
{
    public interface IIronMqPusher
    {
        void Push<T>(T command) where T : class;
    }
}