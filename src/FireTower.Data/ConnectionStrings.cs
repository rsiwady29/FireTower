using System.Configuration;

namespace FireTower.Data
{
    public static class ConnectionStrings
    {
        public static string Get()
        {
            string environment = (ConfigurationManager.AppSettings["Environment"] ?? "local").ToLower();
            string connectionStringToUse = ConfigurationManager.ConnectionStrings[environment] == null
                                               ? ConfigurationManager.ConnectionStrings["local"].ConnectionString
                                               : ConfigurationManager.ConnectionStrings[environment].ConnectionString;
            return connectionStringToUse;
        }
    }
}