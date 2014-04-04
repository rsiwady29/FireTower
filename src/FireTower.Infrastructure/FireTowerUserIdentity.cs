using System;
using System.Collections.Generic;
using FireTower.Domain;
using Nancy.Security;
using FireTower.Domain.Entities;

namespace FireTower.Infrastructure
{
    public class FireTowerUserIdentity : IUserIdentity
    {
        public FireTowerUserIdentity(IUserSession session)
        {
            UserSession = session;
        }

        public IUserSession UserSession { get; private set; }

        #region IUserIdentity Members

        public string UserName
        {
            //get { return (User ?? new User()).LastName; }
            get { return (new User()).LastName; }
        }

        public IEnumerable<string> Claims
        {
            get { return new string[] { }; }
        }

        #endregion
    }
       
}