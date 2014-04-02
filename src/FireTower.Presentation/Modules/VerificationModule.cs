using FireTower.Infrastructure;
using Nancy;
using Nancy.ModelBinding;
using FireTower.Data;
using FireTower.Domain;
using FireTower.Domain.Commands;
using FireTower.Domain.Entities;
using FireTower.Presentation.Requests;

namespace FireTower.Presentation.Modules
{
    public class VerificationModule : NancyModule
    {
        public VerificationModule(IReadOnlyRepository readOnlyRepository, ICommandDispatcher commandDispatcher)
        {
            Post["/verify"] = r =>
                                  {
                                      var input = this.Bind<VerifyAccountRequest>();
                                      try {
                                          readOnlyRepository.First<Verification>(
                                              x => x.EmailAddress == input.Email && x.VerificationCode == input.Code);
                                      }catch(ItemNotFoundException<Verification>)
                                      {
                                          return new Response().WithStatusCode(HttpStatusCode.Unauthorized);
                                      }
                                      commandDispatcher.Dispatch(this.VisitorSession(), new ActivateUser(input.Email));
                                      return new Response().WithStatusCode(HttpStatusCode.OK);
                                  };
        }
    }
}