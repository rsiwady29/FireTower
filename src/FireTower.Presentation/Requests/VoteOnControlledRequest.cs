using System;

namespace FireTower.Presentation.Requests
{
    public class VoteOnControlledRequest
    {
        public bool IsControlled { get; set; }

        public Guid DisasterId { get; set; }
    }
}