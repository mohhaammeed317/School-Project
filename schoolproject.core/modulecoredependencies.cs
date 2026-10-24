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
            //config of MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            //config of AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }

    }


}
