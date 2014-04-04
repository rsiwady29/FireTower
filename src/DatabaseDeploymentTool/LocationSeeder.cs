using DomainDrivenDatabaseDeployer;
using FireTower.Domain.Entities;
using NHibernate;

namespace DatabaseDeploymentTool
{
    public class LocationSeeder : IDataSeeder
    {
        readonly ISession _session;

        public LocationSeeder(ISession session)
        {
            _session = session;
        }

        public void Seed()
        {
            _session.Save(new Location
                {
                    LocationId = 106781442691621,
                    LocationName = "San Pedro Sula, Cortes"

                });
        }
    }
}