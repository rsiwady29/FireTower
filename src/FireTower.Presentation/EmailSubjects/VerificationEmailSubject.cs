using System;
using FireTower.Domain;
using FireTower.Domain.Entities;

namespace FireTower.Presentation.EmailSubjects
{
    public class VerificationEmailSubject : IEmailSubject
    {
        #region IEmailTemplate Members

        public Type ForType
        {
            get { return typeof(Verification); }
        }

        public string Text { get { return "FireTower Account Verification"; } }

        #endregion
    }
}