using System;
using FireTower.Domain.CommandHandlers;
using FireTower.Domain.Commands;
using FireTower.Domain.Entities;
using FireTower.Domain.Events;
using FizzWare.NBuilder;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace FireTower.Domain.Specs
{
    public class when_voting_that_a_disaster_is_resolved
    {
        private static IReadOnlyRepository _readOnlyRepository;
        private static PutOutVoteAdder _commandHandler;
        private static VoteOnPutOut _command;
        private static Disaster _disaster;
        private static object _eventRaised;
        private static Guid _userId;
        private static PutOutVoteAdded _expectedEvent;
        private static bool _isPutOut;

        private Establish context =
            () =>
                {
                    _readOnlyRepository = Mock.Of<IReadOnlyRepository>();

                    _commandHandler = new PutOutVoteAdder(_readOnlyRepository);

                    _userId = Guid.NewGuid();

                    Guid disasterId = Guid.NewGuid();

                    _isPutOut = true;

                    _command = new VoteOnPutOut(disasterId, _isPutOut);

                    _disaster =
                        Builder<Disaster>.CreateNew().With(disaster => disaster.Id, disasterId)
                                         .Build();

                    Mock.Get(_readOnlyRepository).Setup(x => x.GetById<Disaster>(disasterId)).Returns(_disaster);

                    _commandHandler.NotifyObservers += x => _eventRaised = x;
                    _expectedEvent = new PutOutVoteAdded(_userId, disasterId, _isPutOut);
                };

        private Because of =
            () => _commandHandler.Handle(UserSession.New(new User { Id = _userId }), _command);

        private It should_add_the_vote_to_the_disaster =
            () => _disaster.PutOutVotes.ShouldContain(x => x.User.Id == _userId && x.IsPutOut == _isPutOut);

        private It should_handle_the_expected_command_type =
            () => _commandHandler.CommandType.ShouldEqual(typeof(VoteOnPutOut));

        private It should_raise_the_expected_event =
            () => _eventRaised.ShouldBeLike(_expectedEvent);
    }
}