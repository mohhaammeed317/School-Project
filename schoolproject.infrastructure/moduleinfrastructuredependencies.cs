using Microsoft.Extensions.DependencyInjection;
using schoolproject.infrastructure.abstracts;
using schoolproject.infrastructure.infrabases;
using schoolproject.infrastructure.repositories;

namespace schoolproject.infrastructure
{
    public  static class moduleinfrastructuredependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<istudentrepository, studentrepository>();
            services.AddTransient(typeof(igenericrepo<>), typeof(genericrepo<>));

            return services;
        }

    }

} 
