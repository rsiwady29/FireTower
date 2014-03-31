using System;
using Nancy.Security;
using FireTower.Data;
using FireTower.Domain;
using FireTower.Domain.Entities;
using FireTower.Domain.Exceptions;
using FireTower.Infrastructure;

namespace FireTower.Presentation
{
    public class ApiUserMapper : IApiUserMapper<Guid>
    {
        readonly IReadOnlyRepository _readOnlyRepo;
        readonly ITimeProvider _timeProvider;

        public ApiUserMapper(IReadOnlyRepository readOnlyRepo, ITimeProvider timeProvider)
        {
            _readOnlyRepo = readOnlyRepo;
            _timeProvider = timeProvider;
        }

        public IUserIdentity GetUserFromAccessToken(Guid token)
        {
            var userSession = GetUserSessionFromToken(token);
            MakeSureTokenHasntExpiredYet(userSession);
            return new FireTowerUserIdentity(userSession.User);
        }

        UserSession GetUserSessionFromToken(Guid token)
        {
            UserSession userSession;
            try
            {
                userSession = _readOnlyRepo.First<UserSession>(x => x.Id == token);
            }
            catch (ItemNotFoundException<UserSession> e)
            {
                throw new TokenDoesNotExistException();
            }
            return userSession;
        }

        void MakeSureTokenHasntExpiredYet(UserSession userSession)
        {
            DateTime expires = userSession.Expires;
            DateTime now = _timeProvider.Now();
            if (expires < now)
            {
                throw new TokenExpiredException();
            }
        }
    }
}