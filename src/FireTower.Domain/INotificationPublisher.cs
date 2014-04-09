using System;

namespace FireTower.Domain
{
    public interface INotificationPublisher
    {
        void Publish(Guid userId, object @message);
    }
}