using FireTower.Domain;
using FireTower.Domain.Commands;
using FireTower.Infrastructure;
using FireTower.Presentation.Requests;
using Nancy;
using Nancy.ModelBinding;

namespace FireTower.Presentation.Modules
{
    public class VoteModule : NancyModule
    {
        public VoteModule(ICommandDispatcher commandDispatcher)
        {
            Post["/isControlled"] = r =>
                {
                    var voteRequest = this.Bind<VoteOnControlledRequest>();
                    commandDispatcher.Dispatch(this.UserSession(), new VoteOnControlled(voteRequest.DisasterId, voteRequest.IsControlled));
                    return null;
                };
        }
    }
}