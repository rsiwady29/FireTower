using System;
using AcklenAvenue.Testing.Moq.ExpectedObjects;
using AcklenAvenue.Testing.Nancy;
using FireTower.Domain.Commands;
using FireTower.Presentation.Requests;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace FireTower.Api.Specs
{
    public class when_adding_an_image_to_a_disaster : given_an_image_module
    {
        static BrowserResponse _result;
        const string ImageString = "image string";
        static readonly Guid DisasterId = Guid.NewGuid();

        Because of =
            () => _result = Browser.PostSecureJson("/disasters/" + DisasterId + "/image", new AddImageRequest
                                                                                               {
                                                                                                   Base64Image =
                                                                                                       ImageString
                                                                                               });

        It should_be_ok = () => _result.StatusCode.ShouldEqual(HttpStatusCode.OK);

        It should_dispatch_the_expected_command =
            () =>
            Mock.Get(CommandDispatcher)
                .Verify(
                    x =>
                    x.Dispatch(UserSession,
                               WithExpected.Object(new AddImageToDisaster(DisasterId, ImageString))));
    }
}