using FireTower.Domain;
using FireTower.Presentation.Modules;
using Machine.Specifications;
using Moq;
using Nancy.Testing;

namespace FireTower.Api.Specs.Users
{
    public class given_a_login_module_context
    {
        protected static Browser Browser;
        protected static IReadOnlyRepository ReadOnlyRepository;
        protected static IUserSessionFactory UserSessionFactory;
        protected static IPasswordEncryptor PasswordEncryptor;

        Establish master_context = () =>
                                       {
                                           ReadOnlyRepository = Mock.Of<IReadOnlyRepository>();
                                           UserSessionFactory = Mock.Of<IUserSessionFactory>();
                                           Browser = new Browser(x =>
                                                                     {
                                                                         x.Module<LoginModule>();
                                                                         x.Dependency(ReadOnlyRepository);
                                                                         x.Dependency(UserSessionFactory);
                                                                         PasswordEncryptor =
                                                                             Mock.Of<IPasswordEncryptor>();
                                                                         x.Dependency(PasswordEncryptor);
                                                                     });
                                       };
    }
}