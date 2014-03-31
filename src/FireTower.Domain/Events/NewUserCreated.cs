namespace FireTower.Domain.Events
{
    public class NewUserCreated
    {
        public string Email { get; private set; }
        public int AgreementVersion { get; private set; }

        public NewUserCreated(string email, int agreementVersion)
        {
            Email = email;
            AgreementVersion = agreementVersion;
        }
    }
}