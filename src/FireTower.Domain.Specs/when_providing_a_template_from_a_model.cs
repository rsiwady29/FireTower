using System.Collections.Generic;
using Machine.Specifications;
using Moq;
using FireTower.Domain.Entities;
using It = Machine.Specifications.It;

namespace FireTower.Domain.Specs
{
    public class when_providing_a_template_from_a_model
    {
        const string Template = "template";
        static ITemplateProvider _templateProvider;
        static Verification _model;
        static string _result;

        Establish context =
            () =>
                {
                    var template = Mock.Of<IEmailTemplate>();
                    _templateProvider = new TemplateProvider(new List<IEmailTemplate>
                                                                 {
                                                                     template
                                                                 });

                    Mock.Get(template).Setup(x => x.ForType).Returns(typeof (Verification));

                    Mock.Get(template).Setup(x => x.FormattedText).Returns(Template);

                    _model = new Verification();
                };

        Because of =
            () => _result = _templateProvider.GetTemplateFor(_model);

        It should_return_the_expected_template =
            () => _result.ShouldEqual(Template);
    }
}