using Microsoft.Extensions.DependencyInjection;
using schoolproject.service.abstracts;
using schoolproject.service.immplemintaion;

namespace schoolproject.service
{
    public static class moduleservicedependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<istudentservice, studentservice>();
            return services;
        }

        }
}
