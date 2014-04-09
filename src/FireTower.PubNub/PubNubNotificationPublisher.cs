using System;
using FireTower.Domain;

namespace PubNubMessaging.Core
{
    public class PubNubNotificationPublisher : INotificationPublisher
    {
        readonly Pubnub _pubNub;

        public PubNubNotificationPublisher()
        {
            _pubNub = new Pubnub("pub-c-d968cca2-8900-4867-bb04-0631d8a7fbf3",
                                 "sub-c-e379a784-bff9-11e3-a219-02ee2ddab7fe");
        }

        #region INotificationPublisher Members

        public void Publish(Guid userId, object message)
        {
            Action<object> successCallback = x => { };
            Action<PubnubClientError> errorCallback = x => { throw new Exception(x.Message); };
            _pubNub.Publish(userId.ToString(), message, successCallback, errorCallback);
        }

        #endregion
    }
}