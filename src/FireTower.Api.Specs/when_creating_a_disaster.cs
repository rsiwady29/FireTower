using AcklenAvenue.Testing.Moq.ExpectedObjects;
using AcklenAvenue.Testing.Nancy;
using FireTower.Domain.Commands;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace FireTower.Api.Specs
{
    public class when_creating_a_disaster : given_a_disaster_module
    {
        const double Latitude = 123.11;
        const double Longitude = 421.11;
        const string Location = "Santa Ana";
        
        static readonly CreateNewDisaster CreateNewDisaster =
            new CreateNewDisaster(Location, Latitude, Longitude);

        static BrowserResponse _result;

        Because of =
            () => _result = Browser.PostSecureJson("/Disasters", CreateNewDisaster);

        It should_be_ok = () => _result.StatusCode.ShouldEqual(HttpStatusCode.OK);

        It should_dispatch_the_expected_command =
            () =>
            Mock.Get(CommandDispatcher)
                .Verify(
                    x =>
                    x.Dispatch(UserSession,
                               WithExpected.Object(CreateNewDisaster)));
    }
}