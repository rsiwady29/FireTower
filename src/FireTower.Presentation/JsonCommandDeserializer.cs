using System;
using Newtonsoft.Json;

namespace FireTower.Presentation
{
    public class JsonCommandDeserializer : ICommandDeserializer
    {
        #region ICommandDeserializer Members

        public object Deserialize(string str)
        {
            if (str == null) throw new ArgumentNullException("str");

            try
            {
                var objWithType = JsonConvert.DeserializeObject<JsonObjectWithType>(str);
                Type type = Type.GetType(objWithType.Type);
                if (type == null) throw new Exception("Type name was not valid.");
                return JsonConvert.DeserializeObject(str, type);
            }
            catch (Exception ex)
            {
                throw new InvalidCommandObjectException(ex);
            }
        }

        #endregion

        #region Nested type: JsonObjectWithType

        class JsonObjectWithType
        {
            public string Type { get; set; }
        }

        #endregion
    }
}