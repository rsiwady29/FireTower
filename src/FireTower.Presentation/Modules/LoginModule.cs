using System;
using FireTower.Domain.Commands;
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
            IUserSessionFactory userSessionFactory)
        {
            
            Post["/login"] =
                r =>
                    {
                        var loginInfo = this.Bind<LoginRequest>();
                        try
                        {
                            var user =
                            readOnlyRepository.First<User>(x => x.FacebookId == loginInfo.FacebookId);

                            if (!user.Verified) return new Response().WithStatusCode(HttpStatusCode.Forbidden);

                            var userSession = userSessionFactory.Create(user);

                            return new SuccessfulLoginResponse<Guid>(userSession.Id, userSession.Expires);
                        }
                        catch (ItemNotFoundException<User> ex)
                        {
                            return new Response().WithStatusCode(HttpStatusCode.Unauthorized);
                        }
                    };
            Post["/logout"] =
                r =>
                {
                    var loginInfo = this.Bind<LoginRequest>();
                    try
                    {
                        var session = readOnlyRepository.First<UserSession>(x => x.User.FacebookId == loginInfo.FacebookId);

                        userSessionFactory.Delete(session.Id);

                        return new Response().WithStatusCode(HttpStatusCode.OK);
                    }
                    catch (ItemNotFoundException<UserSession> ex)
                    {
                        return new Response().WithStatusCode(HttpStatusCode.Unauthorized);
                    }
                };
        }

    }
}