﻿using JWTAuthenticatonDemo.Application;
using JWTAuthenticatonDemo.Infrastructure;
using Microsoft.OpenApi.Models;

namespace JWTAuthenticatonDemo.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AppConfigurations(this IServiceCollection services, IConfiguration config)
        {
            services.AddApplicationLayer();
            services.AddInfrastructure(config);
            return services;
        }


        public static IServiceCollection AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                //var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
                //c.IncludeXmlComments(filePath);
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "JWTAuthenticationDemo",
                    Description = "This Api will be responsible for overall data distribution and authorization."
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
            });
            return services;
        }
    }
}
