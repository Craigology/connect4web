using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Connect4.Mappers;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Connect4.Startup))]

namespace Connect4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            var config = GlobalConfiguration.Configuration;

            var assemblies = new[] { Assembly.GetExecutingAssembly() };

            builder.RegisterApiControllers(assemblies);

            builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof (IMapper<,>))
                .AsImplementedInterfaces()
                .InstancePerDependency();                

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
