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
        private static BrowserResponse _result;

        private Establish context = () =>
            {
                _voteOnControlled = new VoteOnControlledRequest
                    {
                        DisasterId = Guid.Empty,
                        IsControlled = true
                    };
            };

        private Because of =
            () => _result = Browser.PostSecureJson("votes/controlled", _voteOnControlled);

        private It should_be_ok = () => _result.StatusCode.ShouldEqual(HttpStatusCode.OK);

        private It should_send_the_command_to_add_vote_if_is_controlled =
            () =>
            Mock.Get(CommandDispatcher)
                .Verify(
                    x =>
                    x.Dispatch(UserSession,
                               WithExpected.Object(new VoteOnControlled(_voteOnControlled.DisasterId,
                                                                        _voteOnControlled.IsControlled))));
    }
}