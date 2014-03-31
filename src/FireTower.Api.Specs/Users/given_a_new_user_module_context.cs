using Machine.Specifications;
using Moq;
using Nancy.Testing;
using FireTower.Domain;
using FireTower.Presentation.Modules;

namespace FireTower.Api.Specs.Users
{
    public class given_a_new_user_module_context
    {
        protected static Browser Browser;
        protected static IReadOnlyRepository ReadOnlyRepository;
        protected static IPasswordEncryptor PasswordEncryptor;
        protected static ICommandDispatcher CommandDispatcher;

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
                                                                     });
                                       };
    }
}