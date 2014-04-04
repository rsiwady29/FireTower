using System;

namespace FireTower.Domain.Events
{
    public class ControlledVoteAdded
    {
        private readonly Guid _userId;
        private readonly Guid _disasterId;
        private readonly bool _isControlled;

        public ControlledVoteAdded(Guid userId, Guid disasterId, bool isControlled)
        {
            _userId = userId;
            _disasterId = disasterId;
            _isControlled = isControlled;
        }
    }
}