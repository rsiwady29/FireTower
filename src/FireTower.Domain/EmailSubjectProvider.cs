using System;
using System.Collections.Generic;
using System.Linq;

namespace FireTower.Domain
{
    public class EmailSubjectProvider : IEmailSubjectProvider
    {
        readonly IEnumerable<IEmailSubject> _emailSubjects;

        public EmailSubjectProvider(IEnumerable<IEmailSubject> emailSubjects)
        {
            _emailSubjects = emailSubjects;
        }

        public string GetSubjectFor<T>(T model)
        {
            IEmailSubject subject = _emailSubjects.FirstOrDefault(x => x.ForType == model.GetType());
            
            if (subject == null)
                throw new Exception(string.Format("No email subject exists for model type '{0}'.", model.GetType()));

            return subject.Text;
        }
    }
}