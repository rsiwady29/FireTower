using FireTower.Domain.Entities;

namespace FireTower.Domain.CommandHandlers
{
    public interface IDisasterMapping
    {
        CreatedDisaster Map(Disaster disaster);
    }
}