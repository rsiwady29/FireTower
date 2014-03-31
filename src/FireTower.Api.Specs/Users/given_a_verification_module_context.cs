using Machine.Specifications;
using Moq;
using Nancy.Testing;
using FireTower.Domain;
using FireTower.Presentation.Modules;

namespace FireTower.Api.Specs.Users
{
    public class given_a_verification_module_context
    {
        protected static Browser Browser;
        protected static IReadOnlyRepository ReadOnlyRepository;

        protected static ICommandDispatcher CommandDispatcher;

        Establish master_context = () =>
                                       {
                                           ReadOnlyRepository = Mock.Of<IReadOnlyRepository>();
                                           CommandDispatcher = Mock.Of<ICommandDispatcher>();
                                           Browser = new Browser(x =>
                                                                     {
                                                                         x.Module<VerificationModule>();
                                                                         x.Dependency(ReadOnlyRepository);
                                                                         x.Dependency(CommandDispatcher);
                                                                     });
                                       };
    }
}