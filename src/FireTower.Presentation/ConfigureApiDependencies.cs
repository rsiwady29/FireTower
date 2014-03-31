using System;
using System.Configuration;
using System.Web;
using AutoMapper;
using Autofac;
using FireTower.Domain;
using FireTower.Domain.CommandDispatchers;
using FireTower.Infrastructure;
using FireTower.IronMq;

namespace FireTower.Presentation
{
    public class ConfigureApiDependencies : IBootstrapperTask<ContainerBuilder>
    {
        #region IBootstrapperTask<ContainerBuilder> Members

        public Action<ContainerBuilder> Task
        {
            get
            {
                return builder =>
                           {
                               builder.RegisterInstance(Mapper.Engine).As<IMappingEngine>();
                               builder.RegisterType<TokenExpirationProvider>().As<ITokenExpirationProvider>();
                               builder.RegisterType<ApiUserMapper>().As<IApiUserMapper<Guid>>();
                               
                               ChooseCommandDispatcherBasedOnRoleType(builder);
                               SelfSubscribeWorkerToQueue();
                           };
            }
        }

        static void SelfSubscribeWorkerToQueue()
        {
            if (IsWorker() && HttpContext.Current != null)
            {
                Uri url = HttpContext.Current.Request.Url;
                new IronMqSubscriber().Subscribe("commands",
                                                 string.Format("{0}://{1}:{2}/work", url.Scheme,
                                                               url.Host, url.Port));
            }
        }

        static void ChooseCommandDispatcherBasedOnRoleType(ContainerBuilder builder)
        {
            if (IsWorker())
            {
                builder.RegisterType<SynchronousCommandDispatcher>().As<ICommandDispatcher>();
            }
            else
            {
                builder.RegisterType<IronMqCommandDispatcher>().As<ICommandDispatcher>();
            }
        }

        static bool IsWorker()
        {
            bool isWorkerAndShouldHandleCommandsSynchronously
                = (ConfigurationManager.AppSettings["Roles"] ?? "").ToLower().Contains("worker");
            return isWorkerAndShouldHandleCommandsSynchronously;
        }

        #endregion
    }
}