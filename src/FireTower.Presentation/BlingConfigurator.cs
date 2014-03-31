using System;
using System.Reflection;
using BlingBag;
using FireTower.Domain;

namespace FireTower.Presentation
{
    public class BlingConfigurator : IBlingConfigurator<DomainEvent>
    {
        readonly IBlingDispatcher _dispatcher;

        public BlingConfigurator(IBlingDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        #region IBlingConfigurator<DomainEvent> Members

        public Func<EventInfo, bool> EventSelector
        {
            get
            {
                return x => x.EventHandlerType == typeof(DomainEvent);
            }
        }

        public DomainEvent HandleEvent
        {
            get
            {
                return x => _dispatcher.Dispatch(x);
            }
        }

        #endregion
    }
}