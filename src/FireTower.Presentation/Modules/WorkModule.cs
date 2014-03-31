using System.IO;
using Nancy;
using FireTower.Domain;
using FireTower.Domain.Exceptions;

namespace FireTower.Presentation.Modules
{
    public class WorkModule : NancyModule
    {
        public WorkModule(ICommandDispatcher dispatcher, ICommandDeserializer deserializer)
        {
            Post["/work"] = r =>
                                {
                                    object command;
                                    try
                                    {
                                        var reader = new StreamReader(Request.Body);
                                        string str = reader.ReadToEnd();
                                        command = deserializer.Deserialize(str);
                                    }
                                    catch (InvalidCommandObjectException)
                                    {
                                        return new Response().WithStatusCode(HttpStatusCode.BadRequest);
                                    }

                                    try
                                    {
                                        dispatcher.Dispatch(command);
                                        return new Response().WithStatusCode(HttpStatusCode.OK);
                                    }
                                    catch (NoAvailableHandlerException)
                                    {
                                        return new Response().WithStatusCode(HttpStatusCode.NotImplemented);
                                    }
                                };
        }
    }
}