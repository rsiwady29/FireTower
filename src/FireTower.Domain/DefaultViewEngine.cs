using System;

namespace FireTower.Domain
{
    public class DefaultViewEngine : IViewEngine
    {
        public string Render<T>(T model, string formattedString)
        {            
            if (model == null) return formattedString;

            var type = model.GetType();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                try
                {
                    var token = "`" + property.Name + "`";
                    var propertyValue = property.GetValue(model, null);
                    if (propertyValue != null)
                        formattedString = formattedString.Replace(token, propertyValue.ToString());
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        string.Format("EmailContentFormatter failed insert model property '{0}' into template.",
                                      property.Name), ex);
                }
            }
            return formattedString;
        }        
    }
}