using System.Collections.Generic;
using Machine.Specifications;
using Moq;
using FireTower.Domain.Entities;
using It = Machine.Specifications.It;

namespace FireTower.Domain.Specs
{
    public class when_getting_a_subject_for_a_model
    {
        const string Subject = "subject";
        static IEmailSubjectProvider _subjectProvider;
        static Verification _model;
        static string _result;
        static IEmailSubject _subject;

        Establish context =
            () =>
                {
                    _subject = Mock.Of<IEmailSubject>();
                    _subjectProvider = new EmailSubjectProvider(new List<IEmailSubject> {_subject});

                    _model = new Verification();

                    Mock.Get(_subject).Setup(x => x.ForType).Returns(_model.GetType());
                    Mock.Get(_subject).Setup(x => x.Text).Returns(Subject);                    
                };

        Because of =
            () => _result = _subjectProvider.GetSubjectFor(_model);

        It should_return_the_expected_subject =
            () => _result.ShouldEqual(Subject);
    }
}