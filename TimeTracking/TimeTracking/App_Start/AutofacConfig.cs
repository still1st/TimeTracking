using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using TimeTracking.Domain.DataAccess;
using TimeTracking.Services;

namespace TimeTracking.App_Start
{
    public static class AutofacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(typeof(TimeTrackingContext).Assembly)
                .Where(t => t.Name.EndsWith("Impl"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(IReportService).Assembly)
                .Where(t => t.Name.EndsWith("Impl"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}