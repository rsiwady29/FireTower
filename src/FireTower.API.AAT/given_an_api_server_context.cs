using System;
using AcklenAvenue.Testing.AAT;
using Machine.Specifications;

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
                throw new NotImplementedException("Please add thr url for the qa server.");
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
    }
}