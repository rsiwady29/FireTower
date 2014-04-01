namespace FireTower.Domain.Commands
{
    public class CreateNewDisaster
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Url { get; set; }

        public CreateNewDisaster()
        {
        }

        public CreateNewDisaster(double lat, double lng, string url)
        {
            Lat = lat;
            Lng = lng;
            Url = url;
        }
    }
}