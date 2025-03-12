using Microsoft.Extensions.DependencyInjection;
using valet.auth.Data;
using valet.auth.Data.Repositories;
using valet.auth.Domain.Interfaces.Repositories;
using valet.core.Data.Repositories;
using valet.core.Domain.Interfaces;

namespace valet.wrapper.Config
{
    public static class RepoConfig
    {


        public static IServiceCollection AddValetContext<TContext>(this IServiceCollection services) where TContext : AuthDbContext
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();

            if (typeof(TContext) == typeof(AuthDbContext))
            {
                services.AddScoped<IRoleRepository, RoleRepository<TContext>>();
                services.AddScoped<IUserRepository, UserRepository<TContext>>();
                services.AddScoped<IUserRepository, UserRepository<TContext>>();

            }
            return services;
        }
    }
}
