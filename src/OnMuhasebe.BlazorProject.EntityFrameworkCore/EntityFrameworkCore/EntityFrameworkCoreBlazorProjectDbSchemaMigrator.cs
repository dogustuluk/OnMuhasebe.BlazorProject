using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnMuhasebe.BlazorProject.Data;
using Volo.Abp.DependencyInjection;

namespace OnMuhasebe.BlazorProject.EntityFrameworkCore;

public class EntityFrameworkCoreBlazorProjectDbSchemaMigrator
    : IBlazorProjectDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreBlazorProjectDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the BlazorProjectDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<BlazorProjectDbContext>()
            .Database
            .MigrateAsync();
    }
}
