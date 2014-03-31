using System;
using System.IO;
using Nancy;
using FireTower.Domain;
using FireTower.Domain.Entities;

namespace FireTower.Presentation.EmailTemplates
{
    public class VerificationEmailTemplate : IEmailTemplate
    {
        readonly string _html;

        public VerificationEmailTemplate(IRootPathProvider rootPathProvider)
        {
            _html = File.ReadAllText(rootPathProvider.GetRootPath() + "/EmailTemplates/verificationEmail.html");
        }
        #region IEmailTemplate Members

        public Type ForType
        {
            get { return typeof (Verification); }
        }

        public string FormattedText
        {
            get { return _html; }
        }

        #endregion
    }
}