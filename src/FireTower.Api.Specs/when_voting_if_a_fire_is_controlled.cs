using System;
using AcklenAvenue.Testing.Moq.ExpectedObjects;
using AcklenAvenue.Testing.Nancy;
using FireTower.Domain.Commands;
using FireTower.Presentation.Requests;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace FireTower.Api.Specs
{
    public class when_voting_if_a_fire_is_controlled : given_a_vote_module
    {
        private static VoteOnControlledRequest _voteOnControlled;

        private Establish context = () =>
            {
                _voteOnControlled = new VoteOnControlledRequest
                    {
                        DisasterId = Guid.Empty,
                        IsControlled = true
                    };
            };

        Because of =
            () => _result = Browser.PostSecureJson("/isControlled", _voteOnControlled);

        It should_be_ok = () => _result.StatusCode.ShouldEqual(HttpStatusCode.OK);

        It should_update_the_severity =
            () =>
            Mock.Get(CommandDispatcher)
                .Verify(
                    x =>
                    x.Dispatch(UserSession,
                               WithExpected.Object(new VoteOnControlled(_voteOnControlled.DisasterId,
                                                                        _voteOnControlled.IsControlled))));

        private static BrowserResponse _result;
    }
}