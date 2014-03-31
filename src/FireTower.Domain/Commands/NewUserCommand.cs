namespace FireTower.Domain.Commands
{
    public class NewUserCommand
    {
        public int AgreementVersion { get; set; }

        public EncryptedPassword EncryptedPassword { get; set; }

        public string Email { get; set; }
    }
}