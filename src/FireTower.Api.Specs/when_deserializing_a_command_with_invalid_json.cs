using System;
using Machine.Specifications;
using FireTower.Presentation;

namespace FireTower.Api.Specs
{
    public class when_deserializing_a_command_with_invalid_json
    {
        static string _invalidJson;
        static ICommandDeserializer _deserializer;
        static Exception _exception;

        Establish context =
            () =>
                {
                    _invalidJson =
                        "{\"$type\":\"AcklenAvenue.Testing.Nancy.TestCommand2, AcklenAvenue.Testing.Nancy\",\"Message\":\"it worked\",\"Success\":true,\"Attempts\":1}";

                    _deserializer = new JsonCommandDeserializer();
                };

        Because of =
            () => _exception = Catch.Exception(() => _deserializer.Deserialize(_invalidJson));

        It should_throw_the_expected_exception =
            () => _exception.ShouldBeOfExactType<InvalidCommandObjectException>();
    }
}