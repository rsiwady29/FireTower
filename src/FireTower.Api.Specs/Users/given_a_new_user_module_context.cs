using FireTower.Domain;
using FireTower.Presentation.Modules;
using Machine.Specifications;
using Moq;
using Nancy.Testing;

namespace FireTower.Api.Specs.Users
{
    public class given_a_new_user_module_context
    {
        protected static Browser Browser;
        protected static IReadOnlyRepository ReadOnlyRepository;
        protected static IPasswordEncryptor PasswordEncryptor;
        protected static ICommandDispatcher CommandDispatcher;

        protected static VisitorSession VisitorSession;

        Establish master_context = () =>
                                       {
                                           ReadOnlyRepository = Mock.Of<IReadOnlyRepository>();
                                           PasswordEncryptor = Mock.Of<IPasswordEncryptor>();
                                           CommandDispatcher = Mock.Of<ICommandDispatcher>();
                                           Browser = new Browser(x =>
                                                                     {
                                                                         x.Module<NewUserModule>();
                                                                         x.Dependency(ReadOnlyRepository);
                                                                         x.Dependency(PasswordEncryptor);
                                                                         x.Dependency(CommandDispatcher);
                                                                         VisitorSession = new VisitorSession();
                                                                         x.WithUserSession(VisitorSession);
                                                                     });
                                       };
    }
}