using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FireTower.Domain.Entities;

namespace FireTower.Data
{
    public class UserSessionAutoMappingOverride : IAutoMappingOverride<UserSession>
    {
        public void Override(AutoMapping<UserSession> mapping)
        {
            mapping.Id(x => x.Id).GeneratedBy.Assigned();
        }
    }
}