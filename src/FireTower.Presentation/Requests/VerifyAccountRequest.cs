namespace FireTower.Presentation.Requests
{
    public class VerifyAccountRequest
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}