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

        public void Dispatch(IUserSession userSession, object command)
        {
            _client.Push(userSession.Id, command);
        }
    }
}