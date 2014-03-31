namespace FireTower.Presentation
{
    public interface ICommandDeserializer
    {
        object Deserialize(string str);
    }
}