namespace FireTower.Domain.Commands
{
    public class CreateNewDisaster
    {
        public CreateNewDisaster(string locationDescription, double lat, double lng, int severity)
        {
            LocationDescription = locationDescription;
            Latitude = lat;
            Longitude = lng;
            FirstSeverity = severity;
        }

        public string LocationDescription { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int FirstSeverity { get; set; }
    }
}