using System;
using MongoDB.Bson;

namespace FireTower.ViewStore
{
    public class DisasterViewModel : IViewModel
    {
        public DisasterViewModel(Guid diasterId, double latitude, double longitude, string firstImageUrl)
        {
            DiasterId = diasterId;
            Location = new[] {longitude, latitude};
            Images = new[] {firstImageUrl};
        }

        public Guid DiasterId { get; set; }

        public double[] Location { get; set; }
        public string[] Images { get; set; }

        #region IViewModel Members

        public BsonObjectId Id { get; set; }

        #endregion
    }
}