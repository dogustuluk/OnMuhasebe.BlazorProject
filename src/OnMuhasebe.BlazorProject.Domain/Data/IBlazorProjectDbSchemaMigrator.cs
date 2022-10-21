using System.Threading.Tasks;

namespace OnMuhasebe.BlazorProject.Data;

public interface IBlazorProjectDbSchemaMigrator
{
    Task MigrateAsync();
}
