using System;

namespace FireTower.Domain.Entities
{
    public class Location: IEntity
    {
        public virtual long LocationId { get; set; }
        public virtual string LocationName { get; set; }

        #region IEntity Members

        public virtual Guid Id { get; set; }

        #endregion
    }
}