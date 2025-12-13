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
    /// <summary>
    /// Provides extension methods to configure and register Valet authentication,
    /// hashing, and Swagger services in the dependency injection container.
    /// </summary>
    public static class ValetConfig
    {
        /// <summary>
        /// Adds and configures Valet services to the service collection,
        /// including authentication, password hashing, and Swagger generation,
        /// based on the provided <see cref="ValetOptions"/> configuration.
        /// </summary>
        /// <typeparam name="TContext">The <see cref="AuthDbContext"/> type used for persistence.</typeparam>
        /// <param name="services">The service collection to which Valet services will be added.</param>
        /// <param name="configuration">
        /// The application configuration used to read JWT settings.
        /// This parameter is optional and only required if <see cref="ValetOptions.EnableValetAuth"/> is set to <c>true</c>.
        /// </param>
        /// <param name="configure">An optional action to configure <see cref="ValetOptions"/>.</param>
        /// <returns>The updated <see cref="IServiceCollection"/> instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="configuration"/> is <c>null</c> but <see cref="ValetOptions.EnableValetAuth"/> is <c>true</c>.
        /// </exception>
        public static IServiceCollection AddValet<TContext>(this IServiceCollection services, IConfiguration? configuration = null, Action<ValetOptions>? configure = null) where TContext : AuthDbContext
        {
            var options = new ValetOptions();
            configure?.Invoke(options);
            services.AddSingleton(options);

            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();

            if (options.EnableValetHash)
            {
                services.UsePasswordHasher();
            }

            if (options.EnableValetAuth)
            {
                if (configuration == null)
                    throw new ArgumentNullException(nameof(configuration), "Configuration is required when EnableValetAuth is true.");

                services.AddScoped<IRoleRepository, RoleRepository<TContext>>();
                services.AddScoped<IUserRepository, UserRepository<TContext>>();
                services.AddScoped<IUserRoleRepository, UserRoleRepository<TContext>>();

                services.UseTokenJwt(configuration);
            }

            if (options.EnableValetSwaggerGen)
            {
                services.UseValetSwaggerGen();
            }

            return services;
        }

        private static IServiceCollection UsePasswordHasher(this IServiceCollection services)
        {
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            return services;
        }

        private static IServiceCollection UseTokenJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var signingKey = configuration.GetValue<string>("Settings:Jwt:Secret");
            var expirationMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationMinutes");

            services.AddSingleton<ITokenGenerator>(new TokenGenerator(signingKey!, expirationMinutes!));
            services.AddSingleton<ITokenValidator>(new TokenValidator(signingKey!));

            services.AddAuthentication()
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(signingKey!)),
                    ClockSkew = TimeSpan.Zero
                };
            });


            return services;
        }

        private static IServiceCollection UseValetSwaggerGen(this IServiceCollection services)
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
