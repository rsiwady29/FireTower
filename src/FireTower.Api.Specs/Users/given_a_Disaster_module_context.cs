using FireTower.Domain;
using FireTower.Presentation.Modules;
using Machine.Specifications;
using Moq;
using Nancy.Testing;

namespace FireTower.Api.Specs.Users
{
    public class given_a_Disaster_module_context
    {

    
        protected static Browser Browser;
        protected static ICommandDispatcher CommandDispatcher;
        protected static IUserSessionFactory UserSessionFactory;
        
        Establish master_context = () =>
            {
                CommandDispatcher = Mock.Of<ICommandDispatcher>();
                Browser = new Browser(x =>
                    {
                        x.Module<DisasterModule>();
                        x.Dependency(CommandDispatcher);                                                                        
                    });
            };
    

    }
}