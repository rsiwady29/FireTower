using AutoMapper;
using Nancy;
using FireTower.Data;
using FireTower.Domain;
using FireTower.Domain.Entities;
using FireTower.Infrastructure;
using FireTower.Presentation.Responses;

namespace FireTower.Presentation.Modules
{
    public class UserModule : NancyModule
    {
        public UserModule(IMappingEngine mappingEngine, IReadOnlyRepository readOnlyRepository)
        {
            Get["/me"] = r => mappingEngine.Map<User, MeResponse>(this.UserSession().User);

            Get["/user/exists"] =
                r =>
                    {
                        bool exists = false;
                        bool activated = false;
                        var email = (string) Request.Query.email;
                        try
                        {
                            var user =
                                readOnlyRepository.First<User>(x => x.Email == email);
                            exists = true;
                            activated = user.Activated;
                        }
                        catch (ItemNotFoundException<User>)
                        {
                        }
                        return new UserExistenceResponse
                                   {
                                       Exists = exists,
                                       Activated = activated,
                                   };
                    };
        }
    }
}