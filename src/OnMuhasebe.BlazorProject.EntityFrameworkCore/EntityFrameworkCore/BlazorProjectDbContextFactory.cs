using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace OnMuhasebe.BlazorProject.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class BlazorProjectDbContextFactory : IDesignTimeDbContextFactory<BlazorProjectDbContext>
{
    public BlazorProjectDbContext CreateDbContext(string[] args)
    {
        BlazorProjectEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<BlazorProjectDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new BlazorProjectDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../OnMuhasebe.BlazorProject.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
