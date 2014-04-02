using FireTower.Domain;
using FireTower.Domain.Commands;
using FireTower.Domain.Entities;
using FireTower.Infrastructure;
using FireTower.Presentation.Requests;
using Nancy;
using Nancy.ModelBinding;

namespace FireTower.Presentation.Modules
{
    public class NewUserModule : NancyModule
    {
        public NewUserModule(IPasswordEncryptor passwordEncryptor, ICommandDispatcher commandDispatcher,
                             IReadOnlyRepository readOnlyRepository)
        {
            Post["/user"] = r =>
                                {
                                    var newUserRequest = this.Bind<NewUserRequest>();
                                    CheckForExistingUser(readOnlyRepository, newUserRequest);
                                    commandDispatcher.Dispatch(this.VisitorSession(), new NewUserCommand
                                                                                       {
                                                                                           Email = newUserRequest.Email,
                                                                                           EncryptedPassword =
                                                                                               passwordEncryptor.Encrypt
                                                                                               (
                                                                                                   newUserRequest.
                                                                                                       Password),
                                                                                           AgreementVersion =
                                                                                               newUserRequest.
                                                                                               AgreementVersion
                                                                                       });
                                    return new Response().WithStatusCode(HttpStatusCode.OK);
                                };
        }

        static void CheckForExistingUser(IReadOnlyRepository readOnlyRepository, NewUserRequest newUserRequest)
        {
            bool exists = true;
            try
            {
                readOnlyRepository.First<User>(x => x.Email == newUserRequest.Email);
            }
            catch
            {
                exists = false;
            }
            if (exists)
                throw new UserAlreadyExistsException();
        }
    }
}