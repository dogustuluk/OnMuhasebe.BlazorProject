using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace OnMuhasebe.BlazorProject.Data;

/* This is used if database provider does't define
 * IBlazorProjectDbSchemaMigrator implementation.
 */
public class NullBlazorProjectDbSchemaMigrator : IBlazorProjectDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
