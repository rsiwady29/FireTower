using AcklenAvenue.Testing.Moq;
using AcklenAvenue.Testing.Nancy;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using FireTower.Data;
using FireTower.Domain.Entities;
using It = Machine.Specifications.It;

namespace FireTower.Api.Specs.Users
{
    public class when_logging_in_with_incorrect_credentials : given_a_login_module_context
    {
        static BrowserResponse _result;

        Establish context =
            () => Mock.Get(ReadOnlyRepository).Setup(x=> x.First(ThatHas.AnExpressionFor<User>().Build()))
                      .Throws(new ItemNotFoundException<User>());

        Because of =
            () =>
            _result = Browser.PostSecureJson("/login", new {email = "incorrect@email.com", password = "incorrect"});

        It should_return_an_unauthorized_response =
            () => _result.StatusCode.ShouldEqual(HttpStatusCode.Unauthorized);
    }
}