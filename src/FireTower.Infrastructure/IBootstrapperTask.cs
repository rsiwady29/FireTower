using System;

namespace FireTower.Infrastructure
{
    public interface IBootstrapperTask<in T>
    {
        Action<T> Task { get; }
    }
}