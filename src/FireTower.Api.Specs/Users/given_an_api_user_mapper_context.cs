using Machine.Specifications;
using Moq;
using FireTower.Domain;
using FireTower.Presentation;

namespace FireTower.Api.Specs.Users
{
    public class given_an_api_user_mapper_context
    {
        protected static ApiUserMapper _mapper;
        protected static IReadOnlyRepository _readOnlyRepo;
        protected static ITimeProvider _timeProvider;

        Establish master_context = () =>
                                       {
                                           _readOnlyRepo = Mock.Of<IReadOnlyRepository>();
                                           _timeProvider = Mock.Of<ITimeProvider>();
                                           _mapper = new ApiUserMapper(_readOnlyRepo, _timeProvider);

                                       };
    }
}