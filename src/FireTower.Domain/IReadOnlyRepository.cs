using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FireTower.Domain
{
    public interface IReadOnlyRepository
    {
        T First<T>(Expression<Func<T, bool>> query) where T : IEntity;
        T GetById<T>(Guid id) where T : IEntity;
        IEnumerable<T> GetAll<T>() where T : IEntity;
        IQueryable<T> Query<T>(Expression<Func<T, bool>> expression) where T : IEntity;
    }
}