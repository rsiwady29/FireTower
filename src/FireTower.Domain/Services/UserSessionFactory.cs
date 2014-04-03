using System;
using FireTower.Domain.Entities;

namespace FireTower.Domain.Services
{
    public class UserSessionFactory : IUserSessionFactory
    {
        readonly ITimeProvider _timeProvider;
        readonly ITokenExpirationProvider _tokenExpirationProvider;
        readonly ITokenGenerator<Guid> _tokenGenerator;
        readonly IWriteableRepository _writeableRepository;

        public UserSessionFactory(IWriteableRepository writeableRepository,
                                  ITimeProvider timeProvider,
                                  ITokenGenerator<Guid> tokenGenerator,
                                  ITokenExpirationProvider tokenExpirationProvider)
        {
            _writeableRepository = writeableRepository;
            _timeProvider = timeProvider;
            _tokenGenerator = tokenGenerator;
            _tokenExpirationProvider = tokenExpirationProvider;
        }

        #region IUserSessionFactory Members

        public UserSession Create(User user)
        {
            var userSession = new UserSession
                                  {
                                      Id = _tokenGenerator.Generate(),
                                      User = user,
                                      Expires = _tokenExpirationProvider.GetExpiration(_timeProvider.Now())
                                  };

            _writeableRepository.Create(userSession);

            return userSession;
        }

        public void Delete(Guid sessionId)
        {
            _writeableRepository.Delete<UserSession>(sessionId);
        }

        #endregion
    }
}