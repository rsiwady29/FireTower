namespace FireTower.Domain
{
    public interface IEmailBodyRenderer
    {
        string Render<T>(T model);
    }
}