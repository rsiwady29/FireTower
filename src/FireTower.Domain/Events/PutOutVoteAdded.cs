using System;

namespace FireTower.Domain.Events
{
    public class PutOutVoteAdded
    {
        public readonly Guid _userId;
        public readonly Guid _disasterId;
        public readonly bool _isPutOut;

        public PutOutVoteAdded(Guid userId, Guid disasterId, bool isPutOut)
        {
            _userId = userId;
            _disasterId = disasterId;
            _isPutOut = isPutOut;
        }
    }
}