using AcklenAvenue.Testing.Moq;
using AcklenAvenue.Testing.Moq.ExpectedObjects;
using AcklenAvenue.Testing.Nancy;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using FireTower.Data;
using FireTower.Domain.Commands;
using FireTower.Domain.Entities;
using It = Machine.Specifications.It;

namespace FireTower.Api.Specs.Users
{
    public class when_attempting_to_verify_an_account_with_an_invalid_4_digit_code : given_a_verification_module_context
    {
        const string TestEmail = "test@email.com";
        const string VerificationCode = "9292";
        static BrowserResponse _result;
        
        Establish context =
            () => Mock.Get(ReadOnlyRepository)
                      .Setup(x => x.First(ThatHas.AnExpressionFor<Verification>().Build()))
                      .Throws(new ItemNotFoundException<Verification>());

        Because of =
            () => _result = Browser.PostSecureJson("/verify", new { email = TestEmail, code = VerificationCode });

        It should_not_mark_the_account_as_verified =
            () =>
            Mock.Get(CommandDispatcher).Verify(
                x => x.Dispatch(WithExpected.Object(new ActivateUser(TestEmail))), Times.Never());

        It should_return_an_unauthorized_response =
            () => _result.StatusCode.ShouldEqual(HttpStatusCode.Unauthorized);
    }
}