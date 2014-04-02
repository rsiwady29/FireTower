using AcklenAvenue.Testing.Moq;
using AcklenAvenue.Testing.Moq.ExpectedObjects;
using AcklenAvenue.Testing.Nancy;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using FireTower.Data;
using FireTower.Domain;
using FireTower.Domain.Commands;
using FireTower.Domain.Entities;
using FireTower.Presentation.Requests;
using It = Machine.Specifications.It;

namespace FireTower.Api.Specs.Users
{
    public class when_creating_a_new_user_account : given_a_new_user_module_context
    {
        static BrowserResponse _result;
        static NewUserRequest _request;
        static EncryptedPassword _encryptedPassword;

        Establish context =
            () =>
                {
                    _request = new NewUserRequest
                                   {
                                       FirstName = "Byron",
                                       LastName = "Sommardahl",
                                       Name = "Byron Sommardahl",
                                       FacebookId = 1817134138,
                                       Locale = "es_ES",
                                       Username = "bsommardahl",
                                       Verified = true
                                   };

                    Mock.Get(ReadOnlyRepository).Setup(x => x.First(ThatHas.AnExpressionFor<User>().Build()))
                        .Throws(new ItemNotFoundException<User>());

                    _encryptedPassword = new EncryptedPassword("encrypted password");
                };

        Because of =
            () => _result = Browser.PostSecureJson("/user", _request);

        It should_return_an_ok_response = () => _result.StatusCode.ShouldEqual(HttpStatusCode.OK);

        It should_add_a_command_to_the_queue =
            () => Mock.Get(CommandDispatcher).Verify(x => x.Dispatch(VisitorSession, WithExpected.Object(new NewUserCommand
                                                                                             {
                                                                                                 FirstName = _request.FirstName,
                                                                                                 LastName = _request.LastName,
                                                                                                 Name = _request.Name,
                                                                                                 FacebookId = _request.FacebookId,
                                                                                                 Locale = _request.Locale,
                                                                                                 Username = _request.Username,
                                                                                                 Verified = _request.Verified
                                                                                             })));
    }
}