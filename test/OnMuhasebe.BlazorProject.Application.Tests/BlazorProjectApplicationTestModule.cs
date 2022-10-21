using Volo.Abp.Modularity;

namespace OnMuhasebe.BlazorProject;

[DependsOn(
    typeof(BlazorProjectApplicationModule),
    typeof(BlazorProjectDomainTestModule)
    )]
public class BlazorProjectApplicationTestModule : AbpModule
{

}
