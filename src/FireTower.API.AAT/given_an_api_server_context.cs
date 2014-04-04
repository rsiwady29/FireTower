using System;
using AcklenAvenue.Testing.AAT;
using FireTower.Presentation.Requests;
using FireTower.Presentation.Responses;
using Machine.Specifications;
using RestSharp;

namespace FireTower.API.AAT
{
    public class given_an_api_server_context<T> where T : IAATState
    {
        protected static AATClient Client;

        Establish master_context =
            () => { Client = new AATClient(GetHost()); };

        static string GetHost()
        {
            if (typeof (T) == typeof (DeployedToQA))
            {
                return "https://firetowerapidev.apphb.com";
            }
            if (typeof (T) == typeof (DeployedToQaWorker))
            {
                throw new NotImplementedException("Please add thr url for the qa worker server.");
            }
            else
            {
                return "http://localhost:38397/";
            }
        }

        protected static SuccessfulLoginResponse<Guid> Login()
        {
            IRestResponse<SuccessfulLoginResponse<Guid>> restResponse =
                Client.Execute<SuccessfulLoginResponse<Guid>>("/login", Method.POST,
                                                              new LoginRequest
                                                              {
                                                                  FacebookId = 123456
                                                              });
            return restResponse.Data;
        }

        protected static void RegisterUser()
        {
                Client.Execute("/user", Method.POST,
                                                               new NewUserRequest
                                                                  {
                                                                      FirstName = "Byron",
                                                                      LastName = "Sommardahl",
                                                                      Name = "Byron Sommardahl",
                                                                      FacebookId = 123456,
                                                                      Locale = "es_ES",
                                                                      Username = "bsommardahl",
                                                                      Verified = true
                                                                  });
        }

        
    }
}