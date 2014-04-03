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
    public class when_voting_on_a_severity
    {
        const int severity = 3;
        static IReadOnlyRepository _readOnlyRepository;
        static SeverityVoteAdder _commandHandler;
        static VoteOnSeverity _command;
        static Disaster _disaster;
        static object _eventRaised;
        static Guid _userId;
        static SeverityVoteAdded _expectedEvent;

        Establish context =
            () =>
                {
                    _readOnlyRepository = Mock.Of<IReadOnlyRepository>();
                    _commandHandler = new SeverityVoteAdder(_readOnlyRepository);

                    Guid guid = Guid.NewGuid();
                    _userId = Guid.NewGuid();

                    _command = new VoteOnSeverity(guid, severity);

                    _disaster =
                        Builder<Disaster>.CreateNew().With(disaster => disaster.Id, guid)
                                         .Build();

                    Mock.Get(_readOnlyRepository).Setup(x => x.GetById<Disaster>(guid)).Returns(_disaster);

                    _commandHandler.NotifyObservers += x => _eventRaised = x;
                    _expectedEvent = new SeverityVoteAdded(_userId, guid, severity);
                };

        Because of =
            () => _commandHandler.Handle(UserSession.New(new User {Id = _userId}), _command);

        It should_add_the_vote_to_the_disaster =
            () => _disaster.SeverityVotes.ShouldContain(x => x.User.Id == _userId && x.Severity == severity);

        It should_handle_the_expected_command_type =
            () => _commandHandler.CommandType.ShouldEqual(typeof (VoteOnSeverity));

        It should_raise_the_expected_event =
            () => _eventRaised.ShouldBeLike(_expectedEvent);
    }
}