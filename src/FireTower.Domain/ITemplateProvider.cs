namespace FireTower.Domain
{
    public interface ITemplateProvider
    {
        string GetTemplateFor<T>(T model);
    }
}