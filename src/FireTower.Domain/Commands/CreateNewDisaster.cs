namespace FireTower.Domain.Commands
{
    public class CreateNewDisaster
    {
        public CreateNewDisaster(string locationDescription, double lat, double lng)
        {
            LocationDescription = locationDescription;
            Latitude = lat;
            Longitude = lng;            
        }

        public string LocationDescription { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }        
    }
}