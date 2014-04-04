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
            Post["/user/facebook"] = r =>
                                         {
                                             var newUserRequest = this.Bind<NewUserRequest>();
                                             CheckForExistingFacebookUser(readOnlyRepository, newUserRequest);
                                             DispatchCommand(commandDispatcher, newUserRequest);
                                             return new Response().WithStatusCode(HttpStatusCode.OK);
                                         };

            Post["/user"] = r =>
                                {
                                    var newUserRequest = this.Bind<NewUserRequest>();
                                    CheckForExistingUser(readOnlyRepository, newUserRequest);
                                    commandDispatcher.Dispatch(this.VisitorSession(), new NewUserCommand
                                                                                          {
                                                                                              Email =
                                                                                                  newUserRequest.Email,
                                                                                              EncryptedPassword =
                                                                                                  passwordEncryptor.
                                                                                                  Encrypt(
                                                                                                      newUserRequest.
                                                                                                          Password)
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

        static void DispatchCommand(ICommandDispatcher commandDispatcher,
                                    NewUserRequest newUserRequest)
        {
            commandDispatcher.Dispatch(new UserSession(), new NewUserCommand
                                                              {
                                                                  FirstName = newUserRequest.FirstName,
                                                                  LastName = newUserRequest.LastName,
                                                                  Name = newUserRequest.Name,
                                                                  FacebookId = newUserRequest.FacebookId,
                                                                  Locale = newUserRequest.Locale,
                                                                  Username = newUserRequest.Username,
                                                              });
        }

        static void CheckForExistingFacebookUser(IReadOnlyRepository readOnlyRepository, NewUserRequest newUserRequest)
        {
            bool exists = true;
            try
            {
                readOnlyRepository.First<User>(x => x.FacebookId == newUserRequest.FacebookId);
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