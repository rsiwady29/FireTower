using System;
using AcklenAvenue.Testing.Moq.ExpectedObjects;
using AcklenAvenue.Testing.Nancy;
using FireTower.Domain.Commands;
using FireTower.Domain.Entities;
using FireTower.Presentation.Requests;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace FireTower.Api.Specs
{
    public class when_voting_on_a_severity : given_a_vote_module 
    {
        static VoteOnSeverityRequest _voteOnSeverity;
        static BrowserResponse _result;

        Establish context =
            () =>
                {
                    _voteOnSeverity = new VoteOnSeverityRequest
                        {
                            DisasterId = Guid.Empty,
                            Severity = 3
                        };
                };

        Because of =
            () => _result = Browser.PostSecureJson("votes/severity", _voteOnSeverity);

        It should_be_ok = () => _result.StatusCode.ShouldEqual(HttpStatusCode.OK);

        It should_update_the_severity =
            () =>
            Mock.Get(CommandDispatcher)
                .Verify(
                    x =>
                    x.Dispatch(UserSession,
                               WithExpected.Object(new VoteOnSeverity(_voteOnSeverity.DisasterId,
                                                                      _voteOnSeverity.Severity))));
    }
}