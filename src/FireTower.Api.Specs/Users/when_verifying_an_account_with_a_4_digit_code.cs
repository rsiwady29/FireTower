using System.Net;
using AcklenAvenue.Testing.Moq;
using AcklenAvenue.Testing.Moq.ExpectedObjects;
using AcklenAvenue.Testing.Nancy;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using FireTower.Domain.Commands;
using FireTower.Domain.Entities;
using HttpStatusCode = Nancy.HttpStatusCode;
using It = Machine.Specifications.It;

namespace FireTower.Api.Specs.Users
{
    public class when_verifying_an_account_with_a_4_digit_code : given_a_verification_module_context
    {
        const string TestEmail = "test@email.com";
        const string VerificationCode = "9292";
        static BrowserResponse _result;
        static Verification _matchingVerification;

        Establish context =
            () =>
                {
                    _matchingVerification = new Verification
                                                {
                                                    EmailAddress =
                                                        TestEmail,
                                                    VerificationCode =
                                                        VerificationCode
                                                };
                    Mock.Get(ReadOnlyRepository).Setup(
                        x =>
                        x.First(
                            ThatHas.AnExpressionFor<Verification>().ThatMatches(_matchingVerification).ThatDoesNotMatch(
                                new Verification()).Build())).Returns(_matchingVerification);
                };

        Because of =
            () => _result = Browser.PostSecureJson("/verify", new {email = TestEmail, code = VerificationCode});

        It should_mark_the_account_as_verified =
            () =>
            Mock.Get(CommandDispatcher).Verify(
                x => x.Dispatch(WithExpected.Object(new ActivateUser(TestEmail))));

        It should_return_a_successful_response =
            () => _result.StatusCode.ShouldEqual(HttpStatusCode.OK);
    }
}