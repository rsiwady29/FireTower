using Machine.Specifications;

namespace FireTower.Domain.Specs
{
    public class when_rendering_a_template
    {
        static IViewEngine _templatingEngine;
        static object _model;
        static string _bodyTemplate;
        static string _expectedBody;
        static string _result;

        Establish context =
            () =>
                {
                    _templatingEngine = new DefaultViewEngine();

                    _model =
                        new
                            {
                                firstName = "Viktor",
                                lastName = "Zavala",
                                emailAddress = "something@somewhere.com"
                            };

                    _bodyTemplate = "`firstName` -- !! @ # $ % `lastName` 1234$$% `emailAddress`";
                    _expectedBody = "Viktor -- !! @ # $ % Zavala 1234$$% something@somewhere.com";
                };

        Because of = () => _result = _templatingEngine.Render(_model, _bodyTemplate);

        It should_have_the_expected_body = () => _result.ShouldEqual(_expectedBody);
    }
}