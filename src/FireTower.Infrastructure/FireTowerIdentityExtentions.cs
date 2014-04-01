using FireTower.Domain;
using Nancy;
using FireTower.Domain.Entities;

namespace FireTower.Infrastructure
{
    public static class FireTowerIdentityExtentions
    {
        public static UserSession UserSession(this NancyModule module)
        {
            var identity = module.Context.CurrentUser as FireTowerUserIdentity;
            if (identity == null) throw new NoFireTowerUserException();
            return (UserSession)identity.UserSession;
        }

        public static VisitorSession VisitorSession(this NancyModule module)
        {
            var identity = module.Context.CurrentUser as FireTowerUserIdentity;
            if (identity == null) throw new NoFireTowerUserException();
            return (VisitorSession)identity.UserSession;
        }
    }
}