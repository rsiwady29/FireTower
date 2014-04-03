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
using System.Linq;

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

        Establish context =
            () =>
                {
                    _writeableRepository = Mock.Of<IWriteableRepository>();
                    _commandHandler = new NewDisasterCreator(_writeableRepository);

                    _command = new CreateNewDisaster(DateTime.Today.ToLocalTime(), "LocationDescription1", 123.34, 456.32, "http://url.com", 1);

                    _expectedDisaster =
                        Builder<Disaster>.CreateNew()
                            .With(disaster => disaster.CreatedDate, _command.CreatedDate)
                            .With(disaster => disaster.LocationDescription, _command.LocationDescription)
                            .With(disaster => disaster.Latitude, _command.Latitude)
                            .With(disaster => disaster.Longitude, _command.Longitude)
                            .With(disaster => disaster.Id, Guid.Empty)
                            .With(disaster => disaster.SeverityVotes,
                                  Builder<SeverityVote>.CreateListOfSize(1).All().With(severity => severity.Id, Guid.Empty)
                                      .With(s => s.Severity, _command.FirsSeverity)
                                      .Build())
                            .With(disaster => disaster.Images,
                                  Builder<DisasterImage>.CreateListOfSize(1).All().With(image => image.Id, Guid.Empty)
                                      .With(image => image.Url, _command.FirstImageUrl)
                                      .Build())
                            .Build();

                    Mock.Get(_writeableRepository).Setup(x => x.Create(WithExpected.Object(_expectedDisaster)))
                        .Returns(_expectedDisaster);

                    _commandHandler.NotifyObservers += x => _eventRaised = x;
                    _expectedEvent = new NewDisasterCreated(_expectedDisaster.Id, _command.CreatedDate, _command.LocationDescription, _command.Latitude, _command.Longitude, _command.FirstImageUrl, _command.FirsSeverity);
                };

        Because of =
            () =>
            _commandHandler.Handle(UserSession.New(new User()), _command);

        It should_handle_the_expected_command_type =
            () => _commandHandler.CommandType.ShouldEqual(typeof (CreateNewDisaster));

        It should_raise_the_expected_event =
            () => _eventRaised.ShouldBeLike(_expectedEvent);
    }
}