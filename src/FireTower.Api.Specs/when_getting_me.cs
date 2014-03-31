using AcklenAvenue.Testing.Nancy;
using FizzWare.NBuilder;
using Machine.Specifications;
using Moq;
using Nancy.Testing;
using FireTower.Api.Specs.Users;
using FireTower.Domain.Entities;
using FireTower.Presentation.Responses;
using It = Machine.Specifications.It;

namespace FireTower.Api.Specs
{
    public class when_getting_me : given_a_user_module_context
    {
        static BrowserResponse _result;
        static MeResponse _meResponse;

        Establish context =
            () =>
                {
                    _meResponse = Builder<MeResponse>.CreateNew().Build();
                    Mock.Get(Mapper).Setup(x => x.Map<User, MeResponse>(LoggedInUser))
                        .Returns(_meResponse);
                };

        Because of =
            () => _result = Browser.GetSecureJson("/me");

        It should_return_the_logged_in_user =
            () => _result.Body<MeResponse>().ShouldBeLike(_meResponse);
    }
}