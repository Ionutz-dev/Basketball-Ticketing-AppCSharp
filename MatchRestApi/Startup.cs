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
            HttpConfiguration config = new HttpConfiguration();
            
            config.MapHttpAttributeRoutes();
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var connectionString = ConfigurationManager.ConnectionStrings["identifierDB"].ConnectionString;
            var props = new Dictionary<string, string> { { "ConnectionString", connectionString } };
            
            var matchRepository = new MatchRestRepository(props);
            config.DependencyResolver = new SimpleDependencyResolver(matchRepository);

            app.UseWebApi(config);
        }
    }
    
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
        }
    }
}