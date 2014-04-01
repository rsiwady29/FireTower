using System.Security.Cryptography.X509Certificates;
using FireTower.Domain;
using FireTower.Domain.Commands;
using Nancy;
using Nancy.ModelBinding;

namespace FireTower.Presentation.Modules
{
    public class DisasterModule : NancyModule
    {
        public DisasterModule(ICommandDispatcher commandDispatcher)
        {
            Post["/Disasters"] = Request =>
            {
                commandDispatcher.Dispatch(this.Bind<CreateNewDisaster>()); 
                return null;
            };
        }
    }
}