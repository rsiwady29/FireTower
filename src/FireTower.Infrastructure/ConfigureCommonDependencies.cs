using System;
using Autofac;
using FireTower.Data;
using FireTower.Domain;
using FireTower.Domain.Services;

namespace FireTower.Infrastructure
{
    public class ConfigureCommonDependencies : IBootstrapperTask<ContainerBuilder>
    {
        #region IBootstrapperTask<ContainerBuilder> Members

        public Action<ContainerBuilder> Task
        {
            get
            {
                return container =>
                           {
                               container.RegisterType<ReadOnlyRepository>().As<IReadOnlyRepository>();
                               container.RegisterType<WriteableRepository>().As<IWriteableRepository>();
                               container.RegisterType<HashPasswordEncryptor>().As<IPasswordEncryptor>();
                               container.RegisterType<GuidTokenGenerator>().As<ITokenGenerator<Guid>>();
                               container.RegisterType<UserSessionFactory>().As<IUserSessionFactory>();
                               container.RegisterType<SystemTimeProvider>().As<ITimeProvider>();                               
                               
                               container
                                   .RegisterAssemblyTypes(typeof(ICommandHandler).Assembly)
                                   .AsImplementedInterfaces();

                               //container
                               //    .RegisterAssemblyTypes(typeof (IApiUserMapper<Guid>).Assembly)
                               //    .AsImplementedInterfaces();

                           };
            }
        }

        #endregion
    }
}