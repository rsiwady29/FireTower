using System;
using System.Configuration;
using FireTower.Domain;

namespace FireTower.Presentation
{
    public class TokenExpirationProvider : ITokenExpirationProvider
    {
        public DateTime GetExpiration(DateTime now)
        {
            var expirationDays = Convert.ToInt32(ConfigurationManager.AppSettings["PasswordExpirationDays"]);
            return now.AddDays(expirationDays);
        }
    }
}