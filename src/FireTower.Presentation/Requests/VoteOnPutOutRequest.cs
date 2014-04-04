using System;

namespace FireTower.Presentation.Requests
{
    public class VoteOnPutOutRequest
    {
        public bool IsPutOut { get; set; }

        public Guid DisasterId { get; set; }
    }
}