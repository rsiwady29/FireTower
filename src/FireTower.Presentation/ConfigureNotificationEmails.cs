using System;
using Autofac;
using FireTower.Domain;
using FireTower.Infrastructure;
using FireTower.Presentation.EmailSubjects;
using FireTower.Presentation.EmailTemplates;

namespace FireTower.Presentation
{
    public class ConfigureNotificationEmails : IBootstrapperTask<ContainerBuilder>
    {
        #region IBootstrapperTask<ContainerBuilder> Members

        public Action<ContainerBuilder> Task
        {
            get
            {
                return builder =>
                           {
                               builder.RegisterType<VerificationEmailTemplate>().As<IEmailTemplate>();
                               builder.RegisterType<VerificationEmailSubject>().As<IEmailSubject>();
                           };
            }
        }

        #endregion
    }
}