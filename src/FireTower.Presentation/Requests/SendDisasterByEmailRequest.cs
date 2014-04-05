namespace FireTower.Presentation.Requests
{
    public class SendDisasterByEmailRequest
    {
        public string Email { get; set; }
        public string CreatedDate { get; set; }
        public string LocationDescription { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}