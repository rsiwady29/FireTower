using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private static CreateNewDisaster _createNewDisaster = new CreateNewDisaster(123.11, 421.11, "http://www.thisisaphoto.com");

        private Because of =
            () => _result = Browser.PostSecureJson("/Disasters", _createNewDisaster);

        private It should_dispatch_the_expected_command =
            () =>
                Mock.Get(CommandDispatcher)
                    .Verify(
                        x =>
                            x.Dispatch(
                                WithExpected.Object(_createNewDisaster)));

        private It should_be_ok = () => _result.StatusCode.ShouldEqual(HttpStatusCode.OK);

        private static BrowserResponse _result;
    }
}
