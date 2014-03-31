using System.Configuration;

namespace FireTower.Data
{
    public static class ConnectionStrings
    {
        public static string Get()
        {
            const string local = @"Data Source=.\SQLEXPRESS;Initial Catalog=FireTower;integrated security=true";

            const string remote =
                @"Server=d74a82c5-7b26-4e18-b80e-a2ff01357bd3.sqlserver.sequelizer.com;Database=dbd74a82c57b264e18b80ea2ff01357bd3;User ID=gjcjuajzyluzrjdz;Password=wtRoeis77HjpaPzYpKxMoaHQzjmjCvFZZv2WTHygegynXi3ra7Qe6DPvSbW35hBJ;";

            string environment = (ConfigurationManager.AppSettings["environment"] ?? "").ToLower();
            string connectionStringToUse = local;
            if (environment == "qa")
            {
                connectionStringToUse = remote;
            }

            return connectionStringToUse;
        }
    }
}