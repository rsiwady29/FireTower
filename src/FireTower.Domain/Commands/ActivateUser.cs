namespace FireTower.Domain.Commands
{
    public class ActivateUser
    {
        public string Email { get; private set; }

        public ActivateUser(string email)
        {
            Email = email;
        }
    }
}