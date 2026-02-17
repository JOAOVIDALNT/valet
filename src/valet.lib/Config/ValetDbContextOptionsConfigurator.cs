using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using valet.lib.Auth.Data;

namespace valet.lib.Config;

internal sealed class ValetDbContextOptionsConfigurator<TContext> : IConfigureOptions<DbContextOptions<TContext>> where TContext : AuthDbContext
{
    private readonly IEnumerable<IInterceptor> _interceptors;
    public ValetDbContextOptionsConfigurator(IEnumerable<IInterceptor> interceptors)
    {
        _interceptors = interceptors;
    }
    public void Configure(DbContextOptions<TContext> options)
    {
        if (!_interceptors.Any())
            return;

        var builder = new DbContextOptionsBuilder<TContext>(options);
        builder.AddInterceptors(_interceptors);
    }
}