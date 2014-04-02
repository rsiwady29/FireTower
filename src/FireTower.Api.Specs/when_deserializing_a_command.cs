using System;
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
                    "{\"Message\":\"it worked\",\"Success\":true,\"Attempts\":1,\"Type\":\"AcklenAvenue.Testing.Nancy.TestCommand, AcklenAvenue.Testing.Nancy\"}";

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

    public class when_deserializing_a_command_with_missing_type_property
    {
        static string _invalidJson;
        static ICommandDeserializer _deserializer;
        static Exception _exception;

        Establish context =
            () =>
            {
                _invalidJson =
                    "{\"Message\":\"it worked\",\"Success\":true,\"Attempts\":1}";

                _deserializer = new JsonCommandDeserializer();
            };

        Because of =
            () => _exception = Catch.Exception(() => _deserializer.Deserialize(_invalidJson));

        It should_throw_the_expected_exception =
            () => _exception.ShouldBeOfExactType<InvalidCommandObjectException>();
    }

    public class when_deserializing_a_command_with_invalid_type
    {
        static string _invalidJson;
        static ICommandDeserializer _deserializer;
        static Exception _exception;

        Establish context =
            () =>
            {
                _invalidJson =
                    "{\"Type\":\"Something Invalid\",\"Message\":\"it worked\",\"Success\":true,\"Attempts\":1}";

                _deserializer = new JsonCommandDeserializer();
            };

        Because of =
            () => _exception = Catch.Exception(() => _deserializer.Deserialize(_invalidJson));

        It should_throw_the_expected_exception =
            () => _exception.ShouldBeOfExactType<InvalidCommandObjectException>();
    }

    public class when_deserializing_a_command_with_invalid_json
    {
        static string _invalidJson;
        static ICommandDeserializer _deserializer;
        static Exception _exception;

        Establish context =
            () =>
            {
                _invalidJson =
                    "{\"Type\":\"Invio.NancySpecs.TestCommand, Invio.NancySpecs\",-,\"Message\":\"it worked\",\"Success\":true,\"Attempts\":1}";

                _deserializer = new JsonCommandDeserializer();
            };

        Because of =
            () => _exception = Catch.Exception(() => _deserializer.Deserialize(_invalidJson));

        It should_throw_the_expected_exception =
            () => _exception.ShouldBeOfExactType<InvalidCommandObjectException>();
    }
}