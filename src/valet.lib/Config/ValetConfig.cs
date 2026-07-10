using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using valet.lib.Auth.Data;
using valet.lib.Auth.Data.Repositories;
using valet.lib.Auth.Domain.Interfaces;
using valet.lib.Auth.Domain.Interfaces.Repositories;
using valet.lib.Auth.Service.Hash;
using valet.lib.Auth.Service.Token;
using valet.lib.Core.Audition;
using valet.lib.Core.Data.Repositories;
using valet.lib.Core.Domain.Interfaces;
using valet.lib.Core.Patterns.UseCases;

namespace valet.lib.Config
{
    /// <summary>
    /// Provides extension methods to register and configure Valet services
    /// in the dependency injection container.
    /// </summary>
    /// <remarks>
    /// Features are enabled explicitly through <see cref="ValetOptions"/> fluent methods
    /// such as <c>UseAuth()</c>, <c>UseSwaggerGen()</c>
    /// and <c>UseAuditing()</c>.
    /// 
    /// Only the features explicitly enabled will be registered.
    /// </remarks>
    public static class ValetConfig
    {
        /// <summary>
        /// Adds and configures Valet services to the service collection.
        /// </summary>
        /// <typeparam name="TContext">
        /// The <see cref="AuthDbContext"/> implementation used for persistence.
        /// </typeparam>
        /// <param name="services">
        /// The service collection where Valet services will be registered.
        /// </param>
        /// <param name="configuration">
        /// The application configuration used to read JWT settings.
        /// This parameter is required only when <c>UseAuth()</c> is enabled.
        /// </param>
        /// <param name="configure">
        /// An optional delegate to configure <see cref="ValetOptions"/>.
        /// Use this to enable features such as authentication, hashing,
        /// Swagger integration, auditing, and use case injection.
        /// </param>
        /// <returns>
        /// The updated <see cref="IServiceCollection"/> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when authentication is enabled via <c>UseAuth()</c>
        /// and <paramref name="configuration"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// If no assemblies are specified via <c>AddUseCasesFrom&lt;T&gt;()</c>,
        /// no automatic use case injection will occur.
        /// 
        /// Interceptors registered through <c>UseAuditing()</c> or other
        /// extension methods are added using <c>TryAddEnumerable</c>,
        /// ensuring they do not override interceptors defined by the consumer.
        /// </remarks>
        public static IServiceCollection AddValet<TContext>(
            this IServiceCollection services, 
            IConfiguration? configuration = null, 
            Action<ValetOptions>? configure = null) where TContext : AuthDbContext
        {
            var options = new ValetOptions();
            configure?.Invoke(options);
            services.AddSingleton(options);

            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
            services.AddSingleton<ISystemClock, SystemClock>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();

            if (options.ValetAuthFeature)
            {
                if (configuration == null)
                    throw new ArgumentNullException(nameof(configuration), 
                        "Configuration is required when EnableValetAuth is true.");

                services.AddScoped<IRoleRepository, RoleRepository<TContext>>();
                services.AddScoped<IUserRepository, UserRepository<TContext>>();
                services.AddScoped<IUserRoleRepository, UserRoleRepository<TContext>>();

                services.UseTokenJwt(configuration);
            }

            if (options.ValetSwaggerGenFeature)
                services.UseValetSwaggerGen();

            if (options.UseCasesAssemblies.Any())
            {
                foreach (var assembly in options.UseCasesAssemblies)
                    services.InjectUseCases(assembly);
            }
            
            foreach (var interceptorType in options.Interceptors)
            {
                services.TryAddEnumerable(
                    ServiceDescriptor.Scoped(
                        typeof(IValetAuditInterceptor),
                        interceptorType));
            }

            return services;
        } 
        
        /// <summary>
        /// Applies Valet EF Core auditing interceptor
        /// to the current DbContextOptionsBuilder.
        /// </summary>
        public static DbContextOptionsBuilder EnableAuditing(
            this DbContextOptionsBuilder builder,
            IServiceProvider serviceProvider)
        {
            var interceptors = serviceProvider
                .GetServices<IValetAuditInterceptor>();

            if (interceptors.Any())
                builder.AddInterceptors(interceptors);

            return builder;
        }
        
        private static IServiceCollection InjectUseCases(
            this IServiceCollection services,
            Assembly assembly)
        {
            var baseTypes = new[]
            {
                typeof(Command<>),
                typeof(Command<,>),
                typeof(Query<>),
                typeof(Query<,>)
            };
            
            var typesToRegister = assembly.GetTypes()
                .Where(t =>
                    !t.IsAbstract &&
                    !t.IsInterface &&
                    baseTypes.Any(bt => IsSubclassOfGeneric(t, bt)));

            foreach (var type in typesToRegister)
                services.TryAddScoped(type);

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
                options.AddSecurityRequirement(_ => new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecuritySchemeReference("Bearer", null, null),
                        new List<string>()
                    }
                });
            });

            return services;
        }
        
        private static bool IsSubclassOfGeneric(Type type, Type generic)
        {
            Type? currentType = type;

            while (currentType != null && currentType != typeof(object))
            {
                if (currentType.IsGenericType &&
                    currentType.GetGenericTypeDefinition() == generic)
                    return true;

                currentType = currentType.BaseType;
            }

            return false;
        }
    }
}
