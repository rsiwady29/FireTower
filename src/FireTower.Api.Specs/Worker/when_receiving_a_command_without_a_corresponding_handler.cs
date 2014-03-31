using AcklenAvenue.Testing.Nancy;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using FireTower.Domain.Exceptions;
using It = Machine.Specifications.It;

namespace FireTower.Api.Specs.Worker
{
    public class when_receiving_a_command_without_a_corresponding_handler : given_a_work_module_context
    {
        static BrowserResponse _result;
        static TestCommand _command;

        Establish context =
            () =>
                {
                    _command = new TestCommand
                                   {
                                       Message = "this is a test",
                                       Attempts = 1,
                                       Success = true
                                   };

                    Mock.Get(CommandDeserializer).Setup(x => x.Deserialize(Moq.It.IsAny<string>())).Returns(_command);

                    Mock.Get(CommandDispatcher).Setup(x => x.Dispatch(_command)).Throws(
                        new NoAvailableHandlerException(typeof (TestCommand)));
                };

        Because of =
            () => _result = Browser.PostSecureJson("/work", _command);

        It should_return_an_unsuccessful_response =
            () => _result.StatusCode.ShouldEqual(HttpStatusCode.NotImplemented);
    }
}