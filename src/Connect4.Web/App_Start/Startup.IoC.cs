using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using AutofacSerilogIntegration;
using Connect4.Mappers;

namespace Connect4
{
    public partial class Startup
    {
        private void ConfigureIoC()
        {
            var builder = new ContainerBuilder();

            var config = GlobalConfiguration.Configuration;

            var assemblies = new[] { Assembly.GetExecutingAssembly() };

            builder.RegisterApiControllers(assemblies);

            builder.RegisterLogger();

            builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(IMapper<,>))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}