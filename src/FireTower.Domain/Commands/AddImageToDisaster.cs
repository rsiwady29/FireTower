using System;

namespace FireTower.Domain.Commands
{
    public class AddImageToDisaster
    {
        public readonly Guid DisasterId;
        public readonly string Base64ImageString;

        public AddImageToDisaster(Guid disasterId, string base64ImageString)
        {
            DisasterId = disasterId;
            Base64ImageString = base64ImageString;
        }
    }
}