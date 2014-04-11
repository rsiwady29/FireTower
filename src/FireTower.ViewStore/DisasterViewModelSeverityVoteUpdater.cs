using System;
using System.Linq;
using BlingBag;
using FireTower.Domain;
using FireTower.Domain.Entities;
using FireTower.Domain.Events;

namespace FireTower.ViewStore
{
    public class DisasterViewModelSeverityVoteUpdater : IBlingHandler<SeverityVoteAdded>
    {
        readonly IReadOnlyRepository _readOnlyRepository;
        readonly IViewModelRepository _viewModelRepository;

        public DisasterViewModelSeverityVoteUpdater(IViewModelRepository viewModelRepository,
                                                    IReadOnlyRepository readOnlyRepository)
        {
            _viewModelRepository = viewModelRepository;
            _readOnlyRepository = readOnlyRepository;
        }

        #region IBlingHandler<SeverityVoteAdded> Members

        public void Handle(SeverityVoteAdded @event)
        {
            var disasterModel = _viewModelRepository.FindOne<DisasterViewModel>(x => @event.DisasterId.ToString() == x.DisasterId);
            var disaster = _readOnlyRepository.GetById<Disaster>(@event.DisasterId);
            disasterModel.SeverityVotes = disaster.SeverityVotes.Select(x => x.Severity).ToArray();
            _viewModelRepository.Update(disasterModel);
        }

        #endregion
    }
}