using System;
using MongoDB.Bson;

namespace FireTower.ViewStore
{
    public class DisasterViewModel : IViewModel
    {
        public DisasterViewModel(Guid diasterId, DateTime createdDate, string locationDescription, double latitude, double longitude, string firstImageUrl, int firstSeverityVote)
        {
            DiasterId = diasterId;
            CreatedDate = createdDate;
            LocationDescription = locationDescription;
            Location = new[] {longitude, latitude};
            SeverityVotes = new[]{firstSeverityVote};
            Images = new[] {firstImageUrl};
        }

        public Guid DiasterId { get; set; }

        public DateTime CreatedDate { get; set; }
        public string LocationDescription { get; set; }
        public double[] Location { get; set; }
        public int[] SeverityVotes { get; set; }
        public string[] Images { get; set; }



        #region IViewModel Members

        public BsonObjectId Id { get; set; }

        #endregion
    }
}