using System;
using NHibernate;
using FireTower.Domain;

namespace FireTower.Data
{
    public class WriteableRepository : IWriteableRepository
    {
        readonly ISession _session;

        public WriteableRepository(ISession session)
        {
            _session = session;
        }

        public T Create<T>(T itemToCreate) where T : IEntity
        {
            ISession session = _session;
            session.Save(itemToCreate);
            return itemToCreate;
        }

        public T Update<T>(T itemToUpdate) where T : IEntity
        {
            var session = _session;
            session.Update(itemToUpdate);
            return itemToUpdate;
        }

        public void Delete<T>(Guid itemId) where T : IEntity
        {
            var session = _session;
            var itemToDelete = session.Get<T>(itemId);
            session.Delete(itemToDelete);
        }

        
    }
}