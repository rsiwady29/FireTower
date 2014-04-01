using AcklenAvenue.Testing.Nancy;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace FireTower.Api.Specs.Worker
{
    public class when_receiving_a_command : given_a_work_module_context
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
                };

        Because of =
            () => _result = Browser.PostSecureJson("/work", _command);

        It should_process_the_work_item =
            () => Mock.Get(CommandDispatcher).Verify(x => x.Dispatch(UserSession, _command));


        It should_return_a_successful_response =
            () => _result.StatusCode.ShouldEqual(HttpStatusCode.OK);
    }
}