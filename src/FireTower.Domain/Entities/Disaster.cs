using System;
using System.Collections.Generic;

namespace FireTower.Domain.Entities
{
    public class Disaster : IEntity
    {
        IEnumerable<DisasterImage> _images = new List<DisasterImage>();

        protected Disaster()
        {
        }

        public Disaster(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public virtual double Latitude { get; set; }

        public virtual double Longitude { get; set; }

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
    }
}