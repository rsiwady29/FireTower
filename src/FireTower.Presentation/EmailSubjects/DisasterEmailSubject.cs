using System;
using FireTower.Domain;
using FireTower.Domain.Entities;

namespace FireTower.Presentation.EmailSubjects
{
    public class DisasterEmailSubject : IEmailSubject
    {
        #region IEmailTemplate Members

        public Type ForType
        {
            get { return typeof(Disaster); }
        }

        public string Text { get { return "FireTower disaster notification"; } }

        #endregion
    }
}