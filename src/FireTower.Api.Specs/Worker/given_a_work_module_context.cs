using FireTower.Domain;
using FireTower.Domain.Entities;
using FireTower.Presentation;
using FireTower.Presentation.Modules;
using Machine.Specifications;
using Moq;
using Nancy.Testing;

namespace FireTower.Api.Specs.Worker
{
    public class given_a_work_module_context
    {
        protected static Browser Browser;
        protected static ICommandDispatcher CommandDispatcher;
        protected static ICommandDeserializer CommandDeserializer;
        protected static VisitorSession UserSession;

        Establish master_context =
            () =>
                {
                    CommandDispatcher = Mock.Of<ICommandDispatcher>();
                    CommandDeserializer = Mock.Of<ICommandDeserializer>();
                    Browser = new Browser(x =>
                                              {
                                                  x.Module<WorkModule>();
                                                  x.Dependency(CommandDispatcher);
                                                  x.Dependency(CommandDeserializer);
                                                  UserSession = new VisitorSession();
                                                  x.WithUserSession(UserSession);
                                              });
                };
    }
}