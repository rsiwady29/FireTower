using System;
using System.IO;
using FireTower.Domain;
using FireTower.Domain.Entities;
using Nancy;

namespace FireTower.Presentation.EmailTemplates
{
    public class DisasterEmailTemplate : IEmailTemplate
    {
        readonly string _html;

        public DisasterEmailTemplate(IRootPathProvider rootPathProvider)
        {
            _html = File.ReadAllText(rootPathProvider.GetRootPath() + "/EmailTemplates/disasterTemplate.html");
        }
        #region IEmailTemplate Members

        public Type ForType
        {
            get { return typeof (Disaster); }
        }

        public string FormattedText
        {
            get { return _html; }
        }

        #endregion
    }
}