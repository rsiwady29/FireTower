using System;

namespace FireTower.Domain.Commands
{
    public class VoteOnSeverity
    {
        public readonly Guid DisasterId;
        public readonly int Severity;

        public VoteOnSeverity(Guid disasterId, int severity)
        {
            DisasterId = disasterId;
            Severity = severity;
        }
    }
}