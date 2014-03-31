using System;

namespace FireTower.Domain
{
    public interface IWriteableRepository
    {
        void Delete<T>(Guid itemId) where T : IEntity;
        T Update<T>(T itemToUpdate) where T : IEntity;
        T Create<T>(T itemToCreate) where T : IEntity;
    }
}