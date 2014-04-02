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
            get
            {
                if (UserSession is UserSession)
                {
                    User user = ((UserSession)UserSession).User;
                    if (user == null)
                    {
                        throw new Exception("The user should not be null on the user session.");
                    }
                    return user.Email;
                }
                return null;
            }
        }

        public IEnumerable<string> Claims
        {
            get { return new string[] { }; }
        }

        #endregion
    }
       
}