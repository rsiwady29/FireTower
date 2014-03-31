using System;
using Machine.Specifications;
using Moq;
using FireTower.Domain.Entities;
using FireTower.Domain.Services;
using It = Machine.Specifications.It;

namespace FireTower.Domain.Specs
{
    public class when_persisting_a_new_user_session
    {
        static Guid _token;

        static IUserSessionFactory _userSessionFactory;
        static readonly User User = new User();
        static IWriteableRepository _writeableRepository;
        static ITimeProvider _timeProvider;
        static ITokenExpirationProvider _tokenExpirationProvider;
        static ITokenGenerator<Guid> _tokenGenerator;
        static UserSession _result;
        static DateTime _expires;
        static UserSession _expectedUserSession;

        Establish context =
            () =>
                {
                    _timeProvider = Mock.Of<ITimeProvider>();
                    _writeableRepository = Mock.Of<IWriteableRepository>();
                    _tokenExpirationProvider = Mock.Of<ITokenExpirationProvider>();
                    _tokenGenerator = Mock.Of<ITokenGenerator<Guid>>();
                    _userSessionFactory = new UserSessionFactory(_writeableRepository, _timeProvider, _tokenGenerator,
                                                                 _tokenExpirationProvider);

                    DateTime now = DateTime.Now;
                    Mock.Get(_timeProvider).Setup(x => x.Now()).Returns(now);
                    _expires = now.AddDays(-10);

                    Mock.Get(_tokenExpirationProvider).Setup(x => x.GetExpiration(now)).Returns(_expires);

                    _token = Guid.NewGuid();
                    Mock.Get(_tokenGenerator).Setup(x => x.Generate()).Returns(_token);

                    _expectedUserSession = new UserSession
                                               {
                                                   Id = _token,
                                                   User = User,
                                                   Expires = _expires
                                               };
                };

        Because of =
            () => _result = _userSessionFactory.Create(User);

        It should_return_the_new_user_session = () => _result.ShouldBeLike(_expectedUserSession);

        It should_save_the_expected_session_to_the_repo =
            () =>
            Mock.Get(_writeableRepository)
                .Verify(
                    x =>
                    x.Create(Moq.It.Is<UserSession>(y => y.User == User && y.Id == _token && y.Expires == _expires)));
    }
}