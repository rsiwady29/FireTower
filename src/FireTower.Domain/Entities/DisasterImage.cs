using System;

namespace FireTower.Domain.Entities
{
    public class DisasterImage : IEntity
    {
        protected DisasterImage()
        {
        }

        public DisasterImage(string url)
        {
            Url = url;
        }

        public virtual string Url { get; set; }

        #region IEntity Members

        public virtual Guid Id { get; protected set; }

        #endregion
    }
}