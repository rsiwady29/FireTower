using DomainDrivenDatabaseDeployer;
using NHibernate;
using FireTower.Domain.Entities;
using FireTower.Domain.Services;

namespace DatabaseDeploymentTool
{
    public class UserSeeder : IDataSeeder
    {
        readonly ISession _session;

        public UserSeeder(ISession session)
        {
            _session = session;
        }

        #region IDataSeeder Members

        public void Seed()
        {
            var encryptor = new HashPasswordEncryptor();

            _session.Save(new User
                              {
                                  Name = "Byron",
                                  Email = "byron@acklenavenue.com",
                                  Activated = true,
                                  EncryptedPassword = encryptor.Encrypt("yardsale").Password,
                              });

            _session.Save(new User
                              {
                                  Name = "Tester",
                                  Email = "test@test.com",
                                  Activated = true,
                                  EncryptedPassword = encryptor.Encrypt("password").Password
                              });
        }

        #endregion
    }
}