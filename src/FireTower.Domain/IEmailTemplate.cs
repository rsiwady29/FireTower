using System;

namespace FireTower.Domain
{
    public interface IEmailTemplate
    {
        Type ForType { get; }
        string FormattedText { get; }
    }
}