using System;
using Newtonsoft.Json;

namespace FireTower.Presentation
{
    public class JsonCommandDeserializer : ICommandDeserializer
    {        
        public object Deserialize(string str)
        {
            try
            {
                return JsonConvert.DeserializeObject(str, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
            }
            catch (Exception ex)
            {
                throw new InvalidCommandObjectException(ex);
            }            
        }
    }
}