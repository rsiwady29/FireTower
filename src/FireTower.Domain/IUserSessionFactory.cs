using FireTower.Domain.Entities;

namespace FireTower.Domain
{
    public interface IUserSessionFactory
    {
        UserSession Create(User user);
    }
}