using System;

namespace FireTower.Domain.Services
{
    public class GuidTokenGenerator : ITokenGenerator<Guid>
    {
        public Guid Generate()
        {
            return Guid.NewGuid();
        }
    }
}