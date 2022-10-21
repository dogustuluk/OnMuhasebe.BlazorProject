using OnMuhasebe.BlazorProject.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace OnMuhasebe.BlazorProject.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BlazorProjectEntityFrameworkCoreModule),
    typeof(BlazorProjectApplicationContractsModule)
    )]
public class BlazorProjectDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
