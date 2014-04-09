using FireTower.Domain.CommandHandlers;
using FireTower.Domain.Entities;

namespace FireTower.Domain
{
    public class DisasterMapper : IDisasterMapping
    {
        public CreatedDisaster Map(Disaster disaster)
        {
            return new CreatedDisaster()
                {
                    Id = disaster.Id
                };
        }
    }
}