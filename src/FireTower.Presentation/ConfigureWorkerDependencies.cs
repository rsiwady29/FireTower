using System;
using Autofac;
using BlingBag;
using FireTower.Domain;
using FireTower.Domain.CommandDispatchers;
using FireTower.Infrastructure;

namespace FireTower.Presentation
{
    public class ConfigureWorkerDependencies : IBootstrapperTask<ContainerBuilder>
    {
        public Action<ContainerBuilder> Task
        {
            get
            {
                return container =>
                           {
                               container.RegisterType<JsonCommandDeserializer>().As<ICommandDeserializer>();
                               container.RegisterType<SynchronousCommandDispatcher>().As<ICommandDispatcher>();

                               RegisterBlingBagServices(container);
                           };
            }
        }

        static void RegisterBlingBagServices(ContainerBuilder container)
        {
            container.RegisterType<BlingInitializer<DomainEvent>>().As<IBlingInitializer<DomainEvent>>();
            container.RegisterType<BlingConfigurator>().As<IBlingConfigurator<DomainEvent>>();
            container.RegisterType<AutoFacBlingDispatcher>().As<IBlingDispatcher>();
        }
    }
}