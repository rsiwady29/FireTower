using Nancy;
using Nancy.Testing;
using FireTower.Domain.Entities;
using FireTower.Infrastructure;
using FireTower.Presentation;

namespace FireTower.Api.Specs
{
    public static class IdentityTestingExtensions
    {
        public static void WithUser(this ConfigurableBootstrapper.ConfigurableBootstrapperConfigurator config, User user)
        {
            var nancyContext = new NancyContext
                                   {
                                       CurrentUser = new FireTowerUserIdentity(user)
                                   };

            var contextFactory = new TestingContextFactory(nancyContext);

            config.ContextFactory(contextFactory);
        }
    }
}