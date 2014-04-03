using System;
using System.Collections.Generic;

namespace FireTower.Domain.Entities
{
    public class Disaster : IEntity
    {
        IEnumerable<DisasterImage> _images = new List<DisasterImage>();
        IEnumerable<SeverityVote> _severityVotes = new List<SeverityVote>();

        protected Disaster()
        {
        }

        public Disaster(DateTime createdDate, string locationDescription, double latitude, double longitude)
        {
            CreatedDate = createdDate;
            LocationDescription = locationDescription;
            Latitude = latitude;
            Longitude = longitude;
        }

        public virtual DateTime CreatedDate { get; set; }

        public virtual string LocationDescription { get; set; }

        public virtual double Latitude { get; set; }

        public virtual double Longitude { get; set; }

        public virtual IEnumerable<SeverityVote> SeverityVotes
        {
            get { return _severityVotes; }
            protected set { _severityVotes = value; }
        }

        public virtual IEnumerable<DisasterImage> Images
        {
            get { return _images; }
            protected set { _images = value; }
        }

        #region IEntity Members

        public virtual Guid Id { get; set; }

        #endregion

        public virtual DisasterImage AddImage(string imageUrl)
        {
            var disasterImage = new DisasterImage(imageUrl);
            ((IList<DisasterImage>) Images).Add(disasterImage);

            return disasterImage;
        }

        public virtual SeverityVote AddSeverityVote(User user, int severity)
        {
            var severityVote = new SeverityVote(user, severity);
            ((IList<SeverityVote>) SeverityVotes).Add(severityVote);

            return severityVote;
        }
    }
}