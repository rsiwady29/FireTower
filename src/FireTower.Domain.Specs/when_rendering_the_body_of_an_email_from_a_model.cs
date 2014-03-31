using Machine.Specifications;
using Moq;
using FireTower.Domain.Entities;
using It = Machine.Specifications.It;

namespace FireTower.Domain.Specs
{
    public class when_rendering_the_body_of_an_email_from_a_model
    {
        const string Template = "template";
        const string RenderedHtml = "rendered html";
        static IEmailBodyRenderer _renderer;
        static Verification _model;
        static string _result;
        static ITemplateProvider _templateProvider;
        static IViewEngine _viewEngine;

        Establish context =
            () =>
                {
                    _templateProvider = Mock.Of<ITemplateProvider>();
                    _viewEngine = Mock.Of<IViewEngine>();
                    _renderer = new EmailBodyRenderer(_templateProvider, _viewEngine);

                    _model = new Verification {};

                    Mock.Get(_templateProvider).Setup(x => x.GetTemplateFor(_model)).Returns(Template);

                    Mock.Get(_viewEngine).Setup(x => x.Render(_model, Template)).Returns(RenderedHtml);
                };

        Because of =
            () => _result = _renderer.Render(_model);

        It should_return_the_expected_string =
            () => _result.ShouldEqual(RenderedHtml);
    }
}