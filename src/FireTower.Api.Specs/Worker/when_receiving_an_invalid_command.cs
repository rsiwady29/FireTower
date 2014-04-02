using System;
using System.IdentityModel;
using AcklenAvenue.Testing.Nancy;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using FireTower.Presentation;
using It = Machine.Specifications.It;

namespace FireTower.Api.Specs.Worker
{
    public class when_receiving_an_invalid_command : given_a_work_module_context
    {
        static BrowserResponse _result;
        static TestCommand _command;
        static Exception _exception;

        Establish context =
            () =>
            {
                _command = new TestCommand
                {
                    Message = "this is a test",
                    Attempts = 1,
                    Success = true
                };

                Mock.Get(CommandDeserializer).Setup(x => x.Deserialize(Moq.It.IsAny<string>())).Throws(
                    new InvalidCommandObjectException(new Exception()));
            };

        Because of =
            () => _exception = Catch.Exception(() => Browser.PostSecureJson("/work", _command));

        It should_return_a_bad_request_response =
            () => _exception.InnerException.InnerException.ShouldBeOfExactType<BadRequestException>();
    }
}