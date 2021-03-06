using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace FireTower.ViewStore
{
    public class DisasterViewModel : IViewModel
    {
        public DisasterViewModel()
        {
        }

        public DisasterViewModel(Guid disasterId, DateTime createdDate, string locationDescription, double latitude,
                                 double longitude, int firstSeverityVote)
        {
            DisasterId = disasterId;
            CreatedDate = createdDate;
            LocationDescription = locationDescription;
            Location = new[] {longitude, latitude};
            SeverityVotes = new[] {firstSeverityVote};
            Images = new string[] {};
        }

        public Guid DisasterId { get; set; }

        public DateTime CreatedDate { get; set; }
        public string LocationDescription { get; set; }
        public double[] Location { get; set; }
        public int[] SeverityVotes { get; set; }
        public string[] Images { get; set; }

        #region IViewModel Members

        public BsonObjectId Id { get; set; }

        #endregion

        public void AddImage(string imageUrl)
        {
            var images = new List<string>(Images) {imageUrl};
            Images = images.ToArray();
        }
    }
}