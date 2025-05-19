using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using Owin;
using persistence;

[assembly: OwinStartup(typeof(MatchRestApi.Startup))]

namespace MatchRestApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure Web API for self-host
            HttpConfiguration config = new HttpConfiguration();
            
            // Web API routes
            config.MapHttpAttributeRoutes();
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Configure dependency injection
            var connectionString = ConfigurationManager.ConnectionStrings["identifierDB"].ConnectionString;
            var props = new Dictionary<string, string> { { "ConnectionString", connectionString } };
            
            var matchRepository = new MatchRepositoryDb(props);
            config.DependencyResolver = new SimpleDependencyResolver(matchRepository);

            app.UseWebApi(config);
        }
    }

    // Simple dependency resolver for WebAPI
    public class SimpleDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        private readonly IMatchRepository _matchRepository;

        public SimpleDependencyResolver(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(Controllers.MatchController))
                return new Controllers.MatchController(_matchRepository);
            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new List<object>();
        }

        public System.Web.Http.Dependencies.IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
            // Nothing to dispose
        }
    }
}