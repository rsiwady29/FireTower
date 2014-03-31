using Machine.Specifications;
using Moq;
using Nancy.Testing;
using FireTower.Domain;
using FireTower.Presentation;
using FireTower.Presentation.Modules;

namespace FireTower.Api.Specs.Worker
{
    public class given_a_work_module_context
    {
        protected static Browser Browser;
        protected static ICommandDispatcher CommandDispatcher;
        protected static ICommandDeserializer CommandDeserializer;

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
                                              });
                };
    }
}