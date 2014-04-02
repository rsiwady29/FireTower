using System;

namespace FireTower.IronMq
{
    public interface IIronMqPusher
    {
        void Push(Guid userSessionToken, object command);
    }
}