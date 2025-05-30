﻿using JWTAuthenticatonDemo.Application.Common.Interfaces;
using JWTAuthenticatonDemo.Application.Contracts.Repositories;
using JWTAuthenticatonDemo.Application.Contracts.Services;
using JWTAuthenticatonDemo.Application.Settings;
using JWTAuthenticatonDemo.Infrastructure.Authentication;
using JWTAuthenticatonDemo.Infrastructure.Persistence.Contexts;
using JWTAuthenticatonDemo.Infrastructure.Persistence.Repositories;
using JWTAuthenticatonDemo.Infrastructure.Persistence.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Infrastructure
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            #region DbContext
            services.AddDbContext<ApplicationDbContext>(options=>options.UseSqlServer(config.GetConnectionString("AppConn"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)), ServiceLifetime.Transient);
            #endregion

            #region JWTConfiguration
            var jwtSettings = new JWTSettings();
            config.Bind(JWTSettings.SectionName, jwtSettings);
            services.AddSingleton(Options.Create(jwtSettings));
            //services.Configure<JWTSettings>(config.GetSection("JWTSettings"));

            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed  = c =>
                        {
                            c.NoResult();
                            c.Response.StatusCode = 500;
                            c.Response.ContentType = "application/json";
                            return c.Response.WriteAsync(c.Exception.ToString());
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = JsonSerializer.Serialize("401 Not Authorized");
                            return context.Response.WriteAsync(result);
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType= "application/json";
                            var result = JsonSerializer.Serialize("403 Not Authorized ");
                            return context.Response.WriteAsync(result);
                        }
                    };
                });
            #endregion

            #region Services

            services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();

            #endregion

            #region Repositories

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddTransient<ILoginTokenRepository, LoginTokenRepository>();
            #endregion
            return services;
        }
    }
}
