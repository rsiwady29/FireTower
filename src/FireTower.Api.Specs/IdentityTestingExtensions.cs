using FireTower.Domain;
using FireTower.Infrastructure;
using Nancy;
using Nancy.Testing;

namespace FireTower.Api.Specs
{
    public static class IdentityTestingExtensions
    {
        public static void WithUserSession(this ConfigurableBootstrapper.ConfigurableBootstrapperConfigurator config,
                                           IUserSession session)
        {
            var nancyContext = new NancyContext
                                   {
                                       CurrentUser = new FireTowerUserIdentity(session)
                                   };

            var contextFactory = new TestingContextFactory(nancyContext);

            config.ContextFactory(contextFactory);
        }
    }
}