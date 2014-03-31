using System;
using Autofac;
using FireTower.Domain;
using FireTower.Infrastructure;
using FireTower.Mailgun;

namespace FireTower.Presentation
{
    public class ConfigureEventHandlerDependencies : IBootstrapperTask<ContainerBuilder>
    {
        #region IBootstrapperTask<ContainerBuilder> Members

        public Action<ContainerBuilder> Task
        {
            get
            {
                return builder => builder.RegisterType<MailgunSmtpClient>().As<ISmtpClient>();
            }
        }

        #endregion
    }
}