using System;
using AcklenAvenue.Testing.Moq.ExpectedObjects;
using AcklenAvenue.Testing.Nancy;
using FireTower.Domain.Commands;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace FireTower.Api.Specs
{
    public class when_voting_if_a_fire_is_put_out : given_a_vote_module
    {
        static BrowserResponse _result;

        Establish context =
            () =>
                {
                    _voteOnPutOut = new VoteOnPutOut{DisasterId = Guid.NewGuid(), IsPutOut = true};
                };

        Because of =
            () => _result = Browser.PostSecureJson("votes/putout", _voteOnPutOut);

        It should_be_ok = () => _result.StatusCode.ShouldEqual(HttpStatusCode.OK);

        It should_send_the_command_to_add_the_vote_of_put_out_fire =
            () =>
            Mock.Get(CommandDispatcher)
                .Verify(
                    x =>
                    x.Dispatch(UserSession,
                               WithExpected.Object(new VoteOnPutOut(_voteOnPutOut.DisasterId,
                                                                    _voteOnPutOut.IsPutOut))));

        private static VoteOnPutOut _voteOnPutOut;
    }
}