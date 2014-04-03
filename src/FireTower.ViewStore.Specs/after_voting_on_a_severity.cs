using System;
using System.Linq;
using System.Linq.Expressions;
using BlingBag;
using FireTower.Domain;
using FireTower.Domain.Entities;
using FireTower.Domain.Events;
using FizzWare.NBuilder;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace FireTower.ViewStore.Specs
{
    public class after_voting_on_a_severity
    {
        static IBlingHandler<SeverityVoteAdded> _eventHandler;
        static IViewModelRepository _viewModelRepository;
        static Guid _disasterId;
        static DisasterViewModel _oneDisasterViewModel;
        static SeverityVoteAdded _severityVoteAdded;
        static IReadOnlyRepository _readOnlyRepository;
        static Disaster _disaster;

        Establish context =
            () =>
                {
                    _viewModelRepository = Mock.Of<IViewModelRepository>();
                    _readOnlyRepository = Mock.Of<IReadOnlyRepository>();
                    _eventHandler = new DisasterViewModelSeverityVoteUpdater(_viewModelRepository, _readOnlyRepository);
                    _disasterId = Guid.NewGuid();
                    var userId = Guid.NewGuid();
                    _severityVoteAdded = new SeverityVoteAdded(userId, _disasterId, 3);

                    var user = Builder<User>.CreateNew().With(x => x.Id = userId).Build();
                    _disaster = Builder<Disaster>.CreateNew().With(x => x.Id = _disasterId).Build();

                    _oneDisasterViewModel =
                        Builder<DisasterViewModel>.CreateNew().With(x => x.DisasterId = _disasterId).Build();

                    Mock.Get(_readOnlyRepository).Setup(x => x.GetById<Disaster>(_disasterId)).Returns(_disaster);

                    Mock.Get(_viewModelRepository)
                        .Setup(x => x.FindOne(Moq.It.IsAny<Expression<Func<DisasterViewModel, bool>>>()))
                        .Returns(_oneDisasterViewModel);
                };

        Because of =
            () => _eventHandler.Handle(_severityVoteAdded);

        It should_update_the_disaster_in_the_view_model_store =
            () => Mock.Get(_viewModelRepository)
                      .Verify(x => x.Update(_oneDisasterViewModel));
    }
}