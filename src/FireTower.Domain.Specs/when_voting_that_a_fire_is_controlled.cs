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
    public class when_voting_that_a_fire_is_controlled
    {
        static IReadOnlyRepository _readOnlyRepository;
        static ControlledVoteAdder _commandHandler;
        static VoteOnControlled _command;
        static Disaster _disaster;
        static object _eventRaised;
        static Guid _userId;
        static ControlledVoteAdded _expectedEvent;

        Establish context =
            () =>
                {
                    _readOnlyRepository = Mock.Of<IReadOnlyRepository>();

                    _commandHandler = new ControlledVoteAdder(_readOnlyRepository);

                    _userId = Guid.NewGuid();

                    var disasterId = Guid.NewGuid();

                    _isControlled = true;

                    _command = new VoteOnControlled(disasterId, _isControlled);

                    _disaster =
                        Builder<Disaster>.CreateNew().With(disaster => disaster.Id, disasterId )
                                         .Build();

                    Mock.Get(_readOnlyRepository).Setup(x => x.GetById<Disaster>(disasterId)).Returns(_disaster);

                    _commandHandler.NotifyObservers += x => _eventRaised = x;
                    _expectedEvent = new ControlledVoteAdded(_userId, disasterId, _isControlled);
                };

        Because of =
            () => _commandHandler.Handle(UserSession.New(new User { Id = _userId }), _command);

        It should_add_the_vote_to_the_disaster =
            () => _disaster.ControlledVotes.ShouldContain(x => x.User.Id == _userId && x.IsControlled == _isControlled);

        It should_handle_the_expected_command_type =
            () => _commandHandler.CommandType.ShouldEqual(typeof(VoteOnControlled));

        It should_raise_the_expected_event =
            () => _eventRaised.ShouldBeLike(_expectedEvent);

        private static bool _isControlled;
    }
}