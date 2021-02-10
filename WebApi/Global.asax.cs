using Autofac;
using Autofac.Integration.WebApi;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Web.Http;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Modules;
using WebApi.Repositories;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var builder = new ContainerBuilder();
            builder.RegisterModule<AutoMapperModule>();
            builder.RegisterModule<HttpClientModule>();
            builder.RegisterType<JsonRepository>().As<IJsonRepository>().SingleInstance();
            builder.RegisterType<ConcertRepository>().As<IRepository<Concert>>().SingleInstance();
            builder.Register(ctx => new NewsRepository(ctx.ResolveNamed<HttpClient>("NewsClient"))).As<INewsRepository>().InstancePerDependency();

            builder.RegisterApiControllers(typeof(PolicyController).Assembly);
            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}