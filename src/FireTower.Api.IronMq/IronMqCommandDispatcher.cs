using FireTower.Domain;

namespace FireTower.IronMq
{
    public class IronMqCommandDispatcher : ICommandDispatcher
    {
        readonly IIronMqPusher _client;

        public IronMqCommandDispatcher(IIronMqPusher client)
        {
            _client = client;
        }

        public void Dispatch(object command)
        {
            _client.Push(command);
        }
    }
}