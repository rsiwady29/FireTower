using System;

namespace FireTower.Domain
{
    public interface IEmailSubject
    {
        Type ForType { get; }
        string Text { get; }
    }
}