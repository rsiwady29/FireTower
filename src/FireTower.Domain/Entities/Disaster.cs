using System;
using System.Collections.Generic;

namespace FireTower.Domain.Entities
{
    public class Disaster : IEntity
    {
        IEnumerable<DisasterImage> _images = new List<DisasterImage>();
        IEnumerable<DisasterSeverity> _severities = new List<DisasterSeverity>();

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

        public virtual IEnumerable<DisasterImage> Images
        {
            get { return _images; }
            protected set { _images = value; }
        }

        public virtual IEnumerable<DisasterSeverity> Severities
        {
            get { return _severities; }
            protected set { _severities = value; }
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

        public virtual DisasterSeverity AddSeverity(int severity)
        {
            var disasterSeverity = new DisasterSeverity(severity);
            ((IList<DisasterSeverity>)Severities).Add(disasterSeverity);

            return disasterSeverity;
        }
    }
}