using System;

namespace FireTower.Domain.Events
{
    public class NewDisasterCreated
    {
        public readonly Guid DisasterId;
        public readonly double Latitude;
        public readonly double Longitude;
        public readonly string FirstImageUrl;

        public NewDisasterCreated(Guid disasterId, double latitude, double longitude, string firstImageUrl)
        {
            DisasterId = disasterId;
            Latitude = latitude;
            Longitude = longitude;
            FirstImageUrl = firstImageUrl;
        }
    }
}