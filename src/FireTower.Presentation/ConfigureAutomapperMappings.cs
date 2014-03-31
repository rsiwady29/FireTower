using System;
using Autofac;
using FireTower.Domain.Entities;
using FireTower.Infrastructure;
using FireTower.Presentation.Responses;

namespace FireTower.Presentation
{
    public class ConfigureAutomapperMappings : IBootstrapperTask<ContainerBuilder>
    {
        #region IBootstrapperTask<ContainerBuilder> Members

        public Action<ContainerBuilder> Task
        {
            get
            {
                return container =>
                           {
                               AutoMapper.Mapper.CreateMap<User, MeResponse>();
                               
                           };
            }
        }

        #endregion
    }
}