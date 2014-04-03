using System;

namespace FireTower.Domain.Commands
{
    public class CreateNewDisaster
    {
        public DateTime CreatedDate { get; set; }
        public string LocationDescription { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int FirsSeverity { get; set; }
        public string FirstImageUrl { get; set; }

        public CreateNewDisaster()
        {
        }

        public CreateNewDisaster(DateTime createdDate, string locationDescription, double lat, double lng, string url, int severity)
        {
            CreatedDate = createdDate;
            LocationDescription = locationDescription;
            Latitude = lat;
            Longitude = lng;
            FirstImageUrl = url;
            FirsSeverity = severity;
        }
    }
}