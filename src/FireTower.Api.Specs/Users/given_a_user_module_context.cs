using AutoMapper;
using FireTower.Domain;
using FireTower.Domain.Entities;
using FireTower.Presentation.Modules;
using Machine.Specifications;
using Moq;
using Nancy.Testing;

namespace FireTower.Api.Specs.Users
{
    public class given_a_user_module_context
    {
        protected static Browser Browser;
        protected static IMappingEngine Mapper;
        protected static User LoggedInUser;
        protected static IReadOnlyRepository ReadOnlyRepository;
        protected static UserSession UserSession;

        Establish master_context = () =>
                                       {
                                           ReadOnlyRepository = Mock.Of<IReadOnlyRepository>();
                                           Mapper = Mock.Of<IMappingEngine>();
                                           Browser = new Browser(x =>
                                                                     {
                                                                         x.Module<UserModule>();
                                                                         x.Dependency(Mapper);
                                                                         x.Dependency(ReadOnlyRepository);
                                                                         LoggedInUser = new User
                                                                                            {
                                                                                                Verified = true,
                                                                                            };
                                                                         UserSession = UserSession.New(LoggedInUser);
                                                                         x.WithUserSession(UserSession);
                                                                     });
                                       };
    }
}