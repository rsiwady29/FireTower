using System;
using System.Collections.Generic;
using System.Linq;

namespace FireTower.Domain
{
    public class TemplateProvider : ITemplateProvider
    {
        readonly IEnumerable<IEmailTemplate> _templates;

        public TemplateProvider(IEnumerable<IEmailTemplate> templates)
        {
            _templates = templates;
        }

        #region ITemplateProvider Members

        public string GetTemplateFor<T>(T model)
        {
            IEmailTemplate emailTemplate = _templates.FirstOrDefault(x => x.ForType == model.GetType());
            
            if (emailTemplate == null)
                throw new Exception(string.Format("No template available for model type '{0}'.", model.GetType()));

            return emailTemplate.FormattedText;
        }

        #endregion
    }
}