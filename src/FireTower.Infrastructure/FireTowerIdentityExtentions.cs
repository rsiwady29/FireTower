using Nancy;
using FireTower.Domain.Entities;

namespace FireTower.Infrastructure
{
    public static class FireTowerIdentityExtentions
    {
        public static User FireTowerUser(this NancyModule module)
        {
            var identity = module.Context.CurrentUser as FireTowerUserIdentity;
            if (identity == null) throw new NoFireTowerUserException();
            return identity.User;
        }
    }
}