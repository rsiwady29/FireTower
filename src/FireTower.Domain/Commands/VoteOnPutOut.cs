using System;

namespace FireTower.Domain.Commands
{
    public class VoteOnPutOut
    {
        public VoteOnPutOut(Guid disasterId, bool isPutOut)
        {
            DisasterId = disasterId;
            IsPutOut = isPutOut;
        }

        public VoteOnPutOut()
        {
        }

        public bool IsPutOut { get; set; }

        public Guid DisasterId { get; set; }
    }
}