using AutoMapper;
using FireTower.Domain;
using FireTower.Domain.Entities;
using FireTower.Presentation.Modules;
using Machine.Specifications;
using Moq;
using Nancy.Testing;

namespace FireTower.Api.Specs
{
    public class given_an_image_module
    {
        protected static Browser Browser;
        protected static IMappingEngine Mapper;
        protected static ICommandDispatcher CommandDispatcher;
        protected static IReadOnlyRepository ReadOnlyRepo;
        protected static UserSession UserSession;

        Establish master_context =
            () =>
                {
                    Mapper = Mock.Of<IMappingEngine>();
                    CommandDispatcher = Mock.Of<ICommandDispatcher>();
                    ReadOnlyRepo = Mock.Of<IReadOnlyRepository>();

                    Browser = new Browser(x =>
                                              {
                                                  x.Module<ImageModule>();
                                                  x.Dependency(Mapper);
                                                  x.Dependency(CommandDispatcher);
                                                  x.Dependency(ReadOnlyRepo);
                                                  UserSession = UserSession.New(new User());
                                                  x.WithUserSession(UserSession);
                                              });
                };
    }
}