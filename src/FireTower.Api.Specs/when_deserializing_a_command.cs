using AcklenAvenue.Testing.Nancy;
using Machine.Specifications;
using FireTower.Presentation;

namespace FireTower.Api.Specs
{
    public class when_deserializing_a_command
    {
        static string _json;
        static ICommandDeserializer _deserializer;
        static object _result;
        static TestCommand _expectedObject;

        Establish context =
            () =>
                {
                    _json =
                        "{\"$type\":\"AcklenAvenue.Testing.Nancy.TestCommand, AcklenAvenue.Testing.Nancy\",\"Message\":\"it worked\",\"Success\":true,\"Attempts\":1}";

                    _deserializer = new JsonCommandDeserializer();

                    _expectedObject = new TestCommand
                                          {
                                              Message = "it worked",
                                              Attempts = 1,
                                              Success = true
                                          };
                };

        Because of =
            () => _result = _deserializer.Deserialize(_json);

        It should_return_the_expected_command =
            () => _result.ShouldBeLike(_expectedObject);
    }
}