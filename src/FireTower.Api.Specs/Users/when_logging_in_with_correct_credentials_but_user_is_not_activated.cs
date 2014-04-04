using System;
using AcklenAvenue.Testing.Moq;
using AcklenAvenue.Testing.Nancy;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using FireTower.Domain;
using FireTower.Domain.Entities;
using It = Machine.Specifications.It;

namespace FireTower.Api.Specs.Users
{
    public class when_logging_in_with_correct_credentials_but_user_is_not_activated : given_a_login_module_context
    {
        const long FacebookId = 123456;
        static readonly Guid Token = Guid.NewGuid();
        static BrowserResponse _result;
        static User _matching;
        static UserSession _userSession;
        static DateTime _expires;

        Establish context =
            () =>
                {
                    _matching = new User
                                    {
                                        FirstName = "Byron",
                                        LastName = "Sommardahl",
                                        Name = "Byron Sommardahl",
                                        FacebookId = 123456,
                                        Locale = "es_ES",
                                        Username = "bsommardahl",
                                        Verified = false
                                    };

                    Mock.Get(ReadOnlyRepository).Setup(
                        x =>
                        x.First(
                            ThatHas.AnExpressionFor<User>().ThatMatches(_matching).ThatDoesNotMatch(new User()).Build()))
                        .Returns(_matching);
                };

        Because of =
            () => _result = Browser.PostSecureJson("/login", new { facebookId = FacebookId });

        It should_return_a_bad_request =
            () => _result.StatusCode.ShouldEqual(HttpStatusCode.Forbidden);
    }
}