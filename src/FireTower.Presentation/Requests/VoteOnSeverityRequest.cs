using System;

namespace FireTower.Presentation.Requests
{
    public class VoteOnSeverityRequest
    {
        public Guid DisasterId { get; set; }
        public int Severity { get; set; }
    }
}