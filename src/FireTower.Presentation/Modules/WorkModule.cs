using System;
using System.IO;
using System.IdentityModel;
using FireTower.Domain;
using FireTower.Domain.Exceptions;
using FireTower.Infrastructure;
using Nancy;

namespace FireTower.Presentation.Modules
{
    public class WorkModule : NancyModule
    {
        public WorkModule(ICommandDispatcher dispatcher, ICommandDeserializer deserializer)
        {
            Post["/work"] = r =>
                                {
                                    IUserSession userSession = this.UserSession();

                                    object command;
                                    try
                                    {
                                        var reader = new StreamReader(Request.Body);
                                        string str = reader.ReadToEnd();
                                        command = deserializer.Deserialize(str);
                                    }
                                    catch (InvalidCommandObjectException)
                                    {
                                        throw new BadRequestException("Invalid command object.");
                                    }

                                    try
                                    {
                                        dispatcher.Dispatch(userSession, command);
                                        return null;
                                    }
                                    catch (NoAvailableHandlerException)
                                    {
                                        throw new NotImplementedException("The command does not have a handler.");
                                    }
                                };
        }
    }
}