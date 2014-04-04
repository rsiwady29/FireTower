namespace FireTower.Domain.Events
{
    public class NewUserCreated
    {
        public long FacebookId { get; private set; }

        public NewUserCreated(long facebookId)
        {
            FacebookId = facebookId;
        }
    }
}