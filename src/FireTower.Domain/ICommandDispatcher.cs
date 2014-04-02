namespace FireTower.Domain
{
    public interface ICommandDispatcher
    {
        void Dispatch(IUserSession userSession, object command);
    }
}