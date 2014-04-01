using System;
using Autofac;
using Nancy;
using Nancy.Authentication.Stateless;
using Nancy.Bootstrapper;
using FireTower.Domain.Exceptions;
using FireTower.Infrastructure;
using FireTower.Presentation.Modules;
using Nancy.Conventions;

namespace FireTower.Presentation
{
    public class ApiBootstrapper : Bootstrapper
    {
        public ApiBootstrapper()
        {
            AddBootstrapperTask(new ConfigureApiDependencies());
            AddBootstrapperTask(new ConfigureAutomapperMappings());
            AddBootstrapperTask(new ConfigureWorkerDependencies());
            AddBootstrapperTask(new ConfigureNotificationEmails());
            AddBootstrapperTask(new ConfigureEventHandlerDependencies());

        }

        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
        {
            var configuration =
                new StatelessAuthenticationConfiguration(
                    ctx =>
                        {
                            dynamic token = ctx.Request.Query.token;
                            if (!token.HasValue)
                            {
                                return null;
                            }

                            return container.Resolve<IApiUserMapper<Guid>>()
                                .GetUserFromAccessToken(Guid.Parse((string) token));
                        });

            pipelines.OnError += (ctx, err) => HandleExceptions(err, ctx);

            AllowAccessToConsumingSite(pipelines);

            StatelessAuthentication.Enable(pipelines, configuration);

            base.RequestStartup(container, pipelines, context);
        }

        static Response HandleExceptions(Exception err, NancyContext ctx)
        {
            if (err is UserAlreadyExistsException)
            {
                return new Response()
                    .WithStringContents(err.Message)
                    .WithStatusCode(HttpStatusCode.BadRequest);
            }

            if (err is NoFireTowerUserException)
            {
                return
                    new Response()
                        .WithStringContents(
                            "This endpoint requires a valid user account. Did you include a 'token' in your request?")
                        .WithStatusCode(HttpStatusCode.Unauthorized);
            }

            if (err is TokenDoesNotExistException)
            {
                return new Response()
                    .WithStringContents("The token you tried to use is not valid.")
                    .WithStatusCode(HttpStatusCode.Unauthorized);
            }

            if (err is TokenExpiredException)
            {
                return new Response()
                    .WithStringContents(
                        "The token you tried to use has expired. Please log in to get a new one.")
                    .WithStatusCode(HttpStatusCode.Unauthorized);
            }
            return ctx.Response;
        }

        static void AllowAccessToConsumingSite(IPipelines pipelines)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline(x =>
                                                              {
                                                                  x.Response.Headers.Add("Access-Control-Allow-Origin",
                                                                                         "*");
                                                                  x.Response.Headers.Add(
                                                                      "Access-Control-Allow-Methods",
                                                                      "POST,GET,DELETE,PUT,OPTIONS");
                                                              });
        }

        protected override void ConfigureConventions(NancyConventions conventions)
        {
           base.ConfigureConventions(conventions);
 
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("App"));
        }
    }
}