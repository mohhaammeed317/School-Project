using Microsoft.Extensions.DependencyInjection;
using schoolproject.service.abstracts;
using schoolproject.service.immplemintaion;
using System.Reflection;

namespace schoolproject.core
{
    public static class modulecoredependencies
    {

        public static IServiceCollection AddCOREDependencies(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            return services;
        }

    }


}
