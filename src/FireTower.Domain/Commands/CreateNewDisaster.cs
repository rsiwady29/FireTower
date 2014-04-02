namespace FireTower.Domain.Commands
{
    public class CreateNewDisaster
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string FirstImageUrl { get; set; }

        public CreateNewDisaster()
        {
        }

        public CreateNewDisaster(double lat, double lng, string url)
        {
            Latitude = lat;
            Longitude = lng;
            FirstImageUrl = url;
        }
    }
}