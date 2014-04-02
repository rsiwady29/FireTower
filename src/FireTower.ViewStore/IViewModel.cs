using MongoDB.Bson;

namespace FireTower.ViewStore
{
    public interface IViewModel
    {
        BsonObjectId Id { get; set; }
    }
}