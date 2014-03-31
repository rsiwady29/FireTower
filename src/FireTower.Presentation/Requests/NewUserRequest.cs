namespace FireTower.Presentation.Requests
{
    public class NewUserRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public int AgreementVersion { get; set; }
    }
}