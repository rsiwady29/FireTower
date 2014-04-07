using System;

namespace FireTower.Domain.Events
{
    public class NewImageAddedToDisaster
    {
        public readonly Guid UserId;
        public readonly Guid DisasterId;
        public readonly string ImageUrl;

        public NewImageAddedToDisaster(Guid userId, Guid disasterId, string imageUrl)
        {
            UserId = userId;
            DisasterId = disasterId;
            ImageUrl = imageUrl;            
        }
    }
}