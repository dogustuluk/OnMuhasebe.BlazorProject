using OnMuhasebe.BlazorProject.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace OnMuhasebe.BlazorProject;

[DependsOn(
    typeof(BlazorProjectEntityFrameworkCoreTestModule)
    )]
public class BlazorProjectDomainTestModule : AbpModule
{

}
