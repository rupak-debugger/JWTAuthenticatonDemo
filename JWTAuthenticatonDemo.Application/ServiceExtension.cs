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
using JWTAuthenticatonDemo.Application.Behaviors;

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

            //adding fluent validation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            AssemblyScanner.FindValidatorsInAssembly(typeof(Assembly).Assembly)
           .ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
