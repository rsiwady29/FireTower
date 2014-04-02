using System;

namespace FireTower.Domain
{
    public class VisitorSession : IUserSession
    {
        public Guid Id { get; private set; }
    }
}