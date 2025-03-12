using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using valet.lib.Auth.Data;
using valet.lib.Auth.Data.Repositories;
using valet.lib.Auth.Domain.Interfaces.Repositories;
using valet.lib.Core.Data.Repositories;
using valet.lib.Core.Domain.Interfaces;

namespace valet.lib.Config
{
    public static class RepoConfig
    {

        public static IServiceCollection AddValetContext<TContext>(this IServiceCollection services, bool useAuthentication) where TContext : AuthDbContext
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
    }
}
