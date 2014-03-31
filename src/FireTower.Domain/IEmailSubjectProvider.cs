namespace FireTower.Domain
{
    public interface IEmailSubjectProvider
    {
        string GetSubjectFor<T>(T model);
    }
}