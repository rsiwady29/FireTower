using System;
using Blacksmith.Core;

namespace FireTower.IronMq
{
    public class IronMqSubscriber : IIronMqSubscriber
    {
        const string DevToken = "RchaMp7bPTv2VZ_tptENRwzGw7g";
        const string ProjectId = "531779c7cf0dd7000900000e";
        readonly Client _client;

        public IronMqSubscriber()
        {
            _client = new Client(ProjectId, DevToken);
        }

        #region IIronMqSubscriber Members

        public void Subscribe(string queueName, string postUrl)
        {
            try
            {
                _client.Queue<object>(queueName).Subscribe(postUrl);
            }
            catch
            {                
            }
        }

        #endregion
    }
}