using System;
using Nancy;
using Nancy.ModelBinding;
using FireTower.Data;
using FireTower.Domain;
using FireTower.Domain.Entities;
using FireTower.Presentation.Requests;
using FireTower.Presentation.Responses;

namespace FireTower.Presentation.Modules
{
    public class LoginModule : NancyModule
    {
        public LoginModule(
            IReadOnlyRepository readOnlyRepository, 
            IPasswordEncryptor passwordEncryptor,
            IUserSessionFactory userSessionFactory)
        {
            
            Post["/login"] =
                r =>
                    {
                        var loginInfo = this.Bind<LoginRequest>();

                        EncryptedPassword encryptedPassword = passwordEncryptor.Encrypt(loginInfo.Password);

                        try
                        {
                            var user =
                            readOnlyRepository.First<User>(
                                x => x.Email == loginInfo.Email && x.EncryptedPassword == encryptedPassword.Password);

                            if (!user.Activated) return new Response().WithStatusCode(HttpStatusCode.Forbidden);

                            var userSession = userSessionFactory.Create(user);

                            return new SuccessfulLoginResponse<Guid>(userSession.Id, userSession.Expires);
                        }
                        catch (ItemNotFoundException<User> ex)
                        {
                            return new Response().WithStatusCode(HttpStatusCode.Unauthorized);
                        }
                    };
        }
    }
}