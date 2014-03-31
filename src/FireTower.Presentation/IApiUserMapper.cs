using Nancy.Security;

namespace FireTower.Presentation
{
    public interface IApiUserMapper<in T>
    {
        IUserIdentity GetUserFromAccessToken(T token);
    }
}