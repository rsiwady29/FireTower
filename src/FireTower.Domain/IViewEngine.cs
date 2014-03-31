namespace FireTower.Domain
{
    public interface IViewEngine
    {
        string Render<T>(T model, string formattedString);
    }
}