namespace FireTower.Domain
{
    public interface ICommandDispatcher
    {
        void Dispatch(object command);
    }
}