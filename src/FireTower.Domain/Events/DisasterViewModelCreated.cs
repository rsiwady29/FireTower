using System;

namespace FireTower.Domain.Events
{
    public class DisasterViewModelCreated
    {
        public Guid UserId { get; set; }
        public string BsonValue { get; set; }

        public DisasterViewModelCreated(Guid userId, string bsonValue)
        {
            UserId = userId;
            BsonValue = bsonValue;
        }
    }
}