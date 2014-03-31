using Nancy;
using Nancy.ModelBinding;
using FireTower.Domain;
using FireTower.Domain.Commands;
using FireTower.Domain.Entities;
using FireTower.Presentation.Requests;

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
                                    DispatchCommand(passwordEncryptor, commandDispatcher, newUserRequest);
                                    return new Response().WithStatusCode(HttpStatusCode.OK);
                                };
        }

        static void DispatchCommand(IPasswordEncryptor passwordEncryptor, ICommandDispatcher commandDispatcher,
                                    NewUserRequest newUserRequest)
        {
            commandDispatcher.Dispatch(new NewUserCommand
                                           {
                                               Email = newUserRequest.Email,
                                               EncryptedPassword =
                                                   passwordEncryptor.Encrypt(
                                                       newUserRequest.Password),
                                               AgreementVersion =
                                                   newUserRequest.AgreementVersion
                                           });
        }

        static void CheckForExistingUser(IReadOnlyRepository readOnlyRepository, NewUserRequest newUserRequest)
        {
            var exists = true;
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