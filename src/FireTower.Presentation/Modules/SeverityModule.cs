using FireTower.Domain;
using FireTower.Domain.Commands;
using FireTower.Infrastructure;
using FireTower.Presentation.Requests;
using Nancy;
using Nancy.ModelBinding;

namespace FireTower.Presentation.Modules
{
    public class SeverityModule : NancyModule
    {
        public SeverityModule(ICommandDispatcher commandDispatcher)
        {
            Post["/severity"] = r =>
                {
                    var voteRequest = this.Bind<VoteOnSeverityRequest>();
                    commandDispatcher.Dispatch(this.UserSession(), new VoteOnSeverity(voteRequest.DisasterId, voteRequest.Severity));
                    return null;
                };
        }
    }
}