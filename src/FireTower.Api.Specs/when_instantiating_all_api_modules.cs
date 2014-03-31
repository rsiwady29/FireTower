using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Machine.Specifications;
using Nancy;
using FireTower.Infrastructure;
using FireTower.Presentation;

namespace FireTower.Api.Specs
{
    public class when_instantiating_all_api_modules
    {
        static IContainer _container;
        static List<Type> _modules;

        Establish context =
            () =>
                {
                    var typeScanner = new TypeScanner.TypeScanner();
                    _modules =
                        typeScanner.GetTypesWhere(
                            x => x.BaseType == typeof (NancyModule) && x.Assembly.FullName.Contains("FireTower")).ToList();

                    var containerBuilder = new ContainerBuilder();
                    new ConfigureCommonDependencies().Task(containerBuilder);
                    new ConfigureApiDependencies().Task(containerBuilder);
                    new ConfigureWorkerDependencies().Task(containerBuilder);
                    new ConfigureDatabaseWiring().Task(containerBuilder);
                    _modules.ForEach(x => containerBuilder.RegisterType(x));

                    _container = containerBuilder.Build();
                };

        Because of =
            () => _modules.ForEach(x => _container.Resolve(x));

        It should_not_throw_any_exceptions =
            () => { };
    }
}