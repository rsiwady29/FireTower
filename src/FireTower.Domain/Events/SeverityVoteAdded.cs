using System;

namespace FireTower.Domain.Events
{
    public class SeverityVoteAdded
    {
        public readonly Guid DisasterId;
        public readonly int Severity;
        public readonly Guid UserId;

        public SeverityVoteAdded(Guid userId, Guid disasterId, int severity)
        {
            UserId = userId;
            DisasterId = disasterId;
            Severity = severity;
        }
    }
}