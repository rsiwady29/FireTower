using System;

namespace FireTower.Domain
{
    public interface ITimeProvider
    {
        DateTime Now();
    }
}