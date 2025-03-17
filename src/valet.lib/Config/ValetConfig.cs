using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using valet.lib.Auth.Data;
using valet.lib.Auth.Data.Repositories;
using valet.lib.Auth.Domain.Interfaces;
using valet.lib.Auth.Domain.Interfaces.Repositories;
using valet.lib.Auth.Service.Hash;
using valet.lib.Auth.Service.Token;
using valet.lib.Core.Data.Repositories;
using valet.lib.Core.Domain.Interfaces;

namespace valet.lib.Config
{
    public static class ValetConfig
    {

        public static IServiceCollection AddValet<TContext>(this IServiceCollection services, IConfiguration configuration, bool useAuthentication) where TContext : AuthDbContext
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();

            if (useAuthentication)
            {
                services.AddScoped<IRoleRepository, RoleRepository<TContext>>();
                services.AddScoped<IUserRepository, UserRepository<TContext>>();
                services.AddScoped<IUserRepository, UserRepository<TContext>>();
            }
            return services;
        }

        public static IServiceCollection UsePasswordHasher(this IServiceCollection services)
        {
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            return services;
        }

        public static IServiceCollection UseTokenGenerator(this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = configuration.GetValue<string>("Settings:Secret"); // TODO: MAYBE HANDLE THIS POSSIBLE EXCEPTION
            services.AddSingleton<ITokenGenerator>(new TokenGenerator(secretKey!));
            return services;
        }

        public static IServiceCollection UseValetJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = configuration.GetValue<string>("Settings:Secret");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey!))
                };
            });

            return services;
        }

        public static IServiceCollection UseValetSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization using Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }
    }
}
