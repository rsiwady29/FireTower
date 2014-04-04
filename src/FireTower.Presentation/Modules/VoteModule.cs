using System;
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
            Post["votes/controlled"] = r =>
                {
                    var voteRequest = this.Bind<VoteOnControlledRequest>();
                    commandDispatcher.Dispatch(this.UserSession(), new VoteOnControlled(voteRequest.DisasterId, voteRequest.IsControlled));
                    return null;
                };
            Post["votes/severity"] = r =>
            {
                var voteRequest = this.Bind<VoteOnSeverityRequest>();
                commandDispatcher.Dispatch(this.UserSession(), new VoteOnSeverity(voteRequest.DisasterId, voteRequest.Severity));
                return null;
            };
            Post["votes/putout"] = r =>
                {
                    var voteRequest = this.Bind<VoteOnPutOutRequest>();
                    commandDispatcher.Dispatch(this.UserSession(), new VoteOnPutOut(voteRequest.DisasterId, voteRequest.IsPutOut));
                    return null;
                };
        }
    }

    public class VoteOnPutOutRequest
    {
        public bool IsPutOut { get; set; }

        public Guid DisasterId { get; set; }
    }
}