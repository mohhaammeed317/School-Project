using Microsoft.Extensions.DependencyInjection;
using schoolproject.service.abstracts;
using schoolproject.service.immplemintaion;
using System.Reflection;
using MediatR;
using schoolproject.core.behaviour;
using FluentValidation;


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
            
           // Get Validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            // 
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            
            
            return services;
        }

    }


}
