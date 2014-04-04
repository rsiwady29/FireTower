using System;

namespace FireTower.Domain.Commands
{
    public class VoteOnControlled
    {
        public Guid DisasterId { get; set; }
        public bool IsControlled { get; set; }

        public VoteOnControlled(Guid disasterId, bool isControlled)
        {
            DisasterId = disasterId;
            IsControlled = isControlled;
        }
    }
}