using Blacksmith.Core;
using Newtonsoft.Json;

namespace FireTower.IronMq
{
    public class BlackSmithIronMqClientAdapter : IIronMqPusher
    {
        const string DevToken = "RchaMp7bPTv2VZ_tptENRwzGw7g";
        const string ProjectId = "531779c7cf0dd7000900000e";
        readonly Client _client;
        readonly string _queueName;

        public BlackSmithIronMqClientAdapter(string queueName)
        {
            _queueName = queueName;
            _client = new Client(ProjectId, DevToken);

            ConfigurationWrapper.JsonSettings =
                new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    };
        }

        #region IIronMqPusher Members

        public void Push<T>(T command) where T : class
        {
            Queue<T>().Push(command);
        }

        #endregion

        IQueueWrapper<T> Queue<T>() where T : class
        {
            IQueueWrapper<T> queue = _client.Queue<T>(_queueName);
            return queue;
        }
    }
}