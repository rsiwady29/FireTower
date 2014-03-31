using System.Collections.Generic;
using Nancy.Security;
using FireTower.Domain.Entities;

namespace FireTower.Infrastructure
{
    public class FireTowerUserIdentity : IUserIdentity
    {
        public User User { get;private set; }

        public FireTowerUserIdentity(User user)
        {
            User = user;
        }

        #region IUserIdentity Members

        public string UserName
        {
            get { return (User ?? new User()).Email; }
        }

        public IEnumerable<string> Claims
        {
            get { return new string[] {}; }
        }

        #endregion
    }
}