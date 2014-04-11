using System;

namespace FireTower.Domain.Events
{
    public class NewDisasterCreated
    {
        public readonly Guid UserId;
        public readonly Guid DisasterId;
        public readonly DateTime CreatedDate;
        public readonly string LocationDescription;
        public readonly double Latitude;
        public readonly double Longitude;
        
        public NewDisasterCreated(Guid userId, Guid disasterId, DateTime createdDate, string locationDescription, double latitude, double longitude)
        {
            UserId = userId;
            DisasterId = disasterId;

            Latitude = latitude;
            Longitude = longitude;
            CreatedDate = createdDate;
            LocationDescription = locationDescription;         
        }
    }
}