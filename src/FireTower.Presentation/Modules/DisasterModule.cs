using FireTower.Domain;
using FireTower.Domain.Commands;
using FireTower.Infrastructure;
using Nancy;
using Nancy.ModelBinding;

namespace FireTower.Presentation.Modules
{
    public class DisasterModule : NancyModule
    {
        public DisasterModule(ICommandDispatcher commandDispatcher)
        {
            Post["/Disasters"] =
                Request =>
                    {
                        commandDispatcher.Dispatch(this.UserSession(), this.Bind<CreateNewDisaster>());
                        return new Response().WithStatusCode(HttpStatusCode.OK);
                    };
        }
    }
}