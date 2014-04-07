using System;
using FireTower.Domain;
using FireTower.Domain.Commands;
using FireTower.Domain.Entities;
using FireTower.Infrastructure;
using FireTower.Presentation.Requests;
using Nancy;
using Nancy.ModelBinding;

namespace FireTower.Presentation.Modules
{
    public class ImageModule : NancyModule
    {
        public ImageModule(ICommandDispatcher commandDispatcher)
        {
            Post["/disasters/{disasterId}/image"] =
                r =>
                    {
                        var req = this.Bind<AddImageRequest>();
                        UserSession userSession = this.UserSession();
                        commandDispatcher.Dispatch(userSession,
                                                   new AddImageToDisaster(Guid.Parse((string) r.disasterId),
                                                                          req.Base64Image));
                        return null;
                    };
        }
    }
}