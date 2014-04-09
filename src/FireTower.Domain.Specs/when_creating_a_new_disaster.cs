using System;
using AcklenAvenue.Testing.Moq.ExpectedObjects;
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
    public class when_creating_a_new_disaster
    {
        static IWriteableRepository _writeableRepository;
        static ICommandHandler _commandHandler;
        static CreateNewDisaster _command;
        static object _eventRaised;
        static NewDisasterCreated _expectedEvent;
        static Disaster _expectedDisaster;

        static ITimeProvider _timeProvider;
        static DateTime _now;
        static readonly User _user = new User(){Id = Guid.NewGuid()};

        Establish context =
            () =>
                {
                    _writeableRepository = Mock.Of<IWriteableRepository>();
                    _timeProvider = Mock.Of<ITimeProvider>();
                    _commandHandler = new NewDisasterCreator(_writeableRepository, _timeProvider);

                    _now = DateTime.Now;
                    Mock.Get(_timeProvider).Setup(x => x.Now()).Returns(_now);

                    _command = new CreateNewDisaster("LocationDescription1", 123.34, 456.32);

                    _expectedDisaster =
                        Builder<Disaster>.CreateNew()
                            .With(x => x.Id, Guid.Empty)
                            .With(disaster => disaster.CreatedDate, _now)
                            .With(disaster => disaster.LocationDescription, _command.LocationDescription)
                            .With(disaster => disaster.Latitude, _command.Latitude)
                            .With(disaster => disaster.Longitude, _command.Longitude)                            
                            .Build();

                    Mock.Get(_writeableRepository).Setup(x => x.Create(WithExpected.Object(_expectedDisaster)))
                        .Returns(_expectedDisaster);

                    _commandHandler.NotifyObservers += x => _eventRaised = x;
                    _expectedEvent = new NewDisasterCreated(_user.Id, _expectedDisaster.Id, _now, _command.LocationDescription,
                                                            _command.Latitude, _command.Longitude);
                };

        Because of =
            () =>
            _commandHandler.Handle(UserSession.New(_user), _command);

        It should_handle_the_expected_command_type =
            () => _commandHandler.CommandType.ShouldEqual(typeof (CreateNewDisaster));

        It should_raise_the_expected_event =
            () => _eventRaised.ShouldBeLike(_expectedEvent);
    }
}