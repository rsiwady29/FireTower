using System;

namespace FireTower.Domain.Events
{
    public class ControlledVoteAdded
    {
        public readonly Guid _userId;
        public readonly Guid _disasterId;
        public readonly bool _isControlled;

        public ControlledVoteAdded(Guid userId, Guid disasterId, bool isControlled)
        {
            _userId = userId;
            _disasterId = disasterId;
            _isControlled = isControlled;
        }
    }
}