using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;
using JWTAuthenticatonDemo.Application.Models.Authentication;

namespace JWTAuthenticatonDemo.Application
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            //adding Mediatr 
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            //adding Automapper 
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Register Fluent Validation validators
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

           // AssemblyScanner.FindValidatorsInAssembly(typeof(Assembly).Assembly)
           //.ForEach(item => services.AddTransient(item.InterfaceType, item.ValidatorType));


            // Configure MediatR pipeline with Fluent Validation
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            //services.AddInfrastructure(config);
            return services;
        }
    }
}
