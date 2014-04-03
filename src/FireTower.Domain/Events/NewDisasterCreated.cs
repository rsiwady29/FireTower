using System;

namespace FireTower.Domain.Events
{
    public class NewDisasterCreated
    {
        public readonly Guid DisasterId;
        public readonly DateTime CreatedDate;
        public readonly string LocationDescription;
        public readonly double Latitude;
        public readonly double Longitude;
        public readonly string FirstImageUrl;
        public readonly int FirstSeverityVote;

        public NewDisasterCreated(Guid disasterId, DateTime createdDate, string locationDescription, double latitude, double longitude, string firstImageUrl, int firstSeverityVote)
        {
            DisasterId = disasterId;

            Latitude = latitude;
            Longitude = longitude;
            FirstImageUrl = firstImageUrl;
            CreatedDate = createdDate;
            LocationDescription = locationDescription;
            FirstSeverityVote = firstSeverityVote;
        }
    }
}